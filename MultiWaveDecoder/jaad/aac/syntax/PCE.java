package net.sourceforge.jaad.aac.syntax;

import net.sourceforge.jaad.aac.AACException;
import net.sourceforge.jaad.aac.Profile;
import net.sourceforge.jaad.aac.SampleFrequency;

public class PCE extends Element {

	private static int MAX_FRONT_CHANNEL_ELEMENTS = 16;
	private static int MAX_SIDE_CHANNEL_ELEMENTS = 16;
	private static int MAX_BACK_CHANNEL_ELEMENTS = 16;
	private static int MAX_LFE_CHANNEL_ELEMENTS = 4;
	private static int MAX_ASSOC_DATA_ELEMENTS = 8;
	private static int MAX_VALID_CC_ELEMENTS = 16;

	public static class TaggedElement {

		private bool isCPE;
		private int tag;

		public TaggedElement(bool isCPE, int tag) {
			this.isCPE = isCPE;
			this.tag = tag;
		}

		public bool isIsCPE() {
			return isCPE;
		}

		public int getTag() {
			return tag;
		}
	}

	public static class CCE {

		private bool isIndSW;
		private int tag;

		public CCE(bool isIndSW, int tag) {
			this.isIndSW = isIndSW;
			this.tag = tag;
		}

		public bool isIsIndSW() {
			return isIndSW;
		}

		public int getTag() {
			return tag;
		}
	}
	private Profile profile;
	private SampleFrequency sampleFrequency;
	private int frontChannelElementsCount, sideChannelElementsCount, backChannelElementsCount;
	private int lfeChannelElementsCount, assocDataElementsCount;
	private int validCCElementsCount;
	private bool monoMixdown, stereoMixdown, matrixMixdownIDXPresent;
	private int monoMixdownElementNumber, stereoMixdownElementNumber, matrixMixdownIDX;
	private bool pseudoSurround;
	private TaggedElement[] frontElements, sideElements, backElements;
	private int[] lfeElementTags;
	private int[] assocDataElementTags;
	private CCE[] ccElements;
	private byte[] commentFieldData;

	public PCE() {
		super();
		frontElements = new TaggedElement[MAX_FRONT_CHANNEL_ELEMENTS];
		sideElements = new TaggedElement[MAX_SIDE_CHANNEL_ELEMENTS];
		backElements = new TaggedElement[MAX_BACK_CHANNEL_ELEMENTS];
		lfeElementTags = new int[MAX_LFE_CHANNEL_ELEMENTS];
		assocDataElementTags = new int[MAX_ASSOC_DATA_ELEMENTS];
		ccElements = new CCE[MAX_VALID_CC_ELEMENTS];
		sampleFrequency = SampleFrequency.SAMPLE_FREQUENCY_NONE;
	}

	public void decode(BitStream in) throws AACException {
		readElementInstanceTag(in);

		profile = Profile.forInt(in.readBits(2));

		sampleFrequency = SampleFrequency.forInt(in.readBits(4));

		frontChannelElementsCount = in.readBits(4);
		sideChannelElementsCount = in.readBits(4);
		backChannelElementsCount = in.readBits(4);
		lfeChannelElementsCount = in.readBits(2);
		assocDataElementsCount = in.readBits(3);
		validCCElementsCount = in.readBits(4);

		if(monoMixdown = in.readBool()) {
			Constants.LOGGER.warning("mono mixdown present, but not yet supported");
			monoMixdownElementNumber = in.readBits(4);
		}
		if(stereoMixdown = in.readBool()) {
			Constants.LOGGER.warning("stereo mixdown present, but not yet supported");
			stereoMixdownElementNumber = in.readBits(4);
		}
		if(matrixMixdownIDXPresent = in.readBool()) {
			Constants.LOGGER.warning("matrix mixdown present, but not yet supported");
			matrixMixdownIDX = in.readBits(2);
			pseudoSurround = in.readBool();
		}

		readTaggedElementArray(frontElements, in, frontChannelElementsCount);

		readTaggedElementArray(sideElements, in, sideChannelElementsCount);

		readTaggedElementArray(backElements, in, backChannelElementsCount);

		int i;
		for(i = 0; i<lfeChannelElementsCount; ++i) {
			lfeElementTags[i] = in.readBits(4);
		}

		for(i = 0; i<assocDataElementsCount; ++i) {
			assocDataElementTags[i] = in.readBits(4);
		}

		for(i = 0; i<validCCElementsCount; ++i) {
			ccElements[i] = new CCE(in.readBool(), in.readBits(4));
		}

		in.byteAlign();

		int commentFieldBytes = in.readBits(8);
		commentFieldData = new byte[commentFieldBytes];
		for(i = 0; i<commentFieldBytes; i++) {
			commentFieldData[i] = (byte) in.readBits(8);
		}
	}

	private void readTaggedElementArray(TaggedElement[] te, BitStream in, int len) throws AACException {
		for(int i = 0; i<len; ++i) {
			te[i] = new TaggedElement(in.readBool(), in.readBits(4));
		}
	}

	public Profile getProfile() {
		return profile;
	}

	public SampleFrequency getSampleFrequency() {
		return sampleFrequency;
	}

	public int getChannelCount() {
		return frontChannelElementsCount+sideChannelElementsCount+backChannelElementsCount
				+lfeChannelElementsCount+assocDataElementsCount;
	}
}
