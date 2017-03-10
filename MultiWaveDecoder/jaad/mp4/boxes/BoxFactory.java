package net.sourceforge.jaad.mp4.boxes;

import java.io.IOException;
import java.lang.reflect.Constructor;
import java.util.HashMap;
import java.util.Map;
import java.util.logging.ConsoleHandler;
import java.util.logging.Handler;
import java.util.logging.Level;
import java.util.logging.Logger;
import net.sourceforge.jaad.mp4.MP4InputStream;
import net.sourceforge.jaad.mp4.boxes.impl.*;
import net.sourceforge.jaad.mp4.boxes.impl.fd.*;
import net.sourceforge.jaad.mp4.boxes.impl.meta.*;
import net.sourceforge.jaad.mp4.boxes.impl.oma.*;
import net.sourceforge.jaad.mp4.boxes.impl.sampleentries.*;
import net.sourceforge.jaad.mp4.boxes.impl.sampleentries.codec.*;
import net.sourceforge.jaad.mp4.boxes.impl.ESDBox;
import net.sourceforge.jaad.mp4.boxes.impl.drm.FairPlayDataBox;

public class BoxFactory implements BoxTypes {

	private static Logger LOGGER = Logger.getLogger("MP4 Boxes");

	static {
		for(Handler h : LOGGER.getHandlers()) {
			LOGGER.removeHandler(h);
		}
		LOGGER.setLevel(Level.WARNING);

		ConsoleHandler h = new ConsoleHandler();
		h.setLevel(Level.ALL);
		LOGGER.addHandler(h);
	}
	private static Map<Long, Class<? extends BoxImpl>> BOX_CLASSES = new HashMap<Long, Class<? extends BoxImpl>>();
	private static Map<Long, Class<? extends BoxImpl>[]> BOX_MULTIPLE_CLASSES = new HashMap<Long, Class<? extends BoxImpl>[]>();
	private static Map<Long, string[]> PARAMETER = new HashMap<Long, string[]>();

