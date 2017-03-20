using System;
// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public sealed class AACException : Exception
  {
    public AACException(string message) : base(message) { }
    public AACException(Exception e) : base("", e) { }
  }
}
