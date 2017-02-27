
#region # using *.*

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// MD5: 778DC42CF8DAF6C6008E332757030C50
    /// </summary>
    static readonly string TestFileM4A = Environment.ExpandEnvironmentVariables(MusicPathiTunes + @"Blümchen\Für immer und ewig\2-01 Heut' ist mein Tag.m4a");

    public Scanner()
    {
      InitializeComponent();

      var finfo = new FileInfo(TestFileM4A);
      if (!finfo.Exists) throw new FileNotFoundException(finfo.FullName);
      var mp4Data = File.ReadAllBytes(finfo.FullName);

      var md5 = MD5.Create();
      var hash = string.Concat(md5.ComputeHash(mp4Data).Select(x => x.ToString("X2")));
      if (hash != "778DC42CF8DAF6C6008E332757030C50") throw new FileLoadException("MD5 error!");

      var reader = new DirectStreamReader(mp4Data);
      bool probe = AdtsDemuxer.Probe(reader);
      if (!probe) throw new Exception("Probe Error!");

      var demuxer = new AdtsDemuxer(reader);


    }

    private void Scanner_Load(object sender, EventArgs e)
    {

    }
  }
}
