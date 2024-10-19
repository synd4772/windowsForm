using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class ZoomVorm : Form
    {
        public TrackBar trackBar { get; set; }
        public PictureViewer pragueneVorm { get; set; }
        public Bitmap bmp { get; set; }


        public ZoomVorm(PictureViewer praeguneVorm)
        {
            pragueneVorm = praeguneVorm;
            Text = "Zoom window";
            Width = 240;
            Height = 100;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            trackBar = new TrackBar();
            trackBar.Size = new Size(224, 45);
            trackBar.Scroll += new EventHandler(trackBar1_Scroll);
            Controls.Add(trackBar);
            trackBar.Maximum = 100;
            trackBar.Minimum = 1;
            trackBar.SmallChange = 10;


        }
        private void trackBar1_Scroll(object? sender, EventArgs e)
        {
            Console.WriteLine(trackBar.Value);
            if (bmp == null) bmp = (Bitmap)pragueneVorm.pb.Image;
            Size sz = bmp.Size;
            Bitmap zoomed = (Bitmap)pragueneVorm.pb.Image;


            zoomed = new Bitmap(sz.Width * trackBar.Value / 100, sz.Height * trackBar.Value / 100);
            using (Graphics g = Graphics.FromImage(zoomed))
            {

                g.PixelOffsetMode = PixelOffsetMode.Half;

                g.DrawImage(bmp, new Rectangle(Point.Empty, zoomed.Size));
            }

            pragueneVorm.pb.Image = zoomed;


        }
    }
}
