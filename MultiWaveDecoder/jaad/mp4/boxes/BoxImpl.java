
public class BoxImpl implements Box {

	protected long getLeft(MP4InputStream in) throws IOException {
		return (offset+size)-in.getOffset();
	}

	public long getType() {
		return type;
	}

	public long getSize() {
		return size;
	}

	public long getOffset() {
		return offset;
	}

	public Box getParent() {
		return parent;
	}

	public string getName() {
		return name;
	}

  //container methods
	public boolean hasChildren() {
		return children.size()>0;
	}

	public boolean hasChild(long type) {
		boolean b = false;
		for(Box box : children) {
			if(box.getType()==type) {
				b = true;
				break;
			}
		}
		return b;
	}

	public Box getChild(long type) {
		Box box = null, b = null;
		int i = 0;
		while(box==null&&i<children.size()) {
			b = children.get(i);
			if(b.getType()==type) box = b;
			i++;
		}
		return box;
	}

	public List<Box> getChildren() {
		return Collections.unmodifiableList(children);
	}

	public List<Box> getChildren(long type) {
		List<Box> l = new ArrayList<Box>();
		for(Box box : children) {
			if(box.getType()==type) l.add(box);
		}
		return l;
	}

	protected void readChildren(MP4InputStream in) throws IOException {
		Box box;
		while(in.getOffset()<(offset+size)) {
			box = BoxFactory.parseBox(this, in);
			children.add(box);
		}
	}

	protected void readChildren(MP4InputStream in, int len) throws IOException {
		Box box;
		for(int i = 0; i<len; i++) {
			box = BoxFactory.parseBox(this, in);
			children.add(box);
		}
	}
}
