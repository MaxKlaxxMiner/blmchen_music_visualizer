
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

    // --- Testfiles: http://samples.mplayerhq.hu ---

    /// <summary>
    /// Album: Blümchen - Für immer und ewig
    /// Interpret: Blümchen
    /// Titel: Heut' ist mein Tag
    /// Quelle: https://itunes.apple.com/de/album/f%C3%BCr-immer-und-ewig/id209983088
    /// Größe: 7,43 MB (7.797.942 Bytes)
    /// Dauer: 3:34
    /// </summary>
    //static readonly string TestFileM4A = Environment.ExpandEnvironmentVariables(MusicPathiTunes + @"Blümchen\Für immer und ewig\2-01 Heut' ist mein Tag.m4a");
    static readonly string TestFileM4A = Environment.ExpandEnvironmentVariables(MusicPathiTunes + @"Karate Andi\Turbo (Deluxe Version)\03 Eckkneipenhustler.m4a");

    public Scanner()
    {
      InitializeComponent();

      string readFile = "";
      string writeFile = "";

      string path = Environment.ExpandEnvironmentVariables(MusicPathiTunes);
      openFileDialog1.InitialDirectory = path;

      if (Environment.MachineName == "MINI-PC")
      {
        readFile = TestFileM4A;
        writeFile = @"C:\Users\Max\Desktop\prog\Spiele\blmchen_music_visualizer\TestFiles\output.wav";
      }
      else
      {
        if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
        readFile = openFileDialog1.FileName;
        if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
        writeFile = saveFileDialog1.FileName;
      }

      var finfo = new FileInfo(readFile);
      if (!finfo.Exists) throw new FileNotFoundException(finfo.FullName);
      //var m4AData = File.ReadAllBytes(finfo.FullName);

      Main.main(new[] { "-mp4", finfo.FullName, writeFile });
      Console.WriteLine("read.");

      pictureBox1.Image = Main.coverPicture;
    }

    private void Scanner_Load(object sender, EventArgs e)
    {

    }

    private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
    {

    }
  }
}
