// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// ReSharper disable MemberCanBePrivate.Global

namespace MultiWaveDecoder
{
  public sealed class MP4InputStream
  {
    public const int MASK8 = 0xFF;
    public const int MASK16 = 0xFFFF;
    public const string UTF8 = "UTF-8";
    public const string UTF16 = "UTF-16";
    const int BYTE_ORDER_MASK = 0xFEFF;

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
    /// Reads the next byte of data from the input. The value byte is returned as an int in the range 0 to 255.
    /// If no byte is available because the end of the stream has been reached, an EOFException is thrown. This method blocks until input data is available, the end of the stream is detected, or an I/O error occurs.
    /// </summary>
    /// <returns>the next byte of data</returns>
    public int read()
    {
      return (int)readBytes(1);
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
      for (int i = 0; i < b.Length; i++)
      {
        result = (result << 8) | (b[i] & 0xFF);
      }
      return result;
    }

    public byte[] readByteArray(int n)
    {
      var result = new byte[n];
      read(result, 0, n);
      return result;
    }

    /// <summary>
    /// Skips <code>n</code> bytes in the input. This method blocks until all bytes could be skipped, the end of the stream is detected, or an I/O error occurs.
    /// </summary>
    /// <param name="n">the number of bytes to skip</param>
    public void skipBytes(long n)
    {
      long l = 0;
      while (l < n && peeked.Count > 0)
      {
        peeked.Dequeue();
        l++;
      }

      if (l < n)
      {
        var buf = new byte[Math.Min(65536, n - l)];
        while (l < n)
        {
          int next = inStream.Read(buf, 0, (int)Math.Min(buf.Length, n - l));
          if (next == 0) throw new EndOfStreamException();
          l += next;
        }
      }

      offset += l;
    }

    /// <summary>
    /// Reads <code>n</code> bytes from the input as a string. The bytes are directly converted into characters. If not enough bytes could be read, an EOFException is thrown.
    /// This method blocks until all bytes could be read, the end of the stream is detected, or an I/O error occurs.
    /// </summary>
    /// <param name="n">the length of the string.</param>
    /// <returns>the string, that was read</returns>
    public string readString(int n)
    {
      return new string(readByteArray(n).Select(c => (char)c).ToArray());
    }

    /// <summary>
    /// Reads a fixed point number from the input. The number is read as a <code>m.n</code> value, that results from deviding an integer by 2<sup>n</sup>.
    /// </summary>
    /// <param name="m">the number of bits before the point</param>
    /// <param name="n">the number of bits after the point</param>
    /// <returns>a floating point number with the same value</returns>
    public double readFixedPoint(int m, int n)
    {
      int bits = m + n;
      if ((bits % 8) != 0) throw new ArgumentException("number of bits is not a multiple of 8: " + (m + n));
      long l = readBytes(bits / 8);
      double x = Math.Pow(2, n);
      double d = l / x;
      return d;
    }

    /// <summary>
    /// Reads a byte array from the input that is terminated by a specific byte (the 'terminator'). The maximum number of bytes that can be read before 
    /// the terminator must appear must be specified.
    /// 
    /// The terminator will not be included in the returned array.
    /// 
    /// This method blocks until all bytes could be read, the end of the stream is detected, or an I/O error occurs.
    /// </summary>
    /// <param name="max">the maximum number of bytes to read, before the terminator </param>
    /// <param name="terminator">the byte that indicates the end of the array</param>
    /// <returns>the buffer into which the data is read.</returns>
    public byte[] readTerminated(int max, int terminator) // JFIX (unused: terminator)
    {
      var b = new byte[max];
      read(b, 0, max);

      for (int i = 0; i < max; i++)
      {
        if (b[i] == terminator) return b.Take(i).ToArray();
      }

      return b;
    }

