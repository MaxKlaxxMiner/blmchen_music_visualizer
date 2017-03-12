// ReSharper disable InconsistentNaming
namespace MultiWaveDecoder
{
  public sealed class DataEntryUrlBox : FullBox
  {
    bool inFile;
    string location;

    public DataEntryUrlBox() : base("Data Entry Url Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      inFile = (flags & 1) == 1;
      if (!inFile) location = inStream.readUTFString((int)getLeft(inStream));
    }

    public bool isInFile()
    {
      return inFile;
    }

    public string getLocation()
    {
      return location;
    }
  }
}
