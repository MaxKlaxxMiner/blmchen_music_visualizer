
#region # using *.*

using System;

// ReSharper disable MemberCanBePrivate.Global

#endregion

namespace MultiWaveDecoder
{
  public sealed class AdtsDemuxer
  {
    readonly BitstreamReader bitstream;

    public AdtsDemuxer(DirectStreamReader stream)
    {
      if (!Probe(stream)) throw new Exception("no ADTS-Stream");

      bitstream = new BitstreamReader(stream);
    }

    public static bool Probe(DirectStreamReader stream)
    {
      int offset = stream.offset;

      // attempt to find ADTS syncword
      while (stream.Available(2))
      {
        if ((stream.ReadInt(2) & 0xfff6) == 0xfff0)
        {
          stream.Seek(stream.offset - 2);
          return true;
        }
      }

      stream.Seek(offset);
      return false;
    }

    public class AdtsHeader
    {
      public int profile;
      public int samplingIndex;
      public int chanConfig;
      public int frameLength;
      public int numFrames;
    }

    /// <summary>
    /// Reads an ADTS header
    /// See http://wiki.multimedia.cx/index.php?title=ADTS
    /// </summary>
    /// <returns></returns>
    public AdtsHeader ReadHeader()
    {
      var stream = bitstream;

      if (stream.Read(12) != 0xfff) throw new Exception("Invalid ADTS header.");

      var ret = new AdtsHeader();
      stream.Advance(3); // mpeg version and layer
      var protectionAbsent = stream.Read(1) == 1;

      ret.profile = (int)stream.Read(2) + 1;
      ret.samplingIndex = (int)stream.Read(4);

      stream.Advance(1); // private
      ret.chanConfig = (int)stream.Read(3);
      stream.Advance(4); // original/copy, home, copywrite, and copywrite start

      ret.frameLength = (int)stream.Read(13);
      stream.Advance(11); // fullness

      ret.numFrames = (int)stream.Read(2) + 1;

      if (!protectionAbsent) stream.Advance(16);

      return ret;
    }

    //  this.prototype.readChunk = function ()
    //  {
    //    if (!this.sentHeader)
    //    {
    //      var offset = this.stream.offset;
    //      var header = ADTSDemuxer.readHeader(this.bitstream);

    //      this.emit("format", {
    //        formatID: "aac ",
    //        sampleRate: tables.SAMPLE_RATES[header.samplingIndex],
    //        channelsPerFrame: header.chanConfig,
    //        bitsPerChannel: 16
    //      });

    //      // generate a magic cookie from the ADTS header
    //      var cookie = new Uint8Array(2);
    //      cookie[0] = (header.profile << 3) | ((header.samplingIndex >> 1) & 7);
    //      cookie[1] = ((header.samplingIndex & 1) << 7) | (header.chanConfig << 3);
    //      this.emit("cookie", new AV.Buffer(cookie));

    //      this.stream.seek(offset);
    //      this.sentHeader = true;
    //    }

    //    while (this.stream.available(1))
    //    {
    //      var buffer = this.stream.readSingleBuffer(this.stream.remainingBytes());
    //      this.emit("data", buffer);
    //    }
    //  };
  }
}
