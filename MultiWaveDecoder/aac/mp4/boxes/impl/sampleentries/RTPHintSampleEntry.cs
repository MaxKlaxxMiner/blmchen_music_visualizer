// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  public sealed class RTPHintSampleEntry : SampleEntry
  {
    int hintTrackVersion, highestCompatibleVersion;
    long maxPacketSize;

    public RTPHintSampleEntry() : base("RTP Hint Sample Entry") { }

    public override void decode(MP4InputStream inStream)
    {
      if (parent.getType() == BoxType.UNKNOWN_HNTI_BOX)
      {
        // mpeg4 iod
        string content = inStream.readString((int)getLeft(inStream));
      }
      else
      {
        base.decode(inStream);

        hintTrackVersion = (int)inStream.readBytes(2);
        highestCompatibleVersion = (int)inStream.readBytes(2);
        maxPacketSize = inStream.readBytes(4);
      }
    }

    /// <summary>
    /// The maximum packet size indicates the size of the largest packet that this track will generate.
    /// </summary>
    /// <returns>the maximum packet size</returns>
    public long getMaxPacketSize()
    {
      return maxPacketSize;
    }
  }
}
