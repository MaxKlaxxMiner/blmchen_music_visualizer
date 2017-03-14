
public class BoxFactory implements BoxTypes 
{
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
		BOX_CLASSES.put(CHUNK_LARGE_OFFSET_BOX, ChunkOffsetBox.class);
		BOX_CLASSES.put(CLEAN_APERTURE_BOX, CleanApertureBox.class);
		BOX_CLASSES.put(COMPACT_SAMPLE_SIZE_BOX, SampleSizeBox.class);
		BOX_CLASSES.put(DATA_ENTRY_URN_BOX, DataEntryUrnBox.class);
		BOX_CLASSES.put(DEGRADATION_PRIORITY_BOX, DegradationPriorityBox.class);
		BOX_CLASSES.put(FD_ITEM_INFORMATION_BOX, FDItemInformationBox.class);
		BOX_CLASSES.put(FD_SESSION_GROUP_BOX, FDSessionGroupBox.class);
		BOX_CLASSES.put(FEC_RESERVOIR_BOX, FECReservoirBox.class);
		BOX_CLASSES.put(FILE_PARTITION_BOX, FilePartitionBox.class);
		BOX_CLASSES.put(GROUP_ID_TO_NAME_BOX, GroupIDToNameBox.class);
		BOX_CLASSES.put(IPMP_CONTROL_BOX, IPMPControlBox.class);
		BOX_CLASSES.put(IPMP_INFO_BOX, IPMPInfoBox.class);
		BOX_CLASSES.put(ITEM_INFORMATION_BOX, ItemInformationBox.class);
		BOX_CLASSES.put(ITEM_INFORMATION_ENTRY, ItemInformationEntry.class);
		BOX_CLASSES.put(ITEM_LOCATION_BOX, ItemLocationBox.class);
		BOX_CLASSES.put(ITEM_PROTECTION_BOX, ItemProtectionBox.class);
		BOX_CLASSES.put(HINT_MEDIA_HEADER_BOX, HintMediaHeaderBox.class);
		BOX_CLASSES.put(META_BOX_RELATION_BOX, MetaBoxRelationBox.class);
		BOX_CLASSES.put(MOVIE_EXTENDS_BOX, BoxImpl.class);
		BOX_CLASSES.put(MOVIE_EXTENDS_HEADER_BOX, MovieExtendsHeaderBox.class);
		BOX_CLASSES.put(MOVIE_FRAGMENT_BOX, BoxImpl.class);
		BOX_CLASSES.put(MOVIE_FRAGMENT_HEADER_BOX, MovieFragmentHeaderBox.class);
		BOX_CLASSES.put(MOVIE_FRAGMENT_RANDOM_ACCESS_BOX, BoxImpl.class);
		BOX_CLASSES.put(MOVIE_FRAGMENT_RANDOM_ACCESS_OFFSET_BOX, MovieFragmentRandomAccessOffsetBox.class);
		BOX_CLASSES.put(NERO_METADATA_TAGS_BOX, NeroMetadataTagsBox.class);
		BOX_CLASSES.put(ORIGINAL_FORMAT_BOX, OriginalFormatBox.class);
		BOX_CLASSES.put(PADDING_BIT_BOX, PaddingBitBox.class);
		BOX_CLASSES.put(PARTITION_ENTRY, BoxImpl.class);
		BOX_CLASSES.put(PIXEL_ASPECT_RATIO_BOX, PixelAspectRatioBox.class);
		BOX_CLASSES.put(PRIMARY_ITEM_BOX, PrimaryItemBox.class);
		BOX_CLASSES.put(PROGRESSIVE_DOWNLOAD_INFORMATION_BOX, ProgressiveDownloadInformationBox.class);
		BOX_CLASSES.put(PROTECTION_SCHEME_INFORMATION_BOX, BoxImpl.class);
		BOX_CLASSES.put(SAMPLE_DEPENDENCY_TYPE_BOX, SampleDependencyTypeBox.class);
		BOX_CLASSES.put(SAMPLE_GROUP_DESCRIPTION_BOX, SampleGroupDescriptionBox.class);
		BOX_CLASSES.put(SAMPLE_SCALE_BOX, SampleScaleBox.class);
		BOX_CLASSES.put(SAMPLE_TO_GROUP_BOX, SampleToGroupBox.class);
		BOX_CLASSES.put(SCHEME_TYPE_BOX, SchemeTypeBox.class);
		BOX_CLASSES.put(SHADOW_SYNC_SAMPLE_BOX, ShadowSyncSampleBox.class);
		BOX_CLASSES.put(SUB_SAMPLE_INFORMATION_BOX, SubSampleInformationBox.class);
		BOX_CLASSES.put(TRACK_EXTENDS_BOX, TrackExtendsBox.class);
		BOX_CLASSES.put(TRACK_FRAGMENT_BOX, BoxImpl.class);
		BOX_CLASSES.put(TRACK_FRAGMENT_HEADER_BOX, TrackFragmentHeaderBox.class);
		BOX_CLASSES.put(TRACK_FRAGMENT_RANDOM_ACCESS_BOX, TrackFragmentRandomAccessBox.class);
		BOX_CLASSES.put(TRACK_FRAGMENT_RUN_BOX, TrackFragmentRunBox.class);
		BOX_CLASSES.put(TRACK_REFERENCE_BOX, TrackReferenceBox.class);
		BOX_CLASSES.put(TRACK_SELECTION_BOX, TrackSelectionBox.class);
		BOX_CLASSES.put(XML_BOX, XMLBox.class);
		BOX_CLASSES.put(SAMPLE_DEPENDENCY_BOX, SampleDependencyBox.class);
		BOX_CLASSES.put(ID3_TAG_BOX, ID3TagBox.class);
		BOX_CLASSES.put(ITUNES_METADATA_BOX, ITunesMetadataBox.class);
		BOX_CLASSES.put(ITUNES_METADATA_NAME_BOX, ITunesMetadataNameBox.class);
		BOX_CLASSES.put(ALBUM_ARTIST_SORT_BOX, BoxImpl.class);
		BOX_CLASSES.put(CATEGORY_BOX, BoxImpl.class);
		BOX_CLASSES.put(COMMENTS_BOX, BoxImpl.class);
		BOX_CLASSES.put(COMPOSER_SORT_BOX, BoxImpl.class);
		BOX_CLASSES.put(CUSTOM_GENRE_BOX, BoxImpl.class);
		BOX_CLASSES.put(DESCRIPTION_BOX, BoxImpl.class);
		BOX_CLASSES.put(EPISODE_GLOBAL_UNIQUE_ID_BOX, BoxImpl.class);
		BOX_CLASSES.put(GROUPING_BOX, BoxImpl.class);
		BOX_CLASSES.put(HD_VIDEO_BOX, BoxImpl.class);
		BOX_CLASSES.put(ITUNES_ACCOUNT_TYPE_BOX, BoxImpl.class);
		BOX_CLASSES.put(KEYWORD_BOX, BoxImpl.class);
		BOX_CLASSES.put(LONG_DESCRIPTION_BOX, BoxImpl.class);
		BOX_CLASSES.put(LYRICS_BOX, BoxImpl.class);
		BOX_CLASSES.put(PODCAST_BOX, BoxImpl.class);
		BOX_CLASSES.put(PODCAST_URL_BOX, BoxImpl.class);
		BOX_CLASSES.put(TEMPO_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_EPISODE_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_EPISODE_NUMBER_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_NETWORK_NAME_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_SEASON_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_SHOW_BOX, BoxImpl.class);
		BOX_CLASSES.put(TV_SHOW_SORT_BOX, BoxImpl.class);
		BOX_CLASSES.put(THREE_GPP_AUTHOR_BOX, ThreeGPPMetadataBox.class);
		BOX_CLASSES.put(THREE_GPP_CLASSIFICATION_BOX, ThreeGPPMetadataBox.class);
		BOX_CLASSES.put(THREE_GPP_DESCRIPTION_BOX, ThreeGPPMetadataBox.class);
		BOX_CLASSES.put(THREE_GPP_KEYWORDS_BOX, ThreeGPPKeywordsBox.class);
		BOX_CLASSES.put(THREE_GPP_LOCATION_INFORMATION_BOX, ThreeGPPLocationBox.class);
		BOX_CLASSES.put(GOOGLE_HOST_HEADER_BOX, BoxImpl.class);
		BOX_CLASSES.put(GOOGLE_PING_MESSAGE_BOX, BoxImpl.class);
		BOX_CLASSES.put(GOOGLE_PING_URL_BOX, BoxImpl.class);
		BOX_CLASSES.put(GOOGLE_SOURCE_DATA_BOX, BoxImpl.class);
		BOX_CLASSES.put(GOOGLE_START_TIME_BOX, BoxImpl.class);
		BOX_CLASSES.put(GOOGLE_TRACK_DURATION_BOX, BoxImpl.class);
		BOX_CLASSES.put(H263_SAMPLE_ENTRY, VideoSampleEntry.class);
		BOX_CLASSES.put(ENCRYPTED_VIDEO_SAMPLE_ENTRY, VideoSampleEntry.class);
		BOX_CLASSES.put(AC3_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(EAC3_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(DRMS_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(AMR_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(AMR_WB_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(EVRC_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(QCELP_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(SMV_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(ENCRYPTED_AUDIO_SAMPLE_ENTRY, AudioSampleEntry.class);
		BOX_CLASSES.put(TEXT_METADATA_SAMPLE_ENTRY, TextMetadataSampleEntry.class);
		BOX_CLASSES.put(XML_METADATA_SAMPLE_ENTRY, XMLMetadataSampleEntry.class);
		BOX_CLASSES.put(FD_HINT_SAMPLE_ENTRY, FDHintSampleEntry.class);
		BOX_CLASSES.put(H263_SPECIFIC_BOX, H263SpecificBox.class);
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
		BOX_CLASSES.put(FAIRPLAY_USER_KEY_BOX, FairPlayDataBox.class);
		BOX_CLASSES.put(FAIRPLAY_IV_BOX, FairPlayDataBox.class);
		BOX_CLASSES.put(FAIRPLAY_PRIVATE_KEY_BOX, FairPlayDataBox.class);
		//parameter
		PARAMETER.put(ADDITIONAL_METADATA_CONTAINER_BOX, new string[]{"Additional Metadata Container Box"});
		PARAMETER.put(MOVIE_EXTENDS_BOX, new string[]{"Movie Extends Box"});
		PARAMETER.put(MOVIE_FRAGMENT_BOX, new string[]{"Movie Fragment Box"});
		PARAMETER.put(MOVIE_FRAGMENT_RANDOM_ACCESS_BOX, new string[]{"Movie Fragment Random Access Box"});
		PARAMETER.put(PARTITION_ENTRY, new string[]{"Partition Entry"});
		PARAMETER.put(PROTECTION_SCHEME_INFORMATION_BOX, new string[]{"Protection Scheme Information Box"});
		PARAMETER.put(TRACK_FRAGMENT_BOX, new string[]{"Track Fragment Box"});
		PARAMETER.put(ALBUM_ARTIST_SORT_BOX, new string[]{"Album Artist Sort Box"});
		PARAMETER.put(CATEGORY_BOX, new string[]{"Category Box"});
		PARAMETER.put(COMMENTS_BOX, new string[]{"Comments Box"});
		PARAMETER.put(COMPOSER_SORT_BOX, new string[]{"Composer Sort Box"});
		PARAMETER.put(CUSTOM_GENRE_BOX, new string[]{"Custom Genre Box"});
		PARAMETER.put(DESCRIPTION_BOX, new string[]{"Description Cover Box"});
		PARAMETER.put(EPISODE_GLOBAL_UNIQUE_ID_BOX, new string[]{"Episode Global Unique ID Box"});
		PARAMETER.put(GROUPING_BOX, new string[]{"Grouping Box"});
		PARAMETER.put(HD_VIDEO_BOX, new string[]{"HD Video Box"});
		PARAMETER.put(ITUNES_ACCOUNT_TYPE_BOX, new string[]{"iTunes Account Type Box"});
		PARAMETER.put(KEYWORD_BOX, new string[]{"Keyword Box"});
		PARAMETER.put(LONG_DESCRIPTION_BOX, new string[]{"Long Description Box"});
		PARAMETER.put(LYRICS_BOX, new string[]{"Lyrics Box"});
		PARAMETER.put(PODCAST_BOX, new string[]{"Podcast Box"});
		PARAMETER.put(PODCAST_URL_BOX, new string[]{"Podcast URL Box"});
		PARAMETER.put(TEMPO_BOX, new string[]{"Tempo Box"});
		PARAMETER.put(TV_EPISODE_BOX, new string[]{"TV Episode Box"});
		PARAMETER.put(TV_EPISODE_NUMBER_BOX, new string[]{"TV Episode Number Box"});
		PARAMETER.put(TV_NETWORK_NAME_BOX, new string[]{"TV Network Name Box"});
		PARAMETER.put(TV_SEASON_BOX, new string[]{"TV Season Box"});
		PARAMETER.put(TV_SHOW_BOX, new string[]{"TV Show Box"});
		PARAMETER.put(TV_SHOW_SORT_BOX, new string[]{"TV Show Sort Box"});
		PARAMETER.put(THREE_GPP_AUTHOR_BOX, new string[]{"3GPP Author Box"});
		PARAMETER.put(THREE_GPP_CLASSIFICATION_BOX, new string[]{"3GPP Classification Box"});
		PARAMETER.put(THREE_GPP_DESCRIPTION_BOX, new string[]{"3GPP Description Box"});
1		PARAMETER.put(GOOGLE_HOST_HEADER_BOX, new string[]{"Google Host Header Box"});
		PARAMETER.put(GOOGLE_PING_MESSAGE_BOX, new string[]{"Google Ping Message Box"});
		PARAMETER.put(GOOGLE_PING_URL_BOX, new string[]{"Google Ping URL Box"});
		PARAMETER.put(GOOGLE_SOURCE_DATA_BOX, new string[]{"Google Source Data Box"});
		PARAMETER.put(GOOGLE_START_TIME_BOX, new string[]{"Google Start Time Box"});
		PARAMETER.put(GOOGLE_TRACK_DURATION_BOX, new string[]{"Google Track Duration Box"});
		PARAMETER.put(H263_SAMPLE_ENTRY, new string[]{"H263 Video Sample Entry"});
		PARAMETER.put(ENCRYPTED_VIDEO_SAMPLE_ENTRY, new string[]{"Encrypted Video Sample Entry"});
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
}
