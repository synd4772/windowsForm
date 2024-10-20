using KolmRakendust.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.MathQuiz.Logic;
using KolmRakendust.Core.Enums.MathQuiz;
using EMQ = KolmRakendust.Core.Enums.MathQuiz;

namespace KolmRakendust
{
    public partial class MathQuizForm
    {
        public void button_Click(object? sender, EventArgs e)
        {
            
            MQButton? button = sender as MQButton;
            if (button is null) return;

            switch (button.State)
            {
                case EMQ.ButtonState.Start:
                    if(CurrentMode == Mode.Default)
                    {
                        if (this.Timer is null) return;
                        this.Timer.Start();
                    }
                    else this.IMStart();

                    button.State = EMQ.ButtonState.InProcces;
                    button.Text = CurrentMode == Mode.Default ? "Check answers" : "Check answer";
                    break;

                case EMQ.ButtonState.Leave:
                    this.EndMode();
                    this.MainMenu();
                    break;

                case EMQ.ButtonState.InProcces:
                    this.ShowAnswers();
                    if (CurrentMode == Mode.Default) { 
                        if(this.Timer is not null) this.Timer.Stop(); 
                    }
                    else this.IMEnd();
                    button.State = EMQ.ButtonState.Leave;
                    button.Text = "Leave";
                    break;
                case EMQ.ButtonState.Next:
                {
                    if (CheckTheAnswer(CurrentExamples[0])) IMNext();
                    else
                    {
                        this.IMEnd();
                        MessageBox.Show($"{CurrentExamples[0].NUD.Value} isn't correct");
                        this.ShowAnswers();
                    }
                    break;
                }
            }

        }
        public void EndMode()
        {
            this.Controls.Clear();
            if(this.CurrentMode == Mode.Default)
            {
                this.CurrentExamples.Clear();
                TimeLeftLabel = new Label();
                Timer = new System.Windows.Forms.Timer();
            }
            else
            {
                this.CurrentExamples.Clear();
                TimeLeftLabel = new Label();
                Timer = new System.Windows.Forms.Timer();
                return;
            }
        }
        public void ShowAnswers()
        {
            foreach(MathExample example in this.CurrentExamples)
            {
                example.NUD.Value = example.Calculate();
            }
        }
        public static bool CheckTheAnswer(MathExample example)
        {
            if (example.NUD.Text == "" || example.NUD.Text == null) return false;
            if (example.Calculate() == example.NUD.Value)
            {
                return true;
            }
            return false;
        }
        public static MathExample GenerateRandomExample()
        {
            Random random = new Random();
            OperatorType randOperator;
            int y, x;
            Array values = Enum.GetValues(typeof(OperatorType));
            randOperator = (OperatorType)values.GetValue(random.Next(values.Length));

            y = random.Next(1, 10);
            x = random.Next(1, 15);
            MathExample mathEx = new MathExample(x, y, new MathOperator(randOperator));
            return mathEx;
        }
        public static MathExample GenerateRandomExample(int start, int end)
        {
            Random random = new Random();
            OperatorType randOperator;
            int y, x;
            Array values = Enum.GetValues(typeof(OperatorType));
            randOperator = (OperatorType)values.GetValue(random.Next(values.Length));

            y = random.Next(start, end);
            x = random.Next(start, end);
            MathExample mathEx = new MathExample(y, x, new MathOperator(randOperator));
            return mathEx;
        }

        public static void RenderExample(Form form, MathExample example, int y)
        {
            IMathQuiz? mathQuiz = form as IMathQuiz;
            if(mathQuiz is null) return;

            int currentX = mathQuiz.StartExampleX;
            foreach(Control control in example.GetControls())
            {
                Label? label = control as Label;
                if (label is not null)
                {
                    label.Font = new Font("Arial", 18);
                    label.Size = new Size(50,50);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Location = new Point(currentX, y);
                }
                else
                {
                    NumericUpDown? nud = control as NumericUpDown;
                    if(nud is not null)
                    {
                        control.Size = new Size(100, 50);
                        control.Font = new Font("Arial", 18);
                        control.Location = new Point(currentX + 40, y + 10);
                    }

                }
                form.Controls.Add(control);
                currentX += mathQuiz.BetweenControlsX;
            }
        }

    }
}
