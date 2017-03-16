using System;
using System.Linq;
using System.Text;

// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  /// <summary>
  /// This box contains the data for a metadata tag. It is right below an iTunes metadata box (e.g. '@nam') or a custom meta tag box ('----'). A custom meta tag box also contains a 'name'-box declaring the tag's name.
  /// </summary>
  // TODO: use generics here? -> each DataType should return <T> corresponding to its class (string/Integer/...)
  public sealed class ITunesMetadataBox : FullBox
  {
    static readonly string[] TIMESTAMPS = { "yyyy", "yyyy-MM", "yyyy-MM-dd" };

    public enum DataType
    {
      IMPLICIT, // object
      UTF8,     // string
      UTF16,    // string
      HTML,     // string
      XML,      // string
      UUID,     // long
      ISRC,     // string
      MI3P,     // string
      GIF,      // byte[]
      JPEG,     // byte[]
      PNG,      // byte[]
      URL,      // string
      DURATION, // long
      DATETIME, // long
      GENRE,    // int
      INTEGER,  // long
      RIAA,     // int
      UPC,      // string
      BMP,      // byte[]
      UNDEFINED // byte[]
    }

    static readonly DataType[] TYPES =
    {
      DataType.IMPLICIT, DataType.UTF8, DataType.UTF16,DataType.UNDEFINED, DataType.UNDEFINED, DataType.UNDEFINED, DataType.HTML, DataType.XML,
      DataType.UUID, DataType.ISRC, DataType.MI3P, DataType.UNDEFINED,DataType.GIF, DataType.JPEG, DataType.PNG, DataType.URL,
      DataType.DURATION, DataType.DATETIME, DataType.GENRE,DataType.UNDEFINED, DataType.UNDEFINED, DataType.INTEGER,DataType.UNDEFINED,DataType.UNDEFINED,
      DataType.RIAA, DataType.UPC,DataType.UNDEFINED,DataType.BMP
    };

    static DataType forInt(int i)
    {
      return (uint)i < TYPES.Length ? TYPES[i] : DataType.UNDEFINED;
    }

    DataType dataType;
    byte[] data;

    public ITunesMetadataBox() : base("iTunes Metadata Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      dataType = forInt(flags);

      inStream.skipBytes(4); // padding?

      data = new byte[(int)getLeft(inStream)];
      inStream.read(data, 0, data.Length);
    }

    public DataType getDataType()
    {
      return dataType;
    }

    /// <summary>
    /// Returns an unmodifiable array with the raw content, that can be present in different formats.
    /// </summary>
    /// <returns>the raw metadata</returns>
    public byte[] getData()
    {
      return data.ToArray();
    }

    /// <summary>
    /// Returns the content as a text string.
    /// </summary>
    /// <returns>the metadata as text</returns>
    public string getText()
    {
      return Encoding.UTF8.GetString(data);
    }

    /// <summary>
    /// Returns the content as an unsigned 8-bit integer.
    /// </summary>
    /// <returns>the metadata as an integer</returns>
    public long getNumber()
    {
      long l = 0;
      for (int i = 0; i < data.Length; i++)
      {
        l <<= 8;
        l |= (data[i] & 0xFF);
      }
      return l;
    }

    public int getInteger()
    {
      return (int)getNumber();
    }

    /// <summary>
    /// Returns the content as a bool (flag) value.
    /// </summary>
    /// <returns>the metadata as a bool</returns>
    public bool getBoolean()
    {
      return getNumber() != 0;
    }

    public DateTime getDate()
    {
      // timestamp lengths: 4,7,9
      int i = data.Length / 3 - 1;
      DateTime date;
      if (i >= 0 && i < TIMESTAMPS.Length)
      {
        var timestamp = TIMESTAMPS[i];
        throw new NotImplementedException();
        //SimpleDateFormat sdf = new SimpleDateFormat(timestamp);
        //date = sdf.parse(new string(data), new ParsePosition(0));
      }
      else date = DateTime.Parse(getText());

      return date;
    }
  }
}
