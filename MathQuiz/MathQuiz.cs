using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace KolmRakendust
{
    public partial class MathQuiz: Form, IVorm
    {
        public Label lbl { get; set; }
        public Label lbl2 { get; set; }
        public List<List<Label>> numberLabels { get; set; }
        public List<NumericUpDown> numericUpDowns { get; set; }
        public NumericUpDown nud { get; set; } = new NumericUpDown();
        public Button startButton { get; set; }
        public Random random { get; set; } = new Random();
        public Mode? CurrentMode { get; set; }
    }

    public partial class MathQuiz : Form, IVorm
    {
        public string VormName { get; set; } = "Math quiz";

        

        private List<string> numericUpDownNames { get; set; } = new List<string>()
            {
                "sum", "difference", "product", "quotient"
            };
        private List<string> operators { get; set; } = new List<string>()
            {
                "+", "-", "×", "÷"
            };

        public MathQuiz(int x, int y)
        {
            this.Width = x;
            this.Height = y;
            this.Text = "Math Quiz";
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

           
        }
        
        public void SetDefaultParametrs(Label label)
        {
            label.Text = "?";
            label.AutoSize = true;
            label.Size = new Size(20, 50);
            label.Font = new Font("Arial", 18);
            label.TextAlign = ContentAlignment.MiddleCenter;
        }
        public void SetDefaultParametrs(Label label, string op)
        {
            label.Text = op;
            label.AutoSize = false;
            label.Size = new Size(20, 50);
            label.Font = new Font("Arial", 18);
            label.TextAlign = ContentAlignment.MiddleCenter;
        }
        public void StartTheQuiz(object? sender, EventArgs e)
        {
            if (this.CurrentMode is null) return;
            if (this.CurrentMode == Mode.Default)
            {

                startButton.Enabled = false;
                for (int i = 0; i < 4; i++)
                {
                    random = new Random();
                    int addend1 = random.Next(1, 10);
                    int addend2 = random.Next(1, 10);
                    Console.WriteLine($"{i}. {addend1} - {addend2}");

                    numberLabels[i][0].Text = addend1.ToString();
                    numberLabels[i][1].Text = addend2.ToString();

                    numericUpDowns[i].Value = 0;


                }
                lbl.Text = timeLeft.ToString();
                timer1 = new System.Windows.Forms.Timer();
                timer1.Tick += timer1_Tick;
                timer1.Start();
            }
            else
            {

            }
        }

        private bool CheckTheAnswer(MathExample example, decimal answer)
        {
            if (example.Calculate() == answer)
            {
                return true;
            }
            return false;
        }

        private void timer1_Tick(object? sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                lbl.Text = timeLeft / 10 + " seconds";
            }
            else
            {
                timer1.Stop();
                lbl.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                for(int i = 0; i < 4; i++)
                {
                    numericUpDowns[i].Value = Calculate(int.Parse(numberLabels[i][0].Text), int.Parse(numberLabels[i][1].Text), operators[i]);
                }
                startButton.Enabled = true;
            }
        }
        public MathExample GenerateRandomExample()
        {
            EOperator randOperator;
            int y, x;
            Array values = Enum.GetValues(typeof(EOperator));
            randOperator = (EOperator)values.GetValue(random.Next(values.Length));

            y = random.Next(1, 10);
            x = random.Next(1, 15);
            MathExample mathEx = new MathExample(x, y, new MathOperator(randOperator));
            return mathEx;
        }
        public MathExample GenerateRandomExample(int start, int end)
        {
            EOperator randOperator;
            int y, x;
            Array values = Enum.GetValues(typeof(EOperator));
            randOperator = (EOperator)values.GetValue(random.Next(values.Length));

            y = random.Next(start, end);
            x = random.Next(start, end);
            MathExample mathEx = new MathExample(y, x, new MathOperator(randOperator));
            return mathEx;
        }

    }
}
