using System;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace MultiWaveDecoder
{
  // TODO: implement decodeSpectralDataER
  public sealed class Huffman : Codebooks
  {
    static readonly bool[] UNSIGNED = { false, false, true, true, false, false, true, true, true, true, true };
    const int QUAD_LEN = 4;
    const int PAIR_LEN = 2;

    static int findOffset(BitStream inStream, int[,] table)
    {
      int off = 0;
      int len = table[off, 0];
      int cw = inStream.readBits(len);
      while (cw != table[off, 1])
      {
        off++;
        int j = table[off, 0] - len;
        len = table[off, 0];
        cw <<= j;
        cw |= inStream.readBits(j);
      }
      return off;
    }

    static void signValues(BitStream inStream, int[] data, int off, int len)
    {
      for (int i = off; i < off + len; i++)
      {
        if (data[i] != 0)
        {
          if (inStream.readBool()) data[i] = -data[i];
        }
      }
    }

    static int getEscape(BitStream inStream, int s)
    {
      bool neg = s < 0;

      int i = 4;
      while (inStream.readBool())
      {
        i++;
      }
      int j = inStream.readBits(i) | (1 << i);

      return (neg ? -j : j);
    }

    public static int decodeScaleFactor(BitStream inStream)
    {
      int offset = findOffset(inStream, HCB_SF);
      return HCB_SF[offset, 2];
    }

    public static void decodeSpectralData(BitStream inStream, int cb, int[] data, int off)
    {
      var HCB = CODEBOOKS[cb - 1];

      // find index
      int offset = findOffset(inStream, HCB);

      // copy data
      data[off] = HCB[offset, 2];
      data[off + 1] = HCB[offset, 3];
      if (cb < 5)
      {
        data[off + 2] = HCB[offset, 4];
        data[off + 3] = HCB[offset, 5];
      }

      // sign & escape
      if (cb < 11)
      {
        if (UNSIGNED[cb - 1]) signValues(inStream, data, off, cb < 5 ? QUAD_LEN : PAIR_LEN);
      }
      else if (cb == 11 || cb > 15)
      {
        signValues(inStream, data, off, cb < 5 ? QUAD_LEN : PAIR_LEN); //virtual codebooks are always unsigned
        if (Math.Abs(data[off]) == 16) data[off] = getEscape(inStream, data[off]);
        if (Math.Abs(data[off + 1]) == 16) data[off + 1] = getEscape(inStream, data[off + 1]);
      }
      else throw new AACException("Huffman: unknown spectral codebook: " + cb);
    }
  }
}
