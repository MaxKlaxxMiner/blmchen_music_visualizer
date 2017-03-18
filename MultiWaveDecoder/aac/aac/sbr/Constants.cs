// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
namespace MultiWaveDecoder
{
  public abstract class Constants
  {
    static int[] startMinTable = {7, 7, 10, 11, 12, 16, 16, 17, 24, 32, 35, 48};
    static int[] offsetIndexTable = {5, 5, 4, 4, 4, 3, 2, 1, 0, 6, 6, 6};
    static int[][] OFFSET =
    {
		  new [] {-8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7}, // 16000
		  new [] {-5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13},  // 22050
		  new [] {-5, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16},  // 24000
		  new [] {-6, -4, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16},  // 32000
		  new [] {-4, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16, 20},  // 44100-64000
		  new [] {-2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16, 20, 24},  // >64000
		  new [] {0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 16, 20, 24, 28, 33}
	  };
    const int EXTENSION_ID_PS = 2;
    const int MAX_NTSRHFG = 40; // maximum of number_time_slots * rate + HFGen. 16*2+8
    const int MAX_NTSR = 32;    // max number_time_slots * rate, ok for DRM and not DRM mode
    const int MAX_M = 49;       // maximum value for M
    const int MAX_L_E = 5;      // maximum value for L_E
    const int EXT_SBR_DATA = 13;
    const int EXT_SBR_DATA_CRC = 14;
    const int FIXFIX = 0;
    const int FIXVAR = 1;
    const int VARFIX = 2;
    const int VARVAR = 3;
    const int LO_RES = 0;
    const int HI_RES = 1;
    const int NO_TIME_SLOTS_960 = 15;
    const int NO_TIME_SLOTS = 16;
    const int RATE = 2;
    const int NOISE_FLOOR_OFFSET = 6;
    const int T_HFGEN = 8;
    const int T_HFADJ = 2;
  }
}
