using System.Text;

// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public sealed class GenreBox : FullBox
  {
    string languageCode, genre;

    public GenreBox() : base("Genre Box") { }

    public override void decode(MP4InputStream inStream)
    {
      // 3gpp or iTunes
      if (parent.getType() == BoxType.USER_DATA_BOX)
      {
        base.decode(inStream);
        languageCode = BoxUtils.getLanguageCode(inStream.readBytes(2));
        var b = inStream.readTerminated((int)getLeft(inStream), 0);
        genre = Encoding.UTF8.GetString(b);
      }
      else readChildren(inStream);
    }

    public string getLanguageCode()
    {
      return languageCode;
    }

    public string getGenre()
    {
      return genre;
    }
  }
}
