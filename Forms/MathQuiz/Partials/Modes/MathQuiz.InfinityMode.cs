using KolmRakendust.MathQuiz.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMQ = KolmRakendust.Core.Enums.MathQuiz;

namespace KolmRakendust
{
    public partial class MathQuizForm
    {
        public void InfinityMode()
        {
            this.Timer = new System.Windows.Forms.Timer();
            this.Timer.Tick += new EventHandler(this.infinityTimer_Tick);
            this.Timer.Interval = 1000;

            Label countLabel = new Label();
            Label correctAnswersLabel = new Label();
            Label timerLabel = new Label();
            this.TimeLeftLabel = new Label();
            MQButton nextButton = new MQButton();
            MQButton startButton = new MQButton();
            List<Control> controls = new List<Control>()
            {
                countLabel, correctAnswersLabel, timerLabel, TimeLeftLabel, nextButton, startButton
            };

            foreach(MQButton button in new List<MQButton>(){ nextButton, startButton })
            {
                button.Click  += this.button_Click;
                button.Size = new Size(200, 50);
                
            }

            nextButton.State = EMQ.ButtonState.Next;
            nextButton.Text = "Next";
            nextButton.Enabled = false;
            nextButton.Location = new Point(this.Width / 2 - nextButton.Width - 20, this.Height - nextButton.Height * 2);
            nextButton.Name = "NextButton";

            startButton.State = EMQ.ButtonState.Start;
            startButton.Text = "Start Quiz";
            startButton.Location = new Point(this.Width / 2 + startButton.Width / 2 - 100, this.Height - startButton.Height * 2) ;
            startButton.Name = "DynamicalButton";

            countLabel.Text = "Correct answers:";
            countLabel.TextAlign = ContentAlignment.MiddleLeft;
            countLabel.Size = new Size(200, 30);
            
            correctAnswersLabel.Name = "CorrectAnswersLabel";
            correctAnswersLabel.Text = "0";
            correctAnswersLabel.Location = new Point(countLabel.Width, 0);
            correctAnswersLabel.BorderStyle = BorderStyle.FixedSingle;
            correctAnswersLabel.Size = new Size(150, 30);

            timerLabel.Text = "Time:";

            timerLabel.Font = new Font("Arial", 16);
            timerLabel.Size = new Size(timerLabel.Text.Length * 16, 30);
            timerLabel.TextAlign = ContentAlignment.MiddleLeft;
            timerLabel.Location = new Point(0, correctAnswersLabel.Height + 20);

            this.TimeLeftLabel.Text = (this.TimeLeft).ToString();
            this.TimeLeftLabel.BorderStyle = BorderStyle.FixedSingle;

            this.TimeLeftLabel.Name = "timeLabel";
            this.TimeLeftLabel.Size = new Size(150, 30);
            
            this.TimeLeftLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.TimeLeftLabel.Font = new Font("Arial", 16);
            this.TimeLeftLabel.Location = new Point(countLabel.Width, correctAnswersLabel.Height + 20);

            foreach(Control control in controls)
            {
                control.Font = new Font("Arial", 18);
                this.Controls.Add(control);
            }

            

        }

        public void infinityTimer_Tick(object? sender, EventArgs e)
        {
            this.TimeLeft += 1;
            this.TimeLeftLabel.Text = this.TimeLeft.ToString();
        }

        public void IMStart()
        {
            MQButton? nextButton = this.Controls.Find("NextButton", false)[0] as MQButton;
            if (nextButton is not null)
            {
                nextButton.Enabled = true;
            }
            if(this.Timer is not null) this.Timer.Start();
            
            MathExample example = GenerateRandomExample(1, 10);
            RenderExample(this, example, IMStartExampleY);
            CurrentExamples.Add(example);


        }

        public void IMNext()
        {
            Label? label = this.Controls.Find("CorrectAnswersLabel", false)[0] as Label;
            if (label is null) return;

            label.Text = (int.Parse(label.Text) + 1).ToString();
            CurrentExamples[0].ClearControls(this);
            CurrentExamples.Clear();

            MathExample example;
            if (int.Parse(label.Text) >= 5 && int.Parse(label.Text) <= 20)
            {
                example = GenerateRandomExample(1, 20);
            }
            else if (int.Parse(label.Text) >= 21)
            {
                example = GenerateRandomExample(1, 50);
            }
            else
            {
                example = GenerateRandomExample(1, 10);
            }
            
            RenderExample(this, example, IMStartExampleY);
            CurrentExamples.Add(example);

        }

        public void IMEnd()
        {
            MQButton? nextButton = this.Controls.Find("NextButton", false)[0] as MQButton;
            if (nextButton is not null)
            {
                nextButton.Enabled = false;
            }
            if(this.Timer is not null) this.Timer.Stop();
            TimeLeft = 0;
        }
    }
}
