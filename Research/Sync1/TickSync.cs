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
    /// merkt sich die Funktion, welche beim berechnen bei jeden einzelnen Tick verwendet werden soll
    /// </summary>
    readonly Action<TickSync> tickFunction;

    /// <summary>
    /// gibt die Anzahl der insgesamt berechneten Ticks zurück
    /// </summary>
    long tickCount = 0;

    /// <summary>
    /// gibt die Anzahl der insgesamt berechneten Bilder zurück
    /// </summary>
    long frameCount = 0;

    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="ticksPerSecond">Anzahl der Ticks pro Sekunde, welche berechnet werden sollen</param>
    /// <param name="tickFunction">
    ///   Methode, welche zum berechnen der (gleichmäßigen) Ticks verwendet werden soll, 
    ///   Parameter: eigene Klasse
    /// </param>
    public TickSync(double ticksPerSecond, Action<TickSync> tickFunction)
    {
      if (ticksPerSecond < 0.1 || ticksPerSecond > 10000.0) throw new ArgumentOutOfRangeException("ticksPerSecond");
      if (tickFunction == null) throw new ArgumentNullException("tickFunction");

      this.ticksPerSecond = ticksPerSecond;
      this.tickFunction = tickFunction;
    }

    #region # // --- todo ---
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

    /// <summary>
    /// wird aufgerufen, wenn das Zeichnen eines neuen Bildes begonnen wird (gibt die aktuelle Bild-Nummer zurück)
    /// </summary>
    /// <param name="timeStamp">Zeitpunkt, welcher benutzt werden soll (darf immer nur aufsteigend gesetzt werden)</param>
    /// <returns>aktuelle Bildnummer (aufwärts, beginnend bei 1)</returns>
    public long FrameStartDrawing(double timeStamp)
    {
      return ++frameCount;
    }

    /// <summary>
    /// wird aufgerufen, wenn das Zeichnen des Bildes abgeschlossen wurde
    /// </summary>
    /// <param name="timeStamp">Zeitpunkt, welcher benutzt werden soll (darf immer nur aufsteigend gesetzt werden)</param>
    /// <param name="frameId">Nummer des Bildes, welches fertig gezeichnet wurde</param>
    public void FrameFinishDrawing(double timeStamp, long frameId)
    {
      if (frameId != frameCount) throw new NotSupportedException("async multiframe is not supported");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeStamp">Zeitpunkt, welcher benutzt werden soll (darf immer nur aufsteigend gesetzt werden)</param>
    /// <param name="frameId">Nummer des Bildes, welches angezeigt wurde</param>
    public void FrameDirectDisplayed(double timeStamp, long frameId)
    {
      if (frameId != frameCount) throw new NotSupportedException("async multiframe is not supported");
    }
  }
}