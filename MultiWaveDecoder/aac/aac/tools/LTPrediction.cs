// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Linq;
// ReSharper disable UnusedMember.Global
// ReSharper disable AssignmentInConditionalExpression

namespace MultiWaveDecoder
{
  /// <summary>
  /// Long-term prediction
  /// </summary>
  public sealed class LTPrediction : Constants
  {
    static float[] CODEBOOK = { 0.570829f, 0.696616f, 0.813004f, 0.911304f, 0.984900f, 1.067894f, 1.194601f, 1.369533f };
    int frameLength;
    int[] states;
    int coef, lag, lastBand;
    bool lagUpdate;
    bool[] shortUsed, shortLagPresent, longUsed;
    int[] shortLag;

    public LTPrediction(int frameLength)
    {
      this.frameLength = frameLength;
      states = new int[4 * frameLength];
    }

    public void decode(BitStream inStream, ICSInfo info, Profile profile)
    {
      lag = 0;
      if (profile.type == Profile.ProfileType.AAC_LD)
      {
        lagUpdate = inStream.readBool();
        if (lagUpdate) lag = inStream.readBits(10);
      }
      else lag = inStream.readBits(11);
      if (lag > (frameLength << 1)) throw new AACException("LTP lag too large: " + lag);
      coef = inStream.readBits(3);

      int windowCount = info.getWindowCount();

      if (info.isEightShortFrame())
      {
        shortUsed = new bool[windowCount];
        shortLagPresent = new bool[windowCount];
        shortLag = new int[windowCount];
        for (int w = 0; w < windowCount; w++)
        {
          if (shortUsed[w] = inStream.readBool())
          {
            shortLagPresent[w] = inStream.readBool();
            if (shortLagPresent[w]) shortLag[w] = inStream.readBits(4);
          }
        }
      }
      else
      {
        lastBand = Math.Min(info.getMaxSFB(), MAX_LTP_SFB);
        longUsed = new bool[lastBand];
        for (int i = 0; i < lastBand; i++)
        {
          longUsed[i] = inStream.readBool();
        }
      }
    }

    public void setPredictionUnused(int sfb)
    {
      if (longUsed != null) longUsed[sfb] = false;
    }

    public void process(ICStream ics, float[] data, FilterBank filterBank, SampleFrequency sf)
    {
      var info = ics.getInfo();

      if (!info.isEightShortFrame())
      {
        int samples = frameLength << 1;
        var inf = new float[2048];
        var outf = new float[2048];

        for (int i = 0; i < samples; i++)
        {
          inf[i] = states[samples + i - lag] * CODEBOOK[coef];
        }

        throw new NotImplementedException();

        //filterBank.processLTP(info.getWindowSequence(), info.getWindowShape(ICSInfo.CURRENT), info.getWindowShape(ICSInfo.PREVIOUS), inf, outf);

        //if (ics.isTNSDataPresent()) ics.getTNS().process(ics, outf, sf, true);

        var swbOffsets = info.getSWBOffsets();
        int swbOffsetMax = info.getSWBOffsetMax();
        for (int sfb = 0; sfb < lastBand; sfb++)
        {
          if (longUsed[sfb])
          {
            int low = swbOffsets[sfb];
            int high = Math.Min(swbOffsets[sfb + 1], swbOffsetMax);

            for (int bin = low; bin < high; bin++)
            {
              data[bin] += outf[bin];
            }
          }
        }
      }
    }

    public void updateState(float[] time, float[] overlap, Profile profile)
    {
      int i;
      if (profile.type == Profile.ProfileType.AAC_LD)
      {
        for (i = 0; i < frameLength; i++)
        {
          states[i] = states[i + frameLength];
          states[frameLength + i] = states[i + (frameLength * 2)];
          states[(frameLength * 2) + i] = (int)Math.Round(time[i]);
          states[(frameLength * 3) + i] = (int)Math.Round(overlap[i]);
        }
      }
      else
      {
        for (i = 0; i < frameLength; i++)
        {
          states[i] = states[i + frameLength];
          states[frameLength + i] = (int)Math.Round(time[i]);
          states[(frameLength * 2) + i] = (int)Math.Round(overlap[i]);
        }
      }
    }

    public static bool isLTPProfile(Profile profile)
    {
      return profile.type == Profile.ProfileType.AAC_LTP ||
             profile.type == Profile.ProfileType.ER_AAC_LTP ||
             profile.type == Profile.ProfileType.AAC_LD;
    }

    public void copy(LTPrediction ltp)
    {
      Array.Copy(ltp.states, 0, states, 0, states.Length);
      coef = ltp.coef;
      lag = ltp.lag;
      lastBand = ltp.lastBand;
      lagUpdate = ltp.lagUpdate;
      shortUsed = ltp.shortUsed.ToArray();
      shortLagPresent = ltp.shortLagPresent.ToArray();
      shortLag = ltp.shortLag.ToArray();
      longUsed = ltp.longUsed.ToArray();
    }
  }
}
