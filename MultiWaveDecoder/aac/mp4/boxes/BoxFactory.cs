// ReSharper disable InconsistentNaming

using System;

namespace MultiWaveDecoder
{
  public static class BoxFactory
  {
    public static string typeToString(BoxType type)
    {
      uint l = (uint)type;
      var b = new char[4];
      b[0] = (char)((l >> 24) & 0xFF);
      b[1] = (char)((l >> 16) & 0xFF);
      b[2] = (char)((l >> 8) & 0xFF);
      b[3] = (char)(l & 0xFF);
      for (int i = 0; i < b.Length; i++) if (b[i] < ' ') b[i] = ' ';
      return new string(b);
    }

    static BoxImpl forType(BoxType type, long offset)
    {
      try
      {
        switch (type)
        {
          case BoxType.FILE_TYPE_BOX: return new FileTypeBox();
          case BoxType.SKIP_BOX:
          case BoxType.WIDE_BOX:
          case BoxType.FREE_SPACE_BOX: return new FreeSpaceBox();
          case BoxType.MEDIA_DATA_BOX: return new MediaDataBox();
          case BoxType.MOVIE_BOX: return new BoxImpl("Movie Box");
          case BoxType.MOVIE_HEADER_BOX: return new MovieHeaderBox();
          case BoxType.TRACK_BOX: return new BoxImpl("Track Box");
          case BoxType.TRACK_HEADER_BOX: return new TrackHeaderBox();
          case BoxType.MEDIA_BOX: return new BoxImpl("Media Box");
          case BoxType.MEDIA_HEADER_BOX: return new MediaHeaderBox();
          case BoxType.HANDLER_BOX: return new HandlerBox();
          case BoxType.MEDIA_INFORMATION_BOX: return new BoxImpl("Media Information Box");
          case BoxType.SOUND_MEDIA_HEADER_BOX: return new SoundMediaHeaderBox();
          case BoxType.DATA_INFORMATION_BOX: return new BoxImpl("Data Information Box");
          case BoxType.DATA_REFERENCE_BOX: return new DataReferenceBox();
          case BoxType.DATA_ENTRY_URL_BOX: return new DataEntryUrlBox();
          case BoxType.SAMPLE_TABLE_BOX: return new BoxImpl("Sample Table Box");
          case BoxType.SAMPLE_DESCRIPTION_BOX: return new SampleDescriptionBox();
          case BoxType.MP4A_SAMPLE_ENTRY: return new AudioSampleEntry("MPEG-4 Audio Sample Entry");
          case BoxType.ESD_BOX: return new ESDBox();
          case BoxType.UNKNOWN_SBTD_BOX: return new UnknownSbtdBox();
          case BoxType.ITUNES_PURCHASE_INFORMATION_BOX: return new BoxImpl("itunes Purchase Information Box");
          case BoxType.ORIGINAL_FORMAT_BOX: return new OriginalFormatBox();
          case BoxType.SCHEME_TYPE_BOX: return new SchemeTypeBox();
          case BoxType.SCHEME_INFORMATION_BOX: return new BoxImpl("Scheme Information Box");
          case BoxType.FAIRPLAY_USER_ID_BOX: return new FairPlayDataBox();
          case BoxType.FAIRPLAY_CERT_BOX: return new FairPlayDataBox();
          case BoxType.FAIRPLAY_RIGH_BOX: return new FairPlayDataBox();
          case BoxType.FAIRPLAY_CHTB_BOX: return new FairPlayDataBox();
          case BoxType.FAIRPLAY_SIGN_BOX: return new FairPlayDataBox();
          case BoxType.FAIRPLAY_USER_NAME_BOX: return new FairPlayDataBox();
          case BoxType.DECODING_TIME_TO_SAMPLE_BOX: return new DecodingTimeToSampleBox();
          case BoxType.SAMPLE_TO_CHUNK_BOX: return new SampleToChunkBox();
          case BoxType.SAMPLE_SIZE_BOX: return new SampleSizeBox();
          case BoxType.CHUNK_OFFSET_BOX: return new ChunkOffsetBox();
          case BoxType.USER_DATA_BOX: return new BoxImpl("User Data Box");
          case BoxType.META_BOX: return new MetaBox();
          case BoxType.ITUNES_META_LIST_BOX: return new BoxImpl("iTunes Meta List Box");
          case BoxType.CUSTOM_ITUNES_METADATA_BOX: return new BoxImpl("Custom iTunes Metadata Box");
          case BoxType.ITUNES_METADATA_MEAN_BOX: return new ITunesMetadataMeanBox();
          case BoxType.ITUNES_METADATA_BOX: return new ITunesMetadataBox();
          case BoxType.TRACK_NAME_BOX: return new BoxImpl("Track Name Box");
          case BoxType.ARTIST_NAME_BOX: return new BoxImpl("Artist Name Box");
          case BoxType.ALBUM_ARTIST_NAME_BOX: return new BoxImpl("Album Artist Name Box");
          case BoxType.COMPOSER_NAME_BOX: return new BoxImpl("Composer Name Box");
          case BoxType.ALBUM_NAME_BOX: return new BoxImpl("Album Name Box");
          case BoxType.GENRE_BOX: return new GenreBox();
          case BoxType.TRACK_NUMBER_BOX: return new BoxImpl("Track Number Box");
          case BoxType.DISK_NUMBER_BOX: return new BoxImpl("Disk Number Box");
          case BoxType.COMPILATION_PART_BOX: return new BoxImpl("Compilation Part Box");
          case BoxType.GAPLESS_PLAYBACK_BOX: return new BoxImpl("Gapless Playback Box");
          case BoxType.RELEASE_DATE_BOX: return new BoxImpl("Release Date Box");
          case BoxType.ITUNES_PURCHASE_ACCOUNT_BOX: return new BoxImpl("iTunes Purchase Account Box");
          case BoxType.ITUNES_OWNER_NAME_BOX: return new BoxImpl("iTunes Owner Name Box");
          case BoxType.COPYRIGHT_BOX: return new CopyrightBox();
          case BoxType.ITUNES_CATALOGUE_ID_BOX: return new BoxImpl("iTunes Catalogue ID Box");
          case BoxType.RATING_BOX: return new RatingBox();
          case BoxType.UNKNOWN_ATID_BOX: return new BoxImpl("Unknown 'atID' Box");
          case BoxType.UNKNOWN_CMID_BOX: return new BoxImpl("Unknown 'cmID' Box");
          case BoxType.UNKNOWN_PLID_BOX: return new BoxImpl("Unknown 'plID' Box");
          case BoxType.UNKNOWN_GEID_BOX: return new BoxImpl("Unknown 'geID' Box");
          case BoxType.ITUNES_COUNTRY_CODE_BOX: return new BoxImpl("iTunes Country Code Box");
          case BoxType.META_TYPE_BOX: return new BoxImpl("Meta Type Box");
          case BoxType.PURCHASE_DATE_BOX: return new BoxImpl("Purchase Date Box");
          case BoxType.UNKNOWN_XID_BOX: return new BoxImpl("Unknown 'xid ' Box");
          case BoxType.COVER_BOX: return new BoxImpl("Cover Box");

          // --- extra ---
          case BoxType.TRACK_SORT_BOX: return new BoxImpl("Track Sort Box");
          case BoxType.ALBUM_SORT_BOX: return new BoxImpl("Album Sort Box");
          case BoxType.ARTIST_SORT_BOX: return new BoxImpl("Artist Sort Box");
          case BoxType.ENCODER_TOOL_BOX: return new EncoderBox();
          case BoxType.EDIT_BOX: return new BoxImpl("Edit Box");
          case BoxType.EDIT_LIST_BOX: return new EditListBox();

          default:
          {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Logger.LogBoxes("BoxFactory - unknown box type: " + type + " '" + typeToString(type) + "', position: " + offset.ToString("N0"));
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            return new UnknownBox();
          }
        }
      }
      catch (Exception e)
      {
        Logger.LogBoxes("BoxFactory - could not call constructor for " + type + " '" + typeToString(type) + "': " + e);
        return new UnknownBox();
      }
    }

