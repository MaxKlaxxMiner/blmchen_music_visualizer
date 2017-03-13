// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public sealed class SampleSizeBox : FullBox
  {
    long[] sampleSizes;

    public SampleSizeBox() : base("Sample Size Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      bool compact = type == BoxType.COMPACT_SAMPLE_SIZE_BOX;

      int sampleSize;
      if (compact)
      {
        inStream.skipBytes(3);
        sampleSize = inStream.read();
      }
      else sampleSize = (int)inStream.readBytes(4);

      int sampleCount = (int)inStream.readBytes(4);
      sampleSizes = new long[sampleCount];

      if (compact)
      {
        // compact: sampleSize can be 4, 8 or 16 bits
        if (sampleSize == 4)
        {
          for (int i = 0; i < sampleCount; i += 2)
          {
            int x = inStream.read();
            sampleSizes[i] = (x >> 4) & 0xF;
            sampleSizes[i + 1] = x & 0xF;
          }
        }
        else readSizes(inStream, sampleSize / 8);
      }
      else if (sampleSize == 0)
      {
        readSizes(inStream, 4);
      }
      else
      {
        for (int i = 0; i < sampleSizes.Length; i++) sampleSizes[i] = sampleSize;
      }
    }

    private void readSizes(MP4InputStream inStream, int len)
    {
      for (int i = 0; i < sampleSizes.Length; i++)
      {
        sampleSizes[i] = inStream.readBytes(len);
      }
    }

    public int getSampleCount()
    {
      return sampleSizes.Length;
    }

    public long[] getSampleSizes()
    {
      return sampleSizes;
    }
  }
}
