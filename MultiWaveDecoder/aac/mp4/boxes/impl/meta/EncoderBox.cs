namespace MultiWaveDecoder
{
  public sealed class EncoderBox : FullBox
  {
    string data;

    public EncoderBox() : base("Encoder Box") { }

    public override void decode(MP4InputStream inStream)
    {
      if (parent.getType() == BoxType.ITUNES_META_LIST_BOX)
      {
        readChildren(inStream);
      }
      else
      {
        base.decode(inStream);
        data = inStream.readString((int)getLeft(inStream));
      }
    }

    public string getData()
    {
      return data;
    }
  }
}
