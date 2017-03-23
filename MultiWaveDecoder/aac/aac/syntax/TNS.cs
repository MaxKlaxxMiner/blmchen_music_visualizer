// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBeMadeStatic.Global
// ReSharper disable UnusedParameter.Global
namespace MultiWaveDecoder
{
  /// <summary>
  /// Temporal Noise Shaping
  /// </summary>
  public sealed class TNS : Constants
  {
    const int TNS_MAX_ORDER = 20;
    static readonly int[] SHORT_BITS = { 1, 4, 3 };
    static readonly int[] LONG_BITS = { 2, 6, 5 };
    // bitstream
    readonly int[] nFilt;
    readonly int[,] length;
    readonly int[,] order;
    readonly bool[,] direction;
    readonly float[, ,] coef;

    public TNS()
    {
      nFilt = new int[8];
      length = new int[8, 4];
      direction = new bool[8, 4];
      order = new int[8, 4];
      coef = new float[8, 4, TNS_MAX_ORDER];
    }

    static readonly float[] TNS_COEF_1_3 = { 0.00000000f, -0.43388373f, 0.64278758f, 0.34202015f };
    static readonly float[] TNS_COEF_0_3 = { 0.00000000f, -0.43388373f, -0.78183150f, -0.97492790f, 0.98480773f, 0.86602539f, 0.64278758f, 0.34202015f };
    static readonly float[] TNS_COEF_1_4 = { 0.00000000f, -0.20791170f, -0.40673664f, -0.58778524f, 0.67369562f, 0.52643216f, 0.36124167f, 0.18374951f };
    static readonly float[] TNS_COEF_0_4 = { 0.00000000f, -0.20791170f, -0.40673664f, -0.58778524f, -0.74314481f, -0.86602539f, -0.95105654f, -0.99452192f, 0.99573416f, 0.96182561f, 0.89516330f, 0.79801720f, 0.67369562f, 0.52643216f, 0.36124167f, 0.18374951f };
    static readonly float[][] TNS_TABLES = { TNS_COEF_0_3, TNS_COEF_0_4, TNS_COEF_1_3, TNS_COEF_1_4 };

    public void decode(BitStream inStream, ICSInfo info)
    {
      int windowCount = info.getWindowCount();
      var bits = info.isEightShortFrame() ? SHORT_BITS : LONG_BITS;

      int w;
      for (w = 0; w < windowCount; w++)
      {
        if ((nFilt[w] = inStream.readBits(bits[0])) != 0)
        {
          int coefRes = inStream.readBit();

          for (int filt = 0; filt < nFilt[w]; filt++)
          {
            length[w, filt] = inStream.readBits(bits[1]);

            if ((order[w, filt] = inStream.readBits(bits[2])) > 20) throw new AACException("TNS filter out of range: " + order[w, filt]);
            if (order[w, filt] != 0)
            {
              direction[w, filt] = inStream.readBool();
              int coefCompress = inStream.readBit();
              int coefLen = coefRes + 3 - coefCompress;
              int tmp = 2 * coefCompress + coefRes;

              for (int i = 0; i < order[w, filt]; i++)
              {
                coef[w, filt, i] = TNS_TABLES[tmp][inStream.readBits(coefLen)];
              }
            }
          }
        }
      }
    }

    public void process(ICStream ics, float[] spec, SampleFrequency sf, bool decode)
    {
      //TODO...
    }
  }
}
