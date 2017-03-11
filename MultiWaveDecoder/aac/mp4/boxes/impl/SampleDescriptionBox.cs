namespace MultiWaveDecoder
{
  /// <summary>
  /// The sample description table gives detailed information about the coding type used, and any initialization information needed for that coding.
  /// </summary>
  public sealed class SampleDescriptionBox : FullBox
  {
    public SampleDescriptionBox() : base("Sample Description Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      int entryCount = (int)inStream.readBytes(4);

      readChildren(inStream, entryCount);
    }
  }
}
