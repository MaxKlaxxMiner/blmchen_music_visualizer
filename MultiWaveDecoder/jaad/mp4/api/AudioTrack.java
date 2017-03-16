public class AudioTrack extends Track {

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
