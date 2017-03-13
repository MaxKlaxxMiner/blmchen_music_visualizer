// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// The sound media header contains general presentation information, independent of the coding, for audio media. This header is used for all tracks containing audio.
  /// </summary>
  public sealed class SoundMediaHeaderBox : FullBox
  {
    double balance;

    public SoundMediaHeaderBox(): base ("Sound Media Header Box") {}

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      balance = inStream.readFixedPoint(8, 8);
      inStream.skipBytes(2); //reserved
    }

    /// <summary>
    /// The balance is a floating-point number that places mono audio tracks in a stereo space: 0 is centre (the normal value), full left is -1.0 and full right is 1.0.
    /// </summary>
    /// <returns>the stereo balance for a mono track</returns>
    public double getBalance()
    {
      return balance;
    }
  }
}
