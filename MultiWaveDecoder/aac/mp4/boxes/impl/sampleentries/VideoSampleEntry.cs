namespace MultiWaveDecoder
{
  public sealed class VideoSampleEntry : SampleEntry
  {
    int width, height;
    double horizontalResolution, verticalResolution;
    int frameCount, depth;
    string compressorName;

    public VideoSampleEntry(string name) : base(name) { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      inStream.skipBytes(2); //pre-defined: 0
      inStream.skipBytes(2); //reserved
      // 3x32 pre_defined
      inStream.skipBytes(4); //pre-defined: 0
      inStream.skipBytes(4); //pre-defined: 0
      inStream.skipBytes(4); //pre-defined: 0

      width = (int)inStream.readBytes(2);
      height = (int)inStream.readBytes(2);
      horizontalResolution = inStream.readFixedPoint(16, 16);
      verticalResolution = inStream.readFixedPoint(16, 16);
      inStream.skipBytes(4); //reserved
      frameCount = (int)inStream.readBytes(2);

      int len = inStream.read();
      compressorName = inStream.readString(len);
      inStream.skipBytes(31 - len);

      depth = (int)inStream.readBytes(2);
      inStream.skipBytes(2); //pre-defined: -1

      readChildren(inStream);
    }

    /// <summary>
    /// The width is the maximum visual width of the stream described by this sample description, in pixels.
    /// </summary>
    /// <returns></returns>
    public int getWidth()
    {
      return width;
    }

    /// <summary>
    /// The height is the maximum visual height of the stream described by this sample description, in pixels.
    /// </summary>
    /// <returns></returns>
    public int getHeight()
    {
      return height;
    }

    /// <summary>
    /// The horizontal resolution of the image in pixels-per-inch, as a floating point value.
    /// </summary>
    /// <returns></returns>
    public double getHorizontalResolution()
    {
      return horizontalResolution;
    }

    /// <summary>
    /// The vertical resolution of the image in pixels-per-inch, as a floating point value.
    /// </summary>
    /// <returns></returns>
    public double getVerticalResolution()
    {
      return verticalResolution;
    }

    /// <summary>
    /// The frame count indicates how many frames of compressed video are stored in each sample.
    /// </summary>
    /// <returns></returns>
    public int getFrameCount()
    {
      return frameCount;
    }

    /// <summary>
    /// The compressor name, for informative purposes.
    /// </summary>
    /// <returns></returns>
    public string getCompressorName()
    {
      return compressorName;
    }

    /// <summary>
    /// The depth takes one of the following values
    /// DEFAULT_DEPTH (0x18) – images are in colour with no alpha
    /// </summary>
    /// <returns></returns>
    public int getDepth()
    {
      return depth;
    }
  }
}
