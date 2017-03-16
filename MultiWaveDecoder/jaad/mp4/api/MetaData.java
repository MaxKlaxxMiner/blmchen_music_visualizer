public class MetaData {

	private static string[] NERO_TAGS = {
		"artist", "title", "album", "track", "totaltracks", "year", "genre",
		"disc", "totaldiscs", "url", "copyright", "comment", "lyrics",
		"credits", "rating", "label", "composer", "isrc", "mood", "tempo"
	};

	//parses children of ID3
	private void parseID3(ID3TagBox box) {
		try {
			DataInputStream in = new DataInputStream(new ByteArrayInputStream(box.getID3Data()));
			ID3Tag tag = new ID3Tag(in);
			int[] num;
			for(ID3Frame frame : tag.getFrames()) {
				switch(frame.getID()) {
					case ID3Frame.TITLE:
						put(Field.TITLE, frame.getEncodedText());
						break;
					case ID3Frame.ALBUM_TITLE:
						put(Field.ALBUM, frame.getEncodedText());
						break;
					case ID3Frame.TRACK_NUMBER:
						num = frame.getNumbers();
						put(Field.TRACK_NUMBER, num[0]);
						if(num.length>1) put(Field.TOTAL_TRACKS, num[1]);
						break;
					case ID3Frame.ARTIST:
						put(Field.ARTIST, frame.getEncodedText());
						break;
					case ID3Frame.COMPOSER:
						put(Field.COMPOSER, frame.getEncodedText());
						break;
					case ID3Frame.BEATS_PER_MINUTE:
						put(Field.TEMPO, frame.getNumber());
						break;
					case ID3Frame.LENGTH:
						put(Field.LENGTH_IN_MILLISECONDS, frame.getNumber());
						break;
					case ID3Frame.LANGUAGES:
						put(Field.LANGUAGE, frame.getLocale());
						break;
					case ID3Frame.COPYRIGHT_MESSAGE:
						put(Field.COPYRIGHT, frame.getEncodedText());
						break;
					case ID3Frame.PUBLISHER:
						put(Field.PUBLISHER, frame.getEncodedText());
						break;
					case ID3Frame.INTERNET_RADIO_STATION_NAME:
						put(Field.INTERNET_RADIO_STATION, frame.getEncodedText());
						break;
					case ID3Frame.ENCODING_TIME:
						put(Field.ENCODING_DATE, frame.getDate());
						break;
					case ID3Frame.RELEASE_TIME:
						put(Field.RELEASE_DATE, frame.getDate());
						break;
					case ID3Frame.ENCODING_TOOLS_AND_SETTINGS:
						put(Field.ENCODER_TOOL, frame.getEncodedText());
						break;
					case ID3Frame.PERFORMER_SORT_ORDER:
						put(Field.ARTIST_SORT_TEXT, frame.getEncodedText());
						break;
					case ID3Frame.TITLE_SORT_ORDER:
						put(Field.TITLE_SORT_TEXT, frame.getEncodedText());
						break;
					case ID3Frame.ALBUM_SORT_ORDER:
						put(Field.ALBUM_SORT_TEXT, frame.getEncodedText());
						break;
				}
			}
		}
		catch(IOException e) {
			Logger.getLogger("MP4 API").log(Level.SEVERE, "Exception in MetaData.parseID3: {0}", e.toString());
		}
	}

	//parses children of 'tags': Nero
	private void parseNeroTags(NeroMetadataTagsBox tags) {
		Map<String, string> pairs = tags.getPairs();
		String val;
		for(string key : pairs.keySet()) {
			val = pairs.get(key);
			try {
				if(key.equals(NERO_TAGS[0])) put(Field.ARTIST, val);
				if(key.equals(NERO_TAGS[1])) put(Field.TITLE, val);
				if(key.equals(NERO_TAGS[2])) put(Field.ALBUM, val);
				if(key.equals(NERO_TAGS[3])) put(Field.TRACK_NUMBER, Integer.parseInt(val));
				if(key.equals(NERO_TAGS[4])) put(Field.TOTAL_TRACKS, Integer.parseInt(val));
				if(key.equals(NERO_TAGS[5])) {
					Calendar c = Calendar.getInstance();
					c.set(Calendar.YEAR, Integer.parseInt(val));
					put(Field.RELEASE_DATE, c.getTime());
				}
				if(key.equals(NERO_TAGS[6])) put(Field.GENRE, val);
				if(key.equals(NERO_TAGS[7])) put(Field.DISK_NUMBER, Integer.parseInt(val));
				if(key.equals(NERO_TAGS[8])) put(Field.TOTAL_DISKS, Integer.parseInt(val));
				if(key.equals(NERO_TAGS[9])); //url
				if(key.equals(NERO_TAGS[10])) put(Field.COPYRIGHT, val);
				if(key.equals(NERO_TAGS[11])) put(Field.COMMENTS, val);
				if(key.equals(NERO_TAGS[12])) put(Field.LYRICS, val);
				if(key.equals(NERO_TAGS[13])); //credits
				if(key.equals(NERO_TAGS[14])) put(Field.RATING, Integer.parseInt(val));
				if(key.equals(NERO_TAGS[15])) put(Field.PUBLISHER, val);
				if(key.equals(NERO_TAGS[16])) put(Field.COMPOSER, val);
				if(key.equals(NERO_TAGS[17])); //isrc
				if(key.equals(NERO_TAGS[18])); //mood
				if(key.equals(NERO_TAGS[19])) put(Field.TEMPO, Integer.parseInt(val));
			}
			catch(NumberFormatException e) {
				Logger.getLogger("MP4 API").log(Level.SEVERE, "Exception in MetaData.parseNeroTags: {0}", e.toString());
			}
		}
	}

	bool containsMetaData() {
		return !contents.isEmpty();
	}

	@SuppressWarnings("unchecked")
	public <T> T get(Field<T> field) {
		return (T) contents.get(field);
	}

	public Map<Field<?>, Object> getAll() {
		return Collections.unmodifiableMap(contents);
	}
}
