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
			compatible = new Brand[s.Length];
			for(int i = 0; i<s.Length; i++) {
				compatible[i] = Brand.forID(s[i]);
			}
		}
		return compatible;
	}

	public List<Box> getBoxes() {
		return Collections.unmodifiableList(boxes);
	}
}
