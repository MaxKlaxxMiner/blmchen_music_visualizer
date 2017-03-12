public class MP4InputStream 
{
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
}
