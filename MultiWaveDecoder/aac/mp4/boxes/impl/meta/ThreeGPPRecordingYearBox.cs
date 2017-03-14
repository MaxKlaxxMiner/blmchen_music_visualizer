// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  public sealed class ThreeGPPRecordingYearBox : FullBox
  {
    int year;

    public ThreeGPPRecordingYearBox() : base("3GPP Recording Year Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      year = (int) inStream.readBytes(2);
    }

    public int getYear()
    {
      return year;
    }
  }
}
