// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  public sealed class FairPlayDataBox : BoxImpl
  {
    byte[] data;

    public FairPlayDataBox() : base("iTunes FairPlay Data Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      data = new byte[(int)getLeft(inStream)];
      inStream.read(data, 0, data.Length);
    }

    public byte[] getData()
    {
      return data;
    }
  }
}
