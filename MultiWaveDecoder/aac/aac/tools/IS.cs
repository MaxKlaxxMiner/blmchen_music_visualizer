// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// Intensity stereo
  /// </summary>
  public abstract class IS : Constants
  {
    public static void process(CPE cpe, float[] specL, float[] specR)
    {
      var ics = cpe.getRightChannel();
      var info = ics.getInfo();
      var offsets = info.getSWBOffsets();
      int windowGroups = info.getWindowGroupCount();
      int maxSFB = info.getMaxSFB();
      var sfbCB = ics.getSfbCB();
      var sectEnd = ics.getSectEnd();
      var scaleFactors = ics.getScaleFactors();

      int idx = 0, groupOff = 0;
      for (int g = 0; g < windowGroups; g++)
      {
        for (int i = 0; i < maxSFB; )
        {
          int end;
          if (sfbCB[idx] == INTENSITY_HCB || sfbCB[idx] == INTENSITY_HCB2)
          {
            end = sectEnd[idx];
            for (; i < end; i++, idx++)
            {
              int c = sfbCB[idx] == INTENSITY_HCB ? 1 : -1;
              if (cpe.isMSMaskPresent()) c *= cpe.isMSUsed(idx) ? -1 : 1;
              float scale = c * scaleFactors[idx];
              for (int w = 0; w < info.getWindowGroupLength(g); w++)
              {
                int off = groupOff + w * 128 + offsets[i];
                for (int j = 0; j < offsets[i + 1] - offsets[i]; j++)
                {
                  specR[off + j] = specL[off + j] * scale;
                }
              }
            }
          }
          else
          {
            end = sectEnd[idx];
            idx += end - i;
            i = end;
          }
        }
        groupOff += info.getWindowGroupLength(g) * 128;
      }
    }
  }
}
