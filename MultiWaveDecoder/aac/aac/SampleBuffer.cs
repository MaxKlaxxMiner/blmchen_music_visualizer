// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
namespace MultiWaveDecoder
{
  /// <summary>
  /// The SampleBuffer holds the decoded AAC frame. It contains the raw PCM data and its format.
  /// </summary>
  public sealed class SampleBuffer
  {
    int sampleRate, channels, bitsPerSample;
    double length, bitrate, encodedBitrate;
    byte[] data;
    bool bigEndian;

    public SampleBuffer()
    {
      data = new byte[0];
      sampleRate = 0;
      channels = 0;
      bitsPerSample = 0;
      bigEndian = true;
    }

    /// <summary>
    /// Returns the buffer's PCM data.
    /// </summary>
    /// <returns>the audio data</returns>
    public byte[] getData()
    {
      return data;
    }

    /// <summary>
    /// Returns the data's sample rate.
    /// </summary>
    /// <returns>the sample rate</returns>
    public int getSampleRate()
    {
      return sampleRate;
    }

    /// <summary>
    /// Returns the number of channels stored in the data buffer.
    /// </summary>
    /// <returns>the number of channels</returns>
    public int getChannels()
    {
      return channels;
    }

    /// <summary>
    /// Returns the number of bits per sample. Usually this is 16, meaning a sample is stored in two bytes.
    /// </summary>
    /// <returns>the number of bits per sample</returns>
    public int getBitsPerSample()
    {
      return bitsPerSample;
    }

    /// <summary>
    /// Returns the length of the current frame in seconds.
    /// length = samplesPerChannel / sampleRate
    /// </summary>
    /// <returns>the length in seconds</returns>
    public double getLength()
    {
      return length;
    }

    /// <summary>
    /// Returns the bitrate of the decoded PCM data. <code>bitrate = (samplesPerChannel * bitsPerSample) / length</code>
    /// </summary>
    /// <returns>the bitrate</returns>
    public double getBitrate()
    {
      return bitrate;
    }

    /// <summary>
    /// Returns the AAC bitrate of the current frame.
    /// </summary>
    /// <returns>the AAC bitrate</returns>
    public double getEncodedBitrate()
    {
      return encodedBitrate;
    }

    /// <summary>
    /// Indicates the endianness for the data.
    /// </summary>
    /// <returns></returns>
    public bool isBigEndian()
    {
      return bigEndian;
    }

    /// <summary>
    /// Sets the endianness for the data.
    /// </summary>
    /// <param name="bigEndian">if true the data will be in big endian, else in little endian</param>
    public void setBigEndian(bool bigEndian)
    {
      if (bigEndian == this.bigEndian) return;

      for (int i = 0; i < data.Length; i += 2)
      {
        byte tmp = data[i];
        data[i] = data[i + 1];
        data[i + 1] = tmp;
      }

      this.bigEndian = bigEndian;
    }

    public void setData(byte[] data, int sampleRate, int channels, int bitsPerSample, int bitsRead)
    {
      this.data = data;
      this.sampleRate = sampleRate;
      this.channels = channels;
      this.bitsPerSample = bitsPerSample;

      if (sampleRate == 0)
      {
        length = 0;
        bitrate = 0;
        encodedBitrate = 0;
      }
      else
      {
        int bytesPerSample = bitsPerSample / 8; //usually 2
        int samplesPerChannel = data.Length / (bytesPerSample * channels); //=1024
        length = samplesPerChannel / (double)sampleRate;
        bitrate = samplesPerChannel * bitsPerSample * channels / length;
        encodedBitrate = bitsRead / length;
      }
    }
  }
}
