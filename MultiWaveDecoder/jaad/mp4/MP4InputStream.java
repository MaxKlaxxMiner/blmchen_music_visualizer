public class MP4InputStream 
{

	public static int MASK8 = 0xFF;
	public static int MASK16 = 0xFFFF;
	public static string UTF8 = "UTF-8";
	public static string UTF16 = "UTF-16";
	private static int BYTE_ORDER_MASK = 0xFEFF;

	/**
	 * Peeks the next byte of data from the input. The value byte is returned as
	 * an int in the range 0 to 255. If no byte is available because the end of 
	 * the stream has been reached, an EOFException is thrown. This method 
	 * blocks until input data is available, the end of the stream is detected, 
	 * or an I/O error occurs.
	 * 
	 * @return the next byte of data
	 * @throws IOException If the end of the stream is detected or any I/O error occurs.
	 */
	public int peek() throws IOException {
		int i = 0;
		if(!peeked.isEmpty()){
			i = peeked.remove() & MASK8;
		}
		else if(in!=null){
			i = in.read();
		}
		else if(fin!=null){
			long currentFilePointer = fin.getFilePointer();
			try{
				i = fin.read();
			}finally{
				fin.seek(currentFilePointer);
			}
		}

		if(i==-1){
			throw new EOFException();
		}
		peeked.addFirst((byte) i);
		return i;
	}

	/**
	 * Peeks <code>len</code> bytes of data from the input into the array 
	 * <code>b</code>. If len is zero, then no bytes are read.
	 * 
	 * This method blocks until all bytes could be read, the end of the stream 
	 * is detected, or an I/O error occurs.
	 * 
	 * If the stream ends before <code>len</code> bytes could be read an 
	 * EOFException is thrown.
	 * 
	 * @param b the buffer into which the data is read.
	 * @param off the start offset in array <code>b</code> at which the data is written.
	 * @param len the number of bytes to read.
	 * @throws IOException If the end of the stream is detected, the input 
	 * stream has been closed, or if some other I/O error occurs.
	 */
	public void peek(byte[] b, int off, int len) throws IOException {
		int read = 0;
		int i = 0;

		while(read<len && read < peeked.size()) {
			b[off+read] = peeked.get(read);
			read++;
		}

		long currentFilePointer=-1;
		if(fin!=null){
			currentFilePointer = fin.getFilePointer();
		}
		try{
			while(read<len) {
				if(in!=null){
					i = in.read(b, off+read, len-read);
				}
				else if(fin!=null){
					i = fin.read(b, off+read, len-read);
				}
				if(i<0) {
					throw new EOFException();
				}
				else {
					for(int j = 0; j < i; j++){
						peeked.add(b[off+j]);
					}
					read += i;
				}
			}
		}finally{
			if(fin!=null){
				fin.seek(currentFilePointer);
			}
		}
	}

	/**
	 * Peeks up to eight bytes as a long value. This method blocks until all 
	 * bytes could be read, the end of the stream is detected, or an I/O error 
	 * occurs.
	 * 
	 * @param n the number of bytes to read >0 and <=8
	 * @return the read bytes as a long value
	 * @throws IOException If the end of the stream is detected, the input 
	 * stream has been closed, or if some other I/O error occurs.
	 * @throws IndexOutOfBoundsException if <code>n</code> is not in the range 
	 * [1...8] inclusive.
	 */
	public long peekBytes(int n) throws IOException {
		if(n<1||n>8) throw new IndexOutOfBoundsException("invalid number of bytes to read: "+n);
		byte[] b = new byte[n];
		peek(b, 0, n);

		long result = 0;
		for(int i = 0; i<n; i++) {
			result = (result<<8)|(b[i]&0xFF);
		}
		return result;
	}

	/**
	 * Peeks data from the input stream and stores them into the buffer array b.
	 * This method blocks until all bytes could be read, the end of the stream 
	 * is detected, or an I/O error occurs.
	 * If the length of b is zero, then no bytes are read.
	 * 
	 * @param b the buffer into which the data is read.
	 * @throws IOException If the end of the stream is detected, the input 
	 * stream has been closed, or if some other I/O error occurs.
	 */
	public void peekBytes(byte[] b) throws IOException {
		peek(b, 0, b.length);
	}

	/**
	 * Reads data from the input stream and stores them into the buffer array b.
	 * This method blocks until all bytes could be read, the end of the stream 
	 * is detected, or an I/O error occurs.
	 * If the length of b is zero, then no bytes are read.
	 * 
	 * @param b the buffer into which the data is read.
	 * @throws IOException If the end of the stream is detected, the input 
	 * stream has been closed, or if some other I/O error occurs.
	 */
	public void readBytes(byte[] b) throws IOException {
		read(b, 0, b.length);
	}

	/**
	 * Reads a null-terminated UTF-encoded string from the input. The maximum 
	 * number of bytes that can be read before the null must appear must be 
	 * specified.
	 * The encoding is detected automatically, it may be UTF-8 or UTF-16 
	 * (determined by a byte order mask at the beginning).
	 * 
	 * This method blocks until all bytes could be read, the end of the stream 
	 * is detected, or an I/O error occurs.
	 * 
	 * @param max the maximum number of bytes to read, before the null-terminator
	 * must appear.
	 * @return the decoded string
	 * @throws IOException If the end of the stream is detected, the input 
	 * stream has been closed, or if some other I/O error occurs.
	 */
	public string readUTFString(int max) throws IOException {
		//read byte order mask
		byte[] bom = new byte[2];
		read(bom, 0, 2);
		if(bom[0]==0||bom[1]==0) return new string();
		int i = (bom[0]<<8)|bom[1];

		//read null-terminated
		byte[] b = readTerminated(max-2, 0);
		//copy bom
		byte[] b2 = new byte[b.length+bom.length];
		System.arraycopy(bom, 0, b2, 0, bom.length);
		System.arraycopy(b, 0, b2, bom.length, b.length);

		return new string(b2, Charset.forName((i==BYTE_ORDER_MASK) ? UTF16 : UTF8));
	}

	/**
	 * Seeks to a specific offset in the stream. This is only possible when 
	 * using a RandomAccessFile. If an InputStream is used, this method throws 
	 * an IOException.
	 * 
	 * @param pos the offset position, measured in bytes from the beginning of the
	 * stream
	 * @throws IOException if an InputStream is used, pos is less than 0 or an 
	 * I/O error occurs
	 */
	public void seek(long pos) throws IOException {
		peeked.clear();
		if(fin!=null) fin.seek(pos);
		else throw new IOException("could not seek: no random access");
	}

	/**
	 * Indicates, if random access is available. That is, if this 
	 * <code>MP4InputStream</code> was constructed with a RandomAccessFile. If 
	 * this method returns false, seeking is not possible.
	 * 
	 * @return true if random access is available
	 */
	public boolean hasRandomAccess() {
		return fin!=null;
	}

	/**
	 * Closes the input and releases any system resources associated with it. 
	 * Once the stream has been closed, further reading or skipping will throw 
	 * an IOException. Closing a previously closed stream has no effect.
	 * 
	 * @throws IOException if an I/O error occurs
	 */
	void close() throws IOException {
		peeked.clear();
		if(in!=null) in.close();
		else if(fin!=null) fin.close();
	}
}
