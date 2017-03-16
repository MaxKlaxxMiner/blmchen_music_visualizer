using System;
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace MultiWaveDecoder
{
  public sealed class VideoTrack : Track
  {
    public class VideoCodec : Codec
    {
      public enum CodecType
      {
        AVC,
        H263,
        MP4_ASP,
        UNKNOWN_VIDEO_CODEC
      }

      static Codec forType(BoxType type)
      {
        CodecType ac;

        switch (type)
        {
          case BoxType.AVC_SAMPLE_ENTRY: ac = CodecType.AVC; break;
          case BoxType.H263_SAMPLE_ENTRY: ac = CodecType.H263; break;
          case BoxType.MP4V_SAMPLE_ENTRY: ac = CodecType.MP4_ASP; break;
          default: ac = CodecType.UNKNOWN_VIDEO_CODEC; break;
        }

        return new VideoCodec { code = (int)ac };
      }

      public override string ToString()
      {
        return "VideoCodec." + ((CodecType)code);
      }
    }
    private VideoMediaHeaderBox vmhd;
    private VideoSampleEntry sampleEntry;
    private Codec codec;

    public VideoTrack(IBox trak, MP4InputStream inStream)
      : base(trak, inStream)
    {
      throw new NotImplementedException();

      //  Box minf = trak.getChild(BoxType.MEDIA_BOX).getChild(BoxType.MEDIA_INFORMATION_BOX);
      //  vmhd = (VideoMediaHeaderBox) minf.getChild(BoxType.VIDEO_MEDIA_HEADER_BOX);

      //  Box stbl = minf.getChild(BoxType.SAMPLE_TABLE_BOX);

      //  //sample descriptions: 'mp4v' has an ESDBox, all others have a CodecSpecificBox
      //  SampleDescriptionBox stsd = (SampleDescriptionBox) stbl.getChild(BoxType.SAMPLE_DESCRIPTION_BOX);
      //  if(stsd.getChildren().get(0) instanceof VideoSampleEntry) {
      //    sampleEntry = (VideoSampleEntry) stsd.getChildren().get(0);
      //    long type = sampleEntry.getType();
      //    if(type==BoxType.MP4V_SAMPLE_ENTRY) findDecoderSpecificInfo((ESDBox) sampleEntry.getChild(BoxType.ESD_BOX));
      //    else if(type==BoxType.ENCRYPTED_VIDEO_SAMPLE_ENTRY||type==BoxType.DRMS_SAMPLE_ENTRY) {
      //      findDecoderSpecificInfo((ESDBox) sampleEntry.getChild(BoxType.ESD_BOX));
      //      protection = Protection.parse(sampleEntry.getChild(BoxType.PROTECTION_SCHEME_INFORMATION_BOX));
      //    }
      //    else decoderInfo = DecoderInfo.parse((CodecSpecificBox) sampleEntry.getChildren().get(0));

      //    codec = VideoCodec.forType(sampleEntry.getType());
      //  }
      //  else {
      //    sampleEntry = null;
      //    codec = VideoCodec.UNKNOWN_VIDEO_CODEC;
      //  }
    }

    protected override FrameType getType()
    {
      return FrameType.VIDEO;
    }

    protected override Codec getCodec()
    {
      return codec;
    }
  }
}
