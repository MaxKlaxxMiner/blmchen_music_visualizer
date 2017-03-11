using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public abstract class BoxImpl : Box
  {
    readonly string name;

    Box parent;
    readonly List<Box> children = new List<Box>();

    long size;
    BoxType type;
    long offset;

    protected BoxImpl(string name)
    {
      this.name = name;
    }

    /// <summary>
    /// Decodes the given input stream by reading this box and all of its children (if any).
    /// </summary>
    /// <param name="inStream">an input stream</param>
    public abstract void decode(MP4InputStream inStream);

    public void setParams(Box parent, long size, BoxType type, long offset)
    {
      this.size = size;
      this.type = type;
      this.parent = parent;
      this.offset = offset;
    }

    public override string ToString()
    {
      return name + " [" + BoxFactory.typeToString(type) + "] - " + (new { offset, size, children = string.Join(", ", children) });
    }
  }
}
