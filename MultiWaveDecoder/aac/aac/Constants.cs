// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

using System;
using System.Linq;

namespace MultiWaveDecoder
{
  public abstract class Constants
  {
    protected static int[] startMinTable = { 7, 7, 10, 11, 12, 16, 16, 17, 24, 32, 35, 48 };
    protected static int[] offsetIndexTable = { 5, 5, 4, 4, 4, 3, 2, 1, 0, 6, 6, 6 };
    protected static int[,] OFFSET =
    {
		  {-8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7}, // 16000
		  {-5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13},  // 22050
		  {-5, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16},  // 24000
		  {-6, -4, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16},  // 32000
		  {-4, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16, 20},  // 44100-64000
		  {-2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16, 20, 24},  // >64000
		  {0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16, 20, 24, 28, 33}
	  };
    protected const int EXTENSION_ID_PS = 2;
    protected const int MAX_NTSRHFG = 40; // maximum of number_time_slots * rate + HFGen. 16*2+8
    protected const int MAX_NTSR = 32;    // max number_time_slots * rate, ok for DRM and not DRM mode
    protected const int MAX_M = 49;       // maximum value for M
    protected const int MAX_L_E = 5;      // maximum value for L_E
    protected const int EXT_SBR_DATA = 13;
    protected const int EXT_SBR_DATA_CRC = 14;
    protected const int FIXFIX = 0;
    protected const int FIXVAR = 1;
    protected const int VARFIX = 2;
    protected const int VARVAR = 3;
    protected const int LO_RES = 0;
    protected const int HI_RES = 1;
    protected const int NO_TIME_SLOTS_960 = 15;
    protected const int NO_TIME_SLOTS = 16;
    protected const int RATE = 2;
    protected const int NOISE_FLOOR_OFFSET = 6;
    protected const int T_HFGEN = 8;
    protected const int T_HFADJ = 2;

    protected const int MAX_ELEMENTS = 16;
    protected const int BYTE_MASK = 0xFF;
    /// <summary>
    /// 6144 bits/channel
    /// </summary>
    protected const int MIN_INPUT_SIZE = 768;

    // frame length
    protected const int WINDOW_LEN_LONG = 1024;
    protected const int WINDOW_LEN_SHORT = WINDOW_LEN_LONG / 8;
    protected const int WINDOW_SMALL_LEN_LONG = 960;
    protected const int WINDOW_SMALL_LEN_SHORT = WINDOW_SMALL_LEN_LONG / 8;

    // element types
    protected const int ELEMENT_SCE = 0;
    protected const int ELEMENT_CPE = 1;
    protected const int ELEMENT_CCE = 2;
    protected const int ELEMENT_LFE = 3;
    protected const int ELEMENT_DSE = 4;
    protected const int ELEMENT_PCE = 5;
    protected const int ELEMENT_FIL = 6;
    protected const int ELEMENT_END = 7;

    // maximum numbers
    protected const int MAX_WINDOW_COUNT = 8;
    protected const int MAX_WINDOW_GROUP_COUNT = MAX_WINDOW_COUNT;
    protected const int MAX_LTP_SFB = 40;
    protected const int MAX_SECTIONS = 120;
    protected const int MAX_MS_MASK = 128;
    protected const float SQRT2 = 1.414213562f;

    // --- HCB ---
    protected const int ZERO_HCB = 0;
    protected const int ESCAPE_HCB = 11;
    protected const int NOISE_HCB = 13;
    protected const int INTENSITY_HCB2 = 14;
    protected const int INTENSITY_HCB = 15;
    protected const int FIRST_PAIR_HCB = 5;

    // --- Sine Window ---

    static float[] CreateSine(int len)
    {
      return Enumerable.Range(0, len).Select(i => (float)Math.Sin(Math.PI / 2.0 / len * (i + 0.5))).ToArray();
    }

