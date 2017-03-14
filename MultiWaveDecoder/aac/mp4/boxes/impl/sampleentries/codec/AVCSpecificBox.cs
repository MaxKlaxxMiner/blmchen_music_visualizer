// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// defined in ISO 14496-15 as 'AVC Configuration Record'
  /// </summary>
  public sealed class AVCSpecificBox : CodecSpecificBox
  {
    int configurationVersion, profile, level, lengthSize;
    byte profileCompatibility;
    byte[][] sequenceParameterSetNALUnit, pictureParameterSetNALUnit;

    public AVCSpecificBox() : base("AVC Specific Box") { }

    public override void decode(MP4InputStream inStream)
    {
      configurationVersion = inStream.read();
      profile = inStream.read();
      profileCompatibility = (byte)inStream.read();
      level = inStream.read();
      //6 bits reserved, 2 bits 'length size minus one'
      lengthSize = (inStream.read() & 3) + 1;

      int len;
      //3 bits reserved, 5 bits number of sequence parameter sets
      int sequenceParameterSets = inStream.read() & 31;

      sequenceParameterSetNALUnit = new byte[sequenceParameterSets][];
      for (int i = 0; i < sequenceParameterSets; i++)
      {
        len = (int)inStream.readBytes(2);
        sequenceParameterSetNALUnit[i] = inStream.readByteArray(len);
      }

      int pictureParameterSets = inStream.read();

      pictureParameterSetNALUnit = new byte[pictureParameterSets][];
      for (int i = 0; i < pictureParameterSets; i++)
      {
        len = (int)inStream.readBytes(2);
        pictureParameterSetNALUnit[i] = inStream.readByteArray(len);
      }
    }

    public int getConfigurationVersion()
    {
      return configurationVersion;
    }

    /// <summary>
    /// The AVC profile code as defined in ISO/IEC 14496-10.
    /// </summary>
    /// <returns>the AVC profile</returns>
    public int getProfile()
    {
      return profile;
    }

    /// <summary>
    /// The profileCompatibility is a byte defined exactly the same as the byte which occurs between the profileIDC and levelIDC in a sequence parameter set (SPS), as defined in ISO/IEC 14496-10.
    /// </summary>
    /// <returns>the profile compatibility byte</returns>
    public byte getProfileCompatibility()
    {
      return profileCompatibility;
    }

    public int getLevel()
    {
      return level;
    }

    /// <summary>
    /// The length in bytes of the NALUnitLength field in an AVC video sample or AVC parameter set sample of the associated stream. The value of this field 1, 2, or 4 bytes.
    /// </summary>
    /// <returns>the NALUnitLength length in bytes</returns>
    public int getLengthSize()
    {
      return lengthSize;
    }

    /// <summary>
    /// The SPS NAL units, as specified in ISO/IEC 14496-10. SPSs shall occur in order of ascending parameter set identifier with gaps being allowed.
    /// </summary>
    /// <returns>all SPS NAL units</returns>
    public byte[][] getSequenceParameterSetNALUnits()
    {
      return sequenceParameterSetNALUnit;
    }

    /// <summary>
    /// The PPS NAL units, as specified in ISO/IEC 14496-10. PPSs shall occur in order of ascending parameter set identifier with gaps being allowed.
    /// </summary>
    /// <returns></returns>
    public byte[][] getPictureParameterSetNALUnits()
    {
      return pictureParameterSetNALUnit;
    }
  }
}
