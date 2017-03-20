package net.sourceforge.jaad.aac.tools;

import net.sourceforge.jaad.aac.AACException;
import net.sourceforge.jaad.aac.Profile;
import net.sourceforge.jaad.aac.SampleFrequency;
import net.sourceforge.jaad.aac.filterbank.FilterBank;
import net.sourceforge.jaad.aac.syntax.BitStream;
import net.sourceforge.jaad.aac.syntax.Constants;
import net.sourceforge.jaad.aac.syntax.ICSInfo;
import net.sourceforge.jaad.aac.syntax.ICStream;
import java.util.Arrays;

/**
 * Long-term prediction
 * @author in-somnia
 */
public class LTPrediction implements Constants {

	private static float[] CODEBOOK = {
		0.570829f,
		0.696616f,
		0.813004f,
		0.911304f,
		0.984900f,
		1.067894f,
		1.194601f,
		1.369533f
	};
	private int frameLength;
	private int[] states;
	private int coef, lag, lastBand;
	private bool lagUpdate;
	private bool[] shortUsed, shortLagPresent, longUsed;
	private int[] shortLag;

	public LTPrediction(int frameLength) {
		this.frameLength = frameLength;
		states = new int[4*frameLength];
	}

	public void decode(BitStream in, ICSInfo info, Profile profile) throws AACException {
		lag = 0;
		if(profile.equals(Profile.AAC_LD)) {
			lagUpdate = in.readBool();
			if(lagUpdate) lag = in.readBits(10);
		}
		else lag = in.readBits(11);
		if(lag>(frameLength<<1)) throw new AACException("LTP lag too large: "+lag);
		coef = in.readBits(3);

		int windowCount = info.getWindowCount();

		if(info.isEightShortFrame()) {
			shortUsed = new bool[windowCount];
			shortLagPresent = new bool[windowCount];
			shortLag = new int[windowCount];
			for(int w = 0; w<windowCount; w++) {
				if((shortUsed[w] = in.readBool())) {
					shortLagPresent[w] = in.readBool();
					if(shortLagPresent[w]) shortLag[w] = in.readBits(4);
				}
			}
		}
		else {
			lastBand = Math.min(info.getMaxSFB(), MAX_LTP_SFB);
			longUsed = new bool[lastBand];
			for(int i = 0; i<lastBand; i++) {
				longUsed[i] = in.readBool();
			}
		}
	}

	public void setPredictionUnused(int sfb) {
		if(longUsed!=null) longUsed[sfb] = false;
	}

	public void process(ICStream ics, float[] data, FilterBank filterBank, SampleFrequency sf) {
		ICSInfo info = ics.getInfo();

		if(!info.isEightShortFrame()) {
			int samples = frameLength<<1;
			float[] in = new float[2048];
			float[] out = new float[2048];

			for(int i = 0; i<samples; i++) {
				in[i] = states[samples+i-lag]*CODEBOOK[coef];
			}

			filterBank.processLTP(info.getWindowSequence(), info.getWindowShape(ICSInfo.CURRENT),
					info.getWindowShape(ICSInfo.PREVIOUS), in, out);

			if(ics.isTNSDataPresent()) ics.getTNS().process(ics, out, sf, true);

			int[] swbOffsets = info.getSWBOffsets();
			int swbOffsetMax = info.getSWBOffsetMax();
			int low, high, bin;
			for(int sfb = 0; sfb<lastBand; sfb++) {
				if(longUsed[sfb]) {
					low = swbOffsets[sfb];
					high = Math.min(swbOffsets[sfb+1], swbOffsetMax);

					for(bin = low; bin<high; bin++) {
						data[bin] += out[bin];
					}
				}
			}
		}
	}

	public void updateState(float[] time, float[] overlap, Profile profile) {
		int i;
		if(profile.equals(Profile.AAC_LD)) {
			for(i = 0; i<frameLength; i++) {
				states[i] = states[i+frameLength];
				states[frameLength+i] = states[i+(frameLength*2)];
				states[(frameLength*2)+i] = Math.round(time[i]);
				states[(frameLength*3)+i] = Math.round(overlap[i]);
			}
		}
		else {
			for(i = 0; i<frameLength; i++) {
				states[i] = states[i+frameLength];
				states[frameLength+i] = Math.round(time[i]);
				states[(frameLength*2)+i] = Math.round(overlap[i]);
			}
		}
	}

	public static bool isLTPProfile(Profile profile) {
		return profile.equals(Profile.AAC_LTP)||profile.equals(Profile.ER_AAC_LTP)||profile.equals(Profile.AAC_LD);
	}

	public void copy(LTPrediction ltp) {
		System.arraycopy(ltp.states, 0, states, 0, states.Length);
		coef = ltp.coef;
		lag = ltp.lag;
		lastBand = ltp.lastBand;
		lagUpdate = ltp.lagUpdate;
		shortUsed = Arrays.copyOf(ltp.shortUsed, ltp.shortUsed.Length);
		shortLagPresent = Arrays.copyOf(ltp.shortLagPresent, ltp.shortLagPresent.Length);
		shortLag = Arrays.copyOf(ltp.shortLag, ltp.shortLag.Length);
		longUsed = Arrays.copyOf(ltp.longUsed, ltp.longUsed.Length);
	}
}
