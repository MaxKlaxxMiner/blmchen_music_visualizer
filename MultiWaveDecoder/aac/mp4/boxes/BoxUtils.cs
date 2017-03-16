// ReSharper disable InconsistentNaming

using System;

namespace MultiWaveDecoder
{
  public static class BoxUtils
  {
    const long UNDETERMINED = 4294967295;

    public static string getLanguageCode(long l)
    {
      // 1 bit padding, 5*3 bits language code (ISO-639-2/T)
      var c = new char[3];
      c[0] = (char)(((l >> 10) & 31) + 0x60);
      c[1] = (char)(((l >> 5) & 31) + 0x60);
      c[2] = (char)((l & 31) + 0x60);
      return new string(c);
    }

    public static long detectUndetermined(long l)
    {
      long x;
      if (l == UNDETERMINED) x = -1;
      else x = l;
      return x;
    }

    public static DateTime getDate(long ts)
    {
      // in seconds since midnight, Jan. 1, 1904, in UTC time.
      return new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(ts);
    }
  }
}
