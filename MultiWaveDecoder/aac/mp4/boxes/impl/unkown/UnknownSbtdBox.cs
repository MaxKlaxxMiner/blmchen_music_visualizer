using System;

namespace MultiWaveDecoder
{
  /// <summary>
  /// Unkown Box-Type found in iTunes Music Files ("I16" maybe a Format-Code?)
  /// </summary>
  public sealed class UnknownSbtdBox : BoxImpl
  {
    public UnknownSbtdBox(): base("Unknown 'sbtd' Box"){}

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      try
      {
        int val = (int)inStream.readBytes(4);
        if (val != 0) throw new NotImplementedException("val != 0: " + val);

        string tmp = inStream.readString((int)getLeft(inStream));
        if (tmp != "I16") throw new NotImplementedException("tmp != \"I16\": \"" + tmp + "\"");
      }
      catch
      {
        inStream.skipBytes(getLeft(inStream));
      }
    }
  }
}
