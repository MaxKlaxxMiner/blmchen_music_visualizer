// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public enum BoxTypes
  {
    /// <summary>
    /// uuid
    /// </summary>
    EXTENDED_TYPE = 1970628964,

    // --- standard boxes (ISO BMFF) ---

    /// <summary>
    /// meco
    /// </summary>
    ADDITIONAL_METADATA_CONTAINER_BOX = 1835361135,
    /// <summary>
    /// alac
    /// </summary>
    APPLE_LOSSLESS_BOX = 1634492771,
    /// <summary>
    /// bxml
    /// </summary>
    BINARY_XML_BOX = 1652059500,
    /// <summary>
    /// btrt
    /// </summary>
    BIT_RATE_BOX = 1651798644,
    /// <summary>
    /// chpl
    /// </summary>
    CHAPTER_BOX = 1667788908,
    /// <summary>
    /// stco
    /// </summary>
    CHUNK_OFFSET_BOX = 1937007471,
    /// <summary>
    /// co64
    /// </summary>
    CHUNK_LARGE_OFFSET_BOX = 1668232756,
    /// <summary>
    /// clap
    /// </summary>
    CLEAN_APERTURE_BOX = 1668047216,
    /// <summary>
    /// stz2
    /// </summary>
    COMPACT_SAMPLE_SIZE_BOX = 1937013298,
    /// <summary>
    /// ctts
    /// </summary>
    COMPOSITION_TIME_TO_SAMPLE_BOX = 1668576371,
    /// <summary>
    /// cprt
    /// </summary>
    COPYRIGHT_BOX = 1668313716,
    /// <summary>
    /// "urn "
    /// </summary>
    DATA_ENTRY_URN_BOX = 1970433568,
    /// <summary>
    /// "url "
    /// </summary>
    DATA_ENTRY_URL_BOX = 1970433056,
    /// <summary>
    /// dinf
    /// </summary>
    DATA_INFORMATION_BOX = 1684631142,
    /// <summary>
    /// dref
    /// </summary>
    DATA_REFERENCE_BOX = 1685218662,
    /// <summary>
    /// stts
    /// </summary>
    DECODING_TIME_TO_SAMPLE_BOX = 1937011827,
    /// <summary>
    /// stdp
    /// </summary>
    DEGRADATION_PRIORITY_BOX = 1937007728,
    /// <summary>
    /// edts
    /// </summary>
    EDIT_BOX = 1701082227,
    /// <summary>
    /// elst
    /// </summary>
    EDIT_LIST_BOX = 1701606260,
    /// <summary>
    /// fiin
    /// </summary>
    FD_ITEM_INFORMATION_BOX = 1718184302,
    /// <summary>
    /// segr
    /// </summary>
    FD_SESSION_GROUP_BOX = 1936025458,
    /// <summary>
    /// fecr
    /// </summary>
    FEC_RESERVOIR_BOX = 1717920626,
    /// <summary>
    /// fpar
    /// </summary>
    FILE_PARTITION_BOX = 1718641010,
    /// <summary>
    /// ftyp
    /// </summary>
    FILE_TYPE_BOX = 1718909296,
    /// <summary>
    /// free
    /// </summary>
    FREE_SPACE_BOX = 1718773093,
    /// <summary>
    /// gitn
    /// </summary>
    GROUP_ID_TO_NAME_BOX = 1734964334,
    /// <summary>
    /// hdlr
    /// </summary>
    HANDLER_BOX = 1751411826,
    /// <summary>
    /// hmhd
    /// </summary>
    HINT_MEDIA_HEADER_BOX = 1752000612,
    /// <summary>
    /// ipmc
    /// </summary>
    IPMP_CONTROL_BOX = 1768975715,
    /// <summary>
    /// imif
    /// </summary>
    IPMP_INFO_BOX = 1768778086,
    /// <summary>
    /// iinf
    /// </summary>
    ITEM_INFORMATION_BOX = 1768517222,
    /// <summary>
    /// infe
    /// </summary>
    ITEM_INFORMATION_ENTRY = 1768842853,
    /// <summary>
    /// iloc
    /// </summary>
    ITEM_LOCATION_BOX = 1768714083,
    /// <summary>
    /// ipro
    /// </summary>
    ITEM_PROTECTION_BOX = 1768977007,
    /// <summary>
    /// mdia
    /// </summary>
    MEDIA_BOX = 1835297121,
    /// <summary>
    /// mdat
    /// </summary>
    MEDIA_DATA_BOX = 1835295092,
    /// <summary>
    /// mdhd
    /// </summary>
    MEDIA_HEADER_BOX = 1835296868,
    /// <summary>
    /// minf
    /// </summary>
    MEDIA_INFORMATION_BOX = 1835626086,
    /// <summary>
    /// meta
    /// </summary>
    META_BOX = 1835365473,
    /// <summary>
    /// mere
    /// </summary>
    META_BOX_RELATION_BOX = 1835364965,
    /// <summary>
    /// moov
    /// </summary>
    MOVIE_BOX = 1836019574,
    /// <summary>
    /// mvex
    /// </summary>
    MOVIE_EXTENDS_BOX = 1836475768,
    /// <summary>
    /// mehd
    /// </summary>
    MOVIE_EXTENDS_HEADER_BOX = 1835362404,
    /// <summary>
    /// moof
    /// </summary>
    MOVIE_FRAGMENT_BOX = 1836019558,
    /// <summary>
    /// mfhd
    /// </summary>
    MOVIE_FRAGMENT_HEADER_BOX = 1835427940,
    /// <summary>
    /// mfra
    /// </summary>
    MOVIE_FRAGMENT_RANDOM_ACCESS_BOX = 1835430497,
    /// <summary>
    /// mfro
    /// </summary>
    MOVIE_FRAGMENT_RANDOM_ACCESS_OFFSET_BOX = 1835430511,
    /// <summary>
    /// mvhd
    /// </summary>
    MOVIE_HEADER_BOX = 1836476516,
    /// <summary>
    /// tags
    /// </summary>
    NERO_METADATA_TAGS_BOX = 1952540531,
    /// <summary>
    /// nmhd
    /// </summary>
    NULL_MEDIA_HEADER_BOX = 1852663908,
    /// <summary>
    /// frma
    /// </summary>
    ORIGINAL_FORMAT_BOX = 1718775137,
    /// <summary>
    /// padb
    /// </summary>
    PADDING_BIT_BOX = 1885430882,
    /// <summary>
    /// paen
    /// </summary>
    PARTITION_ENTRY = 1885431150,
    /// <summary>
    /// pasp
    /// </summary>
    PIXEL_ASPECT_RATIO_BOX = 1885434736,
    /// <summary>
    /// pitm
    /// </summary>
    PRIMARY_ITEM_BOX = 1885959277,
    /// <summary>
    /// pdin
    /// </summary>
    PROGRESSIVE_DOWNLOAD_INFORMATION_BOX = 1885628782,
    /// <summary>
    /// sinf
    /// </summary>
    PROTECTION_SCHEME_INFORMATION_BOX = 1936289382,
    /// <summary>
    /// sdtp
    /// </summary>
    SAMPLE_DEPENDENCY_TYPE_BOX = 1935963248,
    /// <summary>
    /// stsd
    /// </summary>
    SAMPLE_DESCRIPTION_BOX = 1937011556,
    /// <summary>
    /// sgpd
    /// </summary>
    SAMPLE_GROUP_DESCRIPTION_BOX = 1936158820,
    /// <summary>
    /// stsl
    /// </summary>
    SAMPLE_SCALE_BOX = 1937011564,
    /// <summary>
    /// stsz
    /// </summary>
    SAMPLE_SIZE_BOX = 1937011578,
    /// <summary>
    /// stbl
    /// </summary>
    SAMPLE_TABLE_BOX = 1937007212,
    /// <summary>
    /// stsc
    /// </summary>
    SAMPLE_TO_CHUNK_BOX = 1937011555,
    /// <summary>
    /// sbgp
    /// </summary>
    SAMPLE_TO_GROUP_BOX = 1935828848,
    /// <summary>
    /// schm
    /// </summary>
    SCHEME_TYPE_BOX = 1935894637,
    /// <summary>
    /// schi
    /// </summary>
    SCHEME_INFORMATION_BOX = 1935894633,
    /// <summary>
    /// stsh
    /// </summary>
    SHADOW_SYNC_SAMPLE_BOX = 1937011560,
    /// <summary>
    /// skip
    /// </summary>
    SKIP_BOX = 1936419184,
    /// <summary>
    /// smhd
    /// </summary>
    SOUND_MEDIA_HEADER_BOX = 1936549988,
    /// <summary>
    /// subs
    /// </summary>
    SUB_SAMPLE_INFORMATION_BOX = 1937072755,
    /// <summary>
    /// stss
    /// </summary>
    SYNC_SAMPLE_BOX = 1937011571,
    /// <summary>
    /// trak
    /// </summary>
    TRACK_BOX = 1953653099,
    /// <summary>
    /// trex
    /// </summary>
    TRACK_EXTENDS_BOX = 1953654136,
    /// <summary>
    /// traf
    /// </summary>
    TRACK_FRAGMENT_BOX = 1953653094,
    /// <summary>
    /// tfhd
    /// </summary>
    TRACK_FRAGMENT_HEADER_BOX = 1952868452,
    /// <summary>
    /// tfra
    /// </summary>
    TRACK_FRAGMENT_RANDOM_ACCESS_BOX = 1952871009,
    /// <summary>
    /// trun
    /// </summary>
    TRACK_FRAGMENT_RUN_BOX = 1953658222,
    /// <summary>
    /// tkhd
    /// </summary>
    TRACK_HEADER_BOX = 1953196132,
    /// <summary>
    /// tref
    /// </summary>
    TRACK_REFERENCE_BOX = 1953654118,
    /// <summary>
    /// tsel
    /// </summary>
    TRACK_SELECTION_BOX = 1953719660,
    /// <summary>
    /// udta
    /// </summary>
    USER_DATA_BOX = 1969517665,
    /// <summary>
    /// vmhd
    /// </summary>
    VIDEO_MEDIA_HEADER_BOX = 1986881636,
    /// <summary>
    /// wide
    /// </summary>
    WIDE_BOX = 2003395685,
    /// <summary>
    /// "xml "
    /// </summary>
    XML_BOX = 2020437024,

    // --- mp4 extension ---

    /// <summary>
    /// iods
    /// </summary>
    OBJECT_DESCRIPTOR_BOX = 1768907891,
    /// <summary>
    /// sdep
    /// </summary>
    SAMPLE_DEPENDENCY_BOX = 1935959408,

    // --- metadata: id3 ---

    /// <summary>
    /// id32
    /// </summary>
    ID3_TAG_BOX = 1768174386,

    // --- metadata: itunes ---

    /// <summary>
    /// ilst
    /// </summary>
    ITUNES_META_LIST_BOX = 1768715124,
    /// <summary>
    /// ----
    /// </summary>
    CUSTOM_ITUNES_METADATA_BOX = 757935405,
    /// <summary>
    /// data
    /// </summary>
    ITUNES_METADATA_BOX = 1684108385,
    /// <summary>
    /// name
    /// </summary>
    ITUNES_METADATA_NAME_BOX = 1851878757,
    /// <summary>
    /// mean
    /// </summary>
    ITUNES_METADATA_MEAN_BOX = 1835360622,
    /// <summary>
    /// aART
    /// </summary>
    ALBUM_ARTIST_NAME_BOX = 1631670868,
    /// <summary>
    /// soaa
    /// </summary>
    ALBUM_ARTIST_SORT_BOX = 1936679265,
    /// <summary>
    /// ©alb
    /// </summary>
    ALBUM_NAME_BOX = unchecked((int)2841734242),
    /// <summary>
    /// soal
    /// </summary>
    ALBUM_SORT_BOX = 1936679276,
    /// <summary>
    /// ©ART
    /// </summary>
    ARTIST_NAME_BOX = unchecked((int)2839630420),
    /// <summary>
    /// soar
    /// </summary>
    ARTIST_SORT_BOX = 1936679282,
    /// <summary>
    /// catg
    /// </summary>
    CATEGORY_BOX = 1667331175,
    /// <summary>
    /// ©cmt
    /// </summary>
    COMMENTS_BOX = unchecked((int)2841865588),
    /// <summary>
    /// cpil
    /// </summary>
    COMPILATION_PART_BOX = 1668311404,
    /// <summary>
    /// ©wrt
    /// </summary>
    COMPOSER_NAME_BOX = unchecked((int)2843177588),
    /// <summary>
    /// soco
    /// </summary>
    COMPOSER_SORT_BOX = 1936679791,
    /// <summary>
    /// covr
    /// </summary>
    COVER_BOX = 1668249202,
    /// <summary>
    /// ©gen
    /// </summary>
    CUSTOM_GENRE_BOX = unchecked((int)2842125678),
    /// <summary>
    /// desc
    /// </summary>
    DESCRIPTION_BOX = 1684370275,
    /// <summary>
    /// disk
    /// </summary>
    DISK_NUMBER_BOX = 1684632427,
    /// <summary>
    /// ©enc
    /// </summary>
    ENCODER_NAME_BOX = unchecked((int)2841996899),
    /// <summary>
    /// ©too
    /// </summary>
    ENCODER_TOOL_BOX = unchecked((int)2842980207),
    /// <summary>
    /// egid
    /// </summary>
    EPISODE_GLOBAL_UNIQUE_ID_BOX = 1701276004,
    /// <summary>
    /// pgap
    /// </summary>
    GAPLESS_PLAYBACK_BOX = 1885823344,
    /// <summary>
    /// gnre
    /// </summary>
    GENRE_BOX = 1735291493,
    /// <summary>
    /// ©grp
    /// </summary>
    GROUPING_BOX = unchecked((int)2842129008),
    /// <summary>
    /// hdvd
    /// </summary>
    HD_VIDEO_BOX = 1751414372,
    /// <summary>
    /// apID
    /// </summary>
    ITUNES_PURCHASE_ACCOUNT_BOX = 1634748740,
    /// <summary>
    /// akID
    /// </summary>
    ITUNES_ACCOUNT_TYPE_BOX = 1634421060,
    /// <summary>
    /// cnID
    /// </summary>
    ITUNES_CATALOGUE_ID_BOX = 1668172100,
    /// <summary>
    /// sfID
    /// </summary>
    ITUNES_COUNTRY_CODE_BOX = 1936083268,
    /// <summary>
    /// keyw
    /// </summary>
    KEYWORD_BOX = 1801812343,
    /// <summary>
    /// ldes
    /// </summary>
    LONG_DESCRIPTION_BOX = 1818518899,
    /// <summary>
    /// ©lyr
    /// </summary>
    LYRICS_BOX = unchecked((int)2842458482),
    /// <summary>
    /// stik
    /// </summary>
    META_TYPE_BOX = 1937009003,
    /// <summary>
    /// pcst
    /// </summary>
    PODCAST_BOX = 1885565812,
    /// <summary>
    /// purl
    /// </summary>
    PODCAST_URL_BOX = 1886745196,
    /// <summary>
    /// purd
    /// </summary>
    PURCHASE_DATE_BOX = 1886745188,
    /// <summary>
    /// rtng
    /// </summary>
    RATING_BOX = 1920233063,
    /// <summary>
    /// ©day
    /// </summary>
    RELEASE_DATE_BOX = unchecked((int)2841928057),
    /// <summary>
    /// ©req
    /// </summary>
    REQUIREMENT_BOX = unchecked((int)2842846577),
    /// <summary>
    /// tmpo
    /// </summary>
    TEMPO_BOX = 1953329263,
    /// <summary>
    /// ©nam
    /// </summary>
    TRACK_NAME_BOX = unchecked((int)2842583405),
    /// <summary>
    /// trkn
    /// </summary>
    TRACK_NUMBER_BOX = 1953655662,
    /// <summary>
    /// sonm
    /// </summary>
    TRACK_SORT_BOX = 1936682605,
    /// <summary>
    /// tves
    /// </summary>
    TV_EPISODE_BOX = 1953916275,
    /// <summary>
    /// tven
    /// </summary>
    TV_EPISODE_NUMBER_BOX = 1953916270,
    /// <summary>
    /// tvnn
    /// </summary>
    TV_NETWORK_NAME_BOX = 1953918574,
    /// <summary>
    /// tvsn
    /// </summary>
    TV_SEASON_BOX = 1953919854,
    /// <summary>
    /// tvsh
    /// </summary>
    TV_SHOW_BOX = 1953919848,
    /// <summary>
    /// sosn
    /// </summary>
    TV_SHOW_SORT_BOX = 1936683886,

    // --- metadata: 3gpp ---

    /// <summary>
    /// albm
    /// </summary>
    THREE_GPP_ALBUM_BOX = 1634493037,
    /// <summary>
    /// auth
    /// </summary>
    THREE_GPP_AUTHOR_BOX = 1635087464,
    /// <summary>
    /// clsf
    /// </summary>
    THREE_GPP_CLASSIFICATION_BOX = 1668051814,
    /// <summary>
    /// dscp
    /// </summary>
    THREE_GPP_DESCRIPTION_BOX = 1685283696,
    /// <summary>
    /// kywd
    /// </summary>
    THREE_GPP_KEYWORDS_BOX = 1803122532,
    /// <summary>
    /// loci
    /// </summary>
    THREE_GPP_LOCATION_INFORMATION_BOX = 1819239273,
    /// <summary>
    /// perf
    /// </summary>
    THREE_GPP_PERFORMER_BOX = 1885696614,
    /// <summary>
    /// yrrc
    /// </summary>
    THREE_GPP_RECORDING_YEAR_BOX = 2037543523,
    /// <summary>
    /// titl
    /// </summary>
    THREE_GPP_TITLE_BOX = 1953068140,

    // --- metadata: google/youtube ---

    /// <summary>
    /// gshh
    /// </summary>
    GOOGLE_HOST_HEADER_BOX = 1735616616,
    /// <summary>
    /// gspm
    /// </summary>
    GOOGLE_PING_MESSAGE_BOX = 1735618669,
    /// <summary>
    /// gspu
    /// </summary>
    GOOGLE_PING_URL_BOX = 1735618677,
    /// <summary>
    /// gssd
    /// </summary>
    GOOGLE_SOURCE_DATA_BOX = 1735619428,
    /// <summary>
    /// gsst
    /// </summary>
    GOOGLE_START_TIME_BOX = 1735619444,
    /// <summary>
    /// gstd
    /// </summary>
    GOOGLE_TRACK_DURATION_BOX = 1735619684,

    // --- sample entries ---

    /// <summary>
    /// mp4v
    /// </summary>
    MP4V_SAMPLE_ENTRY = 1836070006,
    /// <summary>
    /// s263
    /// </summary>
    H263_SAMPLE_ENTRY = 1932670515,
    /// <summary>
    /// encv
    /// </summary>
    ENCRYPTED_VIDEO_SAMPLE_ENTRY = 1701733238,
    /// <summary>
    /// avc1
    /// </summary>
    AVC_SAMPLE_ENTRY = 1635148593,
    /// <summary>
    /// mp4a
    /// </summary>
    MP4A_SAMPLE_ENTRY = 1836069985,
    /// <summary>
    /// ac-3
    /// </summary>
    AC3_SAMPLE_ENTRY = 1633889587,
    /// <summary>
    /// ec-3
    /// </summary>
    EAC3_SAMPLE_ENTRY = 1700998451,
    /// <summary>
    /// drms
    /// </summary>
    DRMS_SAMPLE_ENTRY = 1685220723,
    /// <summary>
    /// samr
    /// </summary>
    AMR_SAMPLE_ENTRY = 1935764850,
    /// <summary>
    /// sawb
    /// </summary>
    AMR_WB_SAMPLE_ENTRY = 1935767394,
    /// <summary>
    /// sevc
    /// </summary>
    EVRC_SAMPLE_ENTRY = 1936029283,
    /// <summary>
    /// sqcp
    /// </summary>
    QCELP_SAMPLE_ENTRY = 1936810864,
    /// <summary>
    /// ssmv
    /// </summary>
    SMV_SAMPLE_ENTRY = 1936944502,
    /// <summary>
    /// enca
    /// </summary>
    ENCRYPTED_AUDIO_SAMPLE_ENTRY = 1701733217,
    /// <summary>
    /// mp4s
    /// </summary>
    MPEG_SAMPLE_ENTRY = 1836070003,
    /// <summary>
    /// mett
    /// </summary>
    TEXT_METADATA_SAMPLE_ENTRY = 1835365492,
    /// <summary>
    /// metx
    /// </summary>
    XML_METADATA_SAMPLE_ENTRY = 1835365496,
    /// <summary>
    /// "rtp "
    /// </summary>
    RTP_HINT_SAMPLE_ENTRY = 1920233504,
    /// <summary>
    /// "fdp "
    /// </summary>
    FD_HINT_SAMPLE_ENTRY = 1717858336,

    // --- codec infos ---

    /// <summary>
    /// esds
    /// </summary>
    ESD_BOX = 1702061171,

    // --- video codecs ---

    /// <summary>
    /// d263
    /// </summary>
    H263_SPECIFIC_BOX = 1681012275,
    /// <summary>
    /// avcC
    /// </summary>
    AVC_SPECIFIC_BOX = 1635148611,

    // --- audio codecs ---

    /// <summary>
    /// dac3
    /// </summary>
    AC3_SPECIFIC_BOX = 1684103987,
    /// <summary>
    /// dec3
    /// </summary>
    EAC3_SPECIFIC_BOX = 1684366131,
    /// <summary>
    /// damr
    /// </summary>
    AMR_SPECIFIC_BOX = 1684106610,
    /// <summary>
    /// devc
    /// </summary>
    EVRC_SPECIFIC_BOX = 1684371043,
    /// <summary>
    /// dqcp
    /// </summary>
    QCELP_SPECIFIC_BOX = 1685152624,
    /// <summary>
    /// dsmv
    /// </summary>
    SMV_SPECIFIC_BOX = 1685286262,

    // --- OMA DRM ---

    /// <summary>
    /// odaf
    /// </summary>
    OMA_ACCESS_UNIT_FORMAT_BOX = 1868849510,
    /// <summary>
    /// ohdr
    /// </summary>
    OMA_COMMON_HEADERS_BOX = 1869112434,
    /// <summary>
    /// ccid
    /// </summary>
    OMA_CONTENT_ID_BOX = 1667459428,
    /// <summary>
    /// odda
    /// </summary>
    OMA_CONTENT_OBJECT_BOX = 1868850273,
    /// <summary>
    /// cvru
    /// </summary>
    OMA_COVER_URI_BOX = 1668706933,
    /// <summary>
    /// odhe
    /// </summary>
    OMA_DISCRETE_MEDIA_HEADERS_BOX = 1868851301,
    /// <summary>
    /// odrm
    /// </summary>
    OMA_DRM_CONTAINER_BOX = 1868853869,
    /// <summary>
    /// icnu
    /// </summary>
    OMA_ICON_URI_BOX = 1768124021,
    /// <summary>
    /// infu
    /// </summary>
    OMA_INFO_URL_BOX = 1768842869,
    /// <summary>
    /// lrcu
    /// </summary>
    OMA_LYRICS_URI_BOX = 1819435893,
    /// <summary>
    /// mdri
    /// </summary>
    OMA_MUTABLE_DRM_INFORMATION_BOX = 1835299433,
    /// <summary>
    /// odkm
    /// </summary>
    OMA_KEY_MANAGEMENT_BOX = 1868852077,
    /// <summary>
    /// odrb
    /// </summary>
    OMA_RIGHTS_OBJECT_BOX = 1868853858,
    /// <summary>
    /// odtt
    /// </summary>
    OMA_TRANSACTION_TRACKING_BOX = 1868854388,

    // --- iTunes DRM (FairPlay) ---

    /// <summary>
    /// user
    /// </summary>
    FAIRPLAY_USER_ID_BOX = 1970496882,
    /// <summary>
    /// name
    /// </summary>
    FAIRPLAY_USER_NAME_BOX = 1851878757,
    /// <summary>
    /// "key "
    /// </summary>
    FAIRPLAY_USER_KEY_BOX = 1801812256,
    /// <summary>
    /// iviv
    /// </summary>
    FAIRPLAY_IV_BOX = 1769367926,
    /// <summary>
    /// priv
    /// </summary>
    FAIRPLAY_PRIVATE_KEY_BOX = 1886546294,
  }
}
