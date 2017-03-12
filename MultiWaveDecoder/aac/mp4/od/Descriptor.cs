// ReSharper disable InconsistentNaming

using System.Collections.Generic;

namespace MultiWaveDecoder
{
  /// <summary>
  /// The abstract base class and factory for all descriptors (defined in ISO 14496-1 as 'ObjectDescriptors').
  /// </summary>
  public abstract class Descriptor
  {
    public const int TYPE_OBJECT_DESCRIPTOR = 1;
    public const int TYPE_INITIAL_OBJECT_DESCRIPTOR = 2;
    public const int TYPE_ES_DESCRIPTOR = 3;
    public const int TYPE_DECODER_CONFIG_DESCRIPTOR = 4;
    public const int TYPE_DECODER_SPECIFIC_INFO = 5;
    public const int TYPE_SL_CONFIG_DESCRIPTOR = 6;
    public const int TYPE_ES_ID_INC = 14;
    public const int TYPE_MP4_INITIAL_OBJECT_DESCRIPTOR = 16;

    public static Descriptor createDescriptor(MP4InputStream inStream)
    {
      //read tag and size
      int type = inStream.read();
      int read = 1;
      int size = 0;
      int b = 0;
      do
      {
        b = inStream.read();
        size <<= 7;
        size |= b & 0x7f;
        read++;
      }
      while ((b & 0x80) == 0x80);

      //create descriptor
      Descriptor desc = forTag(type);
      desc.type = type;
      desc.size = size;
      desc.start = inStream.getOffset();

      //decode
      desc.decode(inStream);
      //skip remaining bytes
      long remaining = size - (inStream.getOffset() - desc.start);
      if (remaining > 0)
      {
        Logger.LogBoxes(string.Format("Descriptor: bytes left: {0:N0}, offset: {1:N0}", remaining, inStream.getOffset()));
        inStream.skipBytes(remaining);
      }
      desc.size += read; //include type and size fields

      return desc;
    }

    static Descriptor forTag(int tag)
    {
      switch (tag)
      {
        case TYPE_OBJECT_DESCRIPTOR: return new ObjectDescriptor();
        case TYPE_INITIAL_OBJECT_DESCRIPTOR:
        case TYPE_MP4_INITIAL_OBJECT_DESCRIPTOR: return new InitialObjectDescriptor();
        case TYPE_ES_DESCRIPTOR: return new ESDescriptor();
        case TYPE_DECODER_CONFIG_DESCRIPTOR: return new DecoderConfigDescriptor();
        case TYPE_DECODER_SPECIFIC_INFO: return new DecoderSpecificInfo();
        case TYPE_SL_CONFIG_DESCRIPTOR: return new SLConfigDescriptor();
        default:
        {
          Logger.LogBoxes("Unknown descriptor type: " + tag);
          return new UnknownDescriptor();
        }
      }
    }

    protected int type, size;
    protected long start;
    readonly List<Descriptor> children = new List<Descriptor>();

    public abstract void decode(MP4InputStream inStream);

    // --- children ---
    protected void readChildren(MP4InputStream inStream)
    {
      while ((size - (inStream.getOffset() - start)) > 0)
      {
        Descriptor desc = createDescriptor(inStream);
        children.Add(desc);
      }
    }

    public Descriptor[] getChildren()
    {
      return children.ToArray();
    }

    //getter
    public int getType()
    {
      return type;
    }

    public int getSize()
    {
      return size;
    }
  }
}
