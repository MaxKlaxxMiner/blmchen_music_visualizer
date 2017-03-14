using System.Collections.Generic;

namespace MultiWaveDecoder
{
  /// <summary>
  /// The Track Reference Box provides a reference from the containing track to another track in the presentation. These references are typed. A 'hint'
  /// reference links from the containing hint track to the media data that it reference links from the containing hint track to the media data that it
  /// metadata track to the content which it describes.
  /// 
  /// Exactly one Track Reference Box can be contained within the Track Box.
  /// 
  /// If this box is not present, the track is not referencing any other track in any way. The reference array is sized to fill the reference type box.
  /// </summary>
  public sealed class TrackReferenceBox : FullBox
  {
    string referenceType;
    readonly List<int> trackIDs = new List<int>();

    public TrackReferenceBox() : base("Track Reference Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      referenceType = inStream.readString(4);

      while (getLeft(inStream) > 3)
      {
        trackIDs.Add((int)inStream.readBytes(4));
      }
    }

    /// <summary>
    /// The reference type shall be set to one of the following values: 
    /// <ul>
    /// <li>'hint': the referenced track(s) contain the original media for this hint track.</li>
    /// <li>'cdsc': this track describes the referenced track.</li>
    /// <li>'hind': this track depends on the referenced hint track, i.e., it should only be used if the referenced hint track is used.</li>
    /// </ul>
    /// </summary>
    /// <returns>the reference type</returns>
    public string getReferenceType()
    {
      return referenceType;
    }

    /// <summary>
    /// The track IDs are integers that provide a reference from the containing track to other tracks in the presentation. Track IDs are never re-used and cannot be equal to zero.
    /// </summary>
    /// <returns>the track IDs this box refers to</returns>
    public int[] getTrackIDs()
    {
      return trackIDs.ToArray();
    }
  }
}
