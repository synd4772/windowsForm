using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class MathQuiz : Form, IVorm
    {
        public int timeLeft { get; private set; } = 300;
        public void DefaultMode()
        {
            
            int DistanceBetweenExamples = 45;
            int ExampleY = DistanceBetweenExamples;

            numberLabels = new List<List<Label>>();
            numericUpDowns = new List<NumericUpDown>();

            lbl = new Label();
            lbl.Name = "timeLabel";
            lbl.AutoSize = false;
            lbl.BorderStyle = BorderStyle.FixedSingle;
            lbl.Size = new Size(200, 30);
            lbl.Text = "";
            lbl.Font = new Font("Arial", 16);

            lbl2 = new Label();
            lbl2.Font = new Font("Arial", 16);
            lbl2.Text = "Time Left";
            lbl2.AutoSize = true;

            lbl.Location = new Point(270, 0);
            lbl2.Location = new Point(150, 0);
            Controls.Add(lbl);
            Controls.Add(lbl2);

            for (int i = 0; i < 4; i++)
            {
                Label firstNumber = new Label();
                Label operatorLabel = new Label();
                Label secondNumber = new Label();
                Label equalLabel = new Label();

                NumericUpDown numericUpDown = new NumericUpDown();

                firstNumber.Location = new Point(15 + 100, ExampleY + 10);
                operatorLabel.Location = new Point(55 + 100, ExampleY);
                secondNumber.Location = new Point(95 + 100, ExampleY + 10);
                equalLabel.Location = new Point(135 + 100, ExampleY);
                numericUpDown.Location = new Point(165 + 100, ExampleY + 5);

                firstNumber.Name = "plusLeftLabel";
                secondNumber.Name = "plusRightLabel";
                numericUpDown.Name = numericUpDownNames[i];

                numberLabels.Add(new List<Label>());
                numberLabels[i].Add(firstNumber);
                numberLabels[i].Add(secondNumber);

                SetDefaultParametrs(firstNumber);
                SetDefaultParametrs(operatorLabel, operators[i]);
                SetDefaultParametrs(secondNumber);
                SetDefaultParametrs(equalLabel, "=");

                List<Control> lst = new List<Control>() {
                    firstNumber, operatorLabel, secondNumber, equalLabel, numericUpDown
                    };

                numericUpDown.Font = new Font("Arial", 18);
                numericUpDown.MaximumSize = new Size(115, 100);
                numericUpDown.Minimum = -100;
                numericUpDown.TabIndex = i + 1;
                numericUpDowns.Add(numericUpDown);


                ExampleY += DistanceBetweenExamples;

                foreach (var control in lst)
                {
                    Controls.Add(control);
                }

            }
            startButton = new Button();
            startButton.Name = "startButton";
            startButton.Font = new Font("Arial", 14);
            startButton.AutoSize = true;
            startButton.TabIndex = 0;
            startButton.Click += new EventHandler(StartTheQuiz);
            startButton.Location = new Point(this.Width / 2 - startButton.Size.Width, this.Height - 100);
            startButton.Text = "Start The Quiz";
            Controls.Add(startButton);

        }
        private bool CheckTheAnswer(List<MathExample> examples, List<decimal> answers)
        {
            int index = -1;
            foreach(MathExample example in examples)
            {
                index++;
                if (example.Calculate() != answers[index]) return false;
            }
            return true;
        }
    }
}
