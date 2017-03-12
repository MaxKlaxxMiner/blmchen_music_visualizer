// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// The Copyright box contains a copyright declaration which applies to the entire presentation, when contained within the Movie Box, or, when contained
  /// in a track, to that entire track. There may be multiple copyright boxes using different language codes.
  /// </summary>
  public sealed class CopyrightBox : FullBox
  {
    string languageCode, notice;

    public CopyrightBox() : base("Copyright Box") { }

    public override void decode(MP4InputStream inStream)
    {
      if (parent.getType() == BoxType.USER_DATA_BOX)
      {
        base.decode(inStream);
        //1 bit padding, 5*3 bits language code (ISO-639-2/T)
        languageCode = BoxUtils.getLanguageCode(inStream.readBytes(2));

        notice = inStream.readUTFString((int)getLeft(inStream));
      }
      else if (parent.getType() == BoxType.ITUNES_META_LIST_BOX) readChildren(inStream);
    }

    /// <summary>
    /// The language code for the following text. See ISO 639-2/T for the set of three character codes.
    /// </summary>
    /// <returns></returns>
    public string getLanguageCode()
    {
      return languageCode;
    }

    /// <summary>
    /// The copyright notice.
    /// </summary>
    /// <returns></returns>
    public string getNotice()
    {
      return notice;
    }
  }
}
