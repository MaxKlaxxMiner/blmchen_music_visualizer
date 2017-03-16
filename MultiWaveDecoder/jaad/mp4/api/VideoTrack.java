public class VideoTrack extends Track {

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
