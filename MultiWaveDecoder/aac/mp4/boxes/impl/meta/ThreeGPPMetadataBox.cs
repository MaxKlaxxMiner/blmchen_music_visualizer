// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  public class ThreeGPPMetadataBox : FullBox
  {
    string languageCode, data;

    public ThreeGPPMetadataBox(string name) : base(name) { }

    public override void decode(MP4InputStream inStream)
    {
      decodeCommon(inStream);

      data = inStream.readUTFString((int)getLeft(inStream));
    }

    /// <summary>
    /// called directly by subboxes that don't contain the 'data' string
    /// </summary>
    /// <param name="inStream"></param>
    private void decodeCommon(MP4InputStream inStream)
    {
      base.decode(inStream);
      languageCode = BoxUtils.getLanguageCode(inStream.readBytes(2));
    }

    /// <summary>
    /// The language code for the following text. See ISO 639-2/T for the set of three character codes.
    /// </summary>
    /// <returns></returns>
    public string getLanguageCode()
    {
      return languageCode;
    }

    public string getData()
    {
      return data;
    }
  }
}
