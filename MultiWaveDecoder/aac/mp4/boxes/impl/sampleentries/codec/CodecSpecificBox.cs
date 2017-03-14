// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// The <code>CodecSpecificBox</code> can be used instead of an <code>ESDBox</code> in a sample entry. It contains <code>DecoderSpecificInfo</code>s.
  /// </summary>
  public abstract class CodecSpecificBox : BoxImpl
  {
    long vendor;
    int decoderVersion;

    protected CodecSpecificBox(string name) : base(name) { }

    protected void decodeCommon(MP4InputStream inStream)
    {
      vendor = inStream.readBytes(4);
      decoderVersion = inStream.read();
    }

    public long getVendor()
    {
      return vendor;
    }

    public int getDecoderVersion()
    {
      return decoderVersion;
    }
  }
}
