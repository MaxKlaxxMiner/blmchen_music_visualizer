// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// The <code>InitialObjectDescriptor</code> is a variation of the <code>ObjectDescriptor</code> that shall be used to gain initial access to content.
  /// </summary>
  public sealed class InitialObjectDescriptor : Descriptor
  {
    int objectDescriptorID;
    bool urlPresent, includeInlineProfiles;
    string url;
    int odProfile, sceneProfile, audioProfile, visualProfile, graphicsProfile;

    public override void decode(MP4InputStream inStream)
    {
      // 10 bits objectDescriptorID, 1 bit url flag, 1 bit
      // includeInlineProfiles flag, 4 bits reserved
      int x = (int)inStream.readBytes(2);
      objectDescriptorID = (x >> 6) & 0x3FF;
      urlPresent = ((x >> 5) & 1) == 1;
      includeInlineProfiles = ((x >> 4) & 1) == 1;

      if (urlPresent) url = inStream.readString(size - 2);
      else
      {
        odProfile = inStream.read();
        sceneProfile = inStream.read();
        audioProfile = inStream.read();
        visualProfile = inStream.read();
        graphicsProfile = inStream.read();
      }

      readChildren(inStream);
    }

    /// <summary>
    /// The ID uniquely identifies this ObjectDescriptor within its name scope. It should be within 0 and 1023 exclusively. The value 0 is forbidden and the value 1023 is reserved.
    /// </summary>
    /// <returns>this ObjectDescriptor's ID</returns>
    public int getObjectDescriptorID()
    {
      return objectDescriptorID;
    }

    /// <summary>
    /// A flag that, if set, indicates that the subsequent profile indications take into account the resources needed to process any content that may be inlined.
    /// </summary>
    /// <returns>true if this ObjectDescriptor includes inline profiles</returns>
    public bool includesInlineProfiles()
    {
      return includeInlineProfiles;
    }

    /// <summary>
    /// A flag that indicates the presence of a URL. If set, no profiles are present.
    /// </summary>
    /// <returns>true if a URL is present</returns>
    public bool isURLPresent()
    {
      return urlPresent;
    }

    /// <summary>
    /// A URL string that shall point to another InitialObjectDescriptor. If no URL is present (if <code>isURLPresent()</code> returns false) this method returns null.
    /// </summary>
    /// <returns>a URL string or null if none is present</returns>
    public string getURL()
    {
      return url;
    }

    /// <summary>
    /// A flag that indicates the presence of profiles. If set, no URL is present.
    /// </summary>
    /// <returns>true if profiles are present</returns>
    public bool areProfilesPresent()
    {
      return !urlPresent;
    }

    /// <summary>
    /// TODO: doc
    /// </summary>
    /// <returns></returns>
    public int getODProfile()
    {
      return odProfile;
    }

    /// <summary>
    /// /// An indication of the scene description profile required to process the
    /// content associated with this InitialObjectDescriptor.<br />
    /// The value should be one of the following:
    /// 0x00: reserved for ISO use
    /// 0x01: ISO 14496-1 XXXX profile
    /// 0x02-0x7F: reserved for ISO use
    /// 0x80-0xFD: user private
    /// 0xFE: no scene description profile specified
    /// 0xFF: no scene description capability required
    /// </summary>
    /// <returns>the scene profile</returns>
    public int getSceneProfile()
    {
      return sceneProfile;
    }

    /// <summary>
    /// An indication of the audio profile required to process the content
    /// associated with this InitialObjectDescriptor.<br />
    /// The value should be one of the following:
    /// 0x00: reserved for ISO use
    /// 0x01: ISO 14496-3 XXXX profile
    /// 0x02-0x7F: reserved for ISO use
    /// 0x80-0xFD: user private
    /// 0xFE: no audio profile specified
    /// 0xFF: no audio capability required
    /// </summary>
    /// <returns>the audio profile</returns>
    public int getAudioProfile()
    {
      return audioProfile;
    }

    /// <summary>
    /// An indication of the visual profile required to process the content
    /// associated with this InitialObjectDescriptor.<br />
    /// The value should be one of the following:
    /// 0x00: reserved for ISO use
    /// 0x01: ISO 14496-2 XXXX profile
    /// 0x02-0x7F: reserved for ISO use
    /// 0x80-0xFD: user private
    /// 0xFE: no visual profile specified
    /// 0xFF: no visual capability required
    /// </summary>
    /// <returns>the visual profile</returns>
    public int getVisualProfile()
    {
      return visualProfile;
    }

    /// <summary>
    /// An indication of the graphics profile required to process the content
    /// associated with this InitialObjectDescriptor.<br />
    /// The value should be one of the following:
    /// 0x00: reserved for ISO use
    /// 0x01: ISO 14496-1 XXXX profile
    /// 0x02-0x7F: reserved for ISO use
    /// 0x80-0xFD: user private
    /// 0xFE: no graphics profile specified
    /// 0xFF: no graphics capability required
    /// </summary>
    /// <returns>@return the graphics profile</returns>
    public int getGraphicsProfile()
    {
      return graphicsProfile;
    }
  }
}
