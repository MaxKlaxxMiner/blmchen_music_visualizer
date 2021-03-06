﻿// ReSharper disable NotAccessedField.Local
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

namespace MultiWaveDecoder
{
  public sealed class FFT : Constants
  {
    readonly int length;
    readonly float[,] rev;
    readonly float[,] roots;
    readonly float[] a = new float[2];
    readonly float[] b = new float[2];
    readonly float[] c = new float[2];
    readonly float[] d = new float[2];
    readonly float[] e1 = new float[2];
    readonly float[] e2 = new float[2];

    public FFT(int length)
    {
      this.length = length;
      rev = new float[length, 2];

      switch (length)
      {
        case 64: roots = FFT_TABLE_64; break;
        case 512: roots = FFT_TABLE_512; break;
        case 60: roots = FFT_TABLE_60; break;
        case 480: roots = FFT_TABLE_480; break;
        default: throw new AACException("unexpected FFT length: " + length);
      }
    }

    public void process(float[,] inData, bool forward)
    {
      int imOff = (forward ? 2 : 1);
      int scale = (forward ? length : 1);
      //bit-reversal
      int ii = 0;
      for (int i = 0; i < length; i++)
      {
        rev[i, 0] = inData[ii, 0];
        rev[i, 1] = inData[ii, 1];
        int k = length >> 1;
        while (ii >= k && k > 0)
        {
          ii -= k;
          k >>= 1;
        }
        ii += k;
      }
      for (int i = 0; i < length; i++)
      {
        inData[i, 0] = rev[i, 0];
        inData[i, 1] = rev[i, 1];
      }

      //bottom base-4 round
      for (int i = 0; i < length; i += 4)
      {
        a[0] = inData[i, 0] + inData[i + 1, 0];
        a[1] = inData[i, 1] + inData[i + 1, 1];
        b[0] = inData[i + 2, 0] + inData[i + 3, 0];
        b[1] = inData[i + 2, 1] + inData[i + 3, 1];
        c[0] = inData[i, 0] - inData[i + 1, 0];
        c[1] = inData[i, 1] - inData[i + 1, 1];
        d[0] = inData[i + 2, 0] - inData[i + 3, 0];
        d[1] = inData[i + 2, 1] - inData[i + 3, 1];
        inData[i, 0] = a[0] + b[0];
        inData[i, 1] = a[1] + b[1];
        inData[i + 2, 0] = a[0] - b[0];
        inData[i + 2, 1] = a[1] - b[1];

        e1[0] = c[0] - d[1];
        e1[1] = c[1] + d[0];
        e2[0] = c[0] + d[1];
        e2[1] = c[1] - d[0];
        if (forward)
        {
          inData[i + 1, 0] = e2[0];
          inData[i + 1, 1] = e2[1];
          inData[i + 3, 0] = e1[0];
          inData[i + 3, 1] = e1[1];
        }
        else
        {
          inData[i + 1, 0] = e1[0];
          inData[i + 1, 1] = e1[1];
          inData[i + 3, 0] = e2[0];
          inData[i + 3, 1] = e2[1];
        }
      }

      //iterations from bottom to top
      for (int i = 4; i < length; i <<= 1)
      {
        int shift = i << 1;
        int m = length / shift;
        for (int j = 0; j < length; j += shift)
        {
          for (int k = 0; k < i; k++)
          {
            int km = k * m;
            float rootRe = roots[km, 0];
            float rootIm = roots[km, imOff];
            float zRe = inData[i + j + k, 0] * rootRe - inData[i + j + k, 1] * rootIm;
            float zIm = inData[i + j + k, 0] * rootIm + inData[i + j + k, 1] * rootRe;

            inData[i + j + k, 0] = (inData[j + k, 0] - zRe) * scale;
            inData[i + j + k, 1] = (inData[j + k, 1] - zIm) * scale;
            inData[j + k, 0] = (inData[j + k, 0] + zRe) * scale;
            inData[j + k, 1] = (inData[j + k, 1] + zIm) * scale;
          }
        }
      }
    }
  }
}
