namespace MultiWaveDecoder
{
  /// <summary>
  /// Box implementation that is used for unknown types.
  /// </summary>
  public sealed class UnknownBox : BoxImpl
  {
    public UnknownBox() : base("unknown") { }

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
