// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

using System;

namespace MultiWaveDecoder
{
  /// <summary>
  /// The <code>DecoderInfo</code> object contains the neccessary data to initialize a decoder. A track either contains a <code>DecoderInfo</code>
  /// or a byte-Array called the 'DecoderSpecificInfo', which is e.g. used for AAC.
  /// 
  /// The <code>DecoderInfo</code> object received from a track is a subclass of this class depending on the <code>Codec</code>.
  /// <code>
  /// AudioTrack track = (AudioTrack) movie.getTrack(AudioCodec.AC3);
  /// AC3DecoderInfo info = (AC3DecoderInfo) track.getDecoderInfo();
  /// </code>
  /// </summary>
  public abstract class DecoderInfo
  {
    static DecoderInfo parse(CodecSpecificBox css)
    {
      switch (css.getType())
      {
        case BoxType.H263_SPECIFIC_BOX: throw new NotImplementedException(); // return new H263DecoderInfo(css);
        case BoxType.AMR_SPECIFIC_BOX: throw new NotImplementedException(); // return new AMRDecoderInfo(css);
        case BoxType.EVRC_SPECIFIC_BOX: throw new NotImplementedException(); // return new EVRCDecoderInfo(css);
        case BoxType.QCELP_SPECIFIC_BOX: throw new NotImplementedException(); // return new QCELPDecoderInfo(css);
        case BoxType.SMV_SPECIFIC_BOX: throw new NotImplementedException(); // return new SMVDecoderInfo(css);
        case BoxType.AVC_SPECIFIC_BOX: throw new NotImplementedException(); // return new AVCDecoderInfo(css);
        case BoxType.AC3_SPECIFIC_BOX: throw new NotImplementedException(); // return new AC3DecoderInfo(css);
        case BoxType.EAC3_SPECIFIC_BOX: throw new NotImplementedException(); // return new EAC3DecoderInfo(css);
        default: return new UnknownDecoderInfo();
      }
    }

    private sealed class UnknownDecoderInfo : DecoderInfo { }
  }
}
