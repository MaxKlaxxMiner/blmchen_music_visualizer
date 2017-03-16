using System;
// ReSharper disable InconsistentNaming

namespace MultiWaveDecoder
{
  public sealed class Frame : IComparable<Frame>
  {
    readonly FrameType type;
    readonly long offset;
    readonly long size;
    readonly double time;
    byte[] data;

    public Frame(FrameType type, long offset, long size, double time)
    {
      this.type = type;
      this.offset = offset;
      this.size = size;
      this.time = time;
    }

    public FrameType getType()
    {
      return type;
    }

    public long getOffset()
    {
      return offset;
    }

    public long getSize()
    {
      return size;
    }

    public double getTime()
    {
      return time;
    }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object. 
    /// </summary>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other"/> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other"/>. Greater than zero This instance follows <paramref name="other"/> in the sort order. 
    /// </returns>
    /// <param name="other">An object to compare with this instance. </param>
    public int CompareTo(Frame other)
    {
      //    double d = time-f.time;
      //    //0 should not happen, since frames don't have the same timestamps
      //    return (d<0) ? -1 : ((d>0) ? 1 : 0);
      return 0;
    }

    public void setData(byte[] data)
    {
      this.data = data;
    }

    public byte[] getData()
    {
      return data;
    }
  }
}
