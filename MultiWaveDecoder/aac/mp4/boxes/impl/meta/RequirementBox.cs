namespace MultiWaveDecoder
{
  public sealed class RequirementBox : FullBox
  {
    string requirement;

    public RequirementBox() : base("Requirement Box") { }

    public override void decode(MP4InputStream inStream)
    {
      base.decode(inStream);

      requirement = inStream.readString((int)getLeft(inStream));
    }

    public string getRequirement()
    {
      return requirement;
    }
  }
}
