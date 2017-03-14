namespace MultiWaveDecoder
{
  public sealed class ObjectDescriptorBox : FullBox
  {
    Descriptor objectDescriptor;

    public ObjectDescriptorBox() : base("Object Descriptor Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);
      objectDescriptor = Descriptor.createDescriptor(inStream);
    }

    public Descriptor getObjectDescriptor()
    {
      return objectDescriptor;
    }
  }
}
