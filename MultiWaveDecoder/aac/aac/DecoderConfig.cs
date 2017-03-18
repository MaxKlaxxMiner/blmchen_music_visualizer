using System;
// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedField.Local
#pragma warning disable 169

namespace MultiWaveDecoder
{
  /// <summary>
  /// DecoderConfig that must be passed to the <code>Decoder</code> constructor. Typically it is created via one of the static parsing methods.
  /// </summary>
  public sealed class DecoderConfig : Constants
  {
    Profile profile, extProfile;
    SampleFrequency sampleFrequency;
    ChannelConfiguration channelConfiguration;
    bool frameLengthFlag;
    bool dependsOnCoreCoder;
    int coreCoderDelay;
    bool extensionFlag;
    // extension: SBR
    bool sbrPresent, downSampledSBR, sbrEnabled;
    // extension: error resilience
    bool sectionDataResilience, scalefactorResilience, spectralDataResilience;

    //private DecoderConfig() {
    //profile = Profile.AAC_MAIN;
    //extProfile = Profile.UNKNOWN;
    //sampleFrequency = SampleFrequency.SAMPLE_FREQUENCY_NONE;
    //channelConfiguration = ChannelConfiguration.CHANNEL_CONFIG_UNSUPPORTED;
    //frameLengthFlag = false;
    //sbrPresent = false;
    //downSampledSBR = false;
    //sbrEnabled = true;
    //sectionDataResilience = false;
    //scalefactorResilience = false;
    //spectralDataResilience = false;
    //}

    ///* ========== gets/sets ========== */
    //public ChannelConfiguration getChannelConfiguration() {
    //return channelConfiguration;
    //}

    //public void setChannelConfiguration(ChannelConfiguration channelConfiguration) {
    //this.channelConfiguration = channelConfiguration;
    //}

    //public int getCoreCoderDelay() {
    //return coreCoderDelay;
    //}

    //public void setCoreCoderDelay(int coreCoderDelay) {
    //this.coreCoderDelay = coreCoderDelay;
    //}

    //public bool isDependsOnCoreCoder() {
    //return dependsOnCoreCoder;
    //}

    //public void setDependsOnCoreCoder(bool dependsOnCoreCoder) {
    //this.dependsOnCoreCoder = dependsOnCoreCoder;
    //}

    //public Profile getExtObjectType() {
    //return extProfile;
    //}

    //public void setExtObjectType(Profile extObjectType) {
    //this.extProfile = extObjectType;
    //}

    //public int getFrameLength() {
    //return frameLengthFlag ? WINDOW_SMALL_LEN_LONG : WINDOW_LEN_LONG;
    //}

    //public bool isSmallFrameUsed() {
    //return frameLengthFlag;
    //}

    //public void setSmallFrameUsed(bool shortFrame) {
    //this.frameLengthFlag = shortFrame;
    //}

    //public Profile getProfile() {
    //return profile;
    //}

    //public void setProfile(Profile profile) {
    //this.profile = profile;
    //}

    //public SampleFrequency getSampleFrequency() {
    //return sampleFrequency;
    //}

    //public void setSampleFrequency(SampleFrequency sampleFrequency) {
    //this.sampleFrequency = sampleFrequency;
    //}

    ////=========== SBR =============
    //public bool isSBRPresent() {
    //return sbrPresent;
    //}

    //public bool isSBRDownSampled() {
    //return downSampledSBR;
    //}

    //public bool isSBREnabled() {
    //return sbrEnabled;
    //}

    //public void setSBREnabled(bool enabled) {
    //sbrEnabled = enabled;
    //}

    ////=========== ER =============
    //public bool isScalefactorResilienceUsed() {
    //return scalefactorResilience;
    //}

    //public bool isSectionDataResilienceUsed() {
    //return sectionDataResilience;
    //}

    //public bool isSpectralDataResilienceUsed() {
    //return spectralDataResilience;
    //}

    // --- ======== static builder ========= ---

