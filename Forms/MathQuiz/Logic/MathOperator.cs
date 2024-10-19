using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace KolmRakendust.MathQuiz.Logic
{
    public class MathOperator
    {
        public string? OperatorChar { get; set; }
        public OperatorType OperatorType { get { return OperatorType; } set { OperatorType = value; OperatorChar = GetOperatorChar(value); } }
        public MathOperator(OperatorType op)
        {
            OperatorType = op;
        }
        private string GetOperatorChar(OperatorType op)
        {
            switch (op)
            {
                case OperatorType.Addition: return "+";
                case OperatorType.Subtraction: return "-";
                case OperatorType.Division: return "÷";
                case OperatorType.Multiplication: return "×";
            }
            return "";
        }
    }
}
