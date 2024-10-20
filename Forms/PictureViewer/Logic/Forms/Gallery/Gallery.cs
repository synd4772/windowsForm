using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class Gallery : Form
    {
        public PictureViewer PictureViewerForm { get; set; }
        public Gallery(PictureViewer pictureViewer)
        {
            Width = 800;
            Height = 800;
            this.Text = "Gallery";
            PictureViewerForm = pictureViewer;
        }
    }
}
