using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQ = KolmRakendust.Core.Enums.MathQuiz;

namespace KolmRakendust.Forms.MathQuiz.Controls
{
    public class MQButton : Button // MQ - MathQuiz
    {
        public MQ.ButtonState State { get; set; }
    }
}
