#region # using *.*
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
#endregion

namespace Sync1
{
  public sealed partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    const double TicksPerSecond = 5;
    const double FramesPerSecond = 30;
    const double MaxTime = 10.0;

    static readonly double[] TestValues = { 0.0, 0.1, 0.2, 0.3, 0.5, 0.7, 0.9, 1.0, 0.8, 0.2, -0.6, -1.0, -0.9, -0.8, -0.6, -0.4, -0.2 };
    static readonly int TestCount = TestValues.Length;

    static void DrawSync(Graphics g, float penSize)
    {
      const float CrossSize = 0.01f;

      // --- Animationskurve berechnen und zeichnen ---
      int testIndex = 0;
      int valueId = 0;
      var sync = new TickSync(TicksPerSecond, t =>
      {
        t.UpdateValue(valueId, 0, TestValues[testIndex]);
        testIndex++;
        if (testIndex == TestCount) testIndex = 0; // von vorne beginnen
      });
      valueId = sync.AllocValues(1);

      var pBlue = new Pen(Color.CornflowerBlue, penSize);
      const float FrameStep = (float)(1 / FramesPerSecond);
      for (int frame = 0; frame * FrameStep < MaxTime; frame++)
      {
        double ts = frame * FrameStep;
        long frameId = sync.FrameInitialize(ts);
        sync.FrameStartDrawing(frameId, ts);

        // float val = (float)sync.GetOriginValue(valueId);
        float val = (float)sync.GetValue(frameId, valueId);

        g.DrawLine(pBlue, frame * FrameStep - CrossSize, val - CrossSize, frame * FrameStep + CrossSize, val + CrossSize);
        g.DrawLine(pBlue, frame * FrameStep - CrossSize, val + CrossSize, frame * FrameStep + CrossSize, val - CrossSize);

        sync.FrameFinishDrawing(frameId, ts);
        sync.FrameDisplayed(frameId, ts);
      }


      // --- Basis-Ticks als Rote Kreuze zeichnen ---
      var pRed = new Pen(Color.DarkRed, penSize);

      const float TickStep = (float)(1 / TicksPerSecond);
      for (int tick = 0; tick * TickStep < MaxTime; tick++)
      {
        g.DrawLine(pRed, tick * TickStep - CrossSize, (float)TestValues[tick % TestCount] - CrossSize, tick * TickStep + CrossSize, (float)TestValues[tick % TestCount] + CrossSize);
        g.DrawLine(pRed, tick * TickStep - CrossSize, (float)TestValues[tick % TestCount] + CrossSize, tick * TickStep + CrossSize, (float)TestValues[tick % TestCount] - CrossSize);
      }
    }

    void Draw()
    {
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
      g.DrawLine(pGray, 0f, 0f, (float)MaxTime, 0f);
      g.DrawLine(pGray, 0f, -1f, 0f, +1f);
      var font = new Font("arial", scale * 0.08f / pic.Height);
      var bGray = new SolidBrush(Color.Gray);

      for (int x = 10; x <= (int)(MaxTime * 100); x += 10)
      {
        float len = x % 100 == 0 ? 0.08f : x % 50 == 0 ? 0.04f : 0.02f;
        g.DrawLine(pGray, x * 0.01f, 0f, x * 0.01f, len);
        if (x % 50 != 0) continue;
        string n = (x * 0.01f).ToString("0.0", CultureInfo.InvariantCulture).Replace(".0", "");
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

      // --- Matrix umdrehen (-y) ---
      g.ResetTransform();
      g.TranslateTransform(scale * 0.2f, full);
      g.ScaleTransform(scale, -scale);

      DrawSync(g, 1f / scale);

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
