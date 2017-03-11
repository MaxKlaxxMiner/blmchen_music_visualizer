namespace MultiWaveDecoder
{
  /// <summary>
  /// The data reference object contains a table of data references (normally URLs) that declare the location(s) of the media data used within the presentation.
  /// The data reference index in the sample description ties entries in this table to the samples in the track. A track may be split over several sources in this way.
  /// The data entry is either a DataEntryUrnBox or a DataEntryUrlBox.
  /// </summary>
  public sealed class DataReferenceBox : FullBox
  {
    public DataReferenceBox() : base("Data Reference Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      int entryCount = (int)inStream.readBytes(4);

      readChildren(inStream, entryCount); //DataEntryUrlBox, DataEntryUrnBox
    }
  }
}
