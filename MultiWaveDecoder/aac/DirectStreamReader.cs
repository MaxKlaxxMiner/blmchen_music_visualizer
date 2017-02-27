
#region # using *.*

using System;

#endregion

namespace MultiWaveDecoder
{
  /// <summary>
  /// eigener Streamreader
  /// </summary>
  public sealed class DirectStreamReader
  {
    /// <summary>
    /// merkt sich die Daten des Streams
    /// </summary>
    readonly byte[] data;

    /// <summary>
    /// merkt sich die aktuelle Leseposition
    /// </summary>
    public int offset;

    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="data">Daten, welche für den Streamreader verwendet werden sollen</param>
    public DirectStreamReader(byte[] data)
    {
      this.data = data;
      offset = 0;
    }

    /// <summary>
    /// gibt, ob eine bestimmte Anzahl von Bytes gelesen werden können
    /// </summary>
    /// <param name="count">Anzahl der gewünschten Bytes</param>
    /// <returns>true, wenn die jeweilige Anzahl der Bytes bereit stehen</returns>
    public bool Available(int count)
    {
      return offset >= 0 && offset + count < data.Length;
    }

    /// <summary>
    /// liest einen UInt16-Wert aus dem Stream
    /// </summary>
    /// <returns></returns>
    public ushort ReadUInt16()
    {
      offset += 2;
      return (ushort)(data[offset - 2] + (data[offset - 1] << 8));
    }

    /// <summary>
    /// setzt die Leseposition neu
    /// </summary>
    /// <param name="offset">neue Offset-Position im Stream</param>
    public void Seek(int offset)
    {
      if (offset < 0 || offset > data.Length) throw new ArgumentOutOfRangeException("offset");
      this.offset = offset;
    }

    /// <summary>
    /// gibt den Status als lesbare Zeichenkette zurück
    /// </summary>
    /// <returns>lesbare Zeichenkette</returns>
    public override string ToString()
    {
      return (new { offset, length = data.Length }).ToString();
    }
  }
}
