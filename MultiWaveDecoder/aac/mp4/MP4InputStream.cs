// ReSharper disable InconsistentNaming
using System.Collections.Generic;
using System.IO;

namespace MultiWaveDecoder
{
  public sealed class MP4InputStream
  {
    readonly Stream inStream;
    long offset;
    readonly List<byte> peeked = new List<byte>();

    /// <summary>
    /// Constructs an <code>MP4InputStream</code> that reads from an <code>InputStream</code>. It will have no random access, thus seeking will not be possible.
    /// </summary>
    /// <param name="inStream">an <code>InputStream</code> to read from</param>
    public MP4InputStream(Stream inStream)
    {
      this.inStream = inStream;
      offset = 0;
    }

    /// <summary>
    /// Indicates, if the input has some data left.
    /// </summary>
    /// <returns>true if there is at least one byte left</returns>
    public bool hasLeft()
    {
      bool b;
      if (peeked.Count > 0)
      {
        b = true;
      }
      else
      {
        int i = inStream.ReadByte();
        b = (i != -1);
        if (b) peeked.Add((byte)i);
      }
      return b;
    }

  }
}
