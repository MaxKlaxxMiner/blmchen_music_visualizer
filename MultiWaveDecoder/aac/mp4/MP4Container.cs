using System;
using System.Collections.Generic;
using System.IO;
// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  /**
   * The MP4Container is the central class for the MP4 demultiplexer. It reads the
   * container and gives access to the containing data.
   *
   * The data source can be either an <code>InputStream</code> or a
   * <code>RandomAccessFile</code>. Since the specification does not decree a
   * specific order of the content, the data needed for parsing (the sample
   * tables) may be at the end of the stream. In this case, random access is
   * needed and reading from an <code>InputSteam</code> will cause an exception.
   * Thus, whenever possible, a <code>RandomAccessFile</code> should be used for 
   * local files. Parsing from an <code>InputStream</code> is useful when reading 
   * from a network stream.
   *
   * Each <code>MP4Container</code> can return the used file brand (file format
   * version). Optionally, the following data may be present:
   * <ul>
   * <li>progressive download informations: pairs of download rate and playback
   * delay, see {@link #getDownloadInformationPairs() getDownloadInformationPairs()}</li>
   * <li>a <code>Movie</code></li>
   * </ul>
   *
   * Additionally it gives access to the underlying MP4 boxes, that can be 
   * retrieved by <code>getBoxes()</code>. However, it is not recommended to 
   * access the boxes directly.
   * 
   * @author in-somnia
   */

  public sealed class MP4Container
  {
    readonly MP4InputStream inStream;
    readonly List<Box> boxes = new List<Box>();

    public MP4Container(Stream inStream)
    {
      this.inStream = new MP4InputStream(inStream);

      readContent();
    }

    void readContent()
    {
      // read all boxes
      bool moovFound = false;
      while (inStream.hasLeft())
      {
        var box = BoxFactory.parseBox(null, inStream);
        throw new NotImplementedException();
        //if (boxes.isEmpty() && box.getType() != BoxTypes.FILE_TYPE_BOX) throw new MP4Exception("no MP4 signature found");
        //boxes.add(box);

        //long type = box.getType();
        //if (type == BoxTypes.FILE_TYPE_BOX)
        //{
        //  if (ftyp == null) ftyp = (FileTypeBox)box;
        //}
        //else if (type == BoxTypes.MOVIE_BOX)
        //{
        //  if (movie == null) moov = box;
        //  moovFound = true;
        //}
        //else if (type == BoxTypes.PROGRESSIVE_DOWNLOAD_INFORMATION_BOX)
        //{
        //  if (pdin == null) pdin = (ProgressiveDownloadInformationBox)box;
        //}
        //else if (type == BoxTypes.MEDIA_DATA_BOX)
        //{
        //  if (moovFound) break;
        //  else if (!inStream.hasRandomAccess()) throw new MP4Exception("movie box at end of file, need random access");
        //}
      }
    }
  }
}
