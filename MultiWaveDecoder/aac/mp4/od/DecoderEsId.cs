namespace MultiWaveDecoder
{
  public sealed class DecoderEsId : Descriptor
  {
    int esId;

    public override void decode(MP4InputStream inStream)
    {
      esId = (int)inStream.readBytes(4);
    }

    public int getEsId()
    {
      return esId;
    }
  }
}
