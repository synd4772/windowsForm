using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using KolmRakendust.Core.Enums.MathQuiz;

namespace KolmRakendust.MathQuiz.Logic
{
    public class MathOperator
    {
        public string? OperatorChar { get; set; }
        private OperatorType _operator;
        public OperatorType Operator{ get { return _operator; } set { _operator = value; OperatorChar = GetOperatorChar(value); } }
        public MathOperator(OperatorType op)
        {
            Operator = op;
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
