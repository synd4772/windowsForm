using KolmRakendust.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class PictureViewer : Form, IVorm
    {
        public string VormName { get; set; } = "Picture viewer";
        public List<string> FileNames { get; set; } = new List<string>();
        public TableLayoutPanel tlp{ get; set; } 
        public PictureBox pb { get; set; }
        public CheckBox cb { get; set; }
        public FlowLayoutPanel flp { get; set; }
        public Button btn { get; set; }
        public Button btn2 { get; set; }
        public Button btn3 { get; set; }
        public Button btn4 { get; set; }
        public Button btn5 { get; set; }
        public Button btn6 { get; set; }
        public OpenFileDialog ofd { get; set; }
        public ColorDialog cd { get; set; }
        public ZoomVorm? ZoomVorm { get; set; }
        public Gallery GalleryForm { get; set; }
    }

}
