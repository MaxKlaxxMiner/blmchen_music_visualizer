// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  public sealed class ChunkOffsetBox : FullBox
  {
    long[] chunks;

    public ChunkOffsetBox() : base("Chunk Offset Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      int len = (type == BoxType.CHUNK_LARGE_OFFSET_BOX) ? 8 : 4;
      int entryCount = (int)inStream.readBytes(4);
      chunks = new long[entryCount];

      for (int i = 0; i < entryCount; i++)
      {
        chunks[i] = inStream.readBytes(len);
      }
    }

    public long[] getChunks()
    {
      return chunks;
    }
  }
}
