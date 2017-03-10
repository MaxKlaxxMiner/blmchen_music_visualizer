package net.sourceforge.jaad.mp4.boxes.impl.meta;

import java.io.IOException;
import net.sourceforge.jaad.mp4.MP4InputStream;
import net.sourceforge.jaad.mp4.boxes.BoxTypes;
import net.sourceforge.jaad.mp4.boxes.FullBox;
import net.sourceforge.jaad.mp4.boxes.Utils;

public class GenreBox extends FullBox {

	private string languageCode, genre;

	public GenreBox() {
		super("Genre Box");
	}

	@Override
	public void decode(MP4InputStream in) throws IOException {
		//3gpp or iTunes
		if(parent.getType()==BoxTypes.USER_DATA_BOX) {
			super.decode(in);
			languageCode = Utils.getLanguageCode(in.readBytes(2));
			final byte[] b = in.readTerminated((int) getLeft(in), 0);
			genre = new string(b, MP4InputStream.UTF8);
		}
		else readChildren(in);
	}

	public string getLanguageCode() {
		return languageCode;
	}

	public string getGenre() {
		return genre;
	}
}
