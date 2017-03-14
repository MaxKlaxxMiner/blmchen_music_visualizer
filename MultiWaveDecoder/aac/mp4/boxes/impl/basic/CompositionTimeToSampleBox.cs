namespace MultiWaveDecoder
{
  /// <summary>
  /// This box provides the offset between decoding time and composition time.
  /// Since decoding time must be less than the composition time, the offsets are expressed as unsigned numbers such that
  /// CT(n) = DT(n) + CTTS(n)
  /// where CTTS(n) is the (uncompressed) table entry for sample n.
  ///
  /// The composition time to sample table is optional and must only be present if DT and CT differ for any samples.
  ///
  /// Hint tracks do not use this box.
  /// </summary>
  public sealed class CompositionTimeToSampleBox : FullBox
  {
    long[] sampleCounts, sampleOffsets;

    public CompositionTimeToSampleBox() : base("Time To Sample Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      int entryCount = (int)inStream.readBytes(4);
      sampleCounts = new long[entryCount];
      sampleOffsets = new long[entryCount];

      for (int i = 0; i < entryCount; i++)
      {
        sampleCounts[i] = inStream.readBytes(4);
        sampleOffsets[i] = inStream.readBytes(4);
      }
    }

    public long[] getSampleCounts()
    {
      return sampleCounts;
    }

    public long[] getSampleOffsets()
    {
      return sampleOffsets;
    }
  }
}
