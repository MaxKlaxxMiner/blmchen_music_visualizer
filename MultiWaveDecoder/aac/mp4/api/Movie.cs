using System;
using System.Collections.Generic;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace MultiWaveDecoder
{
  public sealed class Movie
  {
    readonly MP4InputStream inStream;
    readonly MovieHeaderBox mvhd;
    readonly List<Track> tracks = new List<Track>();
    readonly MetaData metaData;
    readonly List<Protection> protections;

    public Movie(IBox moov, MP4InputStream inStream)
    {
      this.inStream = inStream;

      // --- create tracks ---
      mvhd = (MovieHeaderBox)moov.getChild(BoxType.MOVIE_HEADER_BOX);
      foreach (var trackBox in moov.getChildren(BoxType.TRACK_BOX))
      {
        var track = createTrack(trackBox);
        if (track != null) tracks.Add(track);
      }

      // --- read metadata: moov.meta/moov.udta.meta ---
      metaData = new MetaData();
      if (moov.hasChild(BoxType.META_BOX)) metaData.parse(null, moov.getChild(BoxType.META_BOX));
      else if (moov.hasChild(BoxType.USER_DATA_BOX))
      {
        var udta = moov.getChild(BoxType.USER_DATA_BOX);
        if (udta.hasChild(BoxType.META_BOX)) metaData.parse(udta, udta.getChild(BoxType.META_BOX));
      }

      // --- detect DRM ---
      protections = new List<Protection>();
      if (moov.hasChild(BoxType.ITEM_PROTECTION_BOX))
      {
        var ipro = moov.getChild(BoxType.ITEM_PROTECTION_BOX);
        foreach (var sinf in ipro.getChildren(BoxType.PROTECTION_SCHEME_INFORMATION_BOX))
        {
          protections.Add(Protection.parse(sinf));
        }
      }
    }

    // TODO: support hint and meta
    Track createTrack(IBox trak)
    {
      var hdlr = (HandlerBox)trak.getChild(BoxType.MEDIA_BOX).getChild(BoxType.HANDLER_BOX);
      switch ((int)hdlr.getHandlerType())
      {
        case HandlerBox.TYPE_VIDEO: return new VideoTrack(trak, inStream);
        case HandlerBox.TYPE_SOUND: return new AudioTrack(trak, inStream);
        default: return null;
      }
    }

    /// <summary>
    /// Returns an unmodifiable list of all tracks in this movie. The tracks are ordered as they appeare in the file/stream.
    /// </summary>
    /// <returns>the tracks contained by this movie</returns>
    public Track[] getTracks()
    {
      return tracks.ToArray();
    }

    /// <summary>
    /// Returns an unmodifiable list of all tracks in this movie with the specified type. The tracks are ordered as they appeare in the file/stream.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Track[] getTracks(FrameType type)
    {
      var l = new List<Track>();

      foreach (var t in tracks)
      {
        if (t.getType() == type) l.Add(t);
      }

      return l.ToArray();
    }

    /// <summary>
    /// Returns an unmodifiable list of all tracks in this movie whose samples are encoded with the specified codec. The tracks are ordered as they appeare in the file/stream.
    /// </summary>
    /// <param name="codec"></param>
    /// <returns>the tracks contained by this movie with the passed type</returns>
    public Track[] getTracks(Track.Codec codec)
    {
      var l = new List<Track>();

      foreach (var t in tracks)
      {
        if (t.getCodec().code == codec.code && t.getCodec().GetType() == codec.GetType()) l.Add(t);
      }

      return l.ToArray();
    }

    /// <summary>
    /// Indicates if this movie contains metadata. If false the <code>MetaData</code> object returned by <code>getMetaData()</code> will not contain any field.
    /// </summary>
    /// <returns>true if this movie contains any metadata</returns>
    public bool containsMetaData()
    {
      return metaData.containsMetaData();
    }

    /// <summary>
    /// Returns the MetaData object for this movie.
    /// </summary>
    /// <returns>the MetaData for this movie</returns>
    public MetaData getMetaData()
    {
      return metaData;
    }

    /// <summary>
    /// Returns the <code>ProtectionInformation</code> objects that contains details about the DRM systems used. If no protection is present the returned list will be empty.
    /// </summary>
    /// <returns>a list of protection informations</returns>
    public Protection[] getProtections()
    {
      return protections.ToArray();
    }

    /// <summary>
    /// Returns the time this movie was created.
    /// </summary>
    /// <returns>the creation time</returns>
    public DateTime getCreationTime()
    {
      return BoxUtils.getDate(mvhd.getCreationTime());
    }

    /// <summary>
    /// Returns the last time this movie was modified.
    /// </summary>
    /// <returns>the modification time</returns>
    public DateTime getModificationTime()
    {
      return BoxUtils.getDate(mvhd.getModificationTime());
    }

    /// <summary>
    /// Returns the duration in seconds.
    /// </summary>
    /// <returns>the duration</returns>
    public double getDuration()
    {
      return mvhd.getDuration() / (double)mvhd.getTimeScale();
    }

    /// <summary>
    /// Indicates if there are more frames to be read in this movie.
    /// </summary>
    /// <returns>true if there is at least one track in this movie that has at least one more frame to read.</returns>
    public bool hasMoreFrames()
    {
      foreach (var track in tracks)
      {
        if (track.hasMoreFrames()) return true;
      }
      return false;
    }

    /// <summary>
    /// Reads the next frame from this movie (from one of the contained tracks).
    /// The frame is the next in time-order, thus the next for playback. If none of the tracks contains any more frames, null is returned.
    /// </summary>
    /// <returns>the next frame or null if there are no more frames to read from this movie.</returns>
    public Frame readNextFrame()
    {
      Track track = null;

      foreach (var t in tracks)
      {
        if (t.hasMoreFrames() && (track == null || t.getNextTimeStamp() < track.getNextTimeStamp())) track = t;
      }

      return (track == null) ? null : track.readNextFrame();
    }

    public Track[] getTracks(AudioTrack.AudioCodec.CodecType codecType)
    {
      return getTracks(new AudioTrack.AudioCodec(codecType));
    }

    public Track[] getTracks(VideoTrack.VideoCodec.CodecType codecType)
    {
      return getTracks(new VideoTrack.VideoCodec(codecType));
    }
  }
}
