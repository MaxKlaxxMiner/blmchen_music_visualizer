// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.IO;
// ReSharper disable MemberCanBePrivate.Global

namespace MultiWaveDecoder
{
  public sealed class MP4InputStream
  {
    readonly Stream inStream;
    long offset;
    readonly Queue<byte> peeked = new Queue<byte>();

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
    /// Returns the current offset in the stream.
    /// </summary>
    /// <returns>the current offset</returns>
    public long getOffset()
    {
      return offset;
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
        if (b) peeked.Enqueue((byte)i);
      }
      return b;
    }

    /// <summary>
    /// Reads <code>len</code> bytes of data from the input into the array <code>b</code>. If len is zero, then no bytes are read.
    /// 
    /// This method blocks until all bytes could be read, the end of the stream is detected, or an I/O error occurs.
    /// 
    /// If the stream ends before <code>len</code> bytes could be read an IOException is thrown.
    /// </summary>
    /// <param name="b">the buffer into which the data is read.</param>
    /// <param name="off">the start offset in array <code>b</code> at which the data is written.</param>
    /// <param name="len">the number of bytes to read.</param>
    public void read(byte[] b, int off, int len)
    {
      int read = 0;

      while (read < len && peeked.Count > 0)
      {
        b[off + read] = peeked.Dequeue();
        read++;
      }

      while (read < len)
      {
        int bytes = inStream.Read(b, off + read, len - read);
        if (bytes != len - read) throw new IOException();
        read += bytes;
      }

      offset += read;
    }

    /// <summary>
    /// Reads up to eight bytes as a long value. This method blocks until all bytes could be read, the end of the stream is detected, or an I/O error occurs.
    /// </summary>
    /// <param name="n">the number of bytes to read &gt;0 and &lt;=8</param>
    /// <returns>the read bytes as a long value</returns>
    public long readBytes(int n)
    {
      if (n < 1 || n > 8) throw new ArgumentOutOfRangeException("invalid number of bytes to read: " + n);
      var b = new byte[n];
      read(b, 0, n);

      long result = 0;
      for (int i = 0; i < n; i++)
      {
        result = (result << 8) | (b[i] & 0xFF);
      }
      return result;
    }
  }
}
