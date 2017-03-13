using System;

namespace MultiWaveDecoder
{
  public static class Logger
  {
    public static void LogBoxes(string message)
    {
      Console.WriteLine("MP4 Boxes: " + message);
    }

    public static void LogInfo(string message)
    {
      Console.WriteLine("Info: " + message);
    }

    public static void LogServe(string message)
    {
      Console.WriteLine("Serve: " + message);
    }
  }
}
