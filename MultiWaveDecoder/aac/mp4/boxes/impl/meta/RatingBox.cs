// ReSharper disable InconsistentNaming

using System.Text;

namespace MultiWaveDecoder
{
  public sealed class RatingBox : FullBox
  {
    string languageCode, rating;

    public RatingBox() : base("Rating Box") { }

    public override void decode(MP4InputStream inStream)
    {
      // 3gpp or iTunes
      if (parent.getType() == BoxType.USER_DATA_BOX)
      {
        base.decode(inStream);

        // TODO: what to do with both?
        long entity = inStream.readBytes(4);
        long criteria = inStream.readBytes(4);
        languageCode = BoxUtils.getLanguageCode(inStream.readBytes(2));
        var b = inStream.readTerminated((int)getLeft(inStream), 0);
        rating = Encoding.UTF8.GetString(b);
      }
      else readChildren(inStream);
    }

    public string getLanguageCode()
    {
      return languageCode;
    }

    public string getRating()
    {
      return rating;
    }
  }
}
