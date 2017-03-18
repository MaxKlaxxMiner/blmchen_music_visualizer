// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
namespace MultiWaveDecoder
{
  /// <summary>
  /// All possible channel configurations for AAC.
  /// </summary>
  public enum ChannelConfiguration
  {
    /// <summary>
    /// invalid
    /// </summary>
    CHANNEL_CONFIG_UNSUPPORTED = -1,
    /// <summary>
    /// No channel
    /// </summary>
    CHANNEL_CONFIG_NONE = 0,
    /// <summary>
    /// Mono
    /// </summary>
    CHANNEL_CONFIG_MONO = 1,
    /// <summary>
    /// Stereo
    /// </summary>
    CHANNEL_CONFIG_STEREO = 2,
    /// <summary>
    /// Stereo + Center
    /// </summary>
    CHANNEL_CONFIG_STEREO_PLUS_CENTER = 3,
    /// <summary>
    /// Stereo + Center + Rear
    /// </summary>
    CHANNEL_CONFIG_STEREO_PLUS_CENTER_PLUS_REAR_MONO = 4,
    /// <summary>
    /// Five channels
    /// </summary>
    CHANNEL_CONFIG_FIVE = 5,
    /// <summary>
    /// Five channels + LF
    /// </summary>
    CHANNEL_CONFIG_FIVE_PLUS_ONE = 6,
    /// <summary>
    /// Seven channels + LF
    /// </summary>
    CHANNEL_CONFIG_SEVEN_PLUS_ONE = 8
  }
}
