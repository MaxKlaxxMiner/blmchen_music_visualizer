using System;
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace MultiWaveDecoder
{
  public sealed class AudioTrack : Track
  {
    public sealed class AudioCodec : Codec
    {
      public enum CodecType
      {
        AAC,
        AC3,
        AMR,
        AMR_WIDE_BAND,
        EVRC,
        EXTENDED_AC3,
        QCELP,
        SMV,
        UNKNOWN_AUDIO_CODEC
      }

      static Codec forType(BoxType type)
      {
        CodecType ac;

        switch (type)
        {
          case BoxType.MP4A_SAMPLE_ENTRY: ac = CodecType.AAC; break;
          case BoxType.AC3_SAMPLE_ENTRY: ac = CodecType.AC3; break;
          case BoxType.AMR_SAMPLE_ENTRY: ac = CodecType.AMR; break;
          case BoxType.AMR_WB_SAMPLE_ENTRY: ac = CodecType.AMR_WIDE_BAND; break;
          case BoxType.EVRC_SAMPLE_ENTRY: ac = CodecType.EVRC; break;
          case BoxType.EAC3_SAMPLE_ENTRY: ac = CodecType.EXTENDED_AC3; break;
          case BoxType.QCELP_SAMPLE_ENTRY: ac = CodecType.QCELP; break;
          case BoxType.SMV_SAMPLE_ENTRY: ac = CodecType.SMV; break;
          default: ac = CodecType.UNKNOWN_AUDIO_CODEC; break;
        }

        return new AudioCodec { code = (int)ac };
      }
    }

    private SoundMediaHeaderBox smhd;
    private AudioSampleEntry sampleEntry;
    private Codec codec;

    public AudioTrack(IBox trak, MP4InputStream inStream)
      : base(trak, inStream)
    {
      var mdia = trak.getChild(BoxType.MEDIA_BOX);
      var minf = mdia.getChild(BoxType.MEDIA_INFORMATION_BOX);
      smhd = (SoundMediaHeaderBox)minf.getChild(BoxType.SOUND_MEDIA_HEADER_BOX);

      var stbl = minf.getChild(BoxType.SAMPLE_TABLE_BOX);

      throw new NotImplementedException();

      ////sample descriptions: 'mp4a' and 'enca' have an ESDBox, all others have a CodecSpecificBox
      //var stsd = (SampleDescriptionBox) stbl.getChild(BoxType.SAMPLE_DESCRIPTION_BOX);
      //if(stsd.getChildren().get(0) instanceof AudioSampleEntry) {
      //  sampleEntry = (AudioSampleEntry) stsd.getChildren().get(0);
      //  long type = sampleEntry.getType();
      //  if(sampleEntry.hasChild(BoxType.ESD_BOX)) findDecoderSpecificInfo((ESDBox) sampleEntry.getChild(BoxType.ESD_BOX));
      //  else decoderInfo = DecoderInfo.parse((CodecSpecificBox) sampleEntry.getChildren().get(0));

      //  if(type==BoxType.ENCRYPTED_AUDIO_SAMPLE_ENTRY||type==BoxType.DRMS_SAMPLE_ENTRY) {
      //    findDecoderSpecificInfo((ESDBox) sampleEntry.getChild(BoxType.ESD_BOX));
      //    protection = Protection.parse(sampleEntry.getChild(BoxType.PROTECTION_SCHEME_INFORMATION_BOX));
      //    codec = protection.getOriginalFormat();
      //  }
      //  else codec = AudioCodec.forType(sampleEntry.getType());
      //}
      //else {
      //  sampleEntry = null;
      //  codec = AudioCodec.UNKNOWN_AUDIO_CODEC;
      //}
    }

    protected override FrameType getType()
    {
      return FrameType.AUDIO;
    }

    protected override Codec getCodec()
    {
      return codec;
    }
  }
}
