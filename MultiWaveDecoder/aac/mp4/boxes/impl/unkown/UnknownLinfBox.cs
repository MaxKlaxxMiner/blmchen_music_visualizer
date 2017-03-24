using System;

namespace MultiWaveDecoder
{
  /// <summary>
  /// Unkown Box-Type found in iTunes Music Files ("I16" maybe a Format-Code?)
  /// </summary>
  public sealed class UnknownLinfBox : BoxImpl
  {
    public UnknownLinfBox() : base("Unknown 'linf' Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      try
      {
        string tmp = inStream.readString((int)getLeft(inStream));
      }
      catch
      {
        inStream.skipBytes(getLeft(inStream));
      }
    }
  }
}
