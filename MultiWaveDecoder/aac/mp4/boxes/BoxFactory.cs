// ReSharper disable InconsistentNaming

using System;

namespace MultiWaveDecoder
{
  public static class BoxFactory
  {
    public static Box parseBox(Box parent, MP4InputStream inStream)
    {
      long offset = inStream.getOffset();
      long size = inStream.readBytes(4);

      throw new NotImplementedException();
      //long type = inStream.readBytes(4);
      //if(size==1) size = inStream.readBytes(8);
      //if(type==EXTENDED_TYPE) inStream.skipBytes(16);

      ////error protection
      //if(parent!=null) {
      //  long parentLeft = (parent.getOffset()+parent.getSize())-offset;
      //  if(size>parentLeft) throw new IOException("error while decoding box '"+typeToString(type)+"' at offset "+offset+": box too large for parent");
      //}

      //Logger.getLogger("MP4 Boxes").finest(typeToString(type));
      //BoxImpl box = forType(type, inStream.getOffset());
      //box.setParams(parent, size, type, offset);
      //box.decode(inStream);

      ////if box doesn't contain data it only contains children
      //Class<?> cl = box.getClass();
      //if(cl==BoxImpl.class||cl==FullBox.class) box.readChildren(inStream);

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
