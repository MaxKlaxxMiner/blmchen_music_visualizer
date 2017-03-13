namespace MultiWaveDecoder
{
  public sealed class OriginalFormatBox : BoxImpl
  {
    string originalFormat;

    public OriginalFormatBox() : base("Original Format Box") { }

    public override void decode(MP4InputStream inStream)
    {
      originalFormat = inStream.readString(4);
    }

    /// <summary>
    /// The original format is the four-character-code of the original un-transformed sample entry (e.g. 'mp4v' if the stream contains protected MPEG-4 visual material).
    /// </summary>
    /// <returns>the stream's original format</returns>
    public string getOriginalFormat()
    {
      return originalFormat;
    }
  }
}
