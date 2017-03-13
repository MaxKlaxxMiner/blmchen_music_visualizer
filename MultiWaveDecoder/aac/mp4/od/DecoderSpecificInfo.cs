// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// The <code>DecoderSpecificInfo</code> constitutes an opaque container with information for a specific media decoder. Depending on the required amout of
  /// data, two classes with a maximum of 255 and 2<sup>32</sup>-1 bytes of data are provided. The existence and semantics of the
  /// <code>DecoderSpecificInfo</code> depends on the stream type and object profile of the parent <code>DecoderConfigDescriptor</code>.
  /// </summary>
  public sealed class DecoderSpecificInfo : Descriptor
  {
    byte[] data;

    public override void decode(MP4InputStream inStream)
    {
      data = new byte[size];
      inStream.read(data, 0, data.Length);
    }

    /// <summary>
    /// A byte array containing the decoder specific information.
    /// </summary>
    /// <returns>the decoder specific information</returns>
    public byte[] getData()
    {
      return data;
    }
  }
}
