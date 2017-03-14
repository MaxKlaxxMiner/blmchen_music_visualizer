using System.Drawing;

namespace MultiWaveDecoder
{
  /// <summary>
  /// The video media header contains general presentation information, independent of the coding, for video media
  /// </summary>
  public sealed class VideoMediaHeaderBox : FullBox
  {
    long graphicsMode;
    Color color;

    public VideoMediaHeaderBox() : base("Video Media Header Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      graphicsMode = inStream.readBytes(2);
      // 6 byte RGB color
      var c = new int[3];
      for (int i = 0; i < 3; i++)
      {
        c[i] = (inStream.read() & 0xFF) | ((inStream.read() << 8) & 0xFF);
      }
      color = Color.FromArgb(c[0], c[1], c[2]);
    }

    /// <summary>
    /// The graphics mode specifies a composition mode for this video track.
    /// Currently, only one mode is defined:
    /// '0': copy over the existing image
    /// </summary>
    /// <returns></returns>
    public long getGraphicsMode()
    {
      return graphicsMode;
    }

    /// <summary>
    /// A color available for use by graphics modes.
    /// </summary>
    /// <returns></returns>
    public Color getColor()
    {
      return color;
    }
  }
}
