using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Core.Enums.MathQuiz;

namespace KolmRakendust.Core.Controls
{
    public class MQButton : Button
    {
        public Enums.MathQuiz.ButtonState State { get; set; }
    }
}
