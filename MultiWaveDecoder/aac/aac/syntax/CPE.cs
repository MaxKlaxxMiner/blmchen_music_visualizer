// ReSharper disable InconsistentNaming

using System;

namespace MultiWaveDecoder
{
  public sealed class CPE : Element
  {
    MSMask msMask;
    bool[] msUsed = new bool[MAX_MS_MASK];
    bool commonWindow;
    ICStream icsL, icsR;

    public CPE(int frameLength)
    {
      icsL = new ICStream(frameLength);
      icsR = new ICStream(frameLength);
    }

    public void decode(BitStream inStream, DecoderConfig conf)
    {
      var profile = conf.getProfile();
      var sf = conf.getSampleFrequency();
      if (sf.getIndex() == -1) throw new AACException("invalid sample frequency");

      readElementInstanceTag(inStream);

      commonWindow = inStream.readBool();

      throw new NotImplementedException();
      //ICSInfo info = icsL.getInfo();
      //if (commonWindow)
      //{
      //  info.decode(inStream, conf, commonWindow);
      //  icsR.getInfo().setData(info);

      //  msMask = MSMask.forInt(inStream.readBits(2));
      //  if (msMask.equals(MSMask.TYPE_USED))
      //  {
      //    int maxSFB = info.getMaxSFB();
      //    int windowGroupCount = info.getWindowGroupCount();

      //    for (int idx = 0; idx < windowGroupCount * maxSFB; idx++)
      //    {
      //      msUsed[idx] = inStream.readBool();
      //    }
      //  }
      //  else if (msMask.equals(MSMask.TYPE_ALL_1)) Arrays.fill(msUsed, true);
      //  else if (msMask.equals(MSMask.TYPE_ALL_0)) Arrays.fill(msUsed, false);
      //  else throw new AACException("reserved MS mask type used");
      //}
      //else
      //{
      //  msMask = MSMask.TYPE_ALL_0;
      //  Arrays.fill(msUsed, false);
      //}

      //if (profile.isErrorResilientProfile() && (info.isLTPrediction1Present()))
      //{
      //  if (info.ltpData2Present = inStream.readBool()) info.getLTPrediction2().decode(inStream, info, profile);
      //}

      //icsL.decode(inStream, commonWindow, conf);
      //icsR.decode(inStream, commonWindow, conf);
    }

    public ICStream getLeftChannel()
    {
      return icsL;
    }

    public ICStream getRightChannel()
    {
      return icsR;
    }

    public MSMask getMSMask()
    {
      return msMask;
    }

    public bool isMSUsed(int off)
    {
      return msUsed[off];
    }

    public bool isMSMaskPresent()
    {
      return msMask != MSMask.TYPE_ALL_0;
    }

    public bool isCommonWindow()
    {
      return commonWindow;
    }
  }
}
