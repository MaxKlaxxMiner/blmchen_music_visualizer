public abstract class Track {

	protected <T> void parseSampleEntry(Box sampleEntry, Class<T> clazz) {
		T type;
		try {
			type = clazz.newInstance();
			if(sampleEntry.getClass().isInstance(type)) {
				System.out.println("true");
			}
		}
		catch(InstantiationException ex) {
			ex.printStackTrace();
		}
		catch(IllegalAccessException ex) {
			ex.printStackTrace();
		}
	}

	//tkhd
	/**
	 * Returns true if the track is enabled. A disabled track is treated as if
	 * it were not present.
	 * @return true if the track is enabled
	 */
	public bool isEnabled() {
		return tkhd.isTrackEnabled();
	}

	/**
	 * Returns true if the track is used in the presentation.
	 * @return true if the track is used
	 */
	public bool isUsed() {
		return tkhd.isTrackInMovie();
	}

	/**
	 * Returns true if the track is used in previews.
	 * @return true if the track is used in previews
	 */
	public bool isUsedForPreview() {
		return tkhd.isTrackInPreview();
	}

	/**
	 * Returns the time this track was created.
	 * @return the creation time
	 */
	public Date getCreationTime() {
		return Utils.getDate(tkhd.getCreationTime());
	}

	/**
	 * Returns the last time this track was modified.
	 * @return the modification time
	 */
	public Date getModificationTime() {
		return Utils.getDate(tkhd.getModificationTime());
	}

	//mdhd
	/**
	 * Returns the language for this media.
	 * @return the language
	 */
	public Locale getLanguage() {
		return new Locale(mdhd.getLanguage());
	}

	/**
	 * Returns true if the data for this track is present in this file (stream).
	 * If not, <code>getLocation()</code> returns the URL where the data can be
	 * found.
	 * @return true if the data is in this file (stream), false otherwise
	 */
	public bool isInFile() {
		return inFile;
	}

	/**
	 * If the data for this track is not present in this file (if
	 * <code>isInFile</code> returns false), this method returns the data's
	 * location. Else null is returned.
	 * @return the data's location or null if the data is in this file
	 */
	public URL getLocation() {
		return location;
	}

	//info structures
	/**
	 * Returns the decoder specific info, if present. It contains configuration
	 * data for the decoder. If the decoder specific info is not present, the
	 * track contains a <code>DecoderInfo</code>.
	 *
	 * @see #getDecoderInfo() 
	 * @return the decoder specific info
	 */
	public byte[] getDecoderSpecificInfo() {
		return decoderSpecificInfo.getData();
	}

	/**
	 * Returns the <code>DecoderInfo</code>, if present. It contains 
	 * configuration information for the decoder. If the structure is not
	 * present, the track contains a decoder specific info.
	 *
	 * @see #getDecoderSpecificInfo()
	 * @return the codec specific structure
	 */
	public DecoderInfo getDecoderInfo() {
		return decoderInfo;
	}

	/**
	 * Returns the <code>ProtectionInformation</code> object that contains 
	 * details about the DRM system used. If no protection is present this 
	 * method returns null.
	 * 
	 * @return a <code>ProtectionInformation</code> object or null if no 
	 * protection is used
	 */
	public Protection getProtection() {
		return protection;
	}

	//reading
	/**
	 * Indicates if there are more frames to be read in this track.
	 * 
	 * @return true if there is at least one more frame to read.
	 */
	public bool hasMoreFrames() {
		return currentFrame<frames.size();
	}

	/**
	 * Reads the next frame from this track. If it contains no more frames to
	 * read, null is returned.
	 * 
	 * @return the next frame or null if there are no more frames to read
	 * @throws IOException if reading fails
	 */
	public Frame readNextFrame() throws IOException {
		Frame frame = null;
		if(hasMoreFrames()) {
			frame = frames.get(currentFrame);

			long diff = frame.getOffset()-in.getOffset();
			if(diff>0) in.skipBytes(diff);
			else if(diff<0) {
				if(in.hasRandomAccess()) in.seek(frame.getOffset());
				else {
					Logger.getLogger("MP4 API").log(Level.WARNING, "readNextFrame failed: frame {0} already skipped, offset:{1}, stream:{2}", new Object[]{currentFrame, frame.getOffset(), in.getOffset()});
					throw new IOException("frame already skipped and no random access");
				}
			}

			byte[] b = new byte[(int) frame.getSize()];
			try {
				in.readBytes(b);
			}
			catch(EOFException e) {
				Logger.getLogger("MP4 API").log(Level.WARNING, "readNextFrame failed: tried to read {0} bytes at {1}", new Long[]{frame.getSize(), in.getOffset()});
				throw e;
			}
			frame.setData(b);
			currentFrame++;
		}
		return frame;
	}

	/**
	 * This method tries to seek to the frame that is nearest to the given
	 * timestamp. It returns the timestamp of the frame it seeked to or -1 if
	 * none was found.
	 * 
	 * @param timestamp a timestamp to seek to
	 * @return the frame's timestamp that the method seeked to
	 */
	public double seek(double timestamp) {
		//find first frame > timestamp
		Frame frame = null;
		for(int i = 0; i<frames.size(); i++) {
			frame = frames.get(i++);
			if(frame.getTime()>timestamp) {
				currentFrame = i;
				break;
			}
		}
		return (frame==null) ? -1 : frame.getTime();
	}

	/**
	 * Returns the timestamp of the next frame to be read. This is needed to
	 * read frames from a movie that contains multiple tracks.
	 *
	 * @return the next frame's timestamp
	 */
	double getNextTimeStamp() {
		return frames.get(currentFrame).getTime();
	}
}
