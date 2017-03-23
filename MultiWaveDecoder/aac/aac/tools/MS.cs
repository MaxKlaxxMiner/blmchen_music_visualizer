// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// Mid/side stereo
  /// </summary>
  public class MS : Constants
  {

    public static void process(CPE cpe, float[] specL, float[] specR)
    {
      var ics = cpe.getLeftChannel();
      var info = ics.getInfo();
      var offsets = info.getSWBOffsets();
      int windowGroups = info.getWindowGroupCount();
      int maxSFB = info.getMaxSFB();
      var sfbCBl = ics.getSfbCB();
      var sfbCBr = cpe.getRightChannel().getSfbCB();
      int groupOff = 0;
      int idx = 0;

      for (int g = 0; g < windowGroups; g++)
      {
        for (int i = 0; i < maxSFB; i++, idx++)
        {
          if (cpe.isMSUsed(idx) && sfbCBl[idx] < NOISE_HCB && sfbCBr[idx] < NOISE_HCB)
          {
            for (int w = 0; w < info.getWindowGroupLength(g); w++)
            {
              int off = groupOff + w * 128 + offsets[i];
              for (int j = 0; j < offsets[i + 1] - offsets[i]; j++)
              {
                float t = specL[off + j] - specR[off + j];
                specL[off + j] += specR[off + j];
                specR[off + j] = t;
              }
            }
          }
        }
        groupOff += info.getWindowGroupLength(g) * 128;
      }
    }
  }
}
