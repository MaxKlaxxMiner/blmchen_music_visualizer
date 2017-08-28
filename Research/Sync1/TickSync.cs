using System;
using System.Diagnostics;
// ReSharper disable MemberCanBePrivate.Global

namespace Sync1
{
  /// <summary>
  /// Klasse für eine vollständiges Tick-System
  /// </summary>
  public class TickSync
  {
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

    /// <summary>
    /// gibt die Anzahl der insgesamt berechneten Ticks zurück
    /// </summary>
    long tickCount = 0;

    /// <summary>
    /// gibt die Anzahl der insgesamt berechneten Bilder zurück
    /// </summary>
    long frameCount = 0;

    /// <summary>
    /// Zeitpunkt des letzten Ticks (in Sekunden seit Klassen-Initialisierung)
    /// </summary>
    double lastTickTime;
    /// <summary>
    /// Zeitpunkt des nächsten Ticks (in Sekunden seit Klassen-Initialisierung)
    /// </summary>
    double nextTickTime;

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
    }

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
          if (nextTickTime > timeStamp) break; // genügend Ticks berechnet?

          if (nextTickTime > tickTimeLimit) // Zeit-Limit pro Bild überschritten?
          {
            tickCountLimit = 0;
          }
        }

        if (tickCountLimit <= 0 && maxTickSkip && nextTickTime < timeStamp) // Limit überschritten? -> Zeitpunkte angleichen, sofern dies erlaubt ist (sonst beim nächsten Bild versuchen alle Ticks zu berechnen)
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
  }
}