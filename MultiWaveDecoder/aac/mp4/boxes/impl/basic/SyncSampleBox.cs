namespace MultiWaveDecoder
{
  /// <summary>
  /// This box provides a compact marking of the random access points within the stream. The table is arranged in strictly increasing order of sample number.
  /// 
  /// If the sync sample box is not present, every sample is a random access point.
  /// </summary>
  public sealed class SyncSampleBox : FullBox
  {
    long[] sampleNumbers;

    public SyncSampleBox() : base("Sync Sample Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      int entryCount = (int)inStream.readBytes(4);
      sampleNumbers = new long[entryCount];
      for (int i = 0; i < entryCount; i++)
      {
        sampleNumbers[i] = inStream.readBytes(4);
      }
    }

    /// <summary>
    /// Gives the numbers of the samples for each entry that are random access points in the stream.
    /// </summary>
    /// <returns>a list of sample numbers</returns>
    public long[] getSampleNumbers()
    {
      return sampleNumbers;
    }
  }
}
