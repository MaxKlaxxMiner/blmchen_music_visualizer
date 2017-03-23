// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
using System;

namespace MultiWaveDecoder
{
  public abstract class Element : Constants
  {
    int elementInstanceTag;
    SBR sbr;

    protected void readElementInstanceTag(BitStream inStream)
    {
      elementInstanceTag = inStream.readBits(4);
    }

    public int getElementInstanceTag()
    {
      return elementInstanceTag;
    }

    void decodeSBR(BitStream inStream, SampleFrequency sf, int count, bool stereo, bool crc, bool downSampled, bool smallFrames)
    {
      if (sbr == null) sbr = new SBR(smallFrames, elementInstanceTag == ELEMENT_CPE, sf, downSampled);
      throw new NotImplementedException();
      //sbr.decode(inStream, count);
    }

    bool isSBRPresent()
    {
      return sbr != null;
    }

    public SBR getSBR()
    {
      return sbr;
    }
  }
}
