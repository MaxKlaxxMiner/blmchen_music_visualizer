// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  public sealed class ThreeGPPAlbumBox : ThreeGPPMetadataBox
  {
    int trackNumber;

    public ThreeGPPAlbumBox() : base("3GPP Album Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      trackNumber = getLeft(inStream) > 0 ? inStream.read() : -1;
    }

    /// <summary>
    /// The track number (order number) of the media on this album. This is an optional field. If the field is not present, -1 is returned.
    /// </summary>
    /// <returns>the track number</returns>
    public int getTrackNumber()
    {
      return trackNumber;
    }
  }
}
