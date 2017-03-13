using System.Text;

namespace MultiWaveDecoder
{
  /// <summary>
  /// Unkown Box-Type found in iTunes Music Files
  /// </summary>
  public sealed class UnknownAtIdBox : BoxImpl
  {
    public UnknownAtIdBox() : base("Unknown 'atID' Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      var buf = new byte[(int)getLeft(inStream)];
      inStream.read(buf, 0, buf.Length);

      string str = Encoding.ASCII.GetString(buf).Replace('\0', '\n').Replace("\n\n\n\n", "\n").Replace("\n\n\n", "\n").Replace("\n\n", "\n");
    }
  }
}
