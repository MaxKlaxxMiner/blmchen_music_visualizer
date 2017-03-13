namespace MultiWaveDecoder
{
  /// <summary>
  /// Box implementation that is used for unknown types.
  /// </summary>
  public sealed class UnknownBox : BoxImpl
  {
    public UnknownBox() : base("unknown") { }
  }
}
