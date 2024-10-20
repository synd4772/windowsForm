using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class Gallery
    {
        public PictureViewer PictureViewerForm { get; set; }
        public List<string> FileNames { get; set; }
        
        public const int MaxWidth = 200;
        public const int MaxHeight = 200;
        public const int MaxPicturesInRow = 3;
        public const int BetweenPicturesX = 40;
        public const int BetweenPicturesY = 40;
        public const int StartX = 50;
        public const int StartY = 50;
    }
}
