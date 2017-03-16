using System;
// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  /// <summary>
  /// This class contains the metadata for a movie. It parses different metadata types (iTunes tags, ID3).
  /// 
  /// The fields can be read via the <code>get(Field)</code> method using one of the predefined <code>Field</code>s.
  /// </summary>
  public sealed class MetaData
  {
    /// <summary>
    /// moov.udta:
    /// -3gpp boxes
    /// -meta
    /// --ilst
    /// --tags
    /// --meta (no container!)
    /// --tseg
    /// ---tshd
    /// </summary>
    /// <param name="udta"></param>
    /// <param name="meta"></param>
    public void parse(IBox udta, IBox meta)
    {
      throw new NotImplementedException();

      ////standard boxes
      //if (meta.hasChild(BoxType.COPYRIGHT_BOX))
      //{
      //  CopyrightBox cprt = (CopyrightBox)meta.getChild(BoxType.COPYRIGHT_BOX);
      //  put(Field.LANGUAGE, new Locale(cprt.getLanguageCode()));
      //  put(Field.COPYRIGHT, cprt.getNotice());
      //}
      ////3gpp user data
      //if (udta != null) parse3GPPData(udta);
      ////id3, TODO: can be present in different languages
      //if (meta.hasChild(BoxType.ID3_TAG_BOX)) parseID3((ID3TagBox)meta.getChild(BoxType.ID3_TAG_BOX));
      ////itunes
      //if (meta.hasChild(BoxType.ITUNES_META_LIST_BOX)) parseITunesMetaData(meta.getChild(BoxType.ITUNES_META_LIST_BOX));
      ////nero tags
      //if (meta.hasChild(BoxType.NERO_METADATA_TAGS_BOX)) parseNeroTags((NeroMetadataTagsBox)meta.getChild(BoxType.NERO_METADATA_TAGS_BOX));
    }
  }
}
