// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  //open: 3gpp brands

  public sealed class FileTypeBox : BoxImpl
  {
    public const string BRAND_ISO_BASE_MEDIA = "isom";
    public const string BRAND_ISO_BASE_MEDIA_2 = "iso2";
    public const string BRAND_ISO_BASE_MEDIA_3 = "iso3";
    public const string BRAND_MP4_1 = "mp41";
    public const string BRAND_MP4_2 = "mp42";
    public static string BRAND_MOBILE_MP4 = "mmp4";
    public static string BRAND_QUICKTIME = "qm  ";
    public static string BRAND_AVC = "avc1";
    public static string BRAND_AUDIO = "M4A ";
    public static string BRAND_AUDIO_2 = "M4B ";
    public static string BRAND_AUDIO_ENCRYPTED = "M4P ";
    public static string BRAND_MP7 = "mp71";
    string majorBrand;
    string minorVersion;
    string[] compatibleBrands;

    public FileTypeBox() : base("File Type Box") { }

    public string getMajorBrand()
    {
      return majorBrand;
    }

    public string getMinorVersion()
    {
      return minorVersion;
    }

    public string[] getCompatibleBrands()
    {
      return compatibleBrands;
    }

    /// <summary>
    /// Decodes the given input stream by reading this box and all of its children (if any).
    /// </summary>
    /// <param name="inStream">an input stream</param>
    public override void decode(MP4InputStream inStream)
    {
      majorBrand = inStream.readString(4);
      minorVersion = inStream.readString(4);
      compatibleBrands = new string[(int)getLeft(inStream) / 4];
      for (int i = 0; i < compatibleBrands.Length; i++)
      {
        compatibleBrands[i] = inStream.readString(4);
      }
    }
  }
}
