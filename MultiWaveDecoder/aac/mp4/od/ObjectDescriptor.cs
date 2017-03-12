// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// The <code>ObjectDescriptor</code> consists of three different parts:
  ///
  /// The first part uniquely labels the <code>ObjectDescriptor</code> within its name scope by means of an ID. Media objects in the scene description use this
  /// ID to refer to their object descriptor. An optional URL string indicates that the actual object descriptor resides at a remote location.
  ///
  /// The second part is a set of optional descriptors that support the inclusion if future extensions as well as the transport of private data in a backward compatible way.
  ///
  /// The third part consists of a list of <code>ESDescriptors</code>, each providing parameters for a single elementary stream that relates to the media
  /// object as well as an optional set of object content information descriptors.
  /// </summary>
  public sealed class ObjectDescriptor : Descriptor
  {
    int objectDescriptorID;
    bool urlPresent;
    string url;

    public override void decode(MP4InputStream inStream)
    {
      // 10 bits objectDescriptorID, 1 bit url flag, 5 bits reserved
      int x = (int)inStream.readBytes(2);
      objectDescriptorID = (x >> 6) & 0x3FF;
      urlPresent = ((x >> 5) & 1) == 1;

      if (urlPresent) url = inStream.readString(size - 2);

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
  }
}
