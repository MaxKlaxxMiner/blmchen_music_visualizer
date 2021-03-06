﻿namespace MultiWaveDecoder
{
  /// <summary>
  /// The Media Data Box contains the media data. In video tracks, this box would contain video frames. A presentation may contain zero or more Media Data Boxes.
  /// The actual media data follows the type field; its structure is described by the metadata in the movie box.
  /// There may be any number of these boxes in the file (including zero, if all the media data is in other files). The metadata refers to media data by its absolute offset within the file.
  /// </summary>
  public sealed class MediaDataBox : BoxImpl
  {
    public MediaDataBox() : base("Media Data Box"){}
  }
}
