using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBeMadeStatic.Local
// ReSharper disable StaticMemberInGenericType
// ReSharper disable UnusedMember.Global

namespace MultiWaveDecoder
{
  /// <summary>
  /// This class contains the metadata for a movie. It parses different metadata types (iTunes tags, ID3).
  /// 
  /// The fields can be read via the <code>get(Field)</code> method using one of the predefined <code>Field</code>s.
  /// </summary>
  public sealed class MetaData
  {
    #region # static class Fields
    // ReSharper disable once UnusedTypeParameter
    public sealed class Field<T>
    {
      public readonly string name;

      public Field(string name)
      {
        this.name = name;
      }
    }

    public static class Fields
    {
      public static readonly Field<string> ARTIST = new Field<string>("Artist");
      public static readonly Field<string> TITLE = new Field<string>("Title");
      public static readonly Field<string> ALBUM_ARTIST = new Field<string>("Album Artist");
      public static readonly Field<string> ALBUM = new Field<string>("Album");
      public static readonly Field<int> TRACK_NUMBER = new Field<int>("Track Number");
      public static readonly Field<int> TOTAL_TRACKS = new Field<int>("Total Tracks");
      public static readonly Field<int> DISK_NUMBER = new Field<int>("Disk Number");
      public static readonly Field<int> TOTAL_DISKS = new Field<int>("Total disks");
      public static readonly Field<string> COMPOSER = new Field<string>("Composer");
      public static readonly Field<string> COMMENTS = new Field<string>("Comments");
      public static readonly Field<int> TEMPO = new Field<int>("Tempo");
      public static readonly Field<int> LENGTH_IN_MILLISECONDS = new Field<int>("Length in milliseconds");
      public static readonly Field<DateTime> RELEASE_DATE = new Field<DateTime>("Release Date");
      public static readonly Field<string> GENRE = new Field<string>("Genre");
      public static readonly Field<string> ENCODER_NAME = new Field<string>("Encoder Name");
      public static readonly Field<string> ENCODER_TOOL = new Field<string>("Encoder Tool");
      //public static Field<Date> ENCODING_DATE = new Field<Date>("Encoding Date");
      public static readonly Field<string> COPYRIGHT = new Field<string>("Copyright");
      public static readonly Field<string> PUBLISHER = new Field<string>("Publisher");
      public static readonly Field<bool> COMPILATION = new Field<bool>("Part of compilation");
      public static readonly Field<List<Artwork>> COVER_ARTWORKS = new Field<List<Artwork>>("Cover Artworks");
      public static readonly Field<string> GROUPING = new Field<string>("Grouping");
      public static readonly Field<string> LOCATION = new Field<string>("Location");
      public static readonly Field<string> LYRICS = new Field<string>("Lyrics");
      public static readonly Field<int> RATING = new Field<int>("Rating");
      public static readonly Field<int> PODCAST = new Field<int>("Podcast");
      public static readonly Field<string> PODCAST_URL = new Field<string>("Podcast URL");
      public static readonly Field<string> CATEGORY = new Field<string>("Category");
      public static readonly Field<string> KEYWORDS = new Field<string>("Keywords");
      public static readonly Field<int> EPISODE_GLOBAL_UNIQUE_ID = new Field<int>("Episode Global Unique ID");
      public static readonly Field<string> DESCRIPTION = new Field<string>("Description");
      public static readonly Field<string> TV_SHOW = new Field<string>("TV Show");
      public static readonly Field<string> TV_NETWORK = new Field<string>("TV Network");
      public static readonly Field<string> TV_EPISODE = new Field<string>("TV Episode");
      public static readonly Field<int> TV_EPISODE_NUMBER = new Field<int>("TV Episode Number");
      public static readonly Field<int> TV_SEASON = new Field<int>("TV Season");
      public static readonly Field<string> INTERNET_RADIO_STATION = new Field<string>("Internet Radio Station");
      public static readonly Field<string> PURCHASE_DATE = new Field<string>("Purchase Date");
      public static readonly Field<string> GAPLESS_PLAYBACK = new Field<string>("Gapless Playback");
      public static readonly Field<bool> HD_VIDEO = new Field<bool>("HD Video");
      //public static Field<Locale> LANGUAGE = new Field<Locale>("Language");
      // --- sorting ---
      public static readonly Field<string> ARTIST_SORT_TEXT = new Field<string>("Artist Sort Text");
      public static readonly Field<string> TITLE_SORT_TEXT = new Field<string>("Title Sort Text");
      public static readonly Field<string> ALBUM_SORT_TEXT = new Field<string>("Album Sort Text");
    }
    #endregion

