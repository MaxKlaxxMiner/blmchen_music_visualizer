public class MP4Container {

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
