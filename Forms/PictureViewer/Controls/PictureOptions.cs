using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Forms.MathQuiz.Controls;

namespace KolmRakendust.Forms.PictureViewer.Controls
{
    public partial class PictureOptions : UserControl
    {
        public GPictureBox PictureBox { get; set; }
        public ContextMenuStrip ContextMenu { get; set; }
        public Button OptionsButton { get; set; }
        private ToolStripMenuItem option1 { get; set; } = new ToolStripMenuItem("Choose");
        private ToolStripMenuItem option2 { get; set; } = new ToolStripMenuItem("Delete");
        private ToolStripMenuItem option3 { get; set; } = new ToolStripMenuItem("Close");
        private Gallery GalleryForm { get; set; }
        public PictureOptions(string fileName, int width, int height, Gallery gallery)
        {
            GalleryForm = gallery;
            Width = width;
            Height = height;
            PictureBox = new GPictureBox
            {
                FileName = fileName,
                Size = new Size(Width, Height),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            ContextMenu = new ContextMenuStrip();
            PictureBox.Load(PictureBox.FileName);
            option1.Click += ChooseOption;
            option2.Click += DeleteOption;
            option3.Click += CloseOption;

            ContextMenu.Items.AddRange(new ToolStripMenuItem[]
            {
                option1, option2,
            });
            ContextMenu.Items.Add(new ToolStripSeparator());
            ContextMenu.Items.Add(option3);
            OptionsButton = new Button();
            OptionsButton.Text = ":";
            OptionsButton.Size = new Size(20, 20);
            OptionsButton.Location = new Point(PictureBox.Width - OptionsButton.Width, 0);
            OptionsButton.TextAlign = ContentAlignment.MiddleCenter;

            OptionsButton.Click += new EventHandler(optionsButton_Click);
            Controls.Add(OptionsButton);
            Controls.Add(PictureBox);

        }
        private void DeleteOption(object? sender, EventArgs e)
        {
            GalleryForm.FileNames.Remove(PictureBox.FileName);
            GalleryForm.PictureViewerForm.FileNames.Remove(PictureBox.FileName);
            GalleryForm.Controls.Clear();
            GalleryForm.Render();
            Dispose();

        }
        private void ChooseOption(object? sender, EventArgs e)
        {
            GalleryForm.picture_DobleClick(PictureBox, e);
        }
        private void CloseOption(object? sender, EventArgs e)
        {
            return;
        }


        private void optionsButton_Click(object? sender, EventArgs e)
        {
            ContextMenu.Show(OptionsButton, 0, OptionsButton.Height);
        }

    }
}
