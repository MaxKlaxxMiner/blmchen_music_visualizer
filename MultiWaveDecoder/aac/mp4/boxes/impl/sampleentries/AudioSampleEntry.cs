// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  public class AudioSampleEntry : SampleEntry
  {
    int channelCount, sampleSize, sampleRate;

    public AudioSampleEntry(string name) : base(name) { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      inStream.skipBytes(8); //reserved
      channelCount = (int)inStream.readBytes(2);
      sampleSize = (int)inStream.readBytes(2);
      inStream.skipBytes(2); //pre-defined: 0
      inStream.skipBytes(2); //reserved
      sampleRate = (int)inStream.readBytes(2);
      inStream.skipBytes(2); //not used by samplerate

      readChildren(inStream);
    }

    public int getChannelCount()
    {
      return channelCount;
    }

    public int getSampleRate()
    {
      return sampleRate;
    }

    public int getSampleSize()
    {
      return sampleSize;
    }
  }
}
