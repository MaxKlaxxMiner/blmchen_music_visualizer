#region # using *.*
using System;
using System.Collections.Generic;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
#endregion

namespace Sync1
{
  /// <summary>
  /// Klasse für eine vollständiges Tick-System
  /// </summary>
  public sealed class TickSync
  {
    #region # // --- fixe Werte ---
    /// <summary>
    /// merkt sich die Anzahl der Ticks, welche pro Sekunde berechnet werden sollen
    /// </summary>
    readonly double ticksPerSecond;
    /// <summary>
    /// merkt sich die Tick-Schritte in Zeitabständen
    /// </summary>
    readonly double tickInterval;

    /// <summary>
    /// merkt sich die Funktion, welche beim berechnen bei jeden einzelnen Tick verwendet werden soll
    /// </summary>
    readonly Action<TickSync> tickFunction;

    /// <summary>
    /// maximale Anzahl der Ticks, pro gezeichneten Bildes (zuviele Ticks werden pausiert, die gesamte Animation verlangsamt sich)
    /// </summary>
    readonly int maxTicksPerFrame;
    /// <summary>
    /// maximale Zeit in Sekunden, welche für die Ticks pro gezeichneten Bildes aufgewendet werden darf (zu langsame Ticks werden pausiert, die gesamte Animation verlangsamt sich)
    /// </summary>
    readonly double maxTickTimePerFrame;
    /// <summary>
    /// gibt an, ob pausierte Ticks normal weiter verarbeitet werden sollen (true) oder versucht werden soll die fehlende Zeit als Ticks wieder aufzuholen (false)
    /// </summary>
    readonly bool maxTickSkip;
    #endregion

    #region # // --- Variablen ---
    /// <summary>
    /// gibt die Anzahl der insgesamt berechneten Ticks zurück
    /// </summary>
    long tickCount;

    /// <summary>
    /// gibt die Anzahl der insgesamt berechneten Bilder zurück
    /// </summary>
    long frameCount;

    /// <summary>
    /// Zeitpunkt des letzten Ticks (in Sekunden seit Klassen-Initialisierung)
    /// </summary>
    double lastTickTime;
    /// <summary>
    /// Zeitpunkt des nächsten Ticks (in Sekunden seit Klassen-Initialisierung)
    /// </summary>
    double nextTickTime;
    #endregion

    #region # // --- Konstruktor ---
    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="ticksPerSecond">Anzahl der Ticks pro Sekunde, welche berechnet werden sollen</param>
    /// <param name="tickFunction">
    ///   Methode, welche zum berechnen der (gleichmäßigen) Ticks verwendet werden soll, 
    ///   Parameter: eigene Klasse
    /// </param>
    /// <param name="maxTicksPerFrame">[optional] maximale Anzahl der Ticks, pro gezeichneten Bildes (zuviele Ticks werden pausiert, die gesamte Animation verlangsamt sich), default: 10000</param>
    /// <param name="maxTickTimePerFrame">[optional] maximale Zeit in Sekunden, welche für die Ticks pro gezeichneten Bildes aufgewendet werden darf (zu langsame Ticks werden pausiert, die gesamte Animation verlangsamt sich), default: 1.0</param>
    /// <param name="maxTickSkip">[optional] gibt an, ob pausierte Ticks normal weiter verarbeitet werden sollen (true) oder versucht werden soll die fehlende Zeit als Ticks wieder aufzuholen (false), default: true</param>
    public TickSync(double ticksPerSecond, Action<TickSync> tickFunction, int maxTicksPerFrame = 10000, double maxTickTimePerFrame = 1.0, bool maxTickSkip = true)
    {
      if (ticksPerSecond < 0.01 || ticksPerSecond > 100000.0) throw new ArgumentOutOfRangeException("ticksPerSecond");
      if (tickFunction == null) throw new ArgumentNullException("tickFunction");
      if (maxTicksPerFrame < 1) throw new ArgumentOutOfRangeException("maxTicksPerFrame");
      if (maxTickTimePerFrame < 0.0001 || maxTickTimePerFrame > 86400.0) throw new ArgumentOutOfRangeException("maxTickTimePerFrame");

      this.ticksPerSecond = ticksPerSecond;
      this.tickFunction = tickFunction;
      this.maxTicksPerFrame = maxTicksPerFrame;
      this.maxTickTimePerFrame = maxTickTimePerFrame;
      this.maxTickSkip = maxTickSkip;

      tickInterval = 1.0 / ticksPerSecond;
      lastTickTime = 0.0;
      nextTickTime = tickInterval;

      currentValues = new ValueContainer(0.0);

      lastTicks = new[] { currentValues, new ValueContainer(-tickInterval), new ValueContainer(-tickInterval * 2), new ValueContainer(-tickInterval * 3) };
    }
    #endregion

