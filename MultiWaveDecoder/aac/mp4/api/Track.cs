using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  /// <summary>
  /// This class represents a track in a movie.
  /// 
  /// Each track contains either a decoder specific info as a byte array or a <code>DecoderInfo</code> object that contains necessary information for the decoder.
  /// 
  /// TODO: expand javadoc; use generics for subclasses?
  /// </summary>
  public abstract class Track
  {
    readonly MP4InputStream inStream;
    readonly TrackHeaderBox tkhd;
    readonly MediaHeaderBox mdhd;
    readonly List<Frame> frames = new List<Frame>();
    int currentFrame = 0;

    // --- info structures ---
    protected DecoderSpecificInfo decoderSpecificInfo;
    //protected DecoderInfo decoderInfo;
    //protected Protection protection;

    public abstract class Codec
    {
      public int code;
    }

    protected abstract FrameType getType();

    protected abstract Codec getCodec();

    protected Track(IBox trak, MP4InputStream inStream)
    {
      this.inStream = inStream;

      tkhd = (TrackHeaderBox)trak.getChild(BoxType.TRACK_HEADER_BOX);

      var mdia = trak.getChild(BoxType.MEDIA_BOX);
      mdhd = (MediaHeaderBox)mdia.getChild(BoxType.MEDIA_HEADER_BOX);
      var minf = mdia.getChild(BoxType.MEDIA_INFORMATION_BOX);

      var dinf = minf.getChild(BoxType.DATA_INFORMATION_BOX);
      var dref = (DataReferenceBox)dinf.getChild(BoxType.DATA_REFERENCE_BOX);

      // --- sample table ---
      var stbl = minf.getChild(BoxType.SAMPLE_TABLE_BOX);
      if (stbl.hasChildren()) parseSampleTable(stbl);
    }

    private void parseSampleTable(IBox stbl)
    {
      double timeScale = mdhd.getTimeScale();
      var type = getType();

      // sample sizes
      var sampleSizes = ((SampleSizeBox)stbl.getChild(BoxType.SAMPLE_SIZE_BOX)).getSampleSizes();

      // chunk offsets
      ChunkOffsetBox stco;
      if (stbl.hasChild(BoxType.CHUNK_OFFSET_BOX)) stco = (ChunkOffsetBox)stbl.getChild(BoxType.CHUNK_OFFSET_BOX);
      else stco = (ChunkOffsetBox)stbl.getChild(BoxType.CHUNK_LARGE_OFFSET_BOX);
      var chunkOffsets = stco.getChunks();

      // samples to chunks
      var stsc = ((SampleToChunkBox)stbl.getChild(BoxType.SAMPLE_TO_CHUNK_BOX));
      var firstChunks = stsc.getFirstChunks();
      var samplesPerChunk = stsc.getSamplesPerChunk();

      // sample durations/timestamps
      var stts = (DecodingTimeToSampleBox)stbl.getChild(BoxType.DECODING_TIME_TO_SAMPLE_BOX);
      var sampleCounts = stts.getSampleCounts();
      var sampleDeltas = stts.getSampleDeltas();
      var timeOffsets = new long[sampleSizes.Length];
      long tmp = 0;
      long off = 0;
      for (int i = 0; i < sampleCounts.Length; i++)
      {
        for (int j = 0; j < sampleCounts[i]; j++)
        {
          timeOffsets[off + j] = tmp;
          tmp += sampleDeltas[i];
        }
        off += sampleCounts[i];
      }

      // --- create samples ---
      int current = 0;
      // iterate over all chunk groups
      for (int i = 0; i < firstChunks.Length; i++)
      {
        int lastChunk;
        if (i < firstChunks.Length - 1) lastChunk = (int)firstChunks[i + 1] - 1;
        else lastChunk = chunkOffsets.Length;

        // iterate over all chunks in current group
        for (int j = (int)firstChunks[i] - 1; j < lastChunk; j++)
        {
          long offset = chunkOffsets[j];

          // iterate over all samples in current chunk
          for (int k = 0; k < samplesPerChunk[i]; k++)
          {
            // create samples
            var timeStamp = timeOffsets[current] / timeScale;
            frames.Add(new Frame(type, offset, sampleSizes[current], timeStamp));
            offset += sampleSizes[current];
            current++;
          }
        }
      }

      // frames need not to be time-ordered: sort by timestamp
      // TODO: is it possible to add them to the specific position?
      frames.Sort();
    }

    // TODO: implement other entry descriptors
    protected void findDecoderSpecificInfo(ESDBox esds)
    {
      Descriptor ed = esds.getEntryDescriptor();
      var children = ed.getChildren();

      foreach (var e in children)
      {
        var children2 = e.getChildren();
        foreach (var e2 in children2)
        {
          switch (e2.getType())
          {
            case Descriptor.TYPE_DECODER_SPECIFIC_INFO: decoderSpecificInfo = (DecoderSpecificInfo)e2; break;
          }
        }
      }
    }

    public override string ToString()
    {
      return (new { currentFrame, frames = "Frame[" + frames.Count + "]", codec = getCodec() }).ToString();
    }
  }
}
