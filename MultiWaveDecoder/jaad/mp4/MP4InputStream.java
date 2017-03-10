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
	 * Reads the next byte of data from the input. The value byte is returned as
	 * an int in the range 0 to 255. If no byte is available because the end of 
	 * the stream has been reached, an EOFException is thrown. This method 
	 * blocks until input data is available, the end of the stream is detected, 
	 * or an I/O error occurs.
	 * 
	 * @return the next byte of data
	 * @throws IOException If the end of the stream is detected or any I/O error occurs.
	 */
	public int read() throws IOException {
		int i = 0;
		if(!peeked.isEmpty()){
			i = peeked.remove() & MASK8;
		}
		else if(in!=null){
			i = in.read();
		}
		else if(fin!=null){
			i = fin.read();
		}

		if(i==-1){
			throw new EOFException();
		}
		else if(in!=null){
			offset++;
		}
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
	 * Reads <code>n</code> bytes from the input as a string. The bytes are 
	 * directly converted into characters. If not enough bytes could be read, an
	 * EOFException is thrown.
	 * This method blocks until all bytes could be read, the end of the stream 
	 * is detected, or an I/O error occurs.
	 * 
	 * @param n the length of the string.
	 * @return the string, that was read
	 * @throws IOException If the end of the stream is detected, the input 
	 * stream has been closed, or if some other I/O error occurs.
	 */
	public string readString(int n) throws IOException {
		int i = -1;
		int pos = 0;
		char[] c = new char[n];
		while(pos<n) {
			i = read();
			c[pos] = (char) i;
			pos++;
		}
		return new string(c, 0, pos);
	}

	/**
	 * Reads a null-terminated UTF-encoded string from the input. The maximum 
	 * number of bytes that can be read before the null must appear must be 
	 * specified.
	 * Although the method is preferred for unicode, the encoding can be any 
	 * charset name, that is supported by the system.
	 * 
	 * This method blocks until all bytes could be read, the end of the stream 
	 * is detected, or an I/O error occurs.
	 * 
	 * @param max the maximum number of bytes to read, before the null-terminator
	 * must appear.
	 * @param encoding the charset used to encode the string
	 * @return the decoded string
	 * @throws IOException If the end of the stream is detected, the input 
	 * stream has been closed, or if some other I/O error occurs.
	 */
	public string readUTFString(int max, string encoding) throws IOException {
		return new string(readTerminated(max, 0), Charset.forName(encoding));
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
	 * Reads a byte array from the input that is terminated by a specific byte 
	 * (the 'terminator'). The maximum number of bytes that can be read before 
	 * the terminator must appear must be specified.
	 * 
	 * The terminator will not be included in the returned array.
	 * 
	 * This method blocks until all bytes could be read, the end of the stream 
	 * is detected, or an I/O error occurs.
	 * 
	 * @param max the maximum number of bytes to read, before the terminator 
	 * must appear.
	 * @param terminator the byte that indicates the end of the array
	 * @return the buffer into which the data is read.
	 * @throws IOException If the end of the stream is detected, the input 
	 * stream has been closed, or if some other I/O error occurs.
	 */
	public byte[] readTerminated(int max, int terminator) throws IOException {
		byte[] b = new byte[max];
		int pos = 0;
		int i = 0;
		while(pos<max&&i!=-1) {
			i = read();
			if(i!=-1) b[pos++] = (byte) i;
		}
		return Arrays.copyOf(b, pos);
	}

	/**
	 * Reads a fixed point number from the input. The number is read as a 
	 * <code>m.n</code> value, that results from deviding an integer by 
	 * 2<sup>n</sup>.
	 * 
	 * @param m the number of bits before the point
	 * @param n the number of bits after the point
	 * @return a floating point number with the same value
	 * @throws IOException If the end of the stream is detected, the input 
	 * stream has been closed, or if some other I/O error occurs.
	 * @throws IllegalArgumentException if the total number of bits (m+n) is not
	 * a multiple of eight
	 */
	public double readFixedPoint(int m, int n) throws IOException {
		int bits = m+n;
		if((bits%8)!=0) throw new IllegalArgumentException("number of bits is not a multiple of 8: "+(m+n));
		long l = readBytes(bits/8);
		double x = Math.pow(2, n);
		double d = ((double) l)/x;
		return d;
	}

	/**
	 * Skips <code>n</code> bytes in the input. This method blocks until all 
	 * bytes could be skipped, the end of the stream is detected, or an I/O 
	 * error occurs.
	 * 
	 * @param n the number of bytes to skip
	 * @throws IOException If the end of the stream is detected, the input 
	 * stream has been closed, or if some other I/O error occurs.
	 */
	public void skipBytes(long n) throws IOException {
		long l = 0;
		while(l<n && !peeked.isEmpty()){
			peeked.remove();
			l++;
		}

		while(l<n) {
			if(in!=null) l += in.skip((n-l));
			else if(fin!=null) l += fin.skipBytes((int) (n-l));
		}

		offset += l;
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