    static string GetBoxPath(BoxType type, IBox parent)
    {
      if (parent == null)
      {
        return typeToString(type);
      }

      return GetBoxPath(parent.getType(), parent.getParent()) + "." + typeToString(type);
    }

    public static IBox parseBox(IBox parent, MP4InputStream inStream)
    {
      long offset = inStream.getOffset();
      long size = inStream.readBytes(4);
      var type = (BoxType)(uint)inStream.readBytes(4);
      if (size == 1) size = inStream.readBytes(8);
      if (type == BoxType.EXTENDED_TYPE) inStream.skipBytes(16);

      // --- error protection ---
      if (parent != null)
      {
        long parentLeft = (parent.getOffset() + parent.getSize()) - offset;
        if (size > parentLeft) throw new Exception("error while decoding box '" + type + "' ('" + typeToString(type) + "') at offset " + offset.ToString("N0") + ": box too large for parent");
      }

      Logger.LogBoxes("[" + offset.ToString("N0") + "] " + GetBoxPath(type, parent));
      var box = forType(type, inStream.getOffset());
      box.setParams(parent, size, type, offset);
      box.decode(inStream);

      // --- if box doesn't contain data it only contains children ---
      if (box.GetType() == typeof(BoxImpl) || box.GetType() == typeof(FullBox)) box.readChildren(inStream);

      // --- check bytes left ---
      long left = (box.getOffset() + box.getSize()) - inStream.getOffset();
      if (left > 0)
      {
        if (!(box is MediaDataBox) && !(box is UnknownBox) && !(box is FreeSpaceBox))
        {
          Logger.LogInfo(string.Format("bytes left after reading box {0}: left: {1}, offset: {2}", typeToString(type), left, inStream.getOffset()));
        }

        // --- skip left Data ---
        inStream.skipBytes(left);
      }
      else if (left < 0) Logger.LogServe(string.Format("box {0} overread: {1} bytes, offset: {2}", typeToString(type), -left, inStream.getOffset()));

      return box;
    }
  }
}