    #region # static readonly string[] STANDARD_GENRES =
    static readonly string[] STANDARD_GENRES =
    {
		  "undefined",
		  //IDv1 standard
		  "blues",
		  "classic rock",
		  "country",
		  "dance",
		  "disco",
		  "funk",
		  "grunge",
		  "hip hop",
		  "jazz",
		  "metal",
		  "new age",
		  "oldies",
		  "other",
		  "pop",
		  "r and b",
		  "rap",
		  "reggae",
		  "rock",
		  "techno",
		  "industrial",
		  "alternative",
		  "ska",
		  "death metal",
		  "pranks",
		  "soundtrack",
		  "euro techno",
		  "ambient",
		  "trip hop",
		  "vocal",
		  "jazz funk",
		  "fusion",
		  "trance",
		  "classical",
		  "instrumental",
		  "acid",
		  "house",
		  "game",
		  "sound clip",
		  "gospel",
		  "noise",
		  "alternrock",
		  "bass",
		  "soul",
		  "punk",
		  "space",
		  "meditative",
		  "instrumental pop",
		  "instrumental rock",
		  "ethnic",
		  "gothic",
		  "darkwave",
		  "techno industrial",
		  "electronic",
		  "pop folk",
		  "eurodance",
		  "dream",
		  "southern rock",
		  "comedy",
		  "cult",
		  "gangsta",
		  "top ",
		  "christian rap",
		  "pop funk",
		  "jungle",
		  "native american",
		  "cabaret",
		  "new wave",
		  "psychedelic",
		  "rave",
		  "showtunes",
		  "trailer",
		  "lo fi",
		  "tribal",
		  "acid punk",
		  "acid jazz",
		  "polka",
		  "retro",
		  "musical",
		  "rock and roll",
		  //winamp extension
		  "hard rock",
		  "folk",
		  "folk rock",
		  "national folk",
		  "swing",
		  "fast fusion",
		  "bebob",
		  "latin",
		  "revival",
		  "celtic",
		  "bluegrass",
		  "avantgarde",
		  "gothic rock",
		  "progressive rock",
		  "psychedelic rock",
		  "symphonic rock",
		  "slow rock",
		  "big band",
		  "chorus",
		  "easy listening",
		  "acoustic",
		  "humour",
		  "speech",
		  "chanson",
		  "opera",
		  "chamber music",
		  "sonata",
		  "symphony",
		  "booty bass",
		  "primus",
		  "porn groove",
		  "satire",
		  "slow jam",
		  "club",
		  "tango",
		  "samba",
		  "folklore",
		  "ballad",
		  "power ballad",
		  "rhythmic soul",
		  "freestyle",
		  "duet",
		  "punk rock",
		  "drum solo",
		  "a capella",
		  "euro house",
		  "dance hall"
	  };
    #endregion

    /// <summary>
    /// moov.udta:
    /// -3gpp boxes
    /// -meta
    /// --ilst
    /// --tags
    /// --meta (no container!)
    /// --tseg
    /// ---tshd
    /// </summary>
    /// <param name="udta"></param>
    /// <param name="meta"></param>
    public void parse(IBox udta, IBox meta)
    {
      // --- standard boxes ---
      if (meta.hasChild(BoxType.COPYRIGHT_BOX))
      {
        throw new NotImplementedException();
        //var cprt = (CopyrightBox)meta.getChild(BoxType.COPYRIGHT_BOX);
        //put(Field.LANGUAGE, new Locale(cprt.getLanguageCode()));
        //put(Field.COPYRIGHT, cprt.getNotice());
      }

      // --- 3gpp user data ---
      if (udta != null) parse3GPPData(udta);

      // --- id3, TODO: can be present in different languages ---
      if (meta.hasChild(BoxType.ID3_TAG_BOX))
      {
        throw new NotImplementedException();
        // parseID3((ID3TagBox)meta.getChild(BoxType.ID3_TAG_BOX));
      }

      // --- itunes ---
      if (meta.hasChild(BoxType.ITUNES_META_LIST_BOX)) parseITunesMetaData(meta.getChild(BoxType.ITUNES_META_LIST_BOX));

      // --- nero tags ---
      if (meta.hasChild(BoxType.NERO_METADATA_TAGS_BOX))
      {
        throw new NotImplementedException();
        // parseNeroTags((NeroMetadataTagsBox)meta.getChild(BoxType.NERO_METADATA_TAGS_BOX));
      }
    }

