﻿
#region # using *.*

using System;
using System.IO;
using System.Windows.Forms;
using MultiWaveDecoder;

#endregion

namespace Scanner
{
  public sealed partial class Scanner : Form
  {
    /// <summary>
    /// Musik-Ordner für ITunes-Media
    /// </summary>
    const string MusicPathiTunes = @"%USERPROFILE%\Music\iTunes\iTunes Media\Music\";

    /// <summary>
    /// Album: Blümchen - Für immer und ewig
    /// Interpret: Blümchen
    /// Titel: Heut' ist mein Tag
    /// Quelle: https://itunes.apple.com/de/album/f%C3%BCr-immer-und-ewig/id209983088
    /// Größe: 7,43 MB (7.797.942 Bytes)
    /// Dauer: 3:34
    /// </summary>
    static readonly string TestFileM4A = Environment.ExpandEnvironmentVariables(MusicPathiTunes + @"Blümchen\Für immer und ewig\2-01 Heut' ist mein Tag.m4a");

    public Scanner()
    {
      InitializeComponent();

      var finfo = new FileInfo(TestFileM4A);
      if (!finfo.Exists) throw new FileNotFoundException(finfo.FullName);
      var m4AData = File.ReadAllBytes(finfo.FullName);


    }

    private void Scanner_Load(object sender, EventArgs e)
    {

    }
  }
}