	static {
		//classes
		BOX_CLASSES.put(ADDITIONAL_METADATA_CONTAINER_BOX, BoxImpl.class);
		BOX_CLASSES.put(APPLE_LOSSLESS_BOX, AppleLosslessBox.class);
		BOX_CLASSES.put(BINARY_XML_BOX, BinaryXMLBox.class);
		BOX_CLASSES.put(BIT_RATE_BOX, BitRateBox.class);
		BOX_CLASSES.put(CHAPTER_BOX, ChapterBox.class);
		BOX_CLASSES.put(CHUNK_OFFSET_BOX, ChunkOffsetBox.class);
		BOX_CLASSES.put(CHUNK_LARGE_OFFSET_BOX, ChunkOffsetBox.class);
		BOX_CLASSES.put(CLEAN_APERTURE_BOX, CleanApertureBox.class);
		BOX_CLASSES.put(COMPACT_SAMPLE_SIZE_BOX, SampleSizeBox.class);
		BOX_CLASSES.put(COMPOSITION_TIME_TO_SAMPLE_BOX, CompositionTimeToSampleBox.class);
		BOX_CLASSES.put(COPYRIGHT_BOX, CopyrightBox.class);
		BOX_CLASSES.put(DATA_ENTRY_URN_BOX, DataEntryUrnBox.class);
		BOX_CLASSES.put(DATA_ENTRY_URL_BOX, DataEntryUrlBox.class);
		BOX_CLASSES.put(DATA_INFORMATION_BOX, BoxImpl.class);
		BOX_CLASSES.put(DATA_REFERENCE_BOX, DataReferenceBox.class);
		BOX_CLASSES.put(DECODING_TIME_TO_SAMPLE_BOX, DecodingTimeToSampleBox.class);
		BOX_CLASSES.put(DEGRADATION_PRIORITY_BOX, DegradationPriorityBox.class);
		BOX_CLASSES.put(EDIT_BOX, BoxImpl.class);
		BOX_CLASSES.put(EDIT_LIST_BOX, EditListBox.class);
		BOX_CLASSES.put(FD_ITEM_INFORMATION_BOX, FDItemInformationBox.class);
		BOX_CLASSES.put(FD_SESSION_GROUP_BOX, FDSessionGroupBox.class);
		BOX_CLASSES.put(FEC_RESERVOIR_BOX, FECReservoirBox.class);
		BOX_CLASSES.put(FILE_PARTITION_BOX, FilePartitionBox.class);
		BOX_CLASSES.put(FILE_TYPE_BOX, FileTypeBox.class);
		BOX_CLASSES.put(FREE_SPACE_BOX, FreeSpaceBox.class);
		BOX_CLASSES.put(GROUP_ID_TO_NAME_BOX, GroupIDToNameBox.class);
		BOX_CLASSES.put(HANDLER_BOX, HandlerBox.class);
		BOX_CLASSES.put(HINT_MEDIA_HEADER_BOX, HintMediaHeaderBox.class);
		BOX_CLASSES.put(IPMP_CONTROL_BOX, IPMPControlBox.class);
		BOX_CLASSES.put(IPMP_INFO_BOX, IPMPInfoBox.class);
		BOX_CLASSES.put(ITEM_INFORMATION_BOX, ItemInformationBox.class);
		BOX_CLASSES.put(ITEM_INFORMATION_ENTRY, ItemInformationEntry.class);
		BOX_CLASSES.put(ITEM_LOCATION_BOX, ItemLocationBox.class);
		BOX_CLASSES.put(ITEM_PROTECTION_BOX, ItemProtectionBox.class);
		BOX_CLASSES.put(MEDIA_BOX, BoxImpl.class);
		BOX_CLASSES.put(MEDIA_DATA_BOX, MediaDataBox.class);
		BOX_CLASSES.put(MEDIA_HEADER_BOX, MediaHeaderBox.class);
		BOX_CLASSES.put(MEDIA_INFORMATION_BOX, BoxImpl.class);
		BOX_CLASSES.put(META_BOX, MetaBox.class);
		BOX_CLASSES.put(META_BOX_RELATION_BOX, MetaBoxRelationBox.class);
		BOX_CLASSES.put(MOVIE_BOX, BoxImpl.class);
		BOX_CLASSES.put(MOVIE_EXTENDS_BOX, BoxImpl.class);
		BOX_CLASSES.put(MOVIE_EXTENDS_HEADER_BOX, MovieExtendsHeaderBox.class);
		BOX_CLASSES.put(MOVIE_FRAGMENT_BOX, BoxImpl.class);
		BOX_CLASSES.put(MOVIE_FRAGMENT_HEADER_BOX, MovieFragmentHeaderBox.class);
		BOX_CLASSES.put(MOVIE_FRAGMENT_RANDOM_ACCESS_BOX, BoxImpl.class);
		BOX_CLASSES.put(MOVIE_FRAGMENT_RANDOM_ACCESS_OFFSET_BOX, MovieFragmentRandomAccessOffsetBox.class);
		BOX_CLASSES.put(MOVIE_HEADER_BOX, MovieHeaderBox.class);
		BOX_CLASSES.put(NERO_METADATA_TAGS_BOX, NeroMetadataTagsBox.class);
		BOX_CLASSES.put(NULL_MEDIA_HEADER_BOX, FullBox.class);
		BOX_CLASSES.put(ORIGINAL_FORMAT_BOX, OriginalFormatBox.class);
		BOX_CLASSES.put(PADDING_BIT_BOX, PaddingBitBox.class);
		BOX_CLASSES.put(PARTITION_ENTRY, BoxImpl.class);
		BOX_CLASSES.put(PIXEL_ASPECT_RATIO_BOX, PixelAspectRatioBox.class);
		BOX_CLASSES.put(PRIMARY_ITEM_BOX, PrimaryItemBox.class);
		BOX_CLASSES.put(PROGRESSIVE_DOWNLOAD_INFORMATION_BOX, ProgressiveDownloadInformationBox.class);
		BOX_CLASSES.put(PROTECTION_SCHEME_INFORMATION_BOX, BoxImpl.class);
		BOX_CLASSES.put(SAMPLE_DEPENDENCY_TYPE_BOX, SampleDependencyTypeBox.class);
		BOX_CLASSES.put(SAMPLE_DESCRIPTION_BOX, SampleDescriptionBox.class);
		BOX_CLASSES.put(SAMPLE_GROUP_DESCRIPTION_BOX, SampleGroupDescriptionBox.class);
		BOX_CLASSES.put(SAMPLE_SCALE_BOX, SampleScaleBox.class);
		BOX_CLASSES.put(SAMPLE_SIZE_BOX, SampleSizeBox.class);
		BOX_CLASSES.put(SAMPLE_TABLE_BOX, BoxImpl.class);
		BOX_CLASSES.put(SAMPLE_TO_CHUNK_BOX, SampleToChunkBox.class);
		BOX_CLASSES.put(SAMPLE_TO_GROUP_BOX, SampleToGroupBox.class);
		BOX_CLASSES.put(SCHEME_TYPE_BOX, SchemeTypeBox.class);
		BOX_CLASSES.put(SCHEME_INFORMATION_BOX, BoxImpl.class);
		BOX_CLASSES.put(SHADOW_SYNC_SAMPLE_BOX, ShadowSyncSampleBox.class);
		BOX_CLASSES.put(SKIP_BOX, FreeSpaceBox.class);
		BOX_CLASSES.put(SOUND_MEDIA_HEADER_BOX, SoundMediaHeaderBox.class);
		BOX_CLASSES.put(SUB_SAMPLE_INFORMATION_BOX, SubSampleInformationBox.class);
		BOX_CLASSES.put(SYNC_SAMPLE_BOX, SyncSampleBox.class);
		BOX_CLASSES.put(TRACK_BOX, BoxImpl.class);
		BOX_CLASSES.put(TRACK_EXTENDS_BOX, TrackExtendsBox.class);
		BOX_CLASSES.put(TRACK_FRAGMENT_BOX, BoxImpl.class);
		BOX_CLASSES.put(TRACK_FRAGMENT_HEADER_BOX, TrackFragmentHeaderBox.class);
		BOX_CLASSES.put(TRACK_FRAGMENT_RANDOM_ACCESS_BOX, TrackFragmentRandomAccessBox.class);
		BOX_CLASSES.put(TRACK_FRAGMENT_RUN_BOX, TrackFragmentRunBox.class);
		BOX_CLASSES.put(TRACK_HEADER_BOX, TrackHeaderBox.class);
		BOX_CLASSES.put(TRACK_REFERENCE_BOX, TrackReferenceBox.class);
		BOX_CLASSES.put(TRACK_SELECTION_BOX, TrackSelectionBox.class);
		BOX_CLASSES.put(USER_DATA_BOX, BoxImpl.class);
		BOX_CLASSES.put(VIDEO_MEDIA_HEADER_BOX, VideoMediaHeaderBox.class);
		BOX_CLASSES.put(WIDE_BOX, FreeSpaceBox.class);
		BOX_CLASSES.put(XML_BOX, XMLBox.class);
		BOX_CLASSES.put(OBJECT_DESCRIPTOR_BOX, ObjectDescriptorBox.class);
		BOX_CLASSES.put(SAMPLE_DEPENDENCY_BOX, SampleDependencyBox.class);
		BOX_CLASSES.put(ID3_TAG_BOX, ID3TagBox.class);
		BOX_CLASSES.put(ITUNES_META_LIST_BOX, BoxImpl.class);
		BOX_CLASSES.put(CUSTOM_ITUNES_METADATA_BOX, BoxImpl.class);
		BOX_CLASSES.put(ITUNES_METADATA_BOX, ITunesMetadataBox.class);
		BOX_CLASSES.put(ITUNES_METADATA_NAME_BOX, ITunesMetadataNameBox.class);
		BOX_CLASSES.put(ITUNES_METADATA_MEAN_BOX, ITunesMetadataMeanBox.class);
		BOX_CLASSES.put(ALBUM_ARTIST_NAME_BOX, BoxImpl.class);
		BOX_CLASSES.put(ALBUM_ARTIST_SORT_BOX, BoxImpl.class);
		BOX_CLASSES.put(ALBUM_NAME_BOX, BoxImpl.class);
		BOX_CLASSES.put(ALBUM_SORT_BOX, BoxImpl.class);
		BOX_CLASSES.put(ARTIST_NAME_BOX, BoxImpl.class);
		BOX_CLASSES.put(ARTIST_SORT_BOX, BoxImpl.class);
		BOX_CLASSES.put(CATEGORY_BOX, BoxImpl.class);
		BOX_CLASSES.put(COMMENTS_BOX, BoxImpl.class);
		BOX_CLASSES.put(COMPILATION_PART_BOX, BoxImpl.class);
		BOX_CLASSES.put(COMPOSER_NAME_BOX, BoxImpl.class);
		BOX_CLASSES.put(COMPOSER_SORT_BOX, BoxImpl.class);
		BOX_CLASSES.put(COVER_BOX, BoxImpl.class);
		BOX_CLASSES.put(CUSTOM_GENRE_BOX, BoxImpl.class);
		BOX_CLASSES.put(DESCRIPTION_BOX, BoxImpl.class);
		BOX_CLASSES.put(DISK_NUMBER_BOX, BoxImpl.class);
		BOX_CLASSES.put(ENCODER_NAME_BOX, EncoderBox.class);
		BOX_CLASSES.put(ENCODER_TOOL_BOX, EncoderBox.class);
		BOX_CLASSES.put(EPISODE_GLOBAL_UNIQUE_ID_BOX, BoxImpl.class);
		BOX_CLASSES.put(GAPLESS_PLAYBACK_BOX, BoxImpl.class);
		BOX_CLASSES.put(GENRE_BOX, GenreBox.class);
		BOX_CLASSES.put(GROUPING_BOX, BoxImpl.class);
		BOX_CLASSES.put(HD_VIDEO_BOX, BoxImpl.class);
		BOX_CLASSES.put(ITUNES_PURCHASE_ACCOUNT_BOX, BoxImpl.class);
		BOX_CLASSES.put(ITUNES_ACCOUNT_TYPE_BOX, BoxImpl.class);
		BOX_CLASSES.put(ITUNES_CATALOGUE_ID_BOX, BoxImpl.class);
		BOX_CLASSES.put(ITUNES_COUNTRY_CODE_BOX, BoxImpl.class);
		BOX_CLASSES.put(KEYWORD_BOX, BoxImpl.class);
		BOX_CLASSES.put(LONG_DESCRIPTION_BOX, BoxImpl.class);
		BOX_CLASSES.put(LYRICS_BOX, BoxImpl.class);
		BOX_CLASSES.put(META_TYPE_BOX, BoxImpl.class);
		BOX_CLASSES.put(PODCAST_BOX, BoxImpl.class);
		BOX_CLASSES.put(PODCAST_URL_BOX, BoxImpl.class);
		BOX_CLASSES.put(PURCHASE_DATE_BOX, BoxImpl.class);
		BOX_CLASSES.put(RATING_BOX, RatingBox.class);
		BOX_CLASSES.put(RELEASE_DATE_BOX, BoxImpl.class);
		BOX_CLASSES.put(REQUIREMENT_BOX, RequirementBox.class);
		BOX_CLASSES.put(TEMPO_BOX, BoxImpl.class);
		BOX_CLASSES.put(TRACK_NAME_BOX, BoxImpl.class);
		BOX_CLASSES.put(TRACK_NUMBER_BOX, BoxImpl.class);
		BOX_CLASSES.put(TRACK_SORT_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_EPISODE_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_EPISODE_NUMBER_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_NETWORK_NAME_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_SEASON_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_SHOW_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_SHOW_SORT_BOX, BoxImpl.class);
		BOX_CLASSES.put(THREE_GPP_ALBUM_BOX, ThreeGPPAlbumBox.class);
		BOX_CLASSES.put(THREE_GPP_AUTHOR_BOX, ThreeGPPMetadataBox.class);
		BOX_CLASSES.put(THREE_GPP_CLASSIFICATION_BOX, ThreeGPPMetadataBox.class);
		BOX_CLASSES.put(THREE_GPP_DESCRIPTION_BOX, ThreeGPPMetadataBox.class);
		BOX_CLASSES.put(THREE_GPP_KEYWORDS_BOX, ThreeGPPKeywordsBox.class);
		BOX_CLASSES.put(THREE_GPP_LOCATION_INFORMATION_BOX, ThreeGPPLocationBox.class);
		BOX_CLASSES.put(THREE_GPP_PERFORMER_BOX, ThreeGPPMetadataBox.class);
		BOX_CLASSES.put(THREE_GPP_RECORDING_YEAR_BOX, ThreeGPPRecordingYearBox.class);
		BOX_CLASSES.put(THREE_GPP_TITLE_BOX, ThreeGPPMetadataBox.class);
		BOX_CLASSES.put(GOOGLE_HOST_HEADER_BOX, BoxImpl.class);
		BOX_CLASSES.put(GOOGLE_PING_MESSAGE_BOX, BoxImpl.class);
		BOX_CLASSES.put(GOOGLE_PING_URL_BOX, BoxImpl.class);
		BOX_CLASSES.put(GOOGLE_SOURCE_DATA_BOX, BoxImpl.class);
		BOX_CLASSES.put(GOOGLE_START_TIME_BOX, BoxImpl.class);
		BOX_CLASSES.put(GOOGLE_TRACK_DURATION_BOX, BoxImpl.class);
		BOX_CLASSES.put(MP4V_SAMPLE_ENTRY, VideoSampleEntry.class);
		BOX_CLASSES.put(H263_SAMPLE_ENTRY, VideoSampleEntry.class);
		BOX_CLASSES.put(ENCRYPTED_VIDEO_SAMPLE_ENTRY, VideoSampleEntry.class);
		BOX_CLASSES.put(AVC_SAMPLE_ENTRY, VideoSampleEntry.class);
		BOX_CLASSES.put(MP4A_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(AC3_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(EAC3_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(DRMS_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(AMR_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(AMR_WB_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(EVRC_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(QCELP_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(SMV_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(ENCRYPTED_AUDIO_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(MPEG_SAMPLE_ENTRY, MPEGSampleEntry.class);
		BOX_CLASSES.put(TEXT_METADATA_SAMPLE_ENTRY, TextMetadataSampleEntry.class);
		BOX_CLASSES.put(XML_METADATA_SAMPLE_ENTRY, XMLMetadataSampleEntry.class);
		BOX_CLASSES.put(RTP_HINT_SAMPLE_ENTRY, RTPHintSampleEntry.class);
		BOX_CLASSES.put(FD_HINT_SAMPLE_ENTRY, FDHintSampleEntry.class);
		BOX_CLASSES.put(ESD_BOX, ESDBox.class);
		BOX_CLASSES.put(H263_SPECIFIC_BOX, H263SpecificBox.class);
		BOX_CLASSES.put(AVC_SPECIFIC_BOX, AVCSpecificBox.class);
		BOX_CLASSES.put(AC3_SPECIFIC_BOX, AC3SpecificBox.class);
		BOX_CLASSES.put(EAC3_SPECIFIC_BOX, EAC3SpecificBox.class);
		BOX_CLASSES.put(AMR_SPECIFIC_BOX, AMRSpecificBox.class);
		BOX_CLASSES.put(EVRC_SPECIFIC_BOX, EVRCSpecificBox.class);
		BOX_CLASSES.put(QCELP_SPECIFIC_BOX, QCELPSpecificBox.class);
		BOX_CLASSES.put(SMV_SPECIFIC_BOX, SMVSpecificBox.class);
		BOX_CLASSES.put(OMA_ACCESS_UNIT_FORMAT_BOX, OMAAccessUnitFormatBox.class);
		BOX_CLASSES.put(OMA_COMMON_HEADERS_BOX, OMACommonHeadersBox.class);
		BOX_CLASSES.put(OMA_CONTENT_ID_BOX, OMAContentIDBox.class);
		BOX_CLASSES.put(OMA_CONTENT_OBJECT_BOX, OMAContentObjectBox.class);
		BOX_CLASSES.put(OMA_COVER_URI_BOX, OMAURLBox.class);
		BOX_CLASSES.put(OMA_DISCRETE_MEDIA_HEADERS_BOX, OMADiscreteMediaHeadersBox.class);
		BOX_CLASSES.put(OMA_DRM_CONTAINER_BOX, FullBox.class);
		BOX_CLASSES.put(OMA_ICON_URI_BOX, OMAURLBox.class);
		BOX_CLASSES.put(OMA_INFO_URL_BOX, OMAURLBox.class);
		BOX_CLASSES.put(OMA_LYRICS_URI_BOX, OMAURLBox.class);
		BOX_CLASSES.put(OMA_MUTABLE_DRM_INFORMATION_BOX, BoxImpl.class);
		BOX_CLASSES.put(OMA_KEY_MANAGEMENT_BOX, FullBox.class);
		BOX_CLASSES.put(OMA_RIGHTS_OBJECT_BOX, OMARightsObjectBox.class);
		BOX_CLASSES.put(OMA_TRANSACTION_TRACKING_BOX, OMATransactionTrackingBox.class);
		BOX_CLASSES.put(FAIRPLAY_USER_ID_BOX, FairPlayDataBox.class);
		BOX_CLASSES.put(FAIRPLAY_USER_NAME_BOX, FairPlayDataBox.class);
		BOX_CLASSES.put(FAIRPLAY_USER_KEY_BOX, FairPlayDataBox.class);
		BOX_CLASSES.put(FAIRPLAY_IV_BOX, FairPlayDataBox.class);
		BOX_CLASSES.put(FAIRPLAY_PRIVATE_KEY_BOX, FairPlayDataBox.class);
		//parameter
		PARAMETER.put(ADDITIONAL_METADATA_CONTAINER_BOX, new string[]{"Additional Metadata Container Box"});
		PARAMETER.put(DATA_INFORMATION_BOX, new string[]{"Data Information Box"});
		PARAMETER.put(EDIT_BOX, new string[]{"Edit Box"});
		PARAMETER.put(MEDIA_BOX, new string[]{"Media Box"});
		PARAMETER.put(MEDIA_INFORMATION_BOX, new string[]{"Media Information Box"});
		PARAMETER.put(MOVIE_BOX, new string[]{"Movie Box"});
		PARAMETER.put(MOVIE_EXTENDS_BOX, new string[]{"Movie Extends Box"});
		PARAMETER.put(MOVIE_FRAGMENT_BOX, new string[]{"Movie Fragment Box"});
		PARAMETER.put(MOVIE_FRAGMENT_RANDOM_ACCESS_BOX, new string[]{"Movie Fragment Random Access Box"});
		PARAMETER.put(NULL_MEDIA_HEADER_BOX, new string[]{"Null Media Header Box"});
		PARAMETER.put(PARTITION_ENTRY, new string[]{"Partition Entry"});
		PARAMETER.put(PROTECTION_SCHEME_INFORMATION_BOX, new string[]{"Protection Scheme Information Box"});
		PARAMETER.put(SAMPLE_TABLE_BOX, new string[]{"Sample Table Box"});
		PARAMETER.put(SCHEME_INFORMATION_BOX, new string[]{"Scheme Information Box"});
		PARAMETER.put(TRACK_BOX, new string[]{"Track Box"});
		PARAMETER.put(TRACK_FRAGMENT_BOX, new string[]{"Track Fragment Box"});
		PARAMETER.put(USER_DATA_BOX, new string[]{"User Data Box"});
		PARAMETER.put(ITUNES_META_LIST_BOX, new string[]{"iTunes Meta List Box"});
		PARAMETER.put(CUSTOM_ITUNES_METADATA_BOX, new string[]{"Custom iTunes Metadata Box"});
		PARAMETER.put(ALBUM_ARTIST_NAME_BOX, new string[]{"Album Artist Name Box"});
		PARAMETER.put(ALBUM_ARTIST_SORT_BOX, new string[]{"Album Artist Sort Box"});
		PARAMETER.put(ALBUM_NAME_BOX, new string[]{"Album Name Box"});
		PARAMETER.put(ALBUM_SORT_BOX, new string[]{"Album Sort Box"});
		PARAMETER.put(ARTIST_NAME_BOX, new string[]{"Artist Name Box"});
		PARAMETER.put(ARTIST_SORT_BOX, new string[]{"Artist Sort Box"});
		PARAMETER.put(CATEGORY_BOX, new string[]{"Category Box"});
		PARAMETER.put(COMMENTS_BOX, new string[]{"Comments Box"});
		PARAMETER.put(COMPILATION_PART_BOX, new string[]{"Compilation Part Box"});
		PARAMETER.put(COMPOSER_NAME_BOX, new string[]{"Composer Name Box"});
		PARAMETER.put(COMPOSER_SORT_BOX, new string[]{"Composer Sort Box"});
		PARAMETER.put(COVER_BOX, new string[]{"Cover Box"});
		PARAMETER.put(CUSTOM_GENRE_BOX, new string[]{"Custom Genre Box"});
		PARAMETER.put(DESCRIPTION_BOX, new string[]{"Description Cover Box"});
		PARAMETER.put(DISK_NUMBER_BOX, new string[]{"Disk Number Box"});
		PARAMETER.put(EPISODE_GLOBAL_UNIQUE_ID_BOX, new string[]{"Episode Global Unique ID Box"});
		PARAMETER.put(GAPLESS_PLAYBACK_BOX, new string[]{"Gapless Playback Box"});
		PARAMETER.put(GROUPING_BOX, new string[]{"Grouping Box"});
		PARAMETER.put(HD_VIDEO_BOX, new string[]{"HD Video Box"});
		PARAMETER.put(ITUNES_PURCHASE_ACCOUNT_BOX, new string[]{"iTunes Purchase Account Box"});
		PARAMETER.put(ITUNES_ACCOUNT_TYPE_BOX, new string[]{"iTunes Account Type Box"});
		PARAMETER.put(ITUNES_CATALOGUE_ID_BOX, new string[]{"iTunes Catalogue ID Box"});
		PARAMETER.put(ITUNES_COUNTRY_CODE_BOX, new string[]{"iTunes Country Code Box"});
		PARAMETER.put(KEYWORD_BOX, new string[]{"Keyword Box"});
		PARAMETER.put(LONG_DESCRIPTION_BOX, new string[]{"Long Description Box"});
		PARAMETER.put(LYRICS_BOX, new string[]{"Lyrics Box"});
		PARAMETER.put(META_TYPE_BOX, new string[]{"Meta Type Box"});
		PARAMETER.put(PODCAST_BOX, new string[]{"Podcast Box"});
		PARAMETER.put(PODCAST_URL_BOX, new string[]{"Podcast URL Box"});
		PARAMETER.put(PURCHASE_DATE_BOX, new string[]{"Purchase Date Box"});
		PARAMETER.put(RELEASE_DATE_BOX, new string[]{"Release Date Box"});
		PARAMETER.put(TEMPO_BOX, new string[]{"Tempo Box"});
		PARAMETER.put(TRACK_NAME_BOX, new string[]{"Track Name Box"});
		PARAMETER.put(TRACK_NUMBER_BOX, new string[]{"Track Number Box"});
		PARAMETER.put(TRACK_SORT_BOX, new string[]{"Track Sort Box"});
		PARAMETER.put(TV_EPISODE_BOX, new string[]{"TV Episode Box"});
		PARAMETER.put(TV_EPISODE_NUMBER_BOX, new string[]{"TV Episode Number Box"});
		PARAMETER.put(TV_NETWORK_NAME_BOX, new string[]{"TV Network Name Box"});
		PARAMETER.put(TV_SEASON_BOX, new string[]{"TV Season Box"});
		PARAMETER.put(TV_SHOW_BOX, new string[]{"TV Show Box"});
		PARAMETER.put(TV_SHOW_SORT_BOX, new string[]{"TV Show Sort Box"});
		PARAMETER.put(THREE_GPP_AUTHOR_BOX, new string[]{"3GPP Author Box"});
		PARAMETER.put(THREE_GPP_CLASSIFICATION_BOX, new string[]{"3GPP Classification Box"});
		PARAMETER.put(THREE_GPP_DESCRIPTION_BOX, new string[]{"3GPP Description Box"});
		PARAMETER.put(THREE_GPP_PERFORMER_BOX, new string[]{"3GPP Performer Box"});
		PARAMETER.put(THREE_GPP_TITLE_BOX, new string[]{"3GPP Title Box"});
		PARAMETER.put(GOOGLE_HOST_HEADER_BOX, new string[]{"Google Host Header Box"});
		PARAMETER.put(GOOGLE_PING_MESSAGE_BOX, new string[]{"Google Ping Message Box"});
		PARAMETER.put(GOOGLE_PING_URL_BOX, new string[]{"Google Ping URL Box"});
		PARAMETER.put(GOOGLE_SOURCE_DATA_BOX, new string[]{"Google Source Data Box"});
		PARAMETER.put(GOOGLE_START_TIME_BOX, new string[]{"Google Start Time Box"});
		PARAMETER.put(GOOGLE_TRACK_DURATION_BOX, new string[]{"Google Track Duration Box"});
		PARAMETER.put(MP4V_SAMPLE_ENTRY, new string[]{"MPEG-4 Video Sample Entry"});
		PARAMETER.put(H263_SAMPLE_ENTRY, new string[]{"H263 Video Sample Entry"});
		PARAMETER.put(ENCRYPTED_VIDEO_SAMPLE_ENTRY, new string[]{"Encrypted Video Sample Entry"});
		PARAMETER.put(AVC_SAMPLE_ENTRY, new string[]{"AVC Video Sample Entry"});
		PARAMETER.put(MP4A_SAMPLE_ENTRY, new string[]{"MPEG- 4Audio Sample Entry"});
		PARAMETER.put(AC3_SAMPLE_ENTRY, new string[]{"AC-3 Audio Sample Entry"});
		PARAMETER.put(EAC3_SAMPLE_ENTRY, new string[]{"Extended AC-3 Audio Sample Entry"});
		PARAMETER.put(DRMS_SAMPLE_ENTRY, new string[]{"DRMS Audio Sample Entry"});
		PARAMETER.put(AMR_SAMPLE_ENTRY, new string[]{"AMR Audio Sample Entry"});
		PARAMETER.put(AMR_WB_SAMPLE_ENTRY, new string[]{"AMR-Wideband Audio Sample Entry"});
		PARAMETER.put(EVRC_SAMPLE_ENTRY, new string[]{"EVC Audio Sample Entry"});
		PARAMETER.put(QCELP_SAMPLE_ENTRY, new string[]{"QCELP Audio Sample Entry"});
		PARAMETER.put(SMV_SAMPLE_ENTRY, new string[]{"SMV Audio Sample Entry"});
		PARAMETER.put(ENCRYPTED_AUDIO_SAMPLE_ENTRY, new string[]{"Encrypted Audio Sample Entry"});
		PARAMETER.put(OMA_COVER_URI_BOX, new string[]{"OMA DRM Cover URI Box"});
		PARAMETER.put(OMA_DRM_CONTAINER_BOX, new string[]{"OMA DRM Container Box"});
		PARAMETER.put(OMA_ICON_URI_BOX, new string[]{"OMA DRM Icon URI Box"});
		PARAMETER.put(OMA_INFO_URL_BOX, new string[]{"OMA DRM Info URL Box"});
		PARAMETER.put(OMA_LYRICS_URI_BOX, new string[]{"OMA DRM Lyrics URI Box"});
		PARAMETER.put(OMA_MUTABLE_DRM_INFORMATION_BOX, new string[]{"OMA DRM Mutable DRM Information Box"});
	}

	public static Box parseBox(Box parent, MP4InputStream in) throws IOException {
		long offset = in.getOffset();

		long size = in.readBytes(4);
		long type = in.readBytes(4);
		if(size==1) size = in.readBytes(8);
		if(type==EXTENDED_TYPE) in.skipBytes(16);

		//error protection
		if(parent!=null) {
			long parentLeft = (parent.getOffset()+parent.getSize())-offset;
			if(size>parentLeft) throw new IOException("error while decoding box '"+typeToString(type)+"' at offset "+offset+": box too large for parent");
		}

		Logger.getLogger("MP4 Boxes").finest(typeToString(type));
		BoxImpl box = forType(type, in.getOffset());
		box.setParams(parent, size, type, offset);
		box.decode(in);

		//if box doesn't contain data it only contains children
		Class<?> cl = box.getClass();
		if(cl==BoxImpl.class||cl==FullBox.class) box.readChildren(in);

		//check bytes left
		long left = (box.getOffset()+box.getSize())-in.getOffset();
		if(left>0
				&&!(box instanceof MediaDataBox)
				&&!(box instanceof UnknownBox)
				&&!(box instanceof FreeSpaceBox)) LOGGER.log(Level.INFO, "bytes left after reading box {0}: left: {1}, offset: {2}", new Object[]{typeToString(type), left, in.getOffset()});
		else if(left<0) LOGGER.log(Level.SEVERE, "box {0} overread: {1} bytes, offset: {2}", new Object[]{typeToString(type), -left, in.getOffset()});

		//if mdat found and no random access, don't skip
		if(box.getType()!=MEDIA_DATA_BOX||in.hasRandomAccess()) in.skipBytes(left);
		return box;
	}

	//TODO: remove usages
	public static Box parseBox(MP4InputStream in, Class<? extends BoxImpl> boxClass) throws IOException {
		long offset = in.getOffset();

		long size = in.readBytes(4);
		long type = in.readBytes(4);
		if(size==1) size = in.readBytes(8);
		if(type==EXTENDED_TYPE) in.skipBytes(16);

		BoxImpl box = null;
		try {
			box = boxClass.newInstance();
		}
		catch(InstantiationException e) {
		}
		catch(IllegalAccessException e) {
		}

		if(box!=null) {
			box.setParams(null, size, type, offset);
			box.decode(in);
			long left = (box.getOffset()+box.getSize())-in.getOffset();
			in.skipBytes(left);
		}
		return box;
	}

	private static BoxImpl forType(long type, long offset) {
		BoxImpl box = null;

		Long l = Long.valueOf(type);
		if(BOX_CLASSES.containsKey(l)) {
			Class<? extends BoxImpl> cl = BOX_CLASSES.get(l);
			if(PARAMETER.containsKey(l)) {
				string[] s = PARAMETER.get(l);
				try {
					Constructor<? extends BoxImpl> con = cl.getConstructor(string.class);
					box = con.newInstance(s[0]);
				}
				catch(Exception e) {
					LOGGER.log(Level.SEVERE, "BoxFactory: could not call constructor for "+typeToString(type), e);
					box = new UnknownBox();
				}
			}
			else {
				try {
					box = cl.newInstance();
				}
				catch(Exception e) {
					LOGGER.log(Level.SEVERE, "BoxFactory: could not instantiate box "+typeToString(type), e);
				}
			}
		}

		if(box==null) {
			LOGGER.log(Level.INFO, "BoxFactory: unknown box type: {0}; position: {1}", new Object[]{typeToString(type), offset});
			box = new UnknownBox();
		}
		return box;
	}

	public static string typeToString(long l) {
		byte[] b = new byte[4];
		b[0] = (byte) ((l>>24)&0xFF);
		b[1] = (byte) ((l>>16)&0xFF);
		b[2] = (byte) ((l>>8)&0xFF);
		b[3] = (byte) (l&0xFF);
		return new string(b);
	}
}
