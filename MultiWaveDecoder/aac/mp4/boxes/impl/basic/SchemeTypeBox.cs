
// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// The Scheme Type Box identifies the protection scheme.
  /// </summary>
  public sealed class SchemeTypeBox : FullBox
  {
    /// <summary>
    /// itun
    /// </summary>
    public static int ITUNES_SCHEME = 1769239918;

    int schemeType, schemeVersion;
    string schemeURI;

    public SchemeTypeBox() : base("Scheme Type Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      schemeType = (int)inStream.readBytes(4);
      schemeVersion = (int)inStream.readBytes(4);
      schemeURI = (flags & 1) == 1 ? inStream.readUTFString((int)getLeft(inStream)) : null;
    }

    /// <summary>
    /// The scheme type is the code defining the protection scheme.
    /// </summary>
    /// <returns>the scheme type</returns>
    public long getSchemeType()
    {
      return schemeType;
    }

    /// <summary>
    /// The scheme version is the version of the scheme used to create the content.
    /// </summary>
    /// <returns>the scheme version</returns>
    public long getSchemeVersion()
    {
      return schemeVersion;
    }

    /// <summary>
    /// The optional scheme URI allows for the option of directing the user to a web-page if they do not have the scheme installed on their system. It is an absolute URI.
    /// If the scheme URI is not present, this method returns null.
    /// </summary>
    /// <returns>the scheme URI or null, if no URI is present</returns>
    public string getSchemeURI()
    {
      return schemeURI;
    }
  }
}
