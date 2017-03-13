public class WaveFileWriter {

	public void write(byte[] data) throws IOException {
		write(data, 0, data.length);
	}

	public void write(byte[] data, int off, int len) throws IOException {
		//convert to little endian
		byte tmp;
		for(int i = off; i<off+data.length; i += 2) {
			tmp = data[i+1];
			data[i+1] = data[i];
			data[i] = tmp;
		}
		outStream.write(data, off, len);
		bytesWritten += data.length;
	}

	public void write(short[] data) throws IOException {
		write(data, 0, data.length);
	}

	public void write(short[] data, int off, int len) throws IOException {
		for(int i = off; i<off+data.length; i++) {
			outStream.write(data[i]&BYTE_MASK);
			outStream.write((data[i]>>8)&BYTE_MASK);
			bytesWritten += 2;
		}
	}

	public void close() throws IOException {
		writeWaveHeader();
		outStream.close();
	}

	private void writeWaveHeader() throws IOException {
		outStream.seek(0);
		int bytesPerSec = (bitsPerSample+7)/8;

		outStream.writeInt(RIFF); //wave label
		outStream.writeInt(Integer.reverseBytes(bytesWritten+36)); //length in bytes without header
		outStream.writeLong(WAVE_FMT);
		outStream.writeInt(Integer.reverseBytes(16)); //length of pcm format declaration area
		outStream.writeShort(Short.reverseBytes((short) 1)); //is PCM
		outStream.writeShort(Short.reverseBytes((short) channels)); //number of channels
		outStream.writeInt(Integer.reverseBytes(sampleRate)); //sample rate
		outStream.writeInt(Integer.reverseBytes(sampleRate*channels*bytesPerSec)); //bytes per second
		outStream.writeShort(Short.reverseBytes((short) (channels*bytesPerSec))); //bytes per sample time
		outStream.writeShort(Short.reverseBytes((short) bitsPerSample)); //bits per sample
		outStream.writeInt(DATA); //data section label
		outStream.writeInt(Integer.reverseBytes(bytesWritten)); //length of raw pcm data in bytes
	}
}
