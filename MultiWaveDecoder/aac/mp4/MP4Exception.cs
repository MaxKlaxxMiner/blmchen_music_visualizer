using System;

// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public sealed class MP4Exception : Exception
  {
    public MP4Exception(string message): base (message){}
  }
}
