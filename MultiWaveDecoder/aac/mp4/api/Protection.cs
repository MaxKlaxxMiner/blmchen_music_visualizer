// ReSharper disable InconsistentNaming

using System;

namespace MultiWaveDecoder
{
  /// <summary>
  /// This class contains information about a DRM system.
  /// </summary>
  public abstract class Protection
  {
    public enum Scheme
    {
      UNKNOWN = -1,
      ITUNES_FAIR_PLAY = 1769239918
    }

    readonly Track.Codec originalFormat = null;

    protected Protection(IBox sinf)
    {
      throw new NotImplementedException();

      ////original format
      //long type = ((OriginalFormatBox)sinf.getChild(BoxType.ORIGINAL_FORMAT_BOX)).getOriginalFormat();
      //Track.Codec c;
      ////TODO: currently it tests for audio and video codec, can do this any other way?
      //if (!(c = AudioTrack.AudioCodec.forType(type)).equals(AudioTrack.AudioCodec.UNKNOWN_AUDIO_CODEC)) originalFormat = c;
      //else if (!(c = VideoTrack.VideoCodec.forType(type)).equals(VideoTrack.VideoCodec.UNKNOWN_VIDEO_CODEC)) originalFormat = c;
      //else originalFormat = null;
    }

    public static Protection parse(IBox sinf)
    {
      Protection p = null;

      if (sinf.hasChild(BoxType.SCHEME_TYPE_BOX))
      {
        var schm = (SchemeTypeBox)sinf.getChild(BoxType.SCHEME_TYPE_BOX);
        var l = (Scheme)schm.getSchemeType();
        if (l == Scheme.ITUNES_FAIR_PLAY) p = new ITunesProtection(sinf);
      }

      return p ?? new UnknownProtection(sinf);
    }

    public Track.Codec getOriginalFormat()
    {
      return originalFormat;
    }

    public abstract Scheme getScheme();

    /// <summary>
    /// default implementation for unknown protection schemes
    /// </summary>
    sealed class UnknownProtection : Protection
    {
      public UnknownProtection(IBox sinf) : base(sinf) { }

      public override Scheme getScheme()
      {
        return Scheme.UNKNOWN;
      }
    }
  }
}