    protected static readonly float[] SINE_32 = CreateSine(32);
    protected static readonly float[] SINE_120 = CreateSine(120);
    protected static readonly float[] SINE_128 = CreateSine(128);
    protected static readonly float[] SINE_256 = CreateSine(256);
    protected static readonly float[] SINE_960 = CreateSine(960);
    protected static readonly float[] SINE_1024 = CreateSine(1024);

    // --- KBD Window ---

    static float[] CreateKbd(int alpha, int len)
    {
      var f = new double[len];

      double sum = 0;
      double alpha2 = (Math.PI / len * alpha) * (Math.PI / len * alpha);

      for (int i = 0; i < len; i++)
      {
        double tmp = i * (len - i) * alpha2;
        double bessel = 1.0;

        for (int j = 50; j > 0; j--) bessel = bessel * tmp / (j * j) + 1.0;

        sum += bessel;
        f[i] = sum;
      }

      sum++;
      return f.Select(x => (float)Math.Sqrt(x / sum)).ToArray();
    }

    protected static readonly float[] KBD_32 = CreateKbd(8, 32);
    protected static readonly float[] KBD_120 = CreateKbd(6, 120);
    protected static readonly float[] KBD_128 = CreateKbd(6, 128);
    protected static readonly float[] KBD_256 = CreateKbd(5, 256);
    protected static readonly float[] KBD_960 = CreateKbd(4, 960);
    protected static readonly float[] KBD_1024 = CreateKbd(4, 1024);

    // --- FFT Tables ---

    protected static readonly float[,] FFT_TABLE_60 = GenerateFFTTableShort(60);
    protected static readonly float[,] FFT_TABLE_64 = GenerateFFTTableShort(64);
    protected static readonly float[,] FFT_TABLE_480 = GenerateFFTTableLong(480);
    protected static readonly float[,] FFT_TABLE_512 = GenerateFFTTableLong(512);

    static float[,] ConvertDoubleToFloat(double[,] data)
    {
      int xs = data.GetLength(0);
      int ys = data.GetLength(1);
      var result = new float[xs, ys];

      for (int x = 0; x < xs; x++)
      {
        for (int y = 0; y < ys; y++)
        {
          result[x, y] = (float)data[x, y];
        }
      }

      return result;
    }

    static float[,] GenerateFFTTableShort(int len)
    {
      double t = 2.0 * Math.PI / len;
      double cosT = Math.Cos(t);
      double sinT = Math.Sin(t);
      var f = new double[len, 2];

      f[0, 0] = 1;
      f[0, 1] = 0;
      double lastImag = 0.0;

      for (int i = 1; i < len; i++)
      {
        f[i, 0] = f[i - 1, 0] * cosT + lastImag * sinT;
        lastImag = lastImag * cosT - f[i - 1, 0] * sinT;
        f[i, 1] = -lastImag;
      }

      return ConvertDoubleToFloat(f);
    }

    static float[,] GenerateFFTTableLong(int len)
    {
      double t = 2.0 * Math.PI / len;
      double cosT = Math.Cos(t);
      double sinT = Math.Sin(t);
      var f = new double[len, 3];

      f[0, 0] = 1;
      f[0, 1] = 0;
      f[0, 2] = 0;

      for (var i = 1; i < len; i++)
      {
        f[i, 0] = f[i - 1, 0] * cosT + f[i - 1, 2] * sinT;
        f[i, 2] = f[i - 1, 2] * cosT - f[i - 1, 0] * sinT;
        f[i, 1] = -f[i, 2];
      }

      return ConvertDoubleToFloat(f);
    }

    // --- Gain ---

    protected const int BANDS = 4;
    protected const int MAX_CHANNELS = 5;
    protected const int NPQFTAPS = 96;
    protected const int NPEPARTS = 64;	//number of pre-echo inhibition parts
    protected const int ID_GAIN = 16;
    protected static readonly int[] LN_GAIN = { -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
  }
}
