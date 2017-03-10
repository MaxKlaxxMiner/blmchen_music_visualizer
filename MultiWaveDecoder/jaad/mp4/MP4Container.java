public class MP4Container {

	static {
		Logger log = Logger.getLogger("MP4 API");
		for(Handler h : log.getHandlers()) {
			log.removeHandler(h);
		}
		log.setLevel(Level.WARNING);

		ConsoleHandler h = new ConsoleHandler();
		h.setLevel(Level.ALL);
		log.addHandler(h);
	}
	private Brand major, minor;
	private Brand[] compatible;
	private FileTypeBox ftyp;
	private ProgressiveDownloadInformationBox pdin;
	private Box moov;
	private Movie movie;

	private void readContent() throws IOException {
		//read all boxes
		Box box = null;
		long type;
		boolean moovFound = false;
		while(inStream.hasLeft()) {
			box = BoxFactory.parseBox(null, in);
			if(boxes.isEmpty()&&box.getType()!=BoxTypes.FILE_TYPE_BOX) throw new MP4Exception("no MP4 signature found");
			boxes.add(box);

			type = box.getType();
			if(type==BoxTypes.FILE_TYPE_BOX) {
				if(ftyp==null) ftyp = (FileTypeBox) box;
			}
			else if(type==BoxTypes.MOVIE_BOX) {
				if(movie==null) moov = box;
				moovFound = true;
			}
			else if(type==BoxTypes.PROGRESSIVE_DOWNLOAD_INFORMATION_BOX) {
				if(pdin==null) pdin = (ProgressiveDownloadInformationBox) box;
			}
			else if(type==BoxTypes.MEDIA_DATA_BOX) {
				if(moovFound) break;
				else if(!inStream.hasRandomAccess()) throw new MP4Exception("movie box at end of file, need random access");
			}
		}
	}

	public Brand getMajorBrand() {
		if(major==null) major = Brand.forID(ftyp.getMajorBrand());
		return major;
	}

	public Brand getMinorBrand() {
		if(minor==null) minor = Brand.forID(ftyp.getMajorBrand());
		return minor;
	}

	public Brand[] getCompatibleBrands() {
		if(compatible==null) {
			string[] s = ftyp.getCompatibleBrands();
			compatible = new Brand[s.length];
			for(int i = 0; i<s.length; i++) {
				compatible[i] = Brand.forID(s[i]);
			}
		}
		return compatible;
	}

	//TODO: pdin, movie fragments??
	public Movie getMovie() {
		if(moov==null) return null;
		else if(movie==null) movie = new Movie(moov, in);
		return movie;
	}

	public List<Box> getBoxes() {
		return Collections.unmodifiableList(boxes);
	}
}
