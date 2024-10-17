using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class ZoomVorm: Form
    {
        public TrackBar trackBar { get; set; }
        public PictureViewer pragueneVorm { get; set; }
        public Bitmap bmp { get; set; }


        public ZoomVorm(PictureViewer praeguneVorm)
        {
            this.pragueneVorm = praeguneVorm;
            this.Text = "Zoom window";
            this.Width = 240;
            this.Height = 100;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            trackBar = new TrackBar();
            this.trackBar.Size = new System.Drawing.Size(224, 45);
            this.trackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.Controls.Add(trackBar);
            trackBar.Maximum = 100;
            trackBar.Minimum = 1;
            trackBar.SmallChange = 10;


        }
        private void trackBar1_Scroll(object? sender, System.EventArgs e)
        {
            Console.WriteLine(trackBar.Value);
            if (bmp == null) bmp = (Bitmap)pragueneVorm.pb.Image;
            Size sz = bmp.Size;
            Bitmap zoomed = (Bitmap)pragueneVorm.pb.Image;
            

            zoomed = new Bitmap((int)((sz.Width * trackBar.Value) / 100), (int)((sz.Height * trackBar.Value) / 100));
            using (Graphics g = Graphics.FromImage(zoomed))
            {
                
                g.PixelOffsetMode = PixelOffsetMode.Half;

                g.DrawImage(bmp, new Rectangle(Point.Empty, zoomed.Size));
            }

            pragueneVorm.pb.Image = zoomed;
            

        }
    }
}
