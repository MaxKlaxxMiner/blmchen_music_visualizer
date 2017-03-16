public class Movie {

	/**
	 * Returns an unmodifiable list of all tracks in this movie. The tracks are
	 * ordered as they appeare in the file/stream.
	 *
	 * @return the tracks contained by this movie
	 */
	public List<Track> getTracks() {
		return Collections.unmodifiableList(tracks);
	}

	/**
	 * Returns an unmodifiable list of all tracks in this movie with the
	 * specified type. The tracks are ordered as they appeare in the
	 * file/stream.
	 *
	 * @return the tracks contained by this movie with the passed type
	 */
	public List<Track> getTracks(Type type) {
		List<Track> l = new ArrayList<Track>();
		for(Track t : tracks) {
			if(t.getType().equals(type)) l.add(t);
		}
		return Collections.unmodifiableList(l);
	}

	/**
	 * Returns an unmodifiable list of all tracks in this movie whose samples
	 * are encoded with the specified codec. The tracks are ordered as they 
	 * appeare in the file/stream.
	 *
	 * @return the tracks contained by this movie with the passed type
	 */
	public List<Track> getTracks(Track.Codec codec) {
		List<Track> l = new ArrayList<Track>();
		for(Track t : tracks) {
			if(t.getCodec().equals(codec)) l.add(t);
		}
		return Collections.unmodifiableList(l);
	}

	/**
	 * Indicates if this movie contains metadata. If false the <code>MetaData</code>
	 * object returned by <code>getMetaData()</code> will not contain any field.
	 * 
	 * @return true if this movie contains any metadata
	 */
	public bool containsMetaData() {
		return metaData.containsMetaData();
	}

	/**
	 * Returns the MetaData object for this movie.
	 *
	 * @return the MetaData for this movie
	 */
	public MetaData getMetaData() {
		return metaData;
	}

	/**
	 * Returns the <code>ProtectionInformation</code> objects that contains 
	 * details about the DRM systems used. If no protection is present the 
	 * returned list will be empty.
	 * 
	 * @return a list of protection informations
	 */
	public List<Protection> getProtections() {
		return Collections.unmodifiableList(protections);
	}

	//mvhd
	/**
	 * Returns the time this movie was created.
	 * @return the creation time
	 */
	public Date getCreationTime() {
		return Utils.getDate(mvhd.getCreationTime());
	}

	/**
	 * Returns the last time this movie was modified.
	 * @return the modification time
	 */
	public Date getModificationTime() {
		return Utils.getDate(mvhd.getModificationTime());
	}

	/**
	 * Returns the duration in seconds.
	 * @return the duration
	 */
	public double getDuration() {
		return (double) mvhd.getDuration()/(double) mvhd.getTimeScale();
	}

	/**
	 * Indicates if there are more frames to be read in this movie.
	 *
	 * @return true if there is at least one track in this movie that has at least one more frame to read.
	 */
	public bool hasMoreFrames() {
		for(Track track : tracks) {
			if(track.hasMoreFrames()) return true;
		}
		return false;
	}

	/**
	 * Reads the next frame from this movie (from one of the contained tracks).
	 * The frame is the next in time-order, thus the next for playback. If none
	 * of the tracks contains any more frames, null is returned.
	 *
	 * @return the next frame or null if there are no more frames to read from this movie.
	 * @throws IOException if reading fails
	 */
	public Frame readNextFrame() throws IOException {
		Track track = null;
		for(Track t : tracks) {
			if(t.hasMoreFrames()&&(track==null||t.getNextTimeStamp()<track.getNextTimeStamp())) track = t;
		}

		return (track==null) ? null : track.readNextFrame();
	}
}
