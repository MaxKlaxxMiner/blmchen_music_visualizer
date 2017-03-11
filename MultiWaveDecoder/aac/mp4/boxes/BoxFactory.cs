// ReSharper disable InconsistentNaming

using System;

namespace MultiWaveDecoder
{
  public static class BoxFactory
  {
    public static string typeToString(BoxType type)
    {
      uint l = (uint)type;
      var b = new char[4];
      b[0] = (char)((l >> 24) & 0xFF);
      b[1] = (char)((l >> 16) & 0xFF);
      b[2] = (char)((l >> 8) & 0xFF);
      b[3] = (char)(l & 0xFF);
      for (int i = 0; i < b.Length; i++) if (b[i] < ' ') b[i] = ' ';
      return new string(b);
    }

    static BoxImpl forType(BoxType type, long offset)
    {
      try
      {
        switch (type)
        {
          case BoxType.FILE_TYPE_BOX: return new FileTypeBox();
          case BoxType.SKIP_BOX:
          case BoxType.WIDE_BOX:
          case BoxType.FREE_SPACE_BOX: return new FreeSpaceBox();
          case BoxType.MEDIA_DATA_BOX: return new MediaDataBox();
          case BoxType.MOVIE_BOX: return new BoxImpl("Movie Box");
          case BoxType.MOVIE_HEADER_BOX: return new MovieHeaderBox();
          case BoxType.TRACK_BOX: return new BoxImpl("Track Box");
          case BoxType.TRACK_HEADER_BOX: return new TrackHeaderBox();
          case BoxType.MEDIA_BOX: return new BoxImpl("Media Box");
          case BoxType.MEDIA_HEADER_BOX: return new MediaHeaderBox();
          default:
          {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Logger.LogBoxes("BoxFactory - unknown box type: " + type + " '" + typeToString(type) + "', position: " + offset.ToString("N0"));
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            return new UnknownBox();
          }
        }
      }
      catch (Exception e)
      {
        Logger.LogBoxes("BoxFactory - could not call constructor for " + type + " '" + typeToString(type) + "': " + e);
        return new UnknownBox();
      }
    }

    public static IBox parseBox(IBox parent, MP4InputStream inStream)
    {
      long offset = inStream.getOffset();
      long size = inStream.readBytes(4);
      var type = (BoxType)(uint)inStream.readBytes(4);
      if (size == 1) size = inStream.readBytes(8);
      if (type == BoxType.EXTENDED_TYPE) inStream.skipBytes(16);

      // --- error protection ---
      if (parent != null)
      {
        long parentLeft = (parent.getOffset() + parent.getSize()) - offset;
        if (size > parentLeft) throw new Exception("error while decoding box '" + type + "' ('" + typeToString(type) + "') at offset " + offset.ToString("N0") + ": box too large for parent");
      }

      Logger.LogBoxes(typeToString(type));
      var box = forType(type, inStream.getOffset());
      box.setParams(parent, size, type, offset);
      box.decode(inStream);

      // --- if box doesn't contain data it only contains children ---
      if (box.GetType() == typeof(BoxImpl) || box.GetType() == typeof(FullBox)) box.readChildren(inStream);

      // --- check bytes left ---
      long left = (box.getOffset() + box.getSize()) - inStream.getOffset();
      if (left > 0
          && !(box is MediaDataBox)
          && !(box is UnknownBox)
          && !(box is FreeSpaceBox)) Logger.LogInfo(string.Format("bytes left after reading box {0}: left: {1}, offset: {2}", typeToString(type), left, inStream.getOffset()));
      else if (left < 0) Logger.LogServe(string.Format("box {0} overread: {1} bytes, offset: {2}", typeToString(type), -left, inStream.getOffset()));

      // --- skip left Data ---
      inStream.skipBytes(left);

      return box;
    }

  }
}
