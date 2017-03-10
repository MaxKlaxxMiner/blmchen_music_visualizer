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
