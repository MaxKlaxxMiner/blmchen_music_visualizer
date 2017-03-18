using System.Linq;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace MultiWaveDecoder
{
  /// <summary>
  /// An enumeration that represents all possible sample frequencies AAC data can have.
  /// </summary>
  public sealed class SampleFrequency
  {
    static readonly SampleFrequency[] values =
    {
      new SampleFrequency(0, 96000, new[]{33, 512}, new[]{31, 9}),
      new SampleFrequency(1, 88200, new[]{33, 512}, new[]{31, 9}),
      new SampleFrequency(2, 64000, new[]{38, 664}, new[]{34, 10}),
      new SampleFrequency(3, 48000, new[]{40, 672}, new[]{40, 14}),
      new SampleFrequency(4, 44100, new[]{40, 672}, new[]{42, 14}),
      new SampleFrequency(5, 32000, new[]{40, 672}, new[]{51, 14}),
      new SampleFrequency(6, 24000, new[]{41, 652}, new[]{46, 14}),
      new SampleFrequency(7, 22050, new[]{41, 652}, new[]{46, 14}),
      new SampleFrequency(8, 16000, new[]{37, 664}, new[]{42, 14}),
      new SampleFrequency(9, 12000, new[]{37, 664}, new[]{42, 14}),
      new SampleFrequency(10, 11025, new[]{37, 664}, new[]{42, 14}),
      new SampleFrequency(11, 8000, new[]{34, 664}, new[]{39, 14}),
      new SampleFrequency(-1, 0, new[]{0, 0}, new[]{0, 0})
    };

    /// <summary>
    /// Returns a sample frequency instance for the given index. If the index is not between 0 and 11 inclusive, SAMPLE_FREQUENCY_NONE is returned.
    /// </summary>
    /// <param name="i"></param>
    /// <returns>a sample frequency with the given index</returns>
    public static SampleFrequency forInt(int i)
    {
      return i >= 0 && i < values.Length ? values[i] : values.Last();
    }

    public static SampleFrequency forFrequency(int i)
    {
      return values.FirstOrDefault(x => x.frequency == i) ?? values.Last();
    }

    readonly int index;
    readonly int frequency;
    readonly int[] prediction;
    readonly int[] maxTNS_SFB;

    SampleFrequency(int index, int frequency, int[] prediction, int[] maxTNS_SFB)
    {
      this.index = index;
      this.frequency = frequency;
      this.prediction = prediction;
      this.maxTNS_SFB = maxTNS_SFB;
    }

    /// <summary>
    /// Returns this sample frequency's index between 0 (96000) and 11 (8000) or -1 if this is SAMPLE_FREQUENCY_NONE.
    /// </summary>
    /// <returns>the sample frequency's index</returns>
    public int getIndex()
    {
      return index;
    }

    /// <summary>
    /// Returns the sample frequency as integer value. This may be a value between 96000 and 8000, or 0 if this is SAMPLE_FREQUENCY_NONE.
    /// </summary>
    /// <returns>the sample frequency</returns>
    public int getFrequency()
    {
      return frequency;
    }

    /// <summary>
    /// Returns the highest scale factor band allowed for ICPrediction at this sample frequency.
    /// 
    /// This method is mainly used internally.
    /// </summary>
    /// <returns>the highest prediction SFB</returns>
    public int getMaximalPredictionSFB()
    {
      return prediction[0];
    }

    /// <summary>
    /// Returns the number of predictors allowed for ICPrediction at this sample frequency.
    /// 
    /// This method is mainly used internally.
    /// </summary>
    /// <returns>the number of ICPredictors</returns>
    public int getPredictorCount()
    {
      return prediction[1];
    }

    /// <summary>
    /// Returns the highest scale factor band allowed for TNS at this sample frequency.
    /// 
    /// This method is mainly used internally.
    /// </summary>
    /// <param name="shortWindow"></param>
    /// <returns>the highest SFB for TNS</returns>
    public int getMaximalTNS_SFB(bool shortWindow)
    {
      return maxTNS_SFB[shortWindow ? 1 : 0];
    }

    public override string ToString()
    {
      return (new { frequency, index, prediction0 = prediction[0], prediction1 = prediction[1], maxTNS_SFB0 = maxTNS_SFB[0], maxTNS_SFB1 = maxTNS_SFB[1] }).ToString();
    }
  }
}
