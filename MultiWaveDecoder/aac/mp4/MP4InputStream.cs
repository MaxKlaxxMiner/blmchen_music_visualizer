// ReSharper disable InconsistentNaming

using System.IO;

namespace MultiWaveDecoder
{
  public sealed class MP4InputStream
  {
    readonly Stream inStream;
    long offset; //only used with InputStream

    /**
     * Constructs an <code>MP4InputStream</code> that reads from an 
     * <code>InputStream</code>. It will have no random access, thus seeking 
     * will not be possible.
     * 
     * @param in an <code>InputStream</code> to read from
     */
    public MP4InputStream(Stream inStream)
    {
      this.inStream = inStream;
      offset = 0;
    }
  }
}
