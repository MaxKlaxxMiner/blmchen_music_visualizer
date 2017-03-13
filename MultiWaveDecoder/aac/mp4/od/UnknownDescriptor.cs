namespace MultiWaveDecoder
{
  /// <summary>
  /// This class is used if any unknown Descriptor is found in a stream. All contents of the Descriptor will be skipped.
  /// </summary>
  public sealed class UnknownDescriptor : Descriptor
  {
    public override void decode(MP4InputStream inStream)
    {
      // content will be skipped
    }
  }
}
