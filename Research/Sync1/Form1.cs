using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sync1
{
  public sealed partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    static readonly double[] TestValues = { 0.0, 0.1, 0.2, 0.3, 0.5, 0.7, 0.9, 1.0, 0.8, 0.2, -0.6, -1.0, -0.9, -0.8, -0.6, -0.4, -0.2 };
    static readonly int TestCount = TestValues.Length;
    static int testIndex;

    void Draw()
    {
      var sync = new TickSync(0.5, t =>
      {
        testIndex++;
      });

      // --- Bild vorbereiten ---
      var pic = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format32bppRgb);
      var g = Graphics.FromImage(pic);
      g.SmoothingMode = SmoothingMode.HighQuality;
      g.Clear(Color.White);

      // --- Matrix für ein einfaches Diagramm anpassen ---
      float scale = pic.Height / 2.1f;
      float full = pic.Height / 2.0f;
      g.TranslateTransform(scale * 0.2f, full);
      g.ScaleTransform(scale, scale);

      // --- Diagramm-Basis zeichnen ---
      var pGray = new Pen(Color.Gray, 1f / scale);
      g.DrawLine(pGray, 0f, 0f, 10f, 0f);
      g.DrawLine(pGray, 0f, -1f, 0f, +1f);
      var font = new Font("arial", scale * 0.08f / pic.Height);
      var bGray = new SolidBrush(Color.Gray);

      for (int x = 10; x < 1000; x += 10)
      {
        float len = x % 100 == 0 ? 0.08f : x % 50 == 0 ? 0.04f : 0.02f;
        g.DrawLine(pGray, x * 0.01f, 0f, x * 0.01f, len);
        if (x % 50 != 0) continue;
        string n = (x * 0.01f).ToString("0.0", CultureInfo.InvariantCulture).TrimEnd('0', '.');
        var m = g.MeasureString(n, font);
        g.DrawString(n, font, bGray, x * 0.01f - m.Width * 0.5f, len + 0.01f);
      }

      for (int y = -100; y <= 100; y += 10)
      {
        float len = y % 100 == 0 ? 0.08f : y % 50 == 0 ? 0.04f : 0.02f;
        g.DrawLine(pGray, 0f, y * 0.01f, -len, y * 0.01f);
        string n = (y * -0.01f).ToString("0.0", CultureInfo.InvariantCulture);
        var m = g.MeasureString(n, font);
        g.DrawString(n, font, bGray, -m.Width - 0.08f, y * 0.01f - m.Height * 0.5f);
      }

      // --- Matrix umdrehen ---
      g.ResetTransform();
      g.TranslateTransform(scale * 0.2f, full);
      g.ScaleTransform(scale, -scale);

      // --- Basis-Ticks als Rote Kreuze zeichnen ---
      var pRed = new Pen(Color.DarkRed, 1f / scale);

      for (int x = 0; x < 100; x++)
      {
        g.DrawLine(pRed, x * 0.1f - 0.01f, (float)TestValues[x % TestCount] - 0.01f, x * 0.1f + 0.01f, (float)TestValues[x % TestCount] + 0.01f);
        g.DrawLine(pRed, x * 0.1f - 0.01f, (float)TestValues[x % TestCount] + 0.01f, x * 0.1f + 0.01f, (float)TestValues[x % TestCount] - 0.01f);
      }

      var pBlue = new Pen(Color.CornflowerBlue, 1f / scale);

      pictureBox1.Image = pic;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      Draw();
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
      Draw();
    }
  }
}
