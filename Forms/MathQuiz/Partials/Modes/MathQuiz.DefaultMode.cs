using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class MathQuizForm
    {

        public void DefaultMode()
        {
            Label lbl = new Label();
            lbl.Text = "Time Left";
            lbl.Font = new Font("Arial", 16);
            lbl.Size = new Size(lbl.Text.Length * 16, 30);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            
            this.TimeLeft.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.TimeLeft.Text = "123";
            this.TimeLeft.BorderStyle = BorderStyle.FixedSingle;

            this.TimeLeft.Name = "timeLabel";
            this.TimeLeft.Size = new Size(200, 30);
            
            
            this.TimeLeft.BackColor = Color.Red;
            this.TimeLeft.TextAlign = ContentAlignment.MiddleCenter;
            this.TimeLeft.Font = new Font("Arial", 16);
            this.TimeLeft.Location = new Point(this.ClientSize.Width, 0);
            this.Controls.Add(lbl);
            this.Controls.Add(TimeLeft);
        }
        //    int DistanceBetweenExamples = 45;
        //    int ExampleY = DistanceBetweenExamples;

        //    numberLabels = new List<List<Label>>();
        //    numericUpDowns = new List<NumericUpDown>();

        //    lbl = new Label();
        //    lbl.Name = "timeLabel";
        //    lbl.AutoSize = false;
        //    lbl.BorderStyle = BorderStyle.FixedSingle;
        //    lbl.Size = new Size(200, 30);
        //    lbl.Text = "";
        //    lbl.Font = new Font("Arial", 16);


        //    CurrentTimeLeft.Font = new Font("Arial", 16);
        //    CurrentTimeLeft.Text = "Time Left";
        //    CurrentTimeLeft.AutoSize = true;

        //    lbl.Location = new Point(270, 0);
        //    CurrentTimeLeft.Location = new Point(150, 0);
        //    Controls.Add(lbl);
        //    Controls.Add(CurrentTimeLeft);

        //    for (int i = 0; i < 4; i++)
        //    {
        //        Label firstNumber = new Label();
        //        Label operatorLabel = new Label();
        //        Label secondNumber = new Label();
        //        Label equalLabel = new Label();

        //        NumericUpDown numericUpDown = new NumericUpDown();

        //        firstNumber.Location = new Point(15 + 100, ExampleY + 10);
        //        operatorLabel.Location = new Point(55 + 100, ExampleY);
        //        secondNumber.Location = new Point(95 + 100, ExampleY + 10);
        //        equalLabel.Location = new Point(135 + 100, ExampleY);
        //        numericUpDown.Location = new Point(165 + 100, ExampleY + 5);

        //        firstNumber.Name = "plusLeftLabel";
        //        secondNumber.Name = "plusRightLabel";
        //        numericUpDown.Name = numericUpDownNames[i];

        //        numberLabels.Add(new List<Label>());
        //        numberLabels[i].Add(firstNumber);
        //        numberLabels[i].Add(secondNumber);

        //        SetDefaultParametrs(firstNumber);
        //        SetDefaultParametrs(operatorLabel, operators[i]);
        //        SetDefaultParametrs(secondNumber);
        //        SetDefaultParametrs(equalLabel, "=");

        //        List<Control> lst = new List<Control>() {
        //                firstNumber, operatorLabel, secondNumber, equalLabel, numericUpDown
        //                };

        //        numericUpDown.Font = new Font("Arial", 18);
        //        numericUpDown.MaximumSize = new Size(115, 100);
        //        numericUpDown.Minimum = -100;
        //        numericUpDown.TabIndex = i + 1;
        //        numericUpDowns.Add(numericUpDown);


        //        ExampleY += DistanceBetweenExamples;

        //        foreach (var control in lst)
        //        {
        //            Controls.Add(control);
        //        }

        //    startButton = new Button();
        //    startButton.Name = "startButton";
        //    startButton.Font = new Font("Arial", 14);
        //    startButton.AutoSize = true;
        //    startButton.TabIndex = 0;
        //    startButton.Click += new EventHandler(StartTheQuiz);
        //    startButton.Location = new Point(this.Width / 2 - startButton.Size.Width, this.Height - 100);
        //    startButton.Text = "Start The Quiz";
        //    Controls.Add(startButton);

        //}
        //private bool CheckTheAnswer(List<MathExample> examples, List<decimal> answers)
        //{
        //    int index = -1;
        //    foreach (MathExample example in examples)
        //    {
        //        index++;
        //        if (example.Calculate() != answers[index]) return false;
        //    }
        //    return true;
        //}
    }
}
