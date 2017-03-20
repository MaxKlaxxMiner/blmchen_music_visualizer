using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  /// <summary>
  /// Command line example, that can decode an AAC file to a WAVE file.
  /// @author in-somnia
  /// 
  /// Info: Source-Fixes in JAAD-lib named "JFIX"
  /// </summary>
  public static class Main
  {
    const string USAGE = "usage:\ndecoder.exe [-mp4] <infile> <outfile>\n\n\t-mp4\tinput file is in MP4 container format";

    static void printUsage()
    {
      Console.WriteLine(USAGE);
      Environment.Exit(1);
    }

    private static void decodeMP4(string inFile, string outFile)
    {
      using (var inFileStream = File.OpenRead(inFile))
      {
        var cont = new MP4Container(inFileStream);

        var movie = cont.getMovie();

        // --- read cover-picture if exists ---
        if (movie.containsMetaData())
        {
          var meta = movie.getMetaData();
          if (meta.contains(MetaData.Fields.COVER_ARTWORKS))
          {
            coverPicture = meta.get(MetaData.Fields.COVER_ARTWORKS).First().getImage();
          }
        }


        var tracks = movie.getTracks(AudioTrack.AudioCodec.CodecType.AAC);
        if (tracks.Length == 0) throw new Exception("movie does not contain any AAC track");
        var track = (AudioTrack)tracks[0];

        using (var wav = new WaveFileWriter(File.Create(outFile), track.getSampleRate(), track.getChannelCount(), track.getSampleSize()))
        {
          var dec = new Decoder(track.getDecoderSpecificInfo());

          var buf = new SampleBuffer();
          //while (track.hasMoreFrames())
          //{
          //  var frame = track.readNextFrame();
          //  dec.decodeFrame(frame.getData(), buf);
          //  wav.write(buf.getData());
          //}
        }
      }
    }

    private static void decodeAAC(string inFile, string outFile)
    {
      throw new NotImplementedException();
      //WaveFileWriter wav = null;
      //try
      //{
      //  ADTSDemultiplexer adts = new ADTSDemultiplexer(new FileInputStream(inFile));
      //  Decoder dec = new Decoder(adts.getDecoderSpecificInfo());

      //  SampleBuffer buf = new SampleBuffer();
      //  byte[] b;
      //  while (true)
      //  {
      //    b = adts.readNextFrame();
      //    dec.decodeFrame(b, buf);

      //    if (wav == null) wav = new WaveFileWriter(new File(outFile), buf.getSampleRate(), buf.getChannels(), buf.getBitsPerSample());
      //    wav.write(buf.getData());
      //  }
      //}
      //finally
      //{
      //  if (wav != null) wav.close();
      //}
    }

    public static Bitmap coverPicture = null;

    public static void main(string[] args)
    {
      //try
      {
        if (args.Length == 3 && args[0] == "-mp4")
        {
          decodeMP4(args[1], args[2]);
          return;
        }

        if (args.Length == 2)
        {
          decodeAAC(args[0], args[1]);
          return;
        }

        printUsage();
      }
      //catch (Exception e)
      //{
      //  Console.WriteLine("error while decoding: " + e);
      //  Environment.Exit(1);
      //}
    }
  }
}
