using System;
using System.Collections.Generic;

namespace MultiWaveDecoder
{
  public class Track
  {
    MP4InputStream inStream;
    TrackHeaderBox tkhd;
    MediaHeaderBox mdhd;
    List<Frame> frames = new List<Frame>();
    int currentFrame = 0;

    // --- info structures ---
    //protected DecoderSpecificInfo decoderSpecificInfo;
    //protected DecoderInfo decoderInfo;
    //protected Protection protection;

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
      //if (stbl.hasChildren()) parseSampleTable(stbl);
    }
  }
}
