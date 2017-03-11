// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  /// <summary>
  /// The movie header box defines overall information which is media-independent, and relevant to the entire presentation considered as a whole.
  /// </summary>
  public class MovieHeaderBox : FullBox
  {
    long creationTime, modificationTime, timeScale, duration;
    double rate, volume;
    double[] matrix = new double[9];
    long nextTrackID;

    public MovieHeaderBox() : base("Movie Header Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      int len = (version == 1) ? 8 : 4;
      creationTime = inStream.readBytes(len);
      modificationTime = inStream.readBytes(len);
      timeScale = inStream.readBytes(4);
      duration = BoxUtils.detectUndetermined(inStream.readBytes(len));

      rate = inStream.readFixedPoint(16, 16);
      volume = inStream.readFixedPoint(8, 8);

      inStream.skipBytes(10); //reserved

      for (int i = 0; i < 9; i++)
      {
        if (i < 6) matrix[i] = inStream.readFixedPoint(16, 16);
        else matrix[i] = inStream.readFixedPoint(2, 30);
      }

      inStream.skipBytes(24); //reserved

      nextTrackID = inStream.readBytes(4);
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
    /// The modification time is an integer that declares the most recent time the presentation was modified in seconds since midnight, Jan. 1, 1904, in UTC time.
    /// </summary>
    /// <returns>the modification time</returns>
    public long getModificationTime()
    {
      return modificationTime;
    }

    /// <summary>
    /// The time-scale is an integer that specifies the time-scale for the entire presentation; this is the number of time units that pass in one second.
    /// For example, a time coordinate system that measures time in sixtieths of a second has a time scale of 60.
    /// </summary>
    /// <returns>the time-scale</returns>
    public long getTimeScale()
    {
      return timeScale;
    }

    /// <summary>
    /// The duration is an integer that declares length of the presentation (in the indicated timescale). This property is derived from the presentation's tracks:
    /// the value of this field corresponds to the duration of the longest track in the presentation. If the duration cannot be determined then duration is set to -1.
    /// </summary>
    /// <returns>the duration of the longest track</returns>
    public long getDuration()
    {
      return duration;
    }

    /// <summary>
    /// The rate is a floting point number that indicates the preferred rate to play the presentation; 1.0 is normal forward playback
    /// </summary>
    /// <returns>the playback rate</returns>
    public double getRate()
    {
      return rate;
    }

    /// <summary>
    /// The volume is a floating point number that indicates the preferred playback volume: 0.0 is mute, 1.0 is normal volume.
    /// </summary>
    /// <returns>the volume</returns>
    public double getVolume()
    {
      return volume;
    }

    /// <summary>
    /// Provides a transformation matrix for the video:
    /// [A,B,U,C,D,V,X,Y,W]
    /// A: width scale
    /// B: width rotate
    /// U: width angle
    /// C: height rotate
    /// D: height scale
    /// V: height angle
    /// X: position from left
    /// Y: position from top
    /// W: divider scale (restricted to 1.0)
    ///
    /// The normal values for scale are 1.0 and for rotate 0.0.
    /// The angles are restricted to 0.0.
    /// </summary>
    /// <returns>the transformation matrix for the video</returns>
    public double[] getTransformationMatrix()
    {
      return matrix;
    }

    /// <summary>
    /// The next-track-ID is a non-zero integer that indicates a value to use for the track ID of the next track to be added to this presentation. Zero
    /// is not a valid track ID value. The value shall be larger than the largest track-ID in use. If this value is equal to all 1s (32-bit), and a new
    /// media track is to be added, then a search must be made in the file for an unused track identifier.
    /// </summary>
    /// <returns>the ID for the next track</returns>
    public long getNextTrackID()
    {
      return nextTrackID;
    }
  }
}
