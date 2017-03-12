// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public sealed class SampleToChunkBox : FullBox
  {
    long[] firstChunks, samplesPerChunk, sampleDescriptionIndex;

    public SampleToChunkBox() : base("Sample To Chunk Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      int entryCount = (int)inStream.readBytes(4);
      firstChunks = new long[entryCount];
      samplesPerChunk = new long[entryCount];
      sampleDescriptionIndex = new long[entryCount];

      for (int i = 0; i < entryCount; i++)
      {
        firstChunks[i] = inStream.readBytes(4);
        samplesPerChunk[i] = inStream.readBytes(4);
        sampleDescriptionIndex[i] = inStream.readBytes(4);
      }
    }

    public long[] getFirstChunks()
    {
      return firstChunks;
    }

    public long[] getSamplesPerChunk()
    {
      return samplesPerChunk;
    }

    public long[] getSampleDescriptionIndex()
    {
      return sampleDescriptionIndex;
    }
  }
}
