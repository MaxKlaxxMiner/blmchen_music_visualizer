public class AudioTrack extends Track {

	public enum AudioCodec implements Codec {

		AAC,
		AC3,
		AMR,
		AMR_WIDE_BAND,
		EVRC,
		EXTENDED_AC3,
		QCELP,
		SMV,
		UNKNOWN_AUDIO_CODEC;

		static Codec forType(long type) {
			Codec ac;
			if(type==BoxType.MP4A_SAMPLE_ENTRY) ac = AAC;
			else if(type==BoxType.AC3_SAMPLE_ENTRY) ac = AC3;
			else if(type==BoxType.AMR_SAMPLE_ENTRY) ac = AMR;
			else if(type==BoxType.AMR_WB_SAMPLE_ENTRY) ac = AMR_WIDE_BAND;
			else if(type==BoxType.EVRC_SAMPLE_ENTRY) ac = EVRC;
			else if(type==BoxType.EAC3_SAMPLE_ENTRY) ac = EXTENDED_AC3;
			else if(type==BoxType.QCELP_SAMPLE_ENTRY) ac = QCELP;
			else if(type==BoxType.SMV_SAMPLE_ENTRY) ac = SMV;
			else ac = UNKNOWN_AUDIO_CODEC;
			return ac;
		}
	}
	private SoundMediaHeaderBox smhd;
	private AudioSampleEntry sampleEntry;
	private Codec codec;

	@Override
	public Type getType() {
		return Type.AUDIO;
	}

	@Override
	public Codec getCodec() {
		return codec;
	}

	/**
	 * The balance is a floating-point number that places mono audio tracks in a
	 * stereo space: 0 is centre (the normal value), full left is -1.0 and full
	 * right is 1.0.
	 *
	 * @return the stereo balance for a this track
	 */
	public double getBalance() {
		return smhd.getBalance();
	}

	/**
	 * Returns the number of channels in this audio track.
	 * @return the number of channels
	 */
	public int getChannelCount() {
		return sampleEntry.getChannelCount();
	}

	/**
	 * Returns the sample rate of this audio track.
	 * @return the sample rate
	 */
	public int getSampleRate() {
		return sampleEntry.getSampleRate();
	}

	/**
	 * Returns the sample size in bits for this track.
	 * @return the sample size
	 */
	public int getSampleSize() {
		return sampleEntry.getSampleSize();
	}

	public double getVolume() {
		return tkhd.getVolume();
	}
}
