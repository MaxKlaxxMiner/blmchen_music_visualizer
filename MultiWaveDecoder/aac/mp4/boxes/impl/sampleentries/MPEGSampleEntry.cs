// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  /// <summary>
  /// The MPEG sample entry is used in MP4 streams other than video, audio and hint. It contains only one <code>ESDBox</code>.
  /// </summary>
  public class MPEGSampleEntry : SampleEntry
  {
    public MPEGSampleEntry():base("MPEG Sample Entry"){}

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      readChildren(inStream);
    }
  }
}
