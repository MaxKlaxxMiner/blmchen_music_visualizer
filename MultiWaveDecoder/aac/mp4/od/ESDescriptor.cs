// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  public class ESDescriptor : Descriptor
  {
    int esID, streamPriority, dependingOnES_ID;
    bool streamDependency, urlPresent, ocrPresent;
    string url;

    public override void decode(MP4InputStream inStream)
    {
      esID = (int)inStream.readBytes(2);

      //1 bit stream dependence flag, 1 it url flag, 1 reserved, 5 bits stream priority
      int flags = inStream.read();
      streamDependency = ((flags >> 7) & 1) == 1;
      urlPresent = ((flags >> 6) & 1) == 1;
      streamPriority = flags & 31;

      if (streamDependency) dependingOnES_ID = (int)inStream.readBytes(2);
      else dependingOnES_ID = -1;

      if (urlPresent)
      {
        int len = inStream.read();
        url = inStream.readString(len);
      }

      readChildren(inStream);
    }

    /// <summary>
    /// The ES_ID provides a unique label for each elementary stream within its name scope. The value should be within 0 and 65535 exclusively. The values 0 and 65535 are reserved.
    /// </summary>
    /// <returns>the elementary stream's ID</returns>
    public int getES_ID()
    {
      return esID;
    }

    /// <summary>
    /// Indicates if an ID of another stream is present, on which this stream depends.
    /// </summary>
    /// <returns>true if the dependingOnES_ID is present</returns>
    public bool hasStreamDependency()
    {
      return streamDependency;
    }

    /// <summary>
    /// The <code>dependingOnES_ID</code> is the ES_ID of another elementary stream on which this elementary stream depends. The stream with the
    /// <code>dependingOnES_ID</code> shall also be associated to this Descriptor. If no value is present (if <code>hasStreamDependency()</code> returns false) this method returns -1.
    /// </summary>
    /// <returns>the dependingOnES_ID value, or -1 if none is present</returns>
    public int getDependingOnES_ID()
    {
      return dependingOnES_ID;
    }

    /// <summary>
    /// A flag that indicates the presence of a URL.
    /// </summary>
    /// <returns>true if a URL is present</returns>
    public bool isURLPresent()
    {
      return urlPresent;
    }

    /// <summary>
    /// A URL string that shall point to the location of an SL-packetized stream
    /// by name. The parameters of the SL-packetized stream that is retrieved
    /// from the URL are fully specified in this ESDescriptor. 
    /// If no URL is present (if <code>isURLPresent()</code> returns false) this
    /// method returns null.
    /// </summary>
    /// <returns>a URL string or null if none is present</returns>
    public string getURL()
    {
      return url;
    }

    /// <summary>
    /// The stream priority indicates a relative measure for the priority of this elementary stream. An elementary stream with a higher priority is more
    /// important than one with a lower priority. The absolute values are not normatively defined.
    /// </summary>
    /// <returns>the stream priority</returns>
    public int getStreamPriority()
    {
      return streamPriority;
    }
  }
}
