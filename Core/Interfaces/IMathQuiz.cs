using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Core.Interfaces
{
    public interface IMathQuiz
    {
        public int BetweenControlsX { get; set; }
        public int DMStartExampleY { get; set; }
        public int StartExampleX { get; set; }
        public int BetweenExamplesY { get; set; }
    }
}
