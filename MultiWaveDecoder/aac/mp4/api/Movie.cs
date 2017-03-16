using System.Collections.Generic;
// ReSharper disable InconsistentNaming

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
  }
}
