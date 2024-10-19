using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.MathQuiz.Logic
{
    public class MathExample
    {
        public int X { get; set; }
        public int Y { get; set; }
        public MathOperator Operator { get; set; }
        public MathExample(int X, int Y, MathOperator op)
        {
            this.X = X;
            this.Y = Y;
            Operator = op;
        }
        public MathExample() { }
        public decimal Calculate()
        {
            switch (this.Operator.OperatorType)
            {
                case OperatorType.Addition:
                    return X + Y;
                case OperatorType.Subtraction:
                    return X - Y;
                case OperatorType.Multiplication:
                    return X * Y;
                case OperatorType.Division:
                    return X / Y;
            }
            return 0;
        }


    }
}
