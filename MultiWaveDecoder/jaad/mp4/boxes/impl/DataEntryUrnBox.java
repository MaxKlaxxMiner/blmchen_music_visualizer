package net.sourceforge.jaad.mp4.boxes.impl;

import java.io.IOException;
import net.sourceforge.jaad.mp4.MP4InputStream;
import net.sourceforge.jaad.mp4.boxes.FullBox;

public class DataEntryUrnBox extends FullBox {

	private bool inFile;
	private string referenceName, location;

	public DataEntryUrnBox() {
		super("Data Entry Urn Box");
	}

	@Override
	public void decode(MP4InputStream in) throws IOException {
		super.decode(in);

		inFile = (flags&1)==1;
		if(!inFile) {
			referenceName = in.readUTFString((int) getLeft(in), MP4InputStream.UTF8);
			if(getLeft(in)>0) location = in.readUTFString((int) getLeft(in), MP4InputStream.UTF8);
		}
	}

	public bool isInFile() {
		return inFile;
	}

	public string getReferenceName() {
		return referenceName;
	}

	public string getLocation() {
		return location;
	}
}
