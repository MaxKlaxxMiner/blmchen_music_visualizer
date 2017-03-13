namespace MultiWaveDecoder
{
  public class FullBox : BoxImpl
  {
    protected int version;
    protected int flags;

    public FullBox(string name) : base(name) { }

    /// <summary>
    /// Decodes the given input stream by reading this box and all of its children (if any).
    /// </summary>
    /// <param name="inStream">an input stream</param>
    public override void decode(MP4InputStream inStream)
    {
      version = inStream.read();
      flags = (int)inStream.readBytes(3);
    }
  }
}