    /// <summary>
    /// parses children of 'ilst': iTunes
    /// </summary>
    /// <param name="ilst"></param>
    void parseITunesMetaData(IBox ilst)
    {
      var boxes = ilst.getChildren();
      foreach (var box in boxes)
      {
        var boxType = box.getType();
        var data = (ITunesMetadataBox)box.getChild(BoxType.ITUNES_METADATA_BOX);

        switch (boxType)
        {
          case BoxType.ARTIST_NAME_BOX: put(Fields.ARTIST, data.getText()); break;
          case BoxType.TRACK_NAME_BOX: put(Fields.TITLE, data.getText()); break;
          case BoxType.ALBUM_ARTIST_NAME_BOX: put(Fields.ALBUM_ARTIST, data.getText()); break;
          case BoxType.ALBUM_NAME_BOX: put(Fields.ALBUM, data.getText()); break;
          case BoxType.TRACK_NUMBER_BOX:
          {
            var b = data.getData();
            put(Fields.TRACK_NUMBER, b[3]);
            put(Fields.TOTAL_TRACKS, b[5]);
          } break;
          case BoxType.DISK_NUMBER_BOX: put(Fields.DISK_NUMBER, data.getInteger()); break;
          case BoxType.COMPOSER_NAME_BOX: put(Fields.COMPOSER, data.getText()); break;
          case BoxType.COMMENTS_BOX: put(Fields.COMMENTS, data.getText()); break;
          case BoxType.TEMPO_BOX: put(Fields.TEMPO, data.getInteger()); break;
          case BoxType.RELEASE_DATE_BOX:
          {
            put(Fields.RELEASE_DATE, data.getDate());
          } break;
          case BoxType.GENRE_BOX:
          case BoxType.CUSTOM_GENRE_BOX:
          {
            string s = null;
            if (data.getDataType() == ITunesMetadataBox.DataType.UTF8) s = data.getText();
            else
            {
              int i = data.getInteger();
              if (i > 0 && i < STANDARD_GENRES.Length) s = STANDARD_GENRES[i];
            }
            if (s != null) put(Fields.GENRE, s);
          } break;
          case BoxType.ENCODER_NAME_BOX: put(Fields.ENCODER_NAME, data.getText()); break;
          case BoxType.ENCODER_TOOL_BOX: put(Fields.ENCODER_TOOL, data.getText()); break;
          case BoxType.COPYRIGHT_BOX: put(Fields.COPYRIGHT, data.getText()); break;
          case BoxType.COMPILATION_PART_BOX: put(Fields.COMPILATION, data.getBoolean()); break;
          case BoxType.COVER_BOX:
          {
            var aw = new Artwork(Artwork.forDataType(data.getDataType()), data.getData());
            if (contents.ContainsKey(Fields.COVER_ARTWORKS.name))
            {
              ((List<Artwork>)contents[Fields.COVER_ARTWORKS.name]).Add(aw);
            }
            else
            {
              put(Fields.COVER_ARTWORKS, new List<Artwork> { aw });
            }
          } break;
          case BoxType.GROUPING_BOX: put(Fields.GROUPING, data.getText()); break;
          case BoxType.LYRICS_BOX: put(Fields.LYRICS, data.getText()); break;
          case BoxType.RATING_BOX: put(Fields.RATING, data.getInteger()); break;
          case BoxType.PODCAST_BOX: put(Fields.PODCAST, data.getInteger()); break;
          case BoxType.PODCAST_URL_BOX: put(Fields.PODCAST_URL, data.getText()); break;
          case BoxType.CATEGORY_BOX: put(Fields.CATEGORY, data.getText()); break;
          case BoxType.KEYWORD_BOX: put(Fields.KEYWORDS, data.getText()); break;
          case BoxType.DESCRIPTION_BOX: put(Fields.DESCRIPTION, data.getText()); break;
          case BoxType.LONG_DESCRIPTION_BOX: put(Fields.DESCRIPTION, data.getText()); break;
          case BoxType.TV_SHOW_BOX: put(Fields.TV_SHOW, data.getText()); break;
          case BoxType.TV_NETWORK_NAME_BOX: put(Fields.TV_NETWORK, data.getText()); break;
          case BoxType.TV_EPISODE_BOX: put(Fields.TV_EPISODE, data.getText()); break;
          case BoxType.TV_EPISODE_NUMBER_BOX: put(Fields.TV_EPISODE_NUMBER, data.getInteger()); break;
          case BoxType.TV_SEASON_BOX: put(Fields.TV_SEASON, data.getInteger()); break;
          case BoxType.PURCHASE_DATE_BOX: put(Fields.PURCHASE_DATE, data.getText()); break;
          case BoxType.GAPLESS_PLAYBACK_BOX: put(Fields.GAPLESS_PLAYBACK, data.getText()); break;
          case BoxType.HD_VIDEO_BOX: put(Fields.HD_VIDEO, data.getBoolean()); break;
          case BoxType.ARTIST_SORT_BOX: put(Fields.ARTIST_SORT_TEXT, data.getText()); break;
          case BoxType.TRACK_SORT_BOX: put(Fields.TITLE_SORT_TEXT, data.getText()); break;
          case BoxType.ALBUM_SORT_BOX: put(Fields.ALBUM_SORT_TEXT, data.getText()); break;
        }
      }
    }

