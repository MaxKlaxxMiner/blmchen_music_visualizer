// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  /// <summary>
  /// The entry sample descriptor (ESD) box is a container for entry descriptors.
  /// If used, it is located in a sample entry. Instead of an <code>ESDBox</code> a <code>CodecSpecificBox</code> may be present.
  /// </summary>
  public sealed class ESDBox : FullBox
  {
    ESDescriptor esd;

    public ESDBox() : base("ESD Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      esd = (ESDescriptor)Descriptor.createDescriptor(inStream);
    }

    public ESDescriptor getEntryDescriptor()
    {
      return esd;
    }
  }
}
