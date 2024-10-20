using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Accessibility;
using KolmRakendust.Core.Controls;
using KolmRakendust.MathQuiz.Logic;
using EMQ = KolmRakendust.Core.Enums.MathQuiz;

namespace KolmRakendust
{
    public partial class MathQuizForm
    {
        public void DefaultMode()
        {
            this.Timer = new System.Windows.Forms.Timer();
            this.Timer.Tick += new EventHandler(defaultTimer_Tick);
            this.Timer.Interval = 100;
            this.TimeLeft = this.DMTimerDuration;

            Label lbl = new Label();
            lbl.Text = "Time Left";
            lbl.Font = new Font("Arial", 16);
            lbl.Size = new Size(lbl.Text.Length * 16, 30);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            
            this.TimeLeftLabel.Text = (this.DMTimerDuration / 10).ToString();
            this.TimeLeftLabel.BorderStyle = BorderStyle.FixedSingle;

            this.TimeLeftLabel.Name = "timeLabel";
            this.TimeLeftLabel.Size = new Size(200, 30);
            
            this.TimeLeftLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.TimeLeftLabel.Font = new Font("Arial", 16);
            this.TimeLeftLabel.Location = new Point(this.Width - this.TimeLeftLabel.Width, 0);
            lbl.Location = new Point(this.Width - this.TimeLeftLabel.Width - lbl.Width - 20, 0);
            this.Controls.Add(lbl);
            this.Controls.Add(TimeLeftLabel);

            this.CurrentExamples.Clear();
            int currentY = this.DMStartExampleY;
            
            for(int i = 0; i < 4; i++)
            {
                MathExample example = GenerateRandomExample(1, 21);
                RenderExample(this, example, currentY);
                CurrentExamples.Add(example);

                currentY += this.BetweenExamplesY;
            }
            MQButton startButton = new MQButton();
            startButton.State = EMQ.ButtonState.Start;
            startButton.Text = "Start Quiz";
            startButton.Name = "DMStartButton";
            startButton.Click += new EventHandler(button_Click);
            startButton.Size = new Size(200, 50);
            startButton.TextAlign = ContentAlignment.MiddleCenter;
            startButton.Font = new Font("Arial", 18);
            startButton.Location = new Point(this.Width / 2 - startButton.Width / 2, this.Height - startButton.Height * 2);
            this.Controls.Add(startButton);
        }

        public void defaultTimer_Tick(object? sender, EventArgs e)
        {
            MQButton? button = this.Controls.Find("DMStartButton", false)[0] as MQButton;
            if (TimeLeft != 0)
            {
                this.TimeLeft -= 1;
                this.TimeLeftLabel.Text = (this.TimeLeft / 10).ToString();
            }
            else
            {
                this.Timer.Stop();
                foreach(MathExample example in this.CurrentExamples)
                {
                    if (CheckTheAnswer(example)) continue; 
                    else
                    {
                        MessageBox.Show("Stupid");
                        this.ShowAnswers();
                        if(button is not null)
                        {
                            button.State = EMQ.ButtonState.Leave;
                            button.Text = "Leave";
                        }
                        return;
                    }
                }
                this.TimeLeftLabel.Text = "Time is end";
                if(button is not null)
                {
                    button.State = EMQ.ButtonState.Leave;
                    button.Text = "Leave";
                }
                MessageBox.Show("Not stupid");
            }
        }
    }
}
