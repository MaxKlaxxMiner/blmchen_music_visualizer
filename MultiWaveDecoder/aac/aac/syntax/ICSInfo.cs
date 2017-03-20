﻿// ReSharper disable InconsistentNaming

using System;
// ReSharper disable MemberCanBePrivate.Global

namespace MultiWaveDecoder
{
  public sealed class ICSInfo : Constants
  {
    //public class ICSInfo implements Constants, ScaleFactorBands {

    public static int WINDOW_SHAPE_SINE = 0;
    public static int WINDOW_SHAPE_KAISER = 1;
    public static int PREVIOUS = 0;
    public static int CURRENT = 1;

    public enum WindowSequence
    {
      ONLY_LONG_SEQUENCE = 0,
      LONG_START_SEQUENCE = 1,
      EIGHT_SHORT_SEQUENCE = 2,
      LONG_STOP_SEQUENCE = 3
    }

    int frameLength;
    WindowSequence windowSequence;
    int[] windowShape;
    int maxSFB;

    // prediction
    bool predictionDataPresent;
    //ICPrediction icPredict;
    bool ltpData1Present, ltpData2Present;
    //LTPrediction ltPredict1, ltPredict2;

    // windows/sfbs
    int windowCount;
    int windowGroupCount;
    int[] windowGroupLength;
    int swbCount;
    int[] swbOffsets;

    public ICSInfo(int frameLength)
    {
      this.frameLength = frameLength;
      windowShape = new int[2];
      windowSequence = WindowSequence.ONLY_LONG_SEQUENCE;
      windowGroupLength = new int[MAX_WINDOW_GROUP_COUNT];
      ltpData1Present = false;
      ltpData2Present = false;
    }

    // --- ========== decoding ========== ---
    public void decode(BitStream inStream, DecoderConfig conf, bool commonWindow)
    {
      var sf = conf.getSampleFrequency();
      if (sf.getIndex() == -1) throw new AACException("invalid sample frequency");

      inStream.skipBit(); //reserved
      windowSequence = (WindowSequence)inStream.readBits(2);
      windowShape[PREVIOUS] = windowShape[CURRENT];
      windowShape[CURRENT] = inStream.readBit();

      windowGroupCount = 1;
      windowGroupLength[0] = 1;
      if (windowSequence == WindowSequence.EIGHT_SHORT_SEQUENCE)
      {
        maxSFB = inStream.readBits(4);
        int i;
        for (i = 0; i < 7; i++)
        {
          if (inStream.readBool()) windowGroupLength[windowGroupCount - 1]++;
          else
          {
            windowGroupCount++;
            windowGroupLength[windowGroupCount - 1] = 1;
          }
        }
        windowCount = 8;
        throw new NotImplementedException();
        //swbOffsets = SWB_OFFSET_SHORT_WINDOW[sf.getIndex()];
        //swbCount = SWB_SHORT_WINDOW_COUNT[sf.getIndex()];
        predictionDataPresent = false;
      }
      else
      {
        maxSFB = inStream.readBits(6);
        windowCount = 1;
        throw new NotImplementedException();
        //swbOffsets = SWB_OFFSET_LONG_WINDOW[sf.getIndex()];
        //swbCount = SWB_LONG_WINDOW_COUNT[sf.getIndex()];
        //predictionDataPresent = inStream.readBool();
        //if (predictionDataPresent) readPredictionData(inStream, conf.getProfile(), sf, commonWindow);
      }
    }

    //  private void readPredictionData(BitStream in, Profile profile, SampleFrequency sf, bool commonWindow) throws AACException {
    //    switch(profile) {
    //      case AAC_MAIN:
    //        if(icPredict==null) icPredict = new ICPrediction();
    //        icPredict.decode(in, maxSFB, sf);
    //        break;
    //      case AAC_LTP:
    //        if(ltpData1Present = in.readBool()) {
    //          if(ltPredict1==null) ltPredict1 = new LTPrediction(frameLength);
    //          ltPredict1.decode(in, this, profile);
    //        }
    //        if(commonWindow) {
    //          if(ltpData2Present = in.readBool()) {
    //            if(ltPredict2==null) ltPredict2 = new LTPrediction(frameLength);
    //            ltPredict2.decode(in, this, profile);
    //          }
    //        }
    //        break;
    //      case ER_AAC_LTP:
    //        if(!commonWindow) {
    //          if(ltpData1Present = in.readBool()) {
    //            if(ltPredict1==null) ltPredict1 = new LTPrediction(frameLength);
    //            ltPredict1.decode(in, this, profile);
    //          }
    //        }
    //        break;
    //      default:
    //        throw new AACException("unexpected profile for LTP: "+profile);
    //    }
    //  }

    //  /* =========== gets ============ */
    //  public int getMaxSFB() {
    //    return maxSFB;
    //  }

    //  public int getSWBCount() {
    //    return swbCount;
    //  }

    //  public int[] getSWBOffsets() {
    //    return swbOffsets;
    //  }

    //  public int getSWBOffsetMax() {
    //    return swbOffsets[swbCount];
    //  }

    //  public int getWindowCount() {
    //    return windowCount;
    //  }

    //  public int getWindowGroupCount() {
    //    return windowGroupCount;
    //  }

    //  public int getWindowGroupLength(int g) {
    //    return windowGroupLength[g];
    //  }

    //  public WindowSequence getWindowSequence() {
    //    return windowSequence;
    //  }

    //  public bool isEightShortFrame() {
    //    return windowSequence.equals(WindowSequence.EIGHT_SHORT_SEQUENCE);
    //  }

    //  public int getWindowShape(int index) {
    //    return windowShape[index];
    //  }

    //  public bool isICPredictionPresent() {
    //    return predictionDataPresent;
    //  }

    //  public ICPrediction getICPrediction() {
    //    return icPredict;
    //  }

    //  public bool isLTPrediction1Present() {
    //    return ltpData1Present;
    //  }

    //  public LTPrediction getLTPrediction1() {
    //    return ltPredict1;
    //  }

    //  public bool isLTPrediction2Present() {
    //    return ltpData2Present;
    //  }

    //  public LTPrediction getLTPrediction2() {
    //    return ltPredict2;
    //  }

    //  public void unsetPredictionSFB(int sfb) {
    //    if(predictionDataPresent) icPredict.setPredictionUnused(sfb);
    //    if(ltpData1Present) ltPredict1.setPredictionUnused(sfb);
    //    if(ltpData2Present) ltPredict2.setPredictionUnused(sfb);
    //  }

    //  public void setData(ICSInfo info) {
    //    windowSequence = WindowSequence.valueOf(info.windowSequence.name());
    //    windowShape[PREVIOUS] = windowShape[CURRENT];
    //    windowShape[CURRENT] = info.windowShape[CURRENT];
    //    maxSFB = info.maxSFB;
    //    predictionDataPresent = info.predictionDataPresent;
    //    if(predictionDataPresent) icPredict = info.icPredict;
    //    ltpData1Present = info.ltpData1Present;
    //    if(ltpData1Present) {
    //      ltPredict1.copy(info.ltPredict1);
    //      ltPredict2.copy(info.ltPredict2);
    //    }
    //    windowCount = info.windowCount;
    //    windowGroupCount = info.windowGroupCount;
    //    windowGroupLength = Arrays.copyOf(info.windowGroupLength, info.windowGroupLength.Length);
    //    swbCount = info.swbCount;
    //    swbOffsets = Arrays.copyOf(info.swbOffsets, info.swbOffsets.Length);
    //  }
    //}
  }
}
