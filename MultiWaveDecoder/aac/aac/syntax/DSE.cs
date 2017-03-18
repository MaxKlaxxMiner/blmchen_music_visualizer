// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
namespace MultiWaveDecoder
{
  public abstract class DSE : Element
  {
    byte[] dataStreamBytes;

    void decode(BitStream inStream)
    {
      bool byteAlign = inStream.readBool();
      int count = inStream.readBits(8);
      if (count == 255) count += inStream.readBits(8);

      if (byteAlign) inStream.byteAlign();

      dataStreamBytes = new byte[count];
      for (int i = 0; i < count; i++)
      {
        dataStreamBytes[i] = (byte)inStream.readBits(8);
      }
    }
  }
}
