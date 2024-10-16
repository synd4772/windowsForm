using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms  ;



namespace WinFormsApp3
{
    public partial class ZoomVorm: Form
    {
        TrackBar trackBar;
        esimeneVorm pragueneVorm;
        Bitmap bmp;
        public ZoomVorm(esimeneVorm praeguneVorm)
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
        private void trackBar1_Scroll(object sender, System.EventArgs e)
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

    public partial class esimeneVorm : Form
    {
        TableLayoutPanel tlp;
        public PictureBox pb;
        CheckBox cb;
        FlowLayoutPanel flp;
        Button btn;
        Button btn2;
        Button btn3;
        Button btn4;
        Button btn5;
        OpenFileDialog ofd;
        ColorDialog cd;
        ZoomVorm zoomVorm;
        public esimeneVorm(int x, int y)
        {
            this.Width = x;
            this.Height = y;

            this.tlp = new TableLayoutPanel();
            this.tlp.Location = new Point(0, 0);

            this.tlp.Size = new Size(200, 100);
            this.tlp.Dock = DockStyle.Fill;
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
            this.tlp.RowCount = 2;
            this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));

            pb = new PictureBox(); 
            pb.Dock = DockStyle.Fill;
            pb.BorderStyle = BorderStyle.Fixed3D;
            this.tlp.Controls.Add(pb);
            this.tlp.SetColumnSpan(pb, 2);
            cb = new CheckBox();
            cb.Text = "Stretch";
            cb.Size = new Size(100, 200);
            cb.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
            this.tlp.Controls.Add(cb);

            flp = new FlowLayoutPanel();
            flp.Dock= DockStyle.Fill;
            btn = new Button();
            btn2 = new Button();
            btn3 = new Button();
            btn4 = new Button();
            btn5 = new Button();
            List<Button> buttons = new List<Button>() { btn, btn2, btn3, btn4, btn5 };
            string[] texts = { "Close", "Set the background color", "Clear the picture", "Show a picture", "Zoom" };
            int index = -1;
            foreach(var button in buttons)
            {
                index++;

                button.AutoSize = true;
                button.Text = texts[index];
                button.Name = index == 0 ? "closeButton" : (index == 1 ? "backGroundButton" : (index == 2 ? "clearButton" : (index == 3 ? "showButton" : "zoomButton")));
                button.Click += index == 0 ? closeButton : (index == 1 ? backgroundButton_Click : (index == 2 ? clearButton_Click : (index == 3 ? new EventHandler(showPicture) : new EventHandler(zoom_Click))));
                flp.Controls.Add(button);
                
            }
            flp.FlowDirection = FlowDirection.RightToLeft;
            flp.AutoSize = true;
            ofd = new OpenFileDialog();
            cd = new ColorDialog();
            ofd.Title = "Select a picture file.";

            this.tlp.Controls.Add(flp);
            this.Controls.Add(this.tlp);
            
        }

        private void zoom_Click(object sender, EventArgs e)
        {
            
            if (pb.Image is not null)
            {
                this.zoomVorm = new ZoomVorm(this);
                this.zoomVorm.Show();
            }
            else
            {
                MessageBox.Show("Choose a photo", "Error");
                

            }
        }
        private void showPicture(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pb.Load(ofd.FileName);
            }
        }
        private void closeButton(object sender, EventArgs e)
        {
            this.Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
             if (cb.Checked)
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
             else
                    pb.SizeMode = PictureBoxSizeMode.Normal;
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            pb.Image = null;
        }
        private void backgroundButton_Click(object sender, EventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
                pb.BackColor = cd.Color;
        }
    }
}
