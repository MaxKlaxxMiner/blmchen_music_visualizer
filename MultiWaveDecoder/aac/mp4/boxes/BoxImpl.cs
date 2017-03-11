using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public abstract class BoxImpl : IBox
  {
    readonly string name;

    IBox parent;
    readonly List<IBox> children = new List<IBox>();

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

    public void setParams(IBox parent, long size, BoxType type, long offset)
    {
      this.size = size;
      this.type = type;
      this.parent = parent;
      this.offset = offset;
    }

    protected long getLeft(MP4InputStream inStream)
    {
      return (offset + size) - inStream.getOffset();
    }

    public BoxType getType()
    {
      return type;
    }

    public long getSize()
    {
      return size;
    }

    public long getOffset()
    {
      return offset;
    }

    public IBox getParent()
    {
      return parent;
    }

    public string getName()
    {
      return name;
    }

    // --- container methods ---

    public bool hasChildren()
    {
      return children.Count > 0;
    }

    public bool hasChild(BoxType type)
    {
      return children.Any(box => box.getType() == type);
    }

    public IBox getChild(BoxType type)
    {
      return children.FirstOrDefault(box => box.getType() == type);
    }

    public IBox[] getChildren()
    {
      return children.ToArray();
    }

    public IBox[] getChildren(BoxType type)
    {
      return children.Where(box => box.getType() == type).ToArray();
    }

    protected void readChildren(MP4InputStream inStream)
    {
      while (inStream.getOffset() < (offset + size))
      {
        var box = BoxFactory.parseBox(this, inStream);
        children.Add(box);
      }
    }

    protected void readChildren(MP4InputStream inStream, int len)
    {
      for (int i = 0; i < len; i++)
      {
        var box = BoxFactory.parseBox(this, inStream);
        children.Add(box);
      }
    }

    public override string ToString()
    {
      return name + " [" + BoxFactory.typeToString(type) + "] - " + (new { offset, size, children = string.Join(", ", children) });
    }
  }
}
