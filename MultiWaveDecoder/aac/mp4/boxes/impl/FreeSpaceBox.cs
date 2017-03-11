namespace MultiWaveDecoder
{
  /// <summary>
  /// This class is used for all boxes, that are known but don't contain necessary data and can be skipped. This is mainly used for 'skip', 'free' and 'wide'.
  /// </summary>
  public sealed class FreeSpaceBox : BoxImpl
  {
    public FreeSpaceBox(): base("Free Space Box"){}

    public FreeSpaceBox(string name) : base(name) { }

    /// <summary>
    /// Decodes the given input stream by reading this box and all of its children (if any).
    /// </summary>
    /// <param name="inStream">an input stream</param>
    public override void decode(MP4InputStream inStream)
    {
      // no need to read, box will be skipped
    }
  }
}
