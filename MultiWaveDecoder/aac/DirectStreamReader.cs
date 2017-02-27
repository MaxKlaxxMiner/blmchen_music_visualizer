
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
    /// liest einen Big-Endian Int-Wert aus, mit einer bestimmten Anzahl von Bytes, ohne den Offset zu verändern
    /// </summary>
    /// <param name="bytes">Anzahl der Bytes, welche gelesen werden sollen (0-8)</param>
    /// <returns></returns>
    public long PeekInt(int bytes)
    {
      long result = 0;

      int ofs = offset;
      while (--bytes >= 0)
      {
        result = (long)data[ofs++] << bytes * 8;
      }

      return result;
    }

    /// <summary>
    /// liest einen Big-Endian Int-Wert aus, mit einer bestimmten Anzahl von Bytes
    /// </summary>
    /// <param name="bytes">Anzahl der Bytes, welche gelesen werden sollen (0-8)</param>
    /// <returns></returns>
    public long ReadInt(int bytes)
    {
      long result = 0;

      while (--bytes >= 0)
      {
        result += (long)data[offset++] << bytes * 8;
      }

      return result;
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
    /// springt eine bestimmte Anzahl von Bytes weiter
    /// </summary>
    /// <param name="bytes">Anzahl der Bytes, welche übersprungen werden sollen (darf auch negativ sein)</param>
    public void Advance(int bytes)
    {
      Seek(offset + bytes);
    }

    /// <summary>
    /// gibt den Status als lesbare Zeichenkette zurück
    /// </summary>
    /// <returns>lesbare Zeichenkette</returns>
    public override string ToString()
    {
      return (new { offset, length = data.Length, avail = data.Length - offset }).ToString();
    }
  }
}
