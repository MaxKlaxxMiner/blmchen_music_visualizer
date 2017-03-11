// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// The media header declares overall information that is media-independent, and relevant to characteristics of the media in a track.
  /// </summary>
  public sealed class MediaHeaderBox : FullBox
  {
    long creationTime, modificationTime, timeScale, duration;
    string language;

    public MediaHeaderBox() : base("Media Header Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      int len = (version == 1) ? 8 : 4;
      creationTime = inStream.readBytes(len);
      modificationTime = inStream.readBytes(len);
      timeScale = inStream.readBytes(4);
      duration = BoxUtils.detectUndetermined(inStream.readBytes(len));

      language = BoxUtils.getLanguageCode(inStream.readBytes(2));

      inStream.skipBytes(2); //pre-defined: 0
    }

    /// <summary>
    /// The creation time is an integer that declares the creation time of the presentation in seconds since midnight, Jan. 1, 1904, in UTC time.
    /// </summary>
    /// <returns>the creation time</returns>
    public long getCreationTime()
    {
      return creationTime;
    }

    /// <summary>
    /// The modification time is an integer that declares the most recent time the presentation was modified in seconds since midnight, Jan. 1, 1904,
    /// </summary>
    /// <returns>in UTC time.</returns>
    public long getModificationTime()
    {
      return modificationTime;
    }

    /// <summary>
    /// The time-scale is an integer that specifies the time-scale for this media; this is the number of time units that pass in one second. 
    /// For example, a time coordinate system that measures time in sixtieths of a second has a time scale of 60.
    /// </summary>
    /// <returns>the time-scale</returns>
    public long getTimeScale()
    {
      return timeScale;
    }

    /// <summary>
    /// The duration is an integer that declares the duration of this media (in the scale of the timescale). If the duration cannot be determined then duration is set to -1.
    /// </summary>
    /// <returns>the duration of this media</returns>
    public long getDuration()
    {
      return duration;
    }

    /// <summary>
    /// Language code for this media as defined in ISO 639-2/T.
    /// </summary>
    /// <returns>the language code</returns>
    public string getLanguage()
    {
      return language;
    }
  }
}
