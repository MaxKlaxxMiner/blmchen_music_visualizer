// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
namespace MultiWaveDecoder
{
  public sealed class SCE_LFE : Element
  {
    readonly ICStream ics;

    public SCE_LFE(int frameLength)
    {
      ics = new ICStream(frameLength);
    }

    public void decode(BitStream inStream, DecoderConfig conf)
    {
      readElementInstanceTag(inStream);
      ics.decode(inStream, false, conf);
    }

    public ICStream getICStream()
    {
      return ics;
    }
  }
}
