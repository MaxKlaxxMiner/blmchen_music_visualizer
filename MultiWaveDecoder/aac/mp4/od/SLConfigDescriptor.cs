using System;
// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public sealed class SLConfigDescriptor : Descriptor
  {
    bool useAccessUnitStart, useAccessUnitEnd, useRandomAccessPoint, usePadding, useTimeStamp, useWallClockTimeStamp, useIdle, duration;
    long timeStampResolution, ocrResolution;
    int timeStampLength, ocrLength, instantBitrateLength, degradationPriorityLength, seqNumberLength;
    long timeScale;
    int accessUnitDuration, compositionUnitDuration;
    long wallClockTimeStamp, startDecodingTimeStamp, startCompositionTimeStamp;
    bool ocrStream;
    int ocrES_ID;

    public override void decode(MP4InputStream inStream)
    {
      bool predefined = inStream.read() == 1;

      int minSize = predefined ? 16 : 2;

      if (size < minSize) return; // JFIX - impossible to read (Encoder may not have considered ISO 14496-1 - 10.2.3 ?)

      int tmp;
      if (!predefined)
      {
        // --- flags ---
        tmp = inStream.read();
        useAccessUnitStart = ((tmp >> 7) & 1) == 1;
        useAccessUnitEnd = ((tmp >> 6) & 1) == 1;
        useRandomAccessPoint = ((tmp >> 5) & 1) == 1;
        usePadding = ((tmp >> 4) & 1) == 1;
        useTimeStamp = ((tmp >> 3) & 1) == 1;
        useWallClockTimeStamp = ((tmp >> 2) & 1) == 1;
        useIdle = ((tmp >> 1) & 1) == 1;
        duration = (tmp & 1) == 1;

        timeStampResolution = inStream.readBytes(4);
        ocrResolution = inStream.readBytes(4);
        timeStampLength = inStream.read();
        ocrLength = inStream.read();
        instantBitrateLength = inStream.read();
        tmp = inStream.read();
        degradationPriorityLength = (tmp >> 4) & 15;
        seqNumberLength = tmp & 15;

        if (duration)
        {
          timeScale = inStream.readBytes(4);
          accessUnitDuration = (int)inStream.readBytes(2);
          compositionUnitDuration = (int)inStream.readBytes(2);
        }

        if (!useTimeStamp)
        {
          if (useWallClockTimeStamp) wallClockTimeStamp = inStream.readBytes(4);
          tmp = (int)Math.Ceiling(2 * timeStampLength / 8.0);
          long tmp2 = inStream.readBytes(tmp);
          long mask = ((1 << timeStampLength) - 1);
          startDecodingTimeStamp = (tmp2 >> timeStampLength) & mask;
          startCompositionTimeStamp = tmp2 & mask;
        }

      }

      tmp = inStream.read();
      ocrStream = ((tmp >> 7) & 1) == 1;
      if (ocrStream) ocrES_ID = (int)inStream.readBytes(2);
    }
  }
}
