// ReSharper disable InconsistentNaming

using System;

namespace MultiWaveDecoder
{
  /// <summary>
  /// DecoderConfig that must be passed to the <code>Decoder</code> constructor. Typically it is created via one of the static parsing methods.
  /// </summary>
  public sealed class DecoderConfig : Constants
  {
    Profile profile, extProfile;
    //private SampleFrequency sampleFrequency;
    //private ChannelConfiguration channelConfiguration;
    //private bool frameLengthFlag;
    //private bool dependsOnCoreCoder;
    //private int coreCoderDelay;
    //private bool extensionFlag;
    ////extension: SBR
    //private bool sbrPresent, downSampledSBR, sbrEnabled;
    ////extension: error resilience
    //private bool sectionDataResilience, scalefactorResilience, spectralDataResilience;

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
        //if(sf==0xF) config.sampleFrequency = SampleFrequency.forFrequency(inStream.readBits(24));
        //else config.sampleFrequency = SampleFrequency.forInt(sf);
        //config.channelConfiguration = ChannelConfiguration.forInt(inStream.readBits(4));

        throw new NotImplementedException();
        //switch(config.profile) {
        //case AAC_SBR:
        //config.extProfile = config.profile;
        //config.sbrPresent = true;
        //sf = inStream.readBits(4);
        ////TODO: 24 bits already read; read again?
        ////if(sf==0xF) config.sampleFrequency = SampleFrequency.forFrequency(inStream.readBits(24));
        ////if sample frequencies are the same: downsample SBR
        //config.downSampledSBR = config.sampleFrequency.getIndex()==sf;
        //config.sampleFrequency = SampleFrequency.forInt(sf);
        //config.profile = readProfile(inStream);
        //break;
        //case AAC_MAIN:
        //case AAC_LC:
        //case AAC_SSR:
        //case AAC_LTP:
        //case ER_AAC_LC:
        //case ER_AAC_LTP:
        //case ER_AAC_LD:
        ////ga-specific info:
        //config.frameLengthFlag = inStream.readBool();
        //if(config.frameLengthFlag) throw new AACException("config uses 960-sample frames, not yet supported"); //TODO: are 960-frames working yet?
        //config.dependsOnCoreCoder = inStream.readBool();
        //if(config.dependsOnCoreCoder) config.coreCoderDelay = inStream.readBits(14);
        //else config.coreCoderDelay = 0;
        //config.extensionFlag = inStream.readBool();

        //if(config.extensionFlag) {
        //if(config.profile.isErrorResilientProfile()) {
        //config.sectionDataResilience = inStream.readBool();
        //config.scalefactorResilience = inStream.readBool();
        //config.spectralDataResilience = inStream.readBool();
        //}
        ////extensionFlag3
        //inStream.skipBit();
        //}

        //if(config.channelConfiguration==ChannelConfiguration.CHANNEL_CONFIG_NONE) {
        ////TODO: is this working correct? -> ISO 14496-3 part 1: 1.A.4.3
        //inStream.skipBits(3); //PCE
        //PCE pce = new PCE();
        //pce.decode(inStream);
        //config.profile = pce.getProfile();
        //config.sampleFrequency = pce.getSampleFrequency();
        //config.channelConfiguration = ChannelConfiguration.forInt(pce.getChannelCount());
        //}

        //if(inStream.getBitsLeft()>10) readSyncExtension(inStream, config);
        //break;
        //default:
        //throw new AACException("profile not supported: "+config.profile.getIndex());
        //}
        //return config;
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

    //private static void readSyncExtension(BitStream in, DecoderConfig config) throws AACException {
    //int type = in.readBits(11);
    //switch(type) {
    //case 0x2B7:
    //Profile profile = Profile.forInt(in.readBits(5));

    //if(profile.equals(Profile.AAC_SBR)) {
    //config.sbrPresent = in.readBool();
    //if(config.sbrPresent) {
    //config.profile = profile;

    //int tmp = in.readBits(4);

    //if(tmp==config.sampleFrequency.getIndex()) config.downSampledSBR = true;
    //if(tmp==15) {
    //throw new AACException("sample rate specified explicitly, not supported yet!");
    ////tmp = in.readBits(24);
    //}
    //}
    //}
    //break;
    //}
    //}
    //}
  }
}
