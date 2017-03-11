// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  /// <summary>
  /// This box specifies the characteristics of a single track. Exactly one Track Header Box is contained in a track. In the absence of an edit list, the
  /// presentation of a track starts at the beginning of the overall presentation. An empty edit is used to offset the start time of a track.
  /// If in a presentation all tracks have neither trackInMovie nor trackInPreview set, then all tracks shall be treated as if both flags were set on all
  /// tracks. Hint tracks should not have the track header flags set, so that they are ignored for local playback and preview.
  /// The width and height in the track header are measured on a notional 'square' (uniform) grid. Track video data is normalized to these dimensions
  /// (logically) before any transformation or placement caused by a layup or composition system. Track (and movie) matrices, if used, also operate in this uniformly-scaled space.
  /// </summary>
  public sealed class TrackHeaderBox : FullBox
  {
    bool enabled, inMovie, inPreview;
    long creationTime, modificationTime, duration;
    int trackID, layer, alternateGroup;
    double volume, width, height;
    readonly double[] matrix = new double[9];

    public TrackHeaderBox() : base("Track Header Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      enabled = (flags & 1) == 1;
      inMovie = (flags & 2) == 2;
      inPreview = (flags & 4) == 4;

      int len = (version == 1) ? 8 : 4;
      creationTime = inStream.readBytes(len);
      modificationTime = inStream.readBytes(len);
      trackID = (int)inStream.readBytes(4);
      inStream.skipBytes(4); //reserved
      duration = BoxUtils.detectUndetermined(inStream.readBytes(len));

      inStream.skipBytes(8); //reserved

      layer = (int)inStream.readBytes(2);
      alternateGroup = (int)inStream.readBytes(2);
      volume = inStream.readFixedPoint(8, 8);

      inStream.skipBytes(2); //reserved

      for (int i = 0; i < 9; i++)
      {
        if (i < 6) matrix[i] = inStream.readFixedPoint(16, 16);
        else matrix[i] = inStream.readFixedPoint(2, 30);
      }

      width = inStream.readFixedPoint(16, 16);
      height = inStream.readFixedPoint(16, 16);
    }

    /// <summary>
    /// A flag indicating that the track is enabled. A disabled track is treated as if it were not present.
    /// </summary>
    /// <returns>true if the track is enabled</returns>
    public bool isTrackEnabled()
    {
      return enabled;
    }

    /// <summary>
    /// A flag indicating that the track is used in the presentation.
    /// </summary>
    /// <returns>true if the track is used</returns>
    public bool isTrackInMovie()
    {
      return inMovie;
    }

    /// <summary>
    /// A flag indicating that the track is used when previewing the presentation.
    /// </summary>
    /// <returns>true if the track is used in previews</returns>
    public bool isTrackInPreview()
    {
      return inPreview;
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
    /// <returns></returns>
    public long getModificationTime()
    {
      return modificationTime;
    }

    /// <summary>
    /// The track ID is an integer that uniquely identifies this track over the entire life-time of this presentation. Track IDs are never re-used and cannot be zero.
    /// </summary>
    /// <returns>the track's ID</returns>
    public int getTrackID()
    {
      return trackID;
    }

    /// <summary>
    /// The duration is an integer that indicates the duration of this track (in the timescale indicated in the Movie Header Box). The value of this field
    /// is equal to the sum of the durations of all of the track's edits. If there is no edit list, then the duration is the sum of the sample 
    /// durations, converted into the timescale in the Movie Header Box. If the duration of this track cannot be determined then this value is -1.
    /// </summary>
    /// <returns>the duration this track</returns>
    public long getDuration()
    {
      return duration;
    }

    /// <summary>
    /// The layer specifies the front-to-back ordering of video tracks; tracks with lower numbers are closer to the viewer. 0 is the normal value, and -1 would be in front of track 0, and so on.
    /// </summary>
    /// <returns>the layer</returns>
    public int getLayer()
    {
      return layer;
    }

    /// <summary>
    /// The alternate group is an integer that specifies a group or collection of tracks. If this field is 0 there is no information on possible
    /// relations to other tracks. If this field is not 0, it should be the same for tracks that contain alternate data for one another and different for
    /// tracks belonging to different such groups. Only one track within an alternate group should be played or streamed at any one time, and must be
    /// distinguishable from other tracks in the group via attributes such as bitrate, codec, language, packet size etc. A group may have only one member.
    /// </summary>
    /// <returns>the alternate group</returns>
    public int getAlternateGroup()
    {
      return alternateGroup;
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
    /// The width specifies the track's visual presentation width as a floating
    /// point values. This needs not be the same as the pixel width of the
    /// images, which is documented in the sample description(s); all images in
    /// the sequence are scaled to this width, before any overall transformation
    /// of the track represented by the matrix. The pixel dimensions of the
    /// images are the default values. 
    /// </summary>
    /// <returns>the image width</returns>
    public double getWidth()
    {
      return width;
    }

    /// <summary>
    /// The height specifies the track's visual presentation height as a floating point value. This needs not be the same as the pixel height of the
    /// images, which is documented in the sample description(s); all images in the sequence are scaled to this height, before any overall transformation
    /// of the track represented by the matrix. The pixel dimensions of the images are the default values.
    /// </summary>
    /// <returns>the image height</returns>
    public double getHeight()
    {
      return height;
    }

    public double[] getMatrix()
    {
      return matrix;
    }
  }
}
