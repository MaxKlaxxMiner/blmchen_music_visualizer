using System;
using System.IO;
// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public sealed class WaveFileWriter : IDisposable
  {
    private const int HEADER_LENGTH = 44;
    private const int RIFF = 1380533830; //'RIFF'
    private const long WAVE_FMT = 6287401410857104416L; //'WAVEfmt '
    private const int DATA = 1684108385; //'data'
    private const int BYTE_MASK = 0xFF;
    readonly Stream outStream;
    int sampleRate;
    int channels;
    int bitsPerSample;
    int bytesWritten = 0;

    public WaveFileWriter(Stream outputStream, int sampleRate, int channels, int bitsPerSample)
    {
      if (!outputStream.CanSeek) throw new ArgumentException("!outputStream.CanSeek");
      if (!outputStream.CanWrite) throw new ArgumentException("!outputStream.CanWrite");

      outStream = outputStream;
      this.sampleRate = sampleRate;
      this.channels = channels;
      this.bitsPerSample = bitsPerSample;

      Write(new byte[HEADER_LENGTH]); //space for the header
    }

    void Write(byte[] buf)
    {
      outStream.Write(buf, 0, buf.Length);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      outStream.Dispose();
    }
  }
}
