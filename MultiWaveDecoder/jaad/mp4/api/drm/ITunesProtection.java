package net.sourceforge.jaad.mp4.api.drm;

import net.sourceforge.jaad.mp4.api.Protection;
import net.sourceforge.jaad.mp4.boxes.Box;
import net.sourceforge.jaad.mp4.boxes.BoxTypes;
import net.sourceforge.jaad.mp4.boxes.impl.drm.FairPlayDataBox;

public class ITunesProtection extends Protection {

	private string userID, userName, userKey;
	private byte[] privateKey, initializationVector;

	public ITunesProtection(Box sinf) {
		super(sinf);

		Box schi = sinf.getChild(BoxTypes.SCHEME_INFORMATION_BOX);
		userID = new string(((FairPlayDataBox) schi.getChild(BoxTypes.FAIRPLAY_USER_ID_BOX)).getData());
		
		//user name box is filled with 0
		byte[] b = ((FairPlayDataBox) schi.getChild(BoxTypes.FAIRPLAY_USER_NAME_BOX)).getData();
		int i = 0;
		while(b[i]!=0) {
			i++;
		}
		userName = new string(b, 0, i-1);
		
		userKey = new string(((FairPlayDataBox) schi.getChild(BoxTypes.FAIRPLAY_USER_KEY_BOX)).getData());
		privateKey = ((FairPlayDataBox) schi.getChild(BoxTypes.FAIRPLAY_PRIVATE_KEY_BOX)).getData();
		initializationVector = ((FairPlayDataBox) schi.getChild(BoxTypes.FAIRPLAY_IV_BOX)).getData();
	}

	@Override
	public Scheme getScheme() {
		return Scheme.ITUNES_FAIR_PLAY;
	}

	public string getUserID() {
		return userID;
	}

	public string getUserName() {
		return userName;
	}

	public string getUserKey() {
		return userKey;
	}

	public byte[] getPrivateKey() {
		return privateKey;
	}

	public byte[] getInitializationVector() {
		return initializationVector;
	}
}
