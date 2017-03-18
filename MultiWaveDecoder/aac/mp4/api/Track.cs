using System;
using System.Collections.Generic;
using System.IO;
// ReSharper disable UnusedMember.Global

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
    protected readonly TrackHeaderBox tkhd;
    readonly MediaHeaderBox mdhd;
    readonly List<Frame> frames = new List<Frame>();
    int currentFrame;

    // --- info structures ---
    protected DecoderSpecificInfo decoderSpecificInfo;
    protected DecoderInfo decoderInfo;
    protected Protection protection;

    public abstract class Codec
    {
      public int code;
    }

    public abstract FrameType getType();

    public abstract Codec getCodec();

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

    //protected <T> void parseSampleEntry(Box sampleEntry, Class<T> clazz) {
    //T type;
    //try {
    //type = clazz.newInstance();
    //if(sampleEntry.getClass().isInstance(type)) {
    //System.out.println("true");
    //}
    //}
    //catch(InstantiationException ex) {
    //ex.printStackTrace();
    //}
    //catch(IllegalAccessException ex) {
    //ex.printStackTrace();
    //}
    //}

    /// <summary>
    /// Returns true if the track is enabled. A disabled track is treated as if it were not present.
    /// </summary>
    /// <returns>true if the track is enabled</returns>
    public bool isEnabled()
    {
      return tkhd.isTrackEnabled();
    }

    /// <summary>
    /// Returns true if the track is used in the presentation.
    /// </summary>
    /// <returns>true if the track is used</returns>
    public bool isUsed()
    {
      return tkhd.isTrackInMovie();
    }

    /// <summary>
    /// Returns true if the track is used in previews.
    /// </summary>
    /// <returns>true if the track is used in previews</returns>
    public bool isUsedForPreview()
    {
      return tkhd.isTrackInPreview();
    }

    /// <summary>
    /// Returns the time this track was created.
    /// </summary>
    /// <returns>the creation time</returns>
    public DateTime getCreationTime()
    {
      return BoxUtils.getDate(tkhd.getCreationTime());
    }

    /// <summary>
    /// Returns the last time this track was modified.
    /// </summary>
    /// <returns>the modification time</returns>
    public DateTime getModificationTime()
    {
      return BoxUtils.getDate(tkhd.getModificationTime());
    }

    /// <summary>
    /// Returns the decoder specific info, if present. It contains configuration data for the decoder. If the decoder specific info is not present, the track contains a <code>DecoderInfo</code>.
    /// </summary>
    /// <returns>the decoder specific info</returns>
    public byte[] getDecoderSpecificInfo()
    {
      return decoderSpecificInfo.getData();
    }

    /// <summary>
    /// Returns the <code>DecoderInfo</code>, if present. It contains configuration information for the decoder. If the structure is not present, the track contains a decoder specific info.
    /// </summary>
    /// <returns>the codec specific structure</returns>
    public DecoderInfo getDecoderInfo()
    {
      return decoderInfo;
    }

    /// <summary>
    /// Returns the <code>ProtectionInformation</code> object that contains details about the DRM system used. If no protection is present this method returns null.
    /// </summary>
    /// <returns>a <code>ProtectionInformation</code> object or null if no protection is used</returns>
    public Protection getProtection()
    {
      return protection;
    }

    /// <summary>
    /// Indicates if there are more frames to be read in this track.
    /// </summary>
    /// <returns>true if there is at least one more frame to read.</returns>
    public bool hasMoreFrames()
    {
      return currentFrame < frames.Count;
    }

    /// <summary>
    /// Reads the next frame from this track. If it contains no more frames to read, null is returned.
    /// </summary>
    /// <returns>the next frame or null if there are no more frames to read</returns>
    public Frame readNextFrame()
    {
      Frame frame = null;
      if (hasMoreFrames())
      {
        frame = frames[currentFrame];

        long diff = frame.getOffset() - inStream.getOffset();
        if (diff > 0)
        {
          inStream.skipBytes(diff);
        }
        else if (diff < 0)
        {
          if (inStream.hasRandomAccess())
          {
            inStream.seek(frame.getOffset());
          }
          else
          {
            Logger.LogServe(string.Format("MP4 API: readNextFrame failed: frame {0:N0} already skipped, offset:{1:N0}, stream:{2:N0}", currentFrame, frame.getOffset(), inStream.getOffset()));
            throw new IOException("frame already skipped and no random access");
          }
        }

        var b = new byte[(int)frame.getSize()];
        try
        {
          inStream.read(b, 0, b.Length);
        }
        catch (Exception)
        {
          Logger.LogServe(string.Format("MP4 API: readNextFrame failed: tried to read {0:N0} bytes at {1:N0}", frame.getSize(), inStream.getOffset()));
          throw;
        }
        frame.setData(b);
        currentFrame++;
      }
      return frame;
    }

    /// <summary>
    /// This method tries to seek to the frame that is nearest to the given timestamp. It returns the timestamp of the frame it seeked to or -1 if none was found.
    /// </summary>
    /// <param name="timestamp">a timestamp to seek to</param>
    /// <returns>the frame's timestamp that the method seeked to</returns>
    public double seek(double timestamp)
    {
      // find first frame > timestamp
      Frame frame = null;
      for (int i = 0; i < frames.Count; i++)
      {
        frame = frames[i++];
        if (frame.getTime() > timestamp)
        {
          currentFrame = i;
          break;
        }
      }
      return (frame == null) ? -1 : frame.getTime();
    }

    /// <summary>
    /// Returns the timestamp of the next frame to be read. This is needed to read frames from a movie that contains multiple tracks.
    /// </summary>
    /// <returns>the next frame's timestamp</returns>
    public double getNextTimeStamp()
    {
      return frames[currentFrame].getTime();
    }

    public override string ToString()
    {
      return (new { currentFrame, frames = "Frame[" + frames.Count + "]", codec = getCodec() }).ToString();
    }
  }
}
