using System;

namespace MultiWaveDecoder
{
  public class Track
  {
    protected Track(IBox trak, MP4InputStream inStream)
    {
      throw new NotImplementedException();

      //this.in = in;

      //tkhd = (TrackHeaderBox) trak.getChild(BoxType.TRACK_HEADER_BOX);

      //Box mdia = trak.getChild(BoxType.MEDIA_BOX);
      //mdhd = (MediaHeaderBox) mdia.getChild(BoxType.MEDIA_HEADER_BOX);
      //Box minf = mdia.getChild(BoxType.MEDIA_INFORMATION_BOX);

      //Box dinf = minf.getChild(BoxType.DATA_INFORMATION_BOX);
      //DataReferenceBox dref = (DataReferenceBox) dinf.getChild(BoxType.DATA_REFERENCE_BOX);
      ////TODO: support URNs
      //if(dref.hasChild(BoxType.DATA_ENTRY_URL_BOX)) {
      //  DataEntryUrlBox url = (DataEntryUrlBox) dref.getChild(BoxType.DATA_ENTRY_URL_BOX);
      //  inFile = url.isInFile();
      //  if(!inFile) {
      //    try {
      //      location = new URL(url.getLocation());
      //    }
      //    catch(MalformedURLException e) {
      //      Logger.getLogger("MP4 API").log(Level.WARNING, "Parsing URL-Box failed: {0}, url: {1}", new string[]{e.toString(), url.getLocation()});
      //      location = null;
      //    }
      //  }
      //}
      ///*else if(dref.containsChild(BoxType.DATA_ENTRY_URN_BOX)) {
      //DataEntryUrnBox urn = (DataEntryUrnBox) dref.getChild(BoxType.DATA_ENTRY_URN_BOX);
      //inFile = urn.isInFile();
      //location = urn.getLocation();
      //}*/
      //else {
      //  inFile = true;
      //  location = null;
      //}

      ////sample table
      //Box stbl = minf.getChild(BoxType.SAMPLE_TABLE_BOX);
      //if(stbl.hasChildren()) {
      //  frames = new ArrayList<Frame>();
      //  parseSampleTable(stbl);
      //}
      //else frames = Collections.emptyList();
      //currentFrame = 0;
    }
  }
}
