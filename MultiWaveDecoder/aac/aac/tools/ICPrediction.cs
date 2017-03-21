using System;
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable AssignmentInConditionalExpression
// ReSharper disable UnusedVariable

namespace MultiWaveDecoder
{
  /// <summary>
  /// Intra-channel prediction used in profile Main
  /// </summary>
  public sealed class ICPrediction
  {
    const float SF_SCALE = 1.0f / -1024.0f;
    const float INV_SF_SCALE = 1.0f / SF_SCALE;
    const int MAX_PREDICTORS = 672;
    const float A = 0.953125f; //61.0 / 64
    const float ALPHA = 0.90625f; //29.0 / 32
    bool predictorReset;
    int predictorResetGroup;
    bool[] predictionUsed;
    readonly PredictorState[] states;

    private sealed class PredictorState
    {
      public float cor0;
      public float cor1;
      public float var0;
      public float var1;
      public float r0 = 1.0f;
      public float r1 = 1.0f;
    }

    public ICPrediction()
    {
      states = new PredictorState[MAX_PREDICTORS];
      resetAllPredictors();
    }

    public void decode(BitStream inStream, int maxSFB, SampleFrequency sf)
    {
      int predictorCount = sf.getPredictorCount();

      if (predictorReset = inStream.readBool()) predictorResetGroup = inStream.readBits(5);

      int maxPredSFB = sf.getMaximalPredictionSFB();
      int length = Math.Min(maxSFB, maxPredSFB);
      predictionUsed = new bool[length];
      for (int sfb = 0; sfb < length; sfb++)
      {
        predictionUsed[sfb] = inStream.readBool();
      }
      Logger.LogInfo(string.Format("ICPrediction: maxSFB={0}, maxPredSFB={1}", maxSFB, maxPredSFB));
      /*//if maxSFB<maxPredSFB set remaining to false
      for(int sfb = length; sfb<maxPredSFB; sfb++) {
      predictionUsed[sfb] = false;
      }*/
    }

    public void setPredictionUnused(int sfb)
    {
      predictionUsed[sfb] = false;
    }

    public void process(ICStream ics, float[] data, SampleFrequency sf)
    {
      var info = ics.getInfo();

      if (info.isEightShortFrame()) resetAllPredictors();
      else
      {
        int len = Math.Min(sf.getMaximalPredictionSFB(), info.getMaxSFB());
        var swbOffsets = info.getSWBOffsets();
        for (int sfb = 0; sfb < len; sfb++)
        {
          for (int k = swbOffsets[sfb]; k < swbOffsets[sfb + 1]; k++)
          {
            predict(data, k, predictionUsed[sfb]);
          }
        }
        if (predictorReset) resetPredictorGroup(predictorResetGroup);
      }
    }

    void resetPredictState(int index)
    {
      if (states[index] == null) states[index] = new PredictorState();
      states[index].r0 = 0;
      states[index].r1 = 0;
      states[index].cor0 = 0;
      states[index].cor1 = 0;
      states[index].var0 = 0x3F80;
      states[index].var1 = 0x3F80;
    }

    void resetAllPredictors()
    {
      int i;
      for (i = 0; i < states.Length; i++)
      {
        resetPredictState(i);
      }
    }

    void resetPredictorGroup(int group)
    {
      int i;
      for (i = group - 1; i < states.Length; i += 30)
      {
        resetPredictState(i);
      }
    }

    void predict(float[] data, int off, bool output)
    {
      if (states[off] == null) states[off] = new PredictorState();
      var state = states[off];
      float r0 = state.r0, r1 = state.r1;
      float cor0 = state.cor0, cor1 = state.cor1;
      float var0 = state.var0, var1 = state.var1;

      float k1 = var0 > 1 ? cor0 * even(A / var0) : 0;
      float k2 = var1 > 1 ? cor1 * even(A / var1) : 0;

      float pv = round(k1 * r0 + k2 * r1);
      if (output) data[off] += pv * SF_SCALE;

      float e0 = (data[off] * INV_SF_SCALE);
      float e1 = e0 - k1 * r0;

      state.cor1 = trunc(ALPHA * cor1 + r1 * e1);
      state.var1 = trunc(ALPHA * var1 + 0.5f * (r1 * r1 + e1 * e1));
      state.cor0 = trunc(ALPHA * cor0 + r0 * e0);
      state.var0 = trunc(ALPHA * var0 + 0.5f * (r0 * r0 + e0 * e0));

      state.r1 = trunc(A * (r0 - k1 * e0));
      state.r0 = trunc(A * e0);
    }

    public static unsafe int floatToIntBits(float f)
    {
      return *(int*)&f;
    }

    public static unsafe float intBitsToFloat(int i)
    {
      return *(float*)&i;
    }

    private float round(float pf)
    {
      return intBitsToFloat((floatToIntBits(pf) + 0x00008000) & unchecked((int)0xFFFF0000));
    }

    private float even(float pf)
    {
      int i = floatToIntBits(pf);
      i = (i + 0x00007FFF + (i & 0x00010000 >> 16)) & unchecked((int)0xFFFF0000);
      return intBitsToFloat(i);
    }

    private float trunc(float pf)
    {
      return intBitsToFloat(floatToIntBits(pf) & unchecked((int)0xFFFF0000));
    }
  }
}