    #region # // --- todo: automatic real-timing ---
    ///// <summary>
    ///// wird aufgerufen, wenn das Zeichnen eines neuen Bildes begonnen wird (gibt die aktuelle Bild-Nummer zurück)
    ///// </summary>
    ///// <returns>aktuelle Bildnummer (aufwärts, beginnend bei 1)</returns>
    //public long FrameStartDrawing()
    //{
    //  return FrameStartDrawing(0.0);
    //}

    ///// <summary>
    ///// wird aufgerufen, wenn das Zeichnen des Bildes abgeschlossen wurde
    ///// </summary>
    ///// <param name="frameId">Nummer des Bildes, welches fertig gezeichnet wurde (default: aktuelle Bildnummer)</param>
    //public void FrameFinishDrawing(long frameId = 0)
    //{
    //  if (frameId == 0) frameId = frameCount;
    //  FrameFinishDrawing(0.0, frameId);
    //}

    ///// <summary>
    ///// wird genau dann aufgerufen, wenn das Bild auf dem Bildschirm angezeigt wurde
    ///// </summary>
    ///// <param name="correctionMs">
    /////   manuelle Korrektur der Bildanzeige,
    /////   kleiner als 0: das Bild wurde bereits vor x Millisekunden gezeigt, 
    /////   größer als 0: das Bild wird erst in etwa X Millisekunden gezeigt werden
    ///// </param>
    ///// <param name="frameId">Nummer des Bildes, welches angezeigt wurde (default: aktuelle Bildnummer)</param>
    //public void FrameDisplayed(double correctionMs = 0.0, long frameId = 0)
    //{
    //  if (frameId == 0) frameId = frameCount;
    //  FrameDirectDisplayed(0.0 + correctionMs, frameId);
    //}
    #endregion

    #region # // --- Frame-Methoden ---
    /// <summary>
    /// wird aufgerufen, wenn das Zeichnen eines neuen Bildes bevor steht (gibt eine eindeutige Bild-Nummer zurück)
    /// </summary>
    /// <param name="timeStamp">Zeitpunkt, welcher benutzt werden soll (darf immer nur aufsteigend gesetzt werden)</param>
    /// <returns>aktuelle Bildnummer (aufwärts, beginnend bei 1)</returns>
    public long FrameInitialize(double timeStamp)
    {
      if (timeStamp < lastTickTime) throw new ArgumentOutOfRangeException("timeStamp");
      if (timeStamp >= nextTickTime) // müssen neue Ticks berechnet werden?
      {
        double tickTimeLimit = nextTickTime + maxTickTimePerFrame;
        int tickCountLimit = maxTicksPerFrame;
        for (; tickCountLimit > 0; tickCountLimit--)
        {
          lastTickTime = nextTickTime;
          nextTickTime += tickInterval;
          tickFunction(this); // Tick-Funktion aufrufen
          tickCount++;

          if (nextTickTime > timeStamp) break; // genügend Ticks berechnet?

          if (nextTickTime > tickTimeLimit) // Zeit-Limit pro Bild überschritten?
          {
            tickCountLimit = 0;
          }
        }

        // Limit überschritten? -> Zeitpunkte angleichen, sofern dies erlaubt ist (sonst werden bei den nächsten Bild versuchen alle Ticks zu berechnen)
        if (tickCountLimit <= 0 && maxTickSkip && nextTickTime < timeStamp)
        {
          nextTickTime = timeStamp;
          lastTickTime = timeStamp - tickInterval;
        }
      }

      return ++frameCount;
    }

