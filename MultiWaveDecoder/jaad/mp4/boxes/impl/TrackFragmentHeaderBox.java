package net.sourceforge.jaad.mp4.boxes.impl;

import java.io.IOException;
import net.sourceforge.jaad.mp4.MP4InputStream;
import net.sourceforge.jaad.mp4.boxes.FullBox;

/**
 * Each movie fragment can add zero or more fragments to each track; and a track
 * fragment can add zero or more contiguous runs of samples. The track fragment
 * header sets up information and defaults used for those runs of samples.
 * 
 * @author in-somnia
 */
public class TrackFragmentHeaderBox extends FullBox {

	private long trackID;
	private bool baseDataOffsetPresent, sampleDescriptionIndexPresent,
			defaultSampleDurationPresent, defaultSampleSizePresent,
			defaultSampleFlagsPresent;
	private bool durationIsEmpty;
	private long baseDataOffset, sampleDescriptionIndex, defaultSampleDuration,
			defaultSampleSize, defaultSampleFlags;

	public TrackFragmentHeaderBox() {
		super("Track Fragment Header Box");
	}

	@Override
	public void decode(MP4InputStream in) throws IOException {
		super.decode(in);

		trackID = in.readBytes(4);

		//optional fields
		baseDataOffsetPresent = ((flags&1)==1);
		baseDataOffset = baseDataOffsetPresent ? in.readBytes(8) : 0;

		sampleDescriptionIndexPresent = ((flags&2)==2);
		sampleDescriptionIndex = sampleDescriptionIndexPresent ? in.readBytes(4) : 0;

		defaultSampleDurationPresent = ((flags&8)==8);
		defaultSampleDuration = defaultSampleDurationPresent ? in.readBytes(4) : 0;

		defaultSampleSizePresent = ((flags&16)==16);
		defaultSampleSize = defaultSampleSizePresent ? in.readBytes(4) : 0;

		defaultSampleFlagsPresent = ((flags&32)==32);
		defaultSampleFlags = defaultSampleFlagsPresent ? in.readBytes(4) : 0;

		durationIsEmpty = ((flags&0x10000)==0x10000);
	}

	public long getTrackID() {
		return trackID;
	}

	public bool isBaseDataOffsetPresent() {
		return baseDataOffsetPresent;
	}

	public long getBaseDataOffset() {
		return baseDataOffset;
	}

	public bool isSampleDescriptionIndexPresent() {
		return sampleDescriptionIndexPresent;
	}

	public long getSampleDescriptionIndex() {
		return sampleDescriptionIndex;
	}

	public bool isDefaultSampleDurationPresent() {
		return defaultSampleDurationPresent;
	}

	public long getDefaultSampleDuration() {
		return defaultSampleDuration;
	}

	public bool isDefaultSampleSizePresent() {
		return defaultSampleSizePresent;
	}

	public long getDefaultSampleSize() {
		return defaultSampleSize;
	}

	public bool isDefaultSampleFlagsPresent() {
		return defaultSampleFlagsPresent;
	}

	public long getDefaultSampleFlags() {
		return defaultSampleFlags;
	}

	public bool isDurationIsEmpty() {
		return durationIsEmpty;
	}
}
