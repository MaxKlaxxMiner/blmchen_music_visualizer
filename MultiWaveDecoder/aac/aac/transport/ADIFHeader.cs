// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
namespace MultiWaveDecoder
{
  public class ADIFHeader
  {
    const int ADIF_ID = 0x41444946; //'ADIF'
    long id;
    bool copyrightIDPresent;
    readonly byte[] copyrightID = new byte[9];
    bool originalCopy, home, bitstreamType;
    int bitrate;
    int pceCount;
    int[] adifBufferFullness;
    PCE[] pces;

    public static bool isPresent(BitStream inStream)
    {
      return inStream.peekBits(32) == ADIF_ID;
    }

    public static ADIFHeader readHeader(BitStream inStream)
    {
      var h = new ADIFHeader();
      h.decode(inStream);
      return h;
    }

    void decode(BitStream inStream)
    {
      id = inStream.readBits(32); //'ADIF'
      if (id != ADIF_ID) throw new AACException("id != ADIF_ID: " + id);
      copyrightIDPresent = inStream.readBool();
      if (copyrightIDPresent)
      {
        for (int i = 0; i < 9; i++)
        {
          copyrightID[i] = (byte)inStream.readBits(8);
        }
      }
      originalCopy = inStream.readBool();
      home = inStream.readBool();
      bitstreamType = inStream.readBool();
      bitrate = inStream.readBits(23);
      pceCount = inStream.readBits(4) + 1;
      pces = new PCE[pceCount];
      adifBufferFullness = new int[pceCount];
      for (int i = 0; i < pceCount; i++)
      {
        if (bitstreamType) adifBufferFullness[i] = -1;
        else adifBufferFullness[i] = inStream.readBits(20);
        pces[i] = new PCE();
        pces[i].decode(inStream);
      }
    }

    public PCE getFirstPCE()
    {
      return pces[0];
    }
  }
}
