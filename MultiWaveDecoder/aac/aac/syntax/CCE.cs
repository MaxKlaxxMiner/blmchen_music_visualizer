using System;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global

namespace MultiWaveDecoder
{
  public class CCE : Element
  {
    public const int BEFORE_TNS = 0;
    public const int AFTER_TNS = 1;
    public const int AFTER_IMDCT = 2;
    static readonly float[] CCE_SCALE =
    {
      1.09050773266525765921f,
      1.18920711500272106672f,
      1.4142135623730950488016887f,
      2f
    };

    readonly ICStream ics;
    float[] iqData;
    int couplingPoint;
    int coupledCount;
    readonly bool[] channelPair;
    readonly int[] idSelect;
    readonly int[] chSelect;
    // [0] shared list of gains
    // [1] list of gains for right channel
    // [2] list of gains for left channel
    // [3] lists of gains for both channels
    readonly float[,] gain;

    CCE(int frameLength)
    {
      ics = new ICStream(frameLength);
      channelPair = new bool[8];
      idSelect = new int[8];
      chSelect = new int[8];
      gain = new float[16, 120];
    }

    public int getCouplingPoint()
    {
      return couplingPoint;
    }

    public int getCoupledCount()
    {
      return coupledCount;
    }

    public bool isChannelPair(int index)
    {
      return channelPair[index];
    }

    public int getIDSelect(int index)
    {
      return idSelect[index];
    }

    public int getCHSelect(int index)
    {
      return chSelect[index];
    }

    void decode(BitStream inStream, DecoderConfig conf)
    {
      couplingPoint = 2 * inStream.readBit();
      coupledCount = inStream.readBits(3);
      int gainCount = 0;
      for (int i = 0; i <= coupledCount; i++)
      {
        gainCount++;
        channelPair[i] = inStream.readBool();
        idSelect[i] = inStream.readBits(4);
        if (channelPair[i])
        {
          chSelect[i] = inStream.readBits(2);
          if (chSelect[i] == 3) gainCount++;
        }
        else chSelect[i] = 2;
      }
      couplingPoint += inStream.readBit();
      couplingPoint |= (couplingPoint >> 1);

      bool sign = inStream.readBool();
      double scale = CCE_SCALE[inStream.readBits(2)];

      ics.decode(inStream, false, conf);
      ICSInfo info = ics.getInfo();
      int windowGroupCount = info.getWindowGroupCount();
      int maxSFB = info.getMaxSFB();
      //TODO:
      int[,] sfbCB = null;//ics.getSectionData().getSfbCB();

      for (int i = 0; i < gainCount; i++)
      {
        int idx = 0;
        int cge = 1;
        int xg = 0;
        float gainCache = 1.0f;
        if (i > 0)
        {
          cge = couplingPoint == 2 ? 1 : inStream.readBit();
          xg = cge == 0 ? 0 : Huffman.decodeScaleFactor(inStream) - 60;
          gainCache = (float)Math.Pow(scale, -xg);
        }
        if (couplingPoint == 2) gain[i, 0] = gainCache;
        else
        {
          for (int g = 0; g < windowGroupCount; g++)
          {
            for (int sfb = 0; sfb < maxSFB; sfb++, idx++)
            {
              if (sfbCB[g, sfb] != ZERO_HCB)
              {
                if (cge == 0)
                {
                  int t = Huffman.decodeScaleFactor(inStream) - 60;
                  if (t != 0)
                  {
                    int s = 1;
                    t = xg += t;
                    if (!sign)
                    {
                      s -= 2 * (t & 0x1);
                      t >>= 1;
                    }
                    gainCache = (float)(Math.Pow(scale, -t) * s);
                  }
                }
                gain[i, idx] = gainCache;
              }
            }
          }
        }
      }
    }

    void process()
    {
      iqData = ics.getInvQuantData();
    }

    public void applyIndependentCoupling(int index, float[] data)
    {
      double g = gain[index, 0];
      for (int i = 0; i < data.Length; i++)
      {
        data[i] = (float)(data[i] + g * iqData[i]);
      }
    }

    public void applyDependentCoupling(int index, float[] data)
    {
      var info = ics.getInfo();
      var swbOffsets = info.getSWBOffsets();
      int windowGroupCount = info.getWindowGroupCount();
      int maxSFB = info.getMaxSFB();
      //TODO:
      int[,] sfbCB = null; //ics.getSectionData().getSfbCB();

      int srcOff = 0;
      int dstOff = 0;

      int idx = 0;
      for (int g = 0; g < windowGroupCount; g++)
      {
        int len = info.getWindowGroupLength(g);
        for (int sfb = 0; sfb < maxSFB; sfb++, idx++)
        {
          if (sfbCB[g, sfb] != ZERO_HCB)
          {
            float x = gain[index, idx];
            for (int group = 0; group < len; group++)
            {
              for (int k = swbOffsets[sfb]; k < swbOffsets[sfb + 1]; k++)
              {
                data[dstOff + group * 128 + k] += x * iqData[srcOff + group * 128 + k];
              }
            }
          }
        }
        dstOff += len * 128;
        srcOff += len * 128;
      }
    }
  }
}