    /// <summary>
    /// Parses the input arrays as a DecoderSpecificInfo, as used in MP4 containers.
    /// </summary>
    /// <param name="data"></param>
    /// <returns>a DecoderConfig</returns>
    public static DecoderConfig parseMP4DecoderSpecificInfo(byte[] data)
    {
      var inStream = new BitStream(data);

      var config = new DecoderConfig();

      try
      {
        config.profile = readProfile(inStream);

        int sf = inStream.readBits(4);
        config.sampleFrequency = sf == 0xf ? SampleFrequency.forFrequency(inStream.readBits(24)) : SampleFrequency.forInt(sf);
        config.channelConfiguration = (ChannelConfiguration)inStream.readBits(4);

        switch (config.profile.type)
        {
          case Profile.ProfileType.AAC_SBR:
          {
            throw new NotImplementedException();
            //config.extProfile = config.profile;
            //config.sbrPresent = true;
            //sf = inStream.readBits(4);
            ////TODO: 24 bits already read; read again?
            ////if(sf==0xF) config.sampleFrequency = SampleFrequency.forFrequency(inStream.readBits(24));
            ////if sample frequencies are the same: downsample SBR
            //config.downSampledSBR = config.sampleFrequency.getIndex()==sf;
            //config.sampleFrequency = SampleFrequency.forInt(sf);
            //config.profile = readProfile(inStream);
          }
          case Profile.ProfileType.AAC_MAIN:
          case Profile.ProfileType.AAC_LC:
          case Profile.ProfileType.AAC_SSR:
          case Profile.ProfileType.AAC_LTP:
          case Profile.ProfileType.ER_AAC_LC:
          case Profile.ProfileType.ER_AAC_LTP:
          case Profile.ProfileType.ER_AAC_LD:
          {
            // ga-specific info:
            config.frameLengthFlag = inStream.readBool();
            if (config.frameLengthFlag) throw new AACException("config uses 960-sample frames, not yet supported"); //TODO: are 960-frames working yet?
            config.dependsOnCoreCoder = inStream.readBool();
            config.coreCoderDelay = config.dependsOnCoreCoder ? inStream.readBits(14) : 0;
            config.extensionFlag = inStream.readBool();

            if (config.extensionFlag)
            {
              if (config.profile.isErrorResilientProfile())
              {
                config.sectionDataResilience = inStream.readBool();
                config.scalefactorResilience = inStream.readBool();
                config.spectralDataResilience = inStream.readBool();
              }
              // extensionFlag3
              inStream.skipBit();
            }

            if (config.channelConfiguration == ChannelConfiguration.CHANNEL_CONFIG_NONE || !config.channelConfiguration.ToString().StartsWith("CHANNEL_", StringComparison.Ordinal))
            {
              // TODO: is this working correct? -> ISO 14496-3 part 1: 1.A.4.3
              inStream.skipBits(3); //PCE

              throw new NotImplementedException();
              //PCE pce = new PCE();
              //pce.decode(inStream);
              //config.profile = pce.getProfile();
              //config.sampleFrequency = pce.getSampleFrequency();
              //config.channelConfiguration = ChannelConfiguration.forInt(pce.getChannelCount());
            }

            if (inStream.getBitsLeft() > 10) readSyncExtension(inStream, config);
          } break;
          default: throw new AACException("profile not supported: " + config.profile.type);
        }
        return config;
      }
      finally
      {
        inStream.destroy();
      }
    }

    private static Profile readProfile(BitStream inStream)
    {
      int i = inStream.readBits(5);
      if (i == 31) i = 32 + inStream.readBits(6);
      return new Profile(i);
    }

    static void readSyncExtension(BitStream inStream, DecoderConfig config)
    {
      int type = inStream.readBits(11);
      switch (type)
      {
        case 0x2B7:
        {
          var profile = new Profile(inStream.readBits(5));

          if (profile.type == Profile.ProfileType.AAC_SBR)
          {
            config.sbrPresent = inStream.readBool();
            if (config.sbrPresent)
            {
              config.profile = profile;

              int tmp = inStream.readBits(4);

              if (tmp == config.sampleFrequency.getIndex()) config.downSampledSBR = true;
              if (tmp == 15)
              {
                throw new AACException("sample rate specified explicitly, not supported yet!");
                // tmp = inStream.readBits(24);
              }
            }
          }
        } break;
      }
    }

    public override string ToString()
    {
      return (new{ profile, extProfile, sampleFrequency = sampleFrequency.getFrequency(), channelConfiguration }).ToString();
    }
  }
}
