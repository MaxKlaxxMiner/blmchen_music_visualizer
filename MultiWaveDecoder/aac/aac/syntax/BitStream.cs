// ReSharper disable InconsistentNaming
// ReSharper disable ClassCanBeSealed.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

using System;

#pragma warning disable 169
namespace MultiWaveDecoder
{
  public class BitStream
  {
    private const int WORD_BITS = 32;
    private const int WORD_BYTES = 4;
    private const int BYTE_MASK = 0xff;
    byte[] buffer;
    /// <summary>
    /// offset in the buffer array
    /// </summary>
    int pos;
    /// <summary>
    /// current 4 bytes, that are read from the buffer
    /// </summary>
    int cache;
    /// <summary>
    /// remaining bits in current cache
    /// </summary>
    protected int bitsCached;
    /// <summary>
    /// number of total bits read
    /// </summary>
    protected int position;

    public BitStream()
    {
    }

    public BitStream(byte[] data)
    {
      setData(data);
    }

    public void destroy()
    {
      reset();
      buffer = null;
    }

    public void setData(byte[] data)
    {
      // make the buffer size an integer number of words
      int size = WORD_BYTES * ((data.Length + WORD_BYTES - 1) / WORD_BYTES);

      // only reallocate if needed
      if (buffer == null || buffer.Length != size) buffer = new byte[size];
      Array.Copy(data, 0, buffer, 0, data.Length);
      reset();
    }

    public void byteAlign()
    {
      int toFlush = bitsCached & 7;
      if (toFlush > 0) skipBits(toFlush);
    }

    public void reset()
    {
      pos = 0;
      bitsCached = 0;
      cache = 0;
      position = 0;
    }

    public int getPosition()
    {
      return position;
    }

    public int getBitsLeft()
    {
      return 8 * (buffer.Length - pos) + bitsCached;
    }

    /// <summary>
    /// Reads the next four bytes.
    /// </summary>
    /// <param name="peek">if true, the stream pointer will not be increased</param>
    /// <returns></returns>
    protected int readCache(bool peek)
    {
      if (pos > buffer.Length - WORD_BYTES) throw new AACException("end of stream");
      int i = ((buffer[pos] & BYTE_MASK) << 24)
            | ((buffer[pos + 1] & BYTE_MASK) << 16)
            | ((buffer[pos + 2] & BYTE_MASK) << 8)
            | (buffer[pos + 3] & BYTE_MASK);
      if (!peek) pos += WORD_BYTES;
      return i;
    }

    public int readBits(int n)
    {
      int result;
      if (bitsCached >= n)
      {
        bitsCached -= n;
        result = (cache >> bitsCached) & maskBits(n);
        position += n;
      }
      else
      {
        position += n;
        int c = cache & maskBits(bitsCached);
        int left = n - bitsCached;
        cache = readCache(false);
        bitsCached = WORD_BITS - left;
        result = ((cache >> bitsCached) & maskBits(left)) | (c << left);
      }
      return result;
    }

    public int readBit()
    {
      int i;
      if (bitsCached > 0)
      {
        bitsCached--;
        i = (cache >> (bitsCached)) & 1;
        position++;
      }
      else
      {
        cache = readCache(false);
        bitsCached = WORD_BITS - 1;
        position++;
        i = (cache >> bitsCached) & 1;
      }
      return i;
    }

    public bool readBool()
    {
      return (readBit() & 0x1) != 0;
    }

    public int peekBits(int n)
    {
      int ret;
      if (bitsCached >= n)
      {
        ret = (cache >> (bitsCached - n)) & maskBits(n);
      }
      else
      {
        //old cache
        int c = cache & maskBits(bitsCached);
        n -= bitsCached;
        //read next & combine
        ret = ((readCache(true) >> WORD_BITS - n) & maskBits(n)) | (c << n);
      }
      return ret;
    }

    public int peekBit()
    {
      int ret;
      if (bitsCached > 0)
      {
        ret = (cache >> (bitsCached - 1)) & 1;
      }
      else
      {
        int word = readCache(true);
        ret = (word >> WORD_BITS - 1) & 1;
      }
      return ret;
    }

    public void skipBits(int n)
    {
      position += n;
      if (n <= bitsCached)
      {
        bitsCached -= n;
      }
      else
      {
        n -= bitsCached;
        while (n >= WORD_BITS)
        {
          n -= WORD_BITS;
          readCache(false);
        }
        if (n > 0)
        {
          cache = readCache(false);
          bitsCached = WORD_BITS - n;
        }
        else
        {
          cache = 0;
          bitsCached = 0;
        }
      }
    }

    public void skipBit()
    {
      position++;
      if (bitsCached > 0)
      {
        bitsCached--;
      }
      else
      {
        cache = readCache(false);
        bitsCached = WORD_BITS - 1;
      }
    }

    public static int maskBits(int n)
    {
      return n == 32 ? -1 : (1 << n) - 1;
    }

    public override string ToString()
    {
      return (new{ position, pos, bitsCached, cache }).ToString();
    }
  }
}
