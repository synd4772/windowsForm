using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Core.Enums.MathQuiz;

namespace KolmRakendust.MathQuiz.Logic
{
    public class MathExample
    {
        private int _x;
        private int _y;
        private MathOperator _operator;
        public int X { get{ return _x; } set{ _x = value; XLabel.Text = value.ToString(); } }
        public int Y { get{ return _y; } set{ _y = value; YLabel.Text = value.ToString(); } }
        public MathOperator Operator { get{ return _operator; } set{ _operator = value; OperatorLabel.Text = _operator.OperatorChar; } }

        public Label XLabel { get; set; } = new Label();
        public Label YLabel { get; set; } = new Label();
        public Label OperatorLabel { get; set; } = new Label();

        public NumericUpDown NUD { get; set; } = new NumericUpDown();

        public void ClearControls(Form form)
        {
            foreach(Control control in this.GetControls())
            {
                form.Controls.Remove(control);
            }
        }

        public MathExample(int X, int Y, MathOperator op)
        {
            this.X = X;
            this.Y = Y;
            Operator = op;

            XLabel.Name = "FirstNumber";
            YLabel.Name = "SecondNumber";
            OperatorLabel.Name = "Operator";
            NUD.Name = "Answer";
            NUD.DecimalPlaces = 2;
            NUD.Maximum = 1000;
            NUD.Minimum = -1000;

        }
        public MathExample() { }
        public decimal Calculate()
        {
            switch (this.Operator.Operator)
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

        public List<Control> GetControls()
        {
            return new List<Control>
            {
                this.XLabel,this.OperatorLabel,  this.YLabel, this.NUD
            };
        }


    }
}
