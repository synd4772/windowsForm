using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Core.Enums.MathQuiz;

namespace KolmRakendust
{
    public class MQButton: Button
    {
        public Core.Enums.MathQuiz.ButtonState State { get; set; }
    }
}
