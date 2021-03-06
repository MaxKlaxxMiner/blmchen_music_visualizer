﻿using System;
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

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

      public AudioCodec(CodecType codec)
      {
        code = (int)codec;
      }

      public static Codec forType(BoxType type)
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

        return new AudioCodec(ac);
      }

      public override string ToString()
      {
        return "AudioCodec." + ((CodecType)code);
      }
    }

    readonly SoundMediaHeaderBox smhd;
    readonly AudioSampleEntry sampleEntry;
    readonly Codec codec;

    public AudioTrack(IBox trak, MP4InputStream inStream)
      : base(trak, inStream)
    {
      var mdia = trak.getChild(BoxType.MEDIA_BOX);
      var minf = mdia.getChild(BoxType.MEDIA_INFORMATION_BOX);
      smhd = (SoundMediaHeaderBox)minf.getChild(BoxType.SOUND_MEDIA_HEADER_BOX);

      var stbl = minf.getChild(BoxType.SAMPLE_TABLE_BOX);

      // sample descriptions: 'mp4a' and 'enca' have an ESDBox, all others have a CodecSpecificBox
      var stsd = (SampleDescriptionBox)stbl.getChild(BoxType.SAMPLE_DESCRIPTION_BOX);

      sampleEntry = stsd.getChildren()[0] as AudioSampleEntry;

      if (sampleEntry != null)
      {
        var type = sampleEntry.getType();
        if (sampleEntry.hasChild(BoxType.ESD_BOX))
        {
          findDecoderSpecificInfo((ESDBox)sampleEntry.getChild(BoxType.ESD_BOX));
        }
        else
        {
          throw new NotImplementedException();
          // decoderInfo = DecoderInfo.parse((CodecSpecificBox)sampleEntry.getChildren()[0]);
        }

        if (type == BoxType.ENCRYPTED_AUDIO_SAMPLE_ENTRY || type == BoxType.DRMS_SAMPLE_ENTRY)
        {
          findDecoderSpecificInfo((ESDBox)sampleEntry.getChild(BoxType.ESD_BOX));
          protection = Protection.parse(sampleEntry.getChild(BoxType.PROTECTION_SCHEME_INFORMATION_BOX));
          codec = protection.getOriginalFormat();
        }
        else codec = AudioCodec.forType(sampleEntry.getType());
      }
      else
      {
        codec = new AudioCodec(AudioCodec.CodecType.UNKNOWN_AUDIO_CODEC);
      }
    }

    public override FrameType getType()
    {
      return FrameType.AUDIO;
    }

    public override Codec getCodec()
    {
      return codec;
    }

    /// <summary>
    /// The balance is a floating-point number that places mono audio tracks in a stereo space: 0 is centre (the normal value), full left is -1.0 and full right is 1.0.
    /// </summary>
    /// <returns>the stereo balance for a this track</returns>
    public double getBalance()
    {
      return smhd.getBalance();
    }

    /// <summary>
    /// Returns the number of channels in this audio track.
    /// </summary>
    /// <returns>the number of channels</returns>
    public int getChannelCount()
    {
      return sampleEntry.getChannelCount();
    }

    /// <summary>
    /// Returns the sample rate of this audio track.
    /// </summary>
    /// <returns>the sample rate</returns>
    public int getSampleRate()
    {
      return sampleEntry.getSampleRate();
    }

    /// <summary>
    /// Returns the sample size in bits for this track.
    /// </summary>
    /// <returns>the sample size</returns>
    public int getSampleSize()
    {
      return sampleEntry.getSampleSize();
    }

    public double getVolume()
    {
      return tkhd.getVolume();
    }
  }
}
