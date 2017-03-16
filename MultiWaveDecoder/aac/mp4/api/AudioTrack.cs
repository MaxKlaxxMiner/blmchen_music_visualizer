using System;

namespace MultiWaveDecoder
{
  public sealed class AudioTrack : Track
  {
    public AudioTrack(IBox trak, MP4InputStream inStream)
      : base(trak, inStream)
    {
      throw new NotImplementedException();

      //super(trak, in);

      //Box mdia = trak.getChild(BoxType.MEDIA_BOX);
      //Box minf = mdia.getChild(BoxType.MEDIA_INFORMATION_BOX);
      //smhd = (SoundMediaHeaderBox) minf.getChild(BoxType.SOUND_MEDIA_HEADER_BOX);

      //Box stbl = minf.getChild(BoxType.SAMPLE_TABLE_BOX);

      ////sample descriptions: 'mp4a' and 'enca' have an ESDBox, all others have a CodecSpecificBox
      //SampleDescriptionBox stsd = (SampleDescriptionBox) stbl.getChild(BoxType.SAMPLE_DESCRIPTION_BOX);
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
  }
}
