using System;
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBeMadeStatic.Local

namespace MultiWaveDecoder
{
  /// <summary>
  /// Main AAC decoder class
  /// </summary>
  public sealed class Decoder : Constants
  {
    readonly DecoderConfig config;
    readonly SyntacticElements syntacticElements;
    readonly FilterBank filterBank;
    readonly BitStream inStream = new BitStream();
    ADIFHeader adifHeader;

    /// <summary>
    /// The methods returns true, if a profile is supported by the decoder.
    /// </summary>
    /// <param name="profile">profile an AAC profile</param>
    /// <returns>true if the specified profile can be decoded</returns>
    public static bool canDecode(Profile profile)
    {
      return profile.supported;
    }

    /// <summary>
    /// Initializes the decoder with a MP4 decoder specific info.
    ///
    /// After this the MP4 frames can be passed to the
    /// <code>decodeFrame(byte[], SampleBuffer)</code> method to decode them.
    /// </summary>
    /// <param name="decoderSpecificInfo">decoderSpecificInfo a byte array containing the decoder specific info from an MP4 container</param>
    public Decoder(byte[] decoderSpecificInfo)
    {
      config = DecoderConfig.parseMP4DecoderSpecificInfo(decoderSpecificInfo);
      if (config == null) throw new ArgumentException("illegal MP4 decoder specific info");

      if (!canDecode(config.getProfile())) throw new AACException("unsupported profile: " + config.getProfile());

      syntacticElements = new SyntacticElements(config);
      filterBank = new FilterBank(config.isSmallFrameUsed(), (int)config.getChannelConfiguration());

      Logger.LogInfo(string.Format("profile: {0}", config.getProfile()));
      Logger.LogInfo(string.Format("sf: {0}", config.getSampleFrequency().getFrequency()));
      Logger.LogInfo(string.Format("channels: {0}", config.getChannelConfiguration()));
    }

    public DecoderConfig getConfig()
    {
      return config;
    }

    /// <summary>
    /// Decodes one frame of AAC data in frame mode and returns the raw PCM data.
    /// </summary>
    /// <param name="frame">the AAC frame</param>
    /// <param name="buffer">a buffer to hold the decoded PCM data</param>
    public void decodeFrame(byte[] frame, SampleBuffer buffer)
    {
      if (frame != null) inStream.setData(frame);
//      try
      {
        decode(buffer);
      }
      //catch (AACException e)
      //{
      //  Logger.LogServe("unexpected end of frame: " + e);
      //}
      // throw new NotImplementedException();
    }

    void decode(SampleBuffer buffer)
    {
      if (ADIFHeader.isPresent(inStream))
      {
        adifHeader = ADIFHeader.readHeader(inStream);
        PCE pce = adifHeader.getFirstPCE();
        config.setProfile(pce.getProfile());
        config.setSampleFrequency(pce.getSampleFrequency());
        config.setChannelConfiguration((ChannelConfiguration)pce.getChannelCount());
      }

      if (!canDecode(config.getProfile())) throw new AACException("unsupported profile: " + config.getProfile());

      syntacticElements.startNewFrame();

      //try
      {
        // 1: bitstream parsing and noiseless coding
        syntacticElements.decode(inStream);
        // 2: spectral processing
        throw new NotImplementedException();
        //syntacticElements.process(filterBank);
        // 3: send to output buffer
        //syntacticElements.sendToOutput(buffer);
      }
      //catch (AACException e)
      //{
      //  buffer.setData(new byte[0], 0, 0, 0, 0);
      //  throw e;
      //}
      //catch (Exception e)
      //{
      //  buffer.setData(new byte[0], 0, 0, 0, 0);
      //  throw new AACException(e);
      //}
      // throw new NotImplementedException();
    }
  }
}
