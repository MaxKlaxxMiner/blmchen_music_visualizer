namespace MultiWaveDecoder
{
  /// <summary>
  /// This class is used for all boxes, that are known but don't contain necessary data and can be skipped. This is mainly used for 'skip', 'free' and 'wide'.
  /// </summary>
  public sealed class FreeSpaceBox : BoxImpl
  {
    public FreeSpaceBox(): base("Free Space Box"){}
  }
}
