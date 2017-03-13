// ReSharper disable InconsistentNaming

using System.Text;

namespace MultiWaveDecoder
{
  /// <summary>
  /// This box within a Media Box declares the process by which the media-data in the track is presented, and thus, the nature of the media in a track. 
  /// For example, a video track would be handled by a video handler.
  ///
  /// This box when present within a Meta Box, declares the structure or format of the 'meta' box contents.
  ///
  /// There is a general handler for metadata streams of any type; the specific format is identified by the sample entry, as for video or audio, for example.
  /// If they are in text, then a MIME format is supplied to document their format; if in XML, each sample is a complete XML document, and the namespace of the XML is also supplied.
  /// </summary>
  public sealed class HandlerBox : FullBox
  {
    // --- ISO BMFF types ---
    public const int TYPE_VIDEO = 1986618469; //vide
    public const int TYPE_SOUND = 1936684398; //soun
    public const int TYPE_HINT = 1751740020; //hint
    public const int TYPE_META = 1835365473; //meta
    public const int TYPE_NULL = 1853189228; //null
    // --- MP4 types ---
    public const int TYPE_ODSM = 1868854125; //odsm
    public const int TYPE_CRSM = 1668445037; //crsm
    public const int TYPE_SDSM = 1935962989; //sdsm
    public const int TYPE_M7SM = 1832350573; //m7sm
    public const int TYPE_OCSM = 1868788589; //ocsm
    public const int TYPE_IPSM = 1768977261; //ipsm
    public const int TYPE_MJSM = 1835692909; //mjsm
    long handlerType;
    string handlerName;

    public HandlerBox() : base("Handler Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      inStream.skipBytes(4); //pre-defined: 0

      handlerType = inStream.readBytes(4);

      inStream.readBytes(4); //reserved
      inStream.readBytes(4); //reserved
      inStream.readBytes(4); //reserved

      handlerName = inStream.readEncodedString((int)getLeft(inStream), Encoding.UTF8);
    }

    /// <summary>
    /// When present in a media box, the handler type is an integer containing one of the following values:
    /// <ul>
    /// <li>'vide': Video track</li>
    /// <li>'soun': Audio track</li>
    /// <li>'hint': Hint track</li>
    /// <li>'meta': Timed Metadata track</li>
    /// </ul>
    ///
    /// When present in a meta box, it contains an appropriate value to indicate the format of the meta box contents. The value 'null' can be used in the
    /// primary meta box to indicate that it is merely being used to hold resources.
    /// </summary>
    /// <returns>the handler type</returns>
    public long getHandlerType()
    {
      return handlerType;
    }

    /// <summary>
    /// The name gives a human-readable name for the track type (for debugging and inspection purposes).
    /// </summary>
    /// <returns>the handler type's name</returns>
    public string getHandlerName()
    {
      return handlerName;
    }
  }
}
