// ReSharper disable InconsistentNaming

using System;

namespace MultiWaveDecoder
{
  /// <summary>
  /// inverse modified discrete cosine transform
  /// </summary>
  public sealed class IMDCT : Constants
  {

    //class IMDCT implements GCConstants, IMDCTTables, Windows {

    static readonly float[][] LONG_WINDOWS = { SINE_256, KBD_256 };
    static readonly float[][] SHORT_WINDOWS = { SINE_32, KBD_32 };
    int frameLen, shortFrameLen, lbLong, lbShort, lbMid;

    public IMDCT(int frameLen)
    {
      this.frameLen = frameLen;
      lbLong = frameLen / BANDS;
      shortFrameLen = frameLen / 8;
      lbShort = shortFrameLen / BANDS;
      lbMid = (lbLong - lbShort) / 2;
    }

    public void process(float[] inData, float[] outData, int winShape, int winShapePrev, ICSInfo.WindowSequence winSeq)
    {
      var buf = new float[frameLen];

      if (winSeq == ICSInfo.WindowSequence.EIGHT_SHORT_SEQUENCE)
      {
        for (int b = 0; b < BANDS; b++)
        {
          for (int j = 0; j < 8; j++)
          {
            for (int i = 0; i < lbShort; i++)
            {
              if (b % 2 == 0) buf[lbLong * b + lbShort * j + i] = inData[shortFrameLen * j + lbShort * b + i];
              else buf[lbLong * b + lbShort * j + i] = inData[shortFrameLen * j + lbShort * b + lbShort - 1 - i];
            }
          }
        }
      }
      else
      {
        for (int b = 0; b < BANDS; b++)
        {
          for (int i = 0; i < lbLong; i++)
          {
            if (b % 2 == 0) buf[lbLong * b + i] = inData[lbLong * b + i];
            else buf[lbLong * b + i] = inData[lbLong * b + lbLong - 1 - i];
          }
        }
      }

      for (int b = 0; b < BANDS; b++)
      {
        process2(buf, outData, winSeq, winShape, winShapePrev, b);
      }
    }

    private void process2(float[] inData, float[] outData, ICSInfo.WindowSequence winSeq, int winShape, int winShapePrev, int band)
    {
      float[] bufIn = new float[lbLong];
      float[] bufOut = new float[lbLong * 2];
      float[] window = new float[lbLong * 2];
      float[] window1 = new float[lbShort * 2];
      float[] window2 = new float[lbShort * 2];

      //init windows
      int i;
      switch (winSeq)
      {
        case ICSInfo.WindowSequence.ONLY_LONG_SEQUENCE:
        {
          for (i = 0; i < lbLong; i++)
          {
            window[i] = LONG_WINDOWS[winShapePrev][i];
            window[lbLong * 2 - 1 - i] = LONG_WINDOWS[winShape][i];
          }
        } break;
        case ICSInfo.WindowSequence.EIGHT_SHORT_SEQUENCE:
        {
          for (i = 0; i < lbShort; i++)
          {
            window1[i] = SHORT_WINDOWS[winShapePrev][i];
            window1[lbShort * 2 - 1 - i] = SHORT_WINDOWS[winShape][i];
            window2[i] = SHORT_WINDOWS[winShape][i];
            window2[lbShort * 2 - 1 - i] = SHORT_WINDOWS[winShape][i];
          }
        } break;
        case ICSInfo.WindowSequence.LONG_START_SEQUENCE:
        {
          for (i = 0; i < lbLong; i++)
          {
            window[i] = LONG_WINDOWS[winShapePrev][i];
          }
          for (i = 0; i < lbMid; i++)
          {
            window[i + lbLong] = 1.0f;
          }

          for (i = 0; i < lbShort; i++)
          {
            window[i + lbMid + lbLong] = SHORT_WINDOWS[winShape][lbShort - 1 - i];
          }
          for (i = 0; i < lbMid; i++)
          {
            window[i + lbMid + lbLong + lbShort] = 0.0f;
          }
        } break;
        case ICSInfo.WindowSequence.LONG_STOP_SEQUENCE:
        {
          for (i = 0; i < lbMid; i++)
          {
            window[i] = 0.0f;
          }
          for (i = 0; i < lbShort; i++)
          {
            window[i + lbMid] = SHORT_WINDOWS[winShapePrev][i];
          }
          for (i = 0; i < lbMid; i++)
          {
            window[i + lbMid + lbShort] = 1.0f;
          }
          for (i = 0; i < lbLong; i++)
          {
            window[i + lbMid + lbShort + lbMid] = LONG_WINDOWS[winShape][lbLong - 1 - i];
          }
        } break;
      }

      int j;
      if (winSeq == ICSInfo.WindowSequence.EIGHT_SHORT_SEQUENCE)
      {
        int k;
        for (j = 0; j < 8; j++)
        {
          for (k = 0; k < lbShort; k++)
          {
            bufIn[k] = inData[band * lbLong + j * lbShort + k];
          }
          if (j == 0) Array.Copy(window1, 0, window, 0, lbShort * 2);
          else Array.Copy(window2, 0, window, 0, lbShort * 2);
          imdct(bufIn, bufOut, window, lbShort);
          for (k = 0; k < lbShort * 2; k++)
          {
            outData[band * lbLong * 2 + j * lbShort * 2 + k] = bufOut[k] / 32.0f;
          }
        }
      }
      else
      {
        for (j = 0; j < lbLong; j++)
        {
          bufIn[j] = inData[band * lbLong + j];
        }
        imdct(bufIn, bufOut, window, lbLong);
        for (j = 0; j < lbLong * 2; j++)
        {
          outData[band * lbLong * 2 + j] = bufOut[j] / 256.0f;
        }
      }
    }

