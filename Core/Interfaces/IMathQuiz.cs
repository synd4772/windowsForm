using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Core.Interfaces
{
    public interface IMathQuiz
    {
        int BetweenControlsX { get; }
        int DMStartExampleY { get; }
        int StartExampleX { get; }
        int BetweenExamplesY { get; }
    }
}