    /// <summary>
    /// parses specific children of 'udta': 3GPP
    /// 
    /// TODO: handle language codes
    /// </summary>
    /// <param name="udta"></param>
    void parse3GPPData(IBox udta)
    {
      if (udta.hasChild(BoxType.THREE_GPP_ALBUM_BOX))
      {
        throw new NotImplementedException();
        //var albm = (ThreeGPPAlbumBox)udta.getChild(BoxType.THREE_GPP_ALBUM_BOX);
        //put(Field.ALBUM, albm.getData());
        //put(Field.TRACK_NUMBER, albm.getTrackNumber());
      }
      //if(udta.hasChild(BoxType.THREE_GPP_AUTHOR_BOX));
      //if(udta.hasChild(BoxType.THREE_GPP_CLASSIFICATION_BOX));
      if (udta.hasChild(BoxType.THREE_GPP_DESCRIPTION_BOX))
      {
        throw new NotImplementedException();
        // put(Field.DESCRIPTION, ((ThreeGPPMetadataBox)udta.getChild(BoxType.THREE_GPP_DESCRIPTION_BOX)).getData());
      }
      if (udta.hasChild(BoxType.THREE_GPP_KEYWORDS_BOX))
      {
        throw new NotImplementedException();
        // put(Field.KEYWORDS, ((ThreeGPPMetadataBox)udta.getChild(BoxType.THREE_GPP_KEYWORDS_BOX)).getData());
      }
      if (udta.hasChild(BoxType.THREE_GPP_LOCATION_INFORMATION_BOX))
      {
        throw new NotImplementedException();
        // put(Field.LOCATION, ((ThreeGPPLocationBox)udta.getChild(BoxType.THREE_GPP_LOCATION_INFORMATION_BOX)).getPlaceName());
      }
      if (udta.hasChild(BoxType.THREE_GPP_PERFORMER_BOX))
      {
        throw new NotImplementedException();
        // put(Field.ARTIST, ((ThreeGPPMetadataBox)udta.getChild(BoxType.THREE_GPP_PERFORMER_BOX)).getData());
      }
      if (udta.hasChild(BoxType.THREE_GPP_RECORDING_YEAR_BOX))
      {
        throw new NotImplementedException();
        //string value = ((ThreeGPPMetadataBox)udta.getChild(BoxType.THREE_GPP_RECORDING_YEAR_BOX)).getData();
        //try
        //{
        //  put(Field.RELEASE_DATE, new Date(Integer.parseInt(value)));
        //}
        //catch (NumberFormatException e)
        //{
        //  Logger.getLogger("MP4 API").log(Level.INFO, "unable to parse 3GPP metadata: recording year value: {0}", value);
        //}
      }
      if (udta.hasChild(BoxType.THREE_GPP_TITLE_BOX))
      {
        throw new NotImplementedException();
        // put(Field.TITLE, ((ThreeGPPMetadataBox)udta.getChild(BoxType.THREE_GPP_TITLE_BOX)).getData());
      }
    }

    readonly Dictionary<string, object> contents = new Dictionary<string, object>();

    void put<T>(Field<T> field, T value)
    {
      contents.Add(field.name, value);
    }

    public bool containsMetaData()
    {
      return contents.Count > 0;
    }

    public bool contains<T>(Field<T> field)
    {
      return contents.ContainsKey(field.name);
    }

    public T get<T>(Field<T> field)
    {
      return (T)contents[field.name];
    }

    public KeyValuePair<string, object>[] getAll()
    {
      return contents.ToArray();
    }
  }
}