    void imdct(float[] inData, float[] outData, float[] window, int n)
    {
      int n2 = n / 2;
      float[,] table, table2;

      throw new NotImplementedException();

      //if (n == 256)
      //{
      //  table = IMDCT_TABLE_256;
      //  table2 = IMDCT_POST_TABLE_256;
      //}
      //else if (n == 32)
      //{
      //  table = IMDCT_TABLE_32;
      //  table2 = IMDCT_POST_TABLE_32;
      //}
      //else throw new AACException("gain control: unexpected IMDCT length");

      //float[] tmp = new float[n];
      //int i;
      //for (i = 0; i < n2; ++i)
      //{
      //  tmp[i] = inData[2 * i];
      //}
      //for (i = n2; i < n; ++i)
      //{
      //  tmp[i] = -inData[2 * n - 1 - 2 * i];
      //}

      ////pre-twiddle
      //float[,] buf = new float[n2, 2];
      //for (i = 0; i < n2; i++)
      //{
      //  buf[i, 0] = (table[i, 0] * tmp[2 * i]) - (table[i, 1] * tmp[2 * i + 1]);
      //  buf[i, 1] = (table[i, 0] * tmp[2 * i + 1]) + (table[i, 1] * tmp[2 * i]);
      //}

      //// fft
      //FFT.process(buf, n2 != 0);

      //// post-twiddle and reordering
      //for (i = 0; i < n2; i++)
      //{
      //  tmp[i] = table2[i, 0] * buf[i, 0] + table2[i, 1] * buf[n2 - 1 - i, 0]
      //      + table2[i, 2] * buf[i, 1] + table2[i, 3] * buf[n2 - 1 - i, 1];
      //  tmp[n - 1 - i] = table2[i, 2] * buf[i, 0] - table2[i, 3] * buf[n2 - 1 - i, 0]
      //      - table2[i, 0] * buf[i, 1] + table2[i, 1] * buf[n2 - 1 - i, 1];
      //}

      //// copy to output and apply window
      //Array.Copy(tmp, n2, outData, 0, n2);
      //for (i = n2; i < n * 3 / 2; ++i)
      //{
      //  outData[i] = -tmp[n * 3 / 2 - 1 - i];
      //}
      //for (i = n * 3 / 2; i < n * 2; ++i)
      //{
      //  outData[i] = -tmp[i - n * 3 / 2];
      //}

      //for (i = 0; i < n; i++)
      //{
      //  outData[i] *= window[i];
      //}
    }
  }

}
