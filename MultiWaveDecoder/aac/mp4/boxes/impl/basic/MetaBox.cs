namespace MultiWaveDecoder
{
  /// <summary>
  /// needs to be defined, because readChildren() is not called by factory
  /// TODO: this class shouldn't be needed. at least here, things become too complicated. change this!!!
  /// </summary>
  public sealed class MetaBox : FullBox
  {
    public MetaBox() : base("Meta Box") { }

    public override void decode(MP4InputStream inStream)
    {
      // some encoders (such as Android's MexiaMuxer) do not include the version and flags fields in the meta box, instead going directly to the hdlr box
      var possibleType = (BoxType)(int)(uint)inStream.peekBytes(8);
      if (possibleType != BoxType.HANDLER_BOX)
      {
        base.decode(inStream);
      }
      readChildren(inStream);
    }
  }
}
