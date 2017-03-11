// ReSharper disable InconsistentNaming

using System;
using System.IO;

namespace MultiWaveDecoder
{
  public static class BoxFactory
  {
    public static string typeToString(BoxTypes type)
    {
      uint l = (uint)type;
      var b = new char[4];
      b[0] = (char)((l >> 24) & 0xFF);
      b[1] = (char)((l >> 16) & 0xFF);
      b[2] = (char)((l >> 8) & 0xFF);
      b[3] = (char)(l & 0xFF);
      return new string(b);
    }

    public static Box parseBox(Box parent, MP4InputStream inStream)
    {
      long offset = inStream.getOffset();
      long size = inStream.readBytes(4);
      var type = (BoxTypes)(uint)inStream.readBytes(4);
      if (size == 1) size = inStream.readBytes(8);
      if (type == BoxTypes.EXTENDED_TYPE) inStream.skipBytes(16);

      // --- error protection ---
      if (parent != null)
      {
        throw new NotImplementedException();
        //long parentLeft = (parent.getOffset() + parent.getSize()) - offset;
        //if (size > parentLeft) throw new IOException("error while decoding box '" + type + "' ('" + typeToString(type) + "') at offset " + offset.ToString("N0") + ": box too large for parent");
      }

      Logger.LogBoxes(typeToString(type));
      //BoxImpl box = forType(type, inStream.getOffset());
      //box.setParams(parent, size, type, offset);
      //box.decode(inStream);

      ////if box doesn't contain data it only contains children
      //Class<?> cl = box.getClass();
      //if(cl==BoxImpl.class||cl==FullBox.class) box.readChildren(inStream);

      throw new NotImplementedException();
      ////check bytes left
      //long left = (box.getOffset()+box.getSize())-inStream.getOffset();
      //if(left>0
      //    &&!(box instanceof MediaDataBox)
      //    &&!(box instanceof UnknownBox)
      //    &&!(box instanceof FreeSpaceBox)) LOGGER.log(Level.INFO, "bytes left after reading box {0}: left: {1}, offset: {2}", new Object[]{typeToString(type), left, inStream.getOffset()});
      //else if(left<0) LOGGER.log(Level.SEVERE, "box {0} overread: {1} bytes, offset: {2}", new Object[]{typeToString(type), -left, inStream.getOffset()});

      ////if mdat found and no random access, don't skip
      //if(box.getType()!=MEDIA_DATA_BOX||inStream.hasRandomAccess()) inStream.skipBytes(left);
      //return box;
    }

  }
}
