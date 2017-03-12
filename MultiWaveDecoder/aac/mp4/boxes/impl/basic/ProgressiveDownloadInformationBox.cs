using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  /// <summary>
  /// The Progressive download information box aids the progressive download of an ISO file. The box contains pairs of numbers (to the end of the box)
  /// specifying combinations of effective file download bitrate in units of bytes/sec and a suggested initial playback delay in units of milliseconds.
  /// 
  /// The download rate can be estimated from the download rate and obtain an upper estimate for a suitable initial delay by linear interpolation between pairs, or by extrapolation from the first or last entry.
  /// </summary>
  public class ProgressiveDownloadInformationBox : FullBox
  {
    readonly List<KeyValuePair<int, int>> pairs = new List<KeyValuePair<int, int>>();

    public ProgressiveDownloadInformationBox()
      : base("Progressive Download Information Box")
    {
    }

    /// <summary>
    /// The map contains pairs of bitrates and playback delay.
    /// </summary>
    /// <returns>the information pairs</returns>
    public List<KeyValuePair<int, int>> getInformationPairs()
    {
      return pairs;
    }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      while (getLeft(inStream) > 0)
      {
        int rate = (int)inStream.readBytes(4);
        int initialDelay = (int)inStream.readBytes(4);
        pairs.Add(new KeyValuePair<int, int>(rate, initialDelay));
      }
    }
  }
}