    /// <summary>
    /// wird aufgerufen, wenn das Zeichnen eines neuen Bildes begonnen wird
    /// </summary>
    /// <param name="timeStamp">Zeitpunkt, welcher benutzt werden soll (darf immer nur aufsteigend gesetzt werden)</param>
    /// <param name="frameId">Nummer des Bildes, welches gezeichnet werden soll</param>
    public void FrameStartDrawing(double timeStamp, long frameId)
    {
      if (timeStamp < lastTickTime) throw new ArgumentOutOfRangeException("timeStamp");
      if (frameId != frameCount) throw new NotSupportedException("async multiframe is not supported");
    }

    /// <summary>
    /// wird aufgerufen, wenn das Zeichnen des Bildes abgeschlossen wurde
    /// </summary>
    /// <param name="timeStamp">Zeitpunkt, welcher benutzt werden soll (darf immer nur aufsteigend gesetzt werden)</param>
    /// <param name="frameId">Nummer des Bildes, welches fertig gezeichnet wurde</param>
    public void FrameFinishDrawing(double timeStamp, long frameId)
    {
      if (timeStamp < lastTickTime) throw new ArgumentOutOfRangeException("timeStamp");
      if (frameId != frameCount) throw new NotSupportedException("async multiframe is not supported");
    }

    /// <summary>
    /// wird genau dann aufgerufen, wenn das Bild auf dem Bildschirm angezeigt wurde (z.B. nach einer Wartephase durch vsync)
    /// </summary>
    /// <param name="timeStamp">Zeitpunkt, welcher benutzt werden soll (darf immer nur aufsteigend gesetzt werden)</param>
    /// <param name="frameId">Nummer des Bildes, welches angezeigt wurde</param>
    public void FrameDisplayed(double timeStamp, long frameId)
    {
      if (timeStamp < lastTickTime) throw new ArgumentOutOfRangeException("timeStamp");
      if (frameId != frameCount) throw new NotSupportedException("async multiframe is not supported");
    }
    #endregion

    #region # // --- Value-System ---
    /// <summary>
    /// Container zum speichern von mehreren Werten
    /// </summary>
    sealed class ValueContainer
    {
      /// <summary>
      /// merkt sich den Zeitstempel der Werte
      /// </summary>
      double timeStamp;
      /// <summary>
      /// Array mit allen Werten
      /// </summary>
      double[] values;
      /// <summary>
      /// Array mit den Identifikationen und Längenangaben
      /// 0: freies Feld
      /// größer als 0: gültige Ident, stellt die Anzahl der Werte dar
      /// kleiner als 0: Wert gehört zu einem anderen Ident (start-ident wird als offset angegeben)
      /// </summary>
      int[] ident;

      /// <summary>
      /// Start-Position, wo der nächste freie Platz gesucht werden soll
      /// </summary>
      int search;
      /// <summary>
      /// Gesamtzahl der Werte, welche noch frei sind
      /// </summary>
      int free;

      /// <summary>
      /// Konstruktor
      /// </summary>
      /// <param name="timeStamp">Zeitstempel, welcher verwendet werden soll</param>
      public ValueContainer(double timeStamp)
      {
        this.timeStamp = timeStamp;
        values = new double[16];
        ident = new int[16];
        search = 0;
        free = 16;
      }

      /// <summary>
      /// setzt alle Werte, welche in einem anderen Container gespeichert wurden
      /// </summary>
      /// <param name="container">Container, wovon die Werte verwendet werden sollen</param>
      /// <param name="timeStamp">Zeitstempel, welcher verwendet werden soll</param>
      public void SetAllValues(ValueContainer container, double timeStamp)
      {
        this.timeStamp = timeStamp;
        search = container.search;
        free = container.free;

        var oldValues = container.values;
        var oldIdent = container.ident;
        var newValues = values;
        var newIdent = ident;

        if (newValues.Length != oldValues.Length) // müssen die Größen neu erstellt werden?
        {
          values = newValues = new double[oldValues.Length];
          ident = newIdent = new int[oldIdent.Length];
        }

        // --- Inhalte kopieren ---
        for (int i = 0; i < newValues.Length; i++)
        {
          newValues[i] = oldValues[i];
        }

        for (int i = 0; i < newIdent.Length; i++)
        {
          newIdent[i] = oldIdent[i];
        }
      }
    }

