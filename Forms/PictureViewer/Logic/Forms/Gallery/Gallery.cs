using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class Gallery : Form
    {
        public Gallery(PictureViewer pictureViewer, List<string> fileNames)
        {
            Width = 800;
            Height = 800;
            this.Text = "Gallery";
            this.FileNames = fileNames;
            PictureViewerForm = pictureViewer;
        }

        public void Render()
        {
            List<GPictureBox> pictureBoxes = this.GetPictureBoxes();
            int currentX = StartX;
            int currentY = StartY;
            foreach(GPictureBox pbox in pictureBoxes)
            {

                if (currentX >= StartX + ((BetweenPicturesX + MaxWidth) * MaxPicturesInRow)){
                    currentY += BetweenPicturesY + MaxHeight;
                    currentX = StartX;
                }
                pbox.Location = new Point(currentX, currentY);
                currentX += BetweenPicturesX + MaxWidth;
               this.Controls.Add(pbox);
            }
        }

        public List<GPictureBox> GetPictureBoxes()
        {
            List<GPictureBox> pictureBoxes = new List<GPictureBox>();
            foreach(string fileName in this.FileNames)
            {
                GPictureBox pbox = new GPictureBox();
                pbox.BorderStyle = BorderStyle.FixedSingle;
                
                pbox.Size = new Size(MaxWidth, MaxHeight);
                pbox.Load(fileName);
                pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                pbox.FileName = fileName;
                pictureBoxes.Add(pbox);
                pbox.DoubleClick += new EventHandler(picture_DobleClick);
               
            }
            return pictureBoxes;
        }
        private void picture_DobleClick(object? sender, EventArgs e)
        {
            GPictureBox? pbox = sender as GPictureBox;
            if(pbox is null) return;
            this.PictureViewerForm.pb.Load(pbox.FileName);
            this.Close();
        }
    }
}
