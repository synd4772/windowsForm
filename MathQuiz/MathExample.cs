using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
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
            this.Operator = op;
        }
        public MathExample() { }
        public decimal Calculate()
        {
            switch (Operator.Operator)
            {
                case EOperator.Addition:
                    return this.X + this.Y;
                case EOperator.Subtraction:
                    return this.X - this.Y;
                case EOperator.Multiplication:
                    return this.X * this.Y;
                case EOperator.Division:
                    return this.X / this.Y;
            }
            return 0;
        }


    }
}
