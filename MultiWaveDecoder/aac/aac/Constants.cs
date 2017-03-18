// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

using System;
using System.Linq;

namespace MultiWaveDecoder
{
  public abstract class Constants
  {
    protected static T[][] Fill<T>(int x1, int x2) { return Enumerable.Range(0, x1).Select(c => new T[x2]).ToArray(); }
    protected static T[][][] Fill<T>(int x1, int x2, int x3) { return Enumerable.Range(0, x1).Select(c => Enumerable.Range(0, x2).Select(z => new T[x3]).ToArray()).ToArray(); }
    protected static T[][][][] Fill<T>(int x1, int x2, int x3, int x4) { return Enumerable.Range(0, x1).Select(c => Enumerable.Range(0, x2).Select(z => Enumerable.Range(0, x3).Select(r => new T[x4]).ToArray()).ToArray()).ToArray(); }

    protected static int[] startMinTable = { 7, 7, 10, 11, 12, 16, 16, 17, 24, 32, 35, 48 };
    protected static int[] offsetIndexTable = { 5, 5, 4, 4, 4, 3, 2, 1, 0, 6, 6, 6 };
    protected static int[][] OFFSET =
    {
		  new [] {-8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7}, // 16000
		  new [] {-5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13},  // 22050
		  new [] {-5, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16},  // 24000
		  new [] {-6, -4, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16},  // 32000
		  new [] {-4, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16, 20},  // 44100-64000
		  new [] {-2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16, 20, 24},  // >64000
		  new [] {0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16, 20, 24, 28, 33}
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

    // --- SineWindow ---

    static float[] CreateSine(int steps)
    {
      return Enumerable.Range(0, steps).Select(i => (float)Math.Sin(Math.PI / 2.0 / steps * (i + 0.5))).ToArray();
    }

    protected static float[] SINE_120 = CreateSine(120);

    protected static float[] SINE_128 = CreateSine(128);

    protected static float[] SINE_960 = CreateSine(960);

    protected static float[] SINE_1024 = CreateSine(1024);
  }
}
