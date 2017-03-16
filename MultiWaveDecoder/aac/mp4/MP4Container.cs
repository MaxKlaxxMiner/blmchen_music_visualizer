using System.Collections.Generic;
using System.IO;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  /// <summary>
  /// The MP4Container is the central class for the MP4 demultiplexer. It reads the container and gives access to the containing data.
  /// 
  /// The data source can be either an <code>InputStream</code> or a <code>RandomAccessFile</code>. Since the specification does not decree a
  /// specific order of the content, the data needed for parsing (the sample tables) may be at the end of the stream. In this case, random access is
  /// needed and reading from an <code>InputSteam</code> will cause an exception. Thus, whenever possible, a <code>RandomAccessFile</code> should be used for 
  /// local files. Parsing from an <code>InputStream</code> is useful when reading from a network stream.
  /// 
  /// Each <code>MP4Container</code> can return the used file brand (file format version). Optionally, the following data may be present:
  /// <ul>
  /// <li>progressive download informations: pairs of download rate and playback
  /// delay, see {@link #getDownloadInformationPairs() getDownloadInformationPairs()}</li>
  /// <li>a <code>Movie</code></li>
  /// </ul>
  ///
  /// Additionally it gives access to the underlying MP4 boxes, that can be retrieved by <code>getBoxes()</code>. However, it is not recommended to access the boxes directly.
  /// </summary>
  public sealed class MP4Container
  {
    readonly MP4InputStream inStream;
    readonly List<IBox> boxes = new List<IBox>();

    //private Brand major, minor;
    //private Brand[] compatible;
    FileTypeBox ftyp;
    ProgressiveDownloadInformationBox pdin;
    IBox moov;
    Movie movie;

    public MP4Container(Stream inStream)
    {
      this.inStream = new MP4InputStream(inStream);

      readContent();
    }

    void readContent()
    {
      // --- read all boxes ---
      while (inStream.hasLeft())
      {
        var box = BoxFactory.parseBox(null, inStream);
        if (boxes.Count == 0 && box.getType() != BoxType.FILE_TYPE_BOX) throw new MP4Exception("no MP4 signature found");
        boxes.Add(box);

        var type = box.getType();
        if (type == BoxType.FILE_TYPE_BOX)
        {
          if (ftyp == null) ftyp = (FileTypeBox)box;
        }
        else if (type == BoxType.MOVIE_BOX)
        {
          if (movie == null) moov = box;
        }
        else if (type == BoxType.PROGRESSIVE_DOWNLOAD_INFORMATION_BOX)
        {
          if (pdin == null) pdin = (ProgressiveDownloadInformationBox)box;
        }
      }
    }

    // TODO: pdin, movie fragments??
    public Movie getMovie()
    {
      if (moov == null) return null;
      return movie ?? (movie = new Movie(moov, inStream));
    }

    public IBox GetBoxFromPath(string path)
    {
      var pathIds = path.Split('.').Where(str => str.Length == 4).Select(str => str[0] * 16777216 + str[1] * 65536 + str[2] * 256 + str[3]).ToArray();

      if (pathIds.Length == 0 || (BoxType)pathIds[0] != moov.getType()) return null;

      var tmp = moov;

      for (int i = 1; tmp != null && i < pathIds.Length; i++)
      {
        tmp = tmp.getChild((BoxType)pathIds[i]);
      }

      return tmp;
    }
  }
}
