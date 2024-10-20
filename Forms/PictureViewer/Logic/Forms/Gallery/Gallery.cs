using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Forms.MathQuiz.Controls;
using KolmRakendust.Forms.PictureViewer.Controls;

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
            Panel scrollablePanel = new Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill
            };

            List<PictureOptions> pictureBoxes = this.GetPictureBoxes();
            int currentX = StartX;
            int currentY = StartY;
            if (pictureBoxes.Count == 0)
            {
                Label label = new Label();
                label.Font = new Font("Arial", 18);

                label.Size = new Size(scrollablePanel.Width, scrollablePanel.Height);
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Text = "nothing :(";
                scrollablePanel.Controls.Add(label);
                this.Controls.Add(scrollablePanel);
                return;
            }
            foreach(PictureOptions pbox in pictureBoxes)
            {

                if (currentX >= StartX + ((BetweenPicturesX + MaxWidth) * MaxPicturesInRow)){
                    currentY += BetweenPicturesY + MaxHeight;
                    currentX = StartX;
                }
                pbox.Location = new Point(currentX, currentY);
                currentX += BetweenPicturesX + MaxWidth;
               scrollablePanel.Controls.Add(pbox);
            }
            this.Controls.Add(scrollablePanel);
        }

        public List<PictureOptions> GetPictureBoxes()
        {
            
            List<PictureOptions> pictureBoxes = new List<PictureOptions>();
            foreach(string fileName in this.FileNames)
            {
                PictureOptions pbox = new PictureOptions(fileName, MaxWidth, MaxHeight, this);
                pbox.PictureBox.DoubleClick += picture_DobleClick;
                pictureBoxes.Add(pbox);

            }
            return pictureBoxes;
        }

        public void picture_DobleClick(object? sender, EventArgs e)
        {
            GPictureBox? pbox = sender as GPictureBox;
            if(pbox is null) return;
            this.PictureViewerForm.pb.Load(pbox.FileName);
            this.Close();
        }


    }
}
