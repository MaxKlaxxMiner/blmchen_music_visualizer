using System.Drawing;
using System.IO;
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public sealed class Artwork
  {
    // TODO: need this enum? it just copies the DataType
    public enum ImageType
    {
      Unknown,
      GIF,
      JPEG,
      PNG,
      BMP
    }

    public static ImageType forDataType(ITunesMetadataBox.DataType dataType)
    {
      switch (dataType)
      {
        case ITunesMetadataBox.DataType.GIF: return ImageType.GIF;
        case ITunesMetadataBox.DataType.JPEG: return ImageType.JPEG;
        case ITunesMetadataBox.DataType.PNG: return ImageType.PNG;
        case ITunesMetadataBox.DataType.BMP: return ImageType.BMP;
        default: return ImageType.Unknown;
      }
    }

    readonly ImageType type;
    readonly byte[] data;
    Bitmap image;

    public Artwork(ImageType type, byte[] data)
    {
      this.type = type;
      this.data = data;
    }

    /// <summary>
    /// Returns the type of data in this artwork. <see cref="ImageType"/>
    /// </summary>
    /// <returns>the data's type</returns>
    public ImageType getType()
    {
      return type;
    }

    /// <summary>
    /// Returns the encoded data of this artwork.
    /// </summary>
    /// <returns>the encoded data</returns>
    public byte[] getData()
    {
      return data;
    }

    /// <summary>
    /// Returns the decoded image, that can be painted.
    /// </summary>
    /// <returns>the decoded image</returns>
    public Bitmap getImage()
    {
      try
      {
        return image ?? (image = new Bitmap(new MemoryStream(data)));
      }
      catch (IOException e)
      {
        Logger.LogServe("MP4 API: Artwork.getImage failed: " + e);
        throw;
      }
    }
  }
}
