using System;
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace MultiWaveDecoder
{
  /// <summary>
  /// Main AAC decoder class
  /// </summary>
  public sealed class Decoder : Constants
  {
    readonly DecoderConfig config;
    SyntacticElements syntacticElements;
    FilterBank filterBank;
    // BitStream inStream;
    // ADIFHeader adifHeader;

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

      //in = new BitStream();

      //LOGGER.log(Level.FINE, "profile: {0}", config.getProfile());
      //LOGGER.log(Level.FINE, "sf: {0}", config.getSampleFrequency().getFrequency());
      //LOGGER.log(Level.FINE, "channels: {0}", config.getChannelConfiguration().getDescription());
      throw new NotImplementedException();
    }

    //public DecoderConfig getConfig() {
    //return config;
    //}

    ///
    /// Decodes one frame of AAC data in frame mode and returns the raw PCM
    /// data.
    /// @param frame the AAC frame
    /// @param buffer a buffer to hold the decoded PCM data
    /// @throws AACException if decoding fails
    ////
    //public void decodeFrame(byte[] frame, SampleBuffer buffer) throws AACException {
    //if(frame!=null) in.setData(frame);
    //try {
    //decode(buffer);
    //}
    //catch(AACException e) {
    //if(!e.isEndOfStream()) throw e;
    //else LOGGER.log(Level.WARNING,"unexpected end of frame",e);
    //}
    //}

    //private void decode(SampleBuffer buffer) throws AACException {
    //if(ADIFHeader.isPresent(in)) {
    //adifHeader = ADIFHeader.readHeader(in);
    //PCE pce = adifHeader.getFirstPCE();
    //config.setProfile(pce.getProfile());
    //config.setSampleFrequency(pce.getSampleFrequency());
    //config.setChannelConfiguration(ChannelConfiguration.forInt(pce.getChannelCount()));
    //}

    //if(!canDecode(config.getProfile())) throw new AACException("unsupported profile: "+config.getProfile().getDescription());

    //syntacticElements.startNewFrame();

    //try {
    ////1: bitstream parsing and noiseless coding
    //syntacticElements.decode(in);
    ////2: spectral processing
    //syntacticElements.process(filterBank);
    ////3: send to output buffer
    //syntacticElements.sendToOutput(buffer);
    //}
    //catch(AACException e) {
    //buffer.setData(new byte[0], 0, 0, 0, 0);
    //throw e;
    //}
    //catch(Exception e) {
    //buffer.setData(new byte[0], 0, 0, 0, 0);
    //throw new AACException(e);
    //}
    //}
    //}
  }
}
