// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  public sealed class ITunesMetadataMeanBox : FullBox
  {
    string domain;

    public ITunesMetadataMeanBox():base("iTunes Metadata Mean Box"){}

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      domain = inStream.readString((int)getLeft(inStream));
    }

    public string getDomain()
    {
      return domain;
    }
  }
}
