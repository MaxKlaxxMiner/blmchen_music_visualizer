// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public class SampleEntry : BoxImpl
  {
    long dataReferenceIndex;

    protected SampleEntry(string name) : base(name) { }

    public override void decode(MP4InputStream inStream)
    {
      inStream.skipBytes(6); // reserved
      dataReferenceIndex = inStream.readBytes(2);
    }

    /// <summary>
    /// The data reference index is an integer that contains the index of the data reference to use to retrieve data associated with samples that use
    /// this sample description. Data references are stored in Data Reference Boxes. The index ranges from 1 to the number of data references.
    /// </summary>
    /// <returns></returns>
    public long getDataReferenceIndex()
    {
      return dataReferenceIndex;
    }
  }
}
