using System;
using System.IO;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming

public sealed class WaveFileWriter : IDisposable
{

  private static int HEADER_LENGTH = 44;
  private static int RIFF = 0x46464952 ; //'RIFF'
  private static long WAVE_FMT = 0x20746d6645564157; //'WAVEfmt '
  private static int DATA = 0x61746164; //'data'
  private static int BYTE_MASK = 0xFF;
  private Stream outStream;
  private int sampleRate;
  private int channels;
  private int bitsPerSample;
  private int bytesWritten;

  public WaveFileWriter(Stream output, int sampleRate, int channels, int bitsPerSample)
  {
    this.sampleRate = sampleRate;
    this.channels = channels;
    this.bitsPerSample = bitsPerSample;
    bytesWritten = 0;

    outStream = output;
    write(new byte[HEADER_LENGTH]); //space for the header
  }

  public void write(byte[] data)
  {
    write(data, 0, data.Length);
  }

  public void write(byte[] data, int off, int len)
  {
    // convert to little endian
    for (int i = off; i < off + data.Length; i += 2)
    {
      byte tmp = data[i + 1];
      data[i + 1] = data[i];
      data[i] = tmp;
    }
    outStream.Write(data, off, len);
    bytesWritten += data.Length;
  }

  public void write(short[] data)
  {
    write(data, 0, data.Length);
  }

  public void write(short[] data, int off, int len)
  {
    for (int i = off; i < off + data.Length; i++)
    {
      outStream.WriteByte((byte)(data[i] & BYTE_MASK));
      outStream.WriteByte((byte)((data[i] >> 8) & BYTE_MASK));
      bytesWritten += 2;
    }
  }

  /// <summary>
  /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
  /// </summary>
  public void Dispose()
  {
		writeWaveHeader();
		outStream.Dispose();
  }

  void writeShort(ushort val)
  {
    outStream.WriteByte((byte)(val >> 0));
    outStream.WriteByte((byte)(val >> 8));
  }

  void writeShort(short val)
  {
    writeShort((ushort)val);
  }

  void writeInt(uint val)
  {
    outStream.WriteByte((byte)(val >> 0));
    outStream.WriteByte((byte)(val >> 8));
    outStream.WriteByte((byte)(val >> 16));
    outStream.WriteByte((byte)(val >> 24));
  }

  void writeInt(int val)
  {
    writeInt((uint)val);
  }

  void writeLong(ulong val)
  {
    outStream.WriteByte((byte)(val >> 0));
    outStream.WriteByte((byte)(val >> 8));
    outStream.WriteByte((byte)(val >> 16));
    outStream.WriteByte((byte)(val >> 24));
    outStream.WriteByte((byte)(val >> 32));
    outStream.WriteByte((byte)(val >> 40));
    outStream.WriteByte((byte)(val >> 48));
    outStream.WriteByte((byte)(val >> 56));
  }

  void writeLong(long val)
  {
    writeLong((ulong)val);
  }

  private void writeWaveHeader()
  {
    outStream.Seek(0, SeekOrigin.Begin);
    int bytesPerSec = (bitsPerSample + 7) / 8;

    writeInt(RIFF); //wave label
    writeInt(bytesWritten + 36); //length in bytes without header
    writeLong(WAVE_FMT);
    writeInt(16); //length of pcm format declaration area
    writeShort(1); //is PCM
    writeShort((short)channels); //number of channels
    writeInt(sampleRate); //sample rate
    writeInt(sampleRate * channels * bytesPerSec); //bytes per second
    writeShort((short)(channels * bytesPerSec)); //bytes per sample time
    writeShort((short)bitsPerSample); //bits per sample
    writeInt(DATA); //data section label
    writeInt(bytesWritten); //length of raw pcm data in bytes
  }

}
