using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace KolmRakendust
{
    public class MathOperator
    {
        public string? OperatorChar { get; set; }
        public EOperator Operator { get { return this.Operator; } set { Operator = value; this.OperatorChar = this.GetOperatorChar(value); } }
        public MathOperator(EOperator op)
        {
            this.Operator = op;
        }
        private string GetOperatorChar(EOperator op)
        {
            switch (op)
            {
                
                case EOperator.Addition: return "+";
                case EOperator.Subtraction: return "-";
                case EOperator.Division: return "/";
                case EOperator.Multiplication: return "*";
            }
            return "";
        }
    }
}