    /// <summary>
    /// Reads a null-terminated UTF-encoded string from the input. The maximum number of bytes that can be read before the null must appear must be specified.
    /// Although the method is preferred for unicode, the encoding can be any charset name, that is supported by the system.
    /// 
    /// This method blocks until all bytes could be read, the end of the stream is detected, or an I/O error occurs.
    /// </summary>
    /// <param name="max">the maximum number of bytes to read, before the null-terminator must appear.</param>
    /// <param name="encoding">the charset used to encode the string</param>
    /// <returns>decoded string</returns>
    public string readEncodedString(int max, Encoding encoding)
    {
      return encoding.GetString(readTerminated(max, 0));
    }

    /// <summary>
    /// Reads a null-terminated UTF-encoded string from the input. The maximum number of bytes that can be read before the null must appear must be specified.
    /// The encoding is detected automatically, it may be UTF-8 or UTF-16 (determined by a byte order mask at the beginning).
    /// 
    /// This method blocks until all bytes could be read, the end of the stream is detected, or an I/O error occurs.
    /// </summary>
    /// <param name="max">the maximum number of bytes to read, before the null-terminator must appear.</param>
    /// <returns>the decoded string</returns>
    public string readUTFString(int max)
    {
      // --- read byte order mask ---
      var bom = new byte[2];
      read(bom, 0, 2);
      if (bom[0] == 0 || bom[1] == 0) return "";
      int i = (bom[0] << 8) | bom[1];

      // --- read null-terminated ---
      var b = readTerminated(max - 2, 0);

      // --- copy bom ---
      var b2 = new byte[b.Length + bom.Length];
      Array.Copy(bom, 0, b2, 0, bom.Length);
      Array.Copy(b, 0, b2, bom.Length, b.Length);

      return i == BYTE_ORDER_MASK ? Encoding.BigEndianUnicode.GetString(b2) : Encoding.UTF8.GetString(b2);
    }

    /// <summary>
    /// Peeks <code>len</code> bytes of data from the input into the array <code>b</code>. If len is zero, then no bytes are read.
    /// 
    /// This method blocks until all bytes could be read, the end of the stream is detected, or an I/O error occurs.
    /// 
    /// If the stream ends before <code>len</code> bytes could be read an EOFException is thrown.
    /// </summary>
    /// <param name="b">the buffer into which the data is read.</param>
    /// <param name="off">the start offset in array <code>b</code> at which the data is written.</param>
    /// <param name="len">the number of bytes to read.</param>
    public void peek(byte[] b, int off, int len)
    {
      int read = 0;

      var allPeekedBytes = peeked.ToArray();
      while (read < len && read < allPeekedBytes.Length)
      {
        b[off + read] = allPeekedBytes[read];
        read++;
      }

      while (read < len)
      {
        int i = inStream.Read(b, off + read, len - read);
        if (i == 0) throw new EndOfStreamException();
        for (int j = 0; j < i; j++)
        {
          peeked.Enqueue(b[off + j]);
        }
        read += i;
      }
    }

    /// <summary>
    /// Peeks up to eight bytes as a long value. This method blocks until all bytes could be read, the end of the stream is detected, or an I/O error occurs.
    /// </summary>
    /// <param name="n">the number of bytes to read &gt;0 and &lt;=8</param>
    /// <returns>the read bytes as a long value</returns>
    public long peekBytes(int n)
    {
      if (n < 1 || n > 8) throw new ArgumentOutOfRangeException("invalid number of bytes to read: " + n);
      var b = new byte[n];
      peek(b, 0, n);

      long result = 0;
      for (int i = 0; i < n; i++)
      {
        result = (result << 8) | (b[i] & 0xFF);
      }
      return result;
    }

    /// <summary>
    /// Peeks data from the input stream and stores them into the buffer array b.
    /// This method blocks until all bytes could be read, the end of the stream  is detected, or an I/O error occurs.
    /// If the length of b is zero, then no bytes are read.
    /// </summary>
    /// <param name="b">b the buffer into which the data is read.</param>
    public void peekBytes(byte[] b)
    {
      peek(b, 0, b.Length);
    }

    public override string ToString()
    {
      return (new { offset }).ToString();
    }
  }
}
