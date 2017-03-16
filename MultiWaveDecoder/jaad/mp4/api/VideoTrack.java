public class VideoTrack extends Track {

	public enum VideoCodec implements Codec {

		AVC,
		H263,
		MP4_ASP,
		UNKNOWN_VIDEO_CODEC;

		static Codec forType(long type) {
			Codec ac;
			if(type==BoxType.AVC_SAMPLE_ENTRY) ac = AVC;
			else if(type==BoxType.H263_SAMPLE_ENTRY) ac = H263;
			else if(type==BoxType.MP4V_SAMPLE_ENTRY) ac = MP4_ASP;
			else ac = UNKNOWN_VIDEO_CODEC;
			return ac;
		}
	}
	private VideoMediaHeaderBox vmhd;
	private VideoSampleEntry sampleEntry;
	private Codec codec;

	@Override
	public Codec getCodec() {
		return codec;
	}

	public int getWidth() {
		return (sampleEntry!=null) ? sampleEntry.getWidth() : 0;
	}

	public int getHeight() {
		return (sampleEntry!=null) ? sampleEntry.getHeight() : 0;
	}

	public double getHorizontalResolution() {
		return (sampleEntry!=null) ? sampleEntry.getHorizontalResolution() : 0;
	}

	public double getVerticalResolution() {
		return (sampleEntry!=null) ? sampleEntry.getVerticalResolution() : 0;
	}

	public int getFrameCount() {
		return (sampleEntry!=null) ? sampleEntry.getFrameCount() : 0;
	}

	public string getCompressorName() {
		return (sampleEntry!=null) ? sampleEntry.getCompressorName() : "";
	}

	public int getDepth() {
		return (sampleEntry!=null) ? sampleEntry.getDepth() : 0;
	}

	public int getLayer() {
		return tkhd.getLayer();
	}
}