    /// <summary>
    /// merkt sich die aktuellen Werte, welche während eines Ticks geändert werden können
    /// </summary>
    ValueContainer currentValues;

    /// <summary>
    /// merkt sich alle Werte der letzten berechneten Ticks, [0] == currentValues
    /// </summary>
    readonly ValueContainer[] lastTicks;

    /// <summary>
    /// reserviert den Platz für neue Werte und gibt die entsprechende ID zurück
    /// </summary>
    /// <param name="valueCount">Anzahl der Werte, welche reserviert werden sollen</param>
    /// <returns>fertige ID</returns>
    public int AllocValues(int valueCount)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// gibt die Anzahl der gespeicherten Werte zurück, oder kleinergleich 0, wenn die ID ungültig ist oder die Werte bereits gelöscht wurden
    /// </summary>
    /// <param name="valueId">ID der Werte, welche abgefragt werden sollen</param>
    /// <returns>Anzahl der gespeicherten Werte oder kleinergleich 0, wenn die ID ungültig ist oder die Werte bereits gelöscht wurden</returns>
    public int GetValueCount(int valueId)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// löscht die Werte einer bestimmten ID und gibt true zurück, falls erfolgreich
    /// </summary>
    /// <param name="valueId">ID der Werte, welche gelöscht werden sollen</param>
    /// <returns>true, wenn die Werte erfolgreich gelöscht werden konnten</returns>
    public bool DeleteValues(int valueId)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// setzt einen bestimmten Wert und gibt true zurück, wenn das Zurücksetzen erfolgreich war
    /// </summary>
    /// <param name="valueId">ID der Werte, wo ein Wert zurückgesetzt werden soll</param>
    /// <param name="valueOffset">Offset innerhalb der Werte</param>
    /// <param name="value">Wert, welcher gesetzt werden soll</param>
    /// <returns>true, wenn die Änderung erfolgreich war</returns>
    public bool ResetValue(int valueId, int valueOffset, double value)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// setzt alle Werte anhand einer ID und gibt true zurück, wenn das Zurücksetzen der Werte erfolgreich war
    /// </summary>
    /// <param name="valueId">ID der Werte, welche geändert werden sollen</param>
    /// <param name="values">Werte, welche gesetzt werden sollen</param>
    /// <returns>true, wenn das Zurücksetzen erfolgreich war</returns>
    public bool ResetValues(int valueId, params double[] values)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// aktualisiert einen der Werte anhand einer ID und gibt true zurück, wenn die Änderung erfolgreich war
    /// </summary>
    /// <param name="valueId">ID der Werte, welche geändert werden sollen</param>
    /// <param name="valueOffset">Offset innerhalb der Werte</param>
    /// <param name="value">Wert, welcher aktualisiert werden soll</param>
    /// <returns>true, wenn das Update erfolgreich war</returns>
    public bool UpdateValue(int valueId, int valueOffset, double value)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// aktualisiert alle Werte anhand einer ID und gibt true zurück, wenn die Änderung erfolgreich war
    /// </summary>
    /// <param name="valueId">ID der Werte, welche geändert werden sollen</param>
    /// <param name="values">Werte, welche aktualisiert werden sollen</param>
    /// <returns>true, wenn das Update erfolgreich war</returns>
    public bool UpdateValues(int valueId, params double[] values)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// gibt den aktuell gespeicherten Wert anhand einer ID zurück (ohne Zwischenberechnung)
    /// </summary>
    /// <param name="valueId">ID der Werte, welche abgefragt werden sollen</param>
    /// <param name="valueOffset">Offset innerhalb der Werte</param>
    /// <returns>fertig ausgelesener Wert oder 0, wenn die ID ungültig ist bzw. der Wert bereits gelöscht wurde</returns>
    public double GetOriginValue(int valueId, int valueOffset = 0)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// gibt die gespeicherten Werte anhand einer ID zurück
    /// </summary>
    /// <param name="valueId">ID der Werte, welche gelesen werden sollen</param>
    /// <param name="valueOffset">Offset innerhalb der Werte</param>
    /// <param name="readArray">Array, wohin die Werte geschrieben werden sollen</param>
    /// <param name="arrayOffset">Startposition innerhalb des Arrays</param>
    /// <param name="count">Anzahl der zu lesenden Werte</param>
    /// <returns>true, wenn der Lesevorgang erfolgreich war</returns>
    public bool GetOriginValues(int valueId, int valueOffset, double[] readArray, int arrayOffset, int count)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// gibt einen normal interpolierten Wert zurück, während ein Bild gezeichnet wird
    /// </summary>
    /// <param name="frameId">Nummer des Bildes, welches momentan gezeichnet wird</param>
    /// <param name="valueId">ID der Werte, welche gelesen werden sollen</param>
    /// <param name="valueOffset">Offset innerhalb der Werte</param>
    /// <returns>fertig berechneter Wert oder 0, wenn die ID ungültig ist bzw. der Wert bereits gelöscht wurde</returns>
    public double GetValue(long frameId, int valueId, int valueOffset = 0)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// gibt einen normal interpolierten Wert mit Einschränkungen zurück, während ein Bild gezeichnet wird
    /// </summary>
    /// <param name="frameId">Nummer des Bildes, welches momentan gezeichnet wird</param>
    /// <param name="valueId">ID der Werte, welche gelesen werden sollen</param>
    /// <param name="minValue">minimal erlaubter Wert (das Ergebnis darf nicht kleiner sein)</param>
    /// <param name="maxValue">maximal erlaubter Wert (das Ergebnis darf nicht größer sein)</param>
    /// <param name="valueOffset">Offset innerhalb der Werte</param>
    /// <returns>fertig berechneter Wert</returns>
    public double GetValueTruncated(long frameId, int valueId, double minValue, double maxValue, int valueOffset = 0)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// gibt einen normal interpolierten Wert mit Überundungsmöglichkeit zurück, während ein Bild gezeichnet wird
    /// </summary>
    /// <param name="frameId">Nummer des Bildes, welches momentan gezeichnet wird</param>
    /// <param name="valueId">ID der Werte, welche gelesen werden sollen</param>
    /// <param name="startValue">Start-Wert, welcher nicht unterschritten werden soll</param>
    /// <param name="endValue">End-Wert, welcher nicht überschritten werden soll</param>
    /// <param name="valueOffset">Offset innerhalb der Werte</param>
    /// <returns>fertig berechneter Wert</returns>
    public double GetValueWrapped(long frameId, int valueId, double startValue, double endValue, int valueOffset = 0)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// [Debug] gibt alle gespeicherten Werte inkl. zugehörie ID zurück (langsam)
    /// </summary>
    /// <returns>Enumerable aller Werte</returns>
    public IEnumerable<KeyValuePair<int, double[]>> DebugGetAllValues()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// [Debug] gibt alle Werte anhand einer bestimmten ID zurück (oder null, falls die ID ungültig ist oder bereits gelöscht wurde)
    /// </summary>
    /// <param name="valueId">ID der Werte, welche gelesen werden sollen</param>
    /// <returns>ausgelesene Werte oder null, wenn die ID ungültig war oder bereits gelöscht wurde</returns>
    public double[] DebugGetValues(int valueId)
    {
      throw new NotImplementedException();
    }
    #endregion

    #region # public override string ToString() // gibt den Status als lesbare Zeichenkette zurück
    /// <summary>
    /// gibt den Status als lesbare Zeichenkette zurück
    /// </summary>
    /// <returns>lesbare Zeichenkette</returns>
    public override string ToString()
    {
      return (new { tickCount, ticksPerSecond, tickTime = nextTickTime, frameCount }).ToString();
    }
    #endregion
  }
}
