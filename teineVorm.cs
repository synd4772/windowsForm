﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace WinFormsApp3
{
    public partial class teineVorm : Form
    {
        Label lbl;
        Label lbl2;

        List<List<Label>> numberLabels;
        List<NumericUpDown> numericUpDowns;
        NumericUpDown nud;

        Button startButton;

        Random random;

        int timeLeft = 300;

        private List<string> numericUpDownNames = new List<string>()
            {
                "sum", "difference", "product", "quotient"
            };
        private List<string> operators = new List<string>()
            {
                "+", "-", "×", "÷"
            };

        public teineVorm(int x, int y)
        {
            this.Width = x;
            this.Height = y;
            this.Text = "Math Quiz";
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

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
                List<Control> lst = new List<Control>();

                Label firstNumber = new Label();
                Label operatorLabel = new Label();
                Label secondNumber = new Label();
                Label equalLabel = new Label();

                firstNumber.Location = new Point(15 + 100, ExampleY + 10);
                
                operatorLabel.Location = new Point(55 + 100, ExampleY);
                secondNumber.Location = new Point(95 + 100, ExampleY + 10);
                equalLabel.Location = new Point(135 + 100, ExampleY);

                firstNumber.Name = "plusLeftLabel";
                secondNumber.Name = "plusRightLabel";

                numberLabels.Add(new List<Label>());
                numberLabels[i].Add(firstNumber);
                numberLabels[i].Add(secondNumber);

                SetDefaultParametrs(firstNumber);
                SetDefaultParametrs(operatorLabel, operators[i]);
                SetDefaultParametrs(secondNumber);
                SetDefaultParametrs(equalLabel, "=");

                lst.Add(firstNumber);
                lst.Add(operatorLabel);
                lst.Add(secondNumber);
                lst.Add(equalLabel);

                NumericUpDown numericUpDown = new NumericUpDown();
                numericUpDown.Font = new Font("Arial", 18);
                numericUpDown.MaximumSize = new Size(115, 100);
                numericUpDown.Location = new Point(165 + 100, ExampleY + 5);
                numericUpDown.Name = numericUpDownNames[i];
                numericUpDown.Minimum = -100;
                numericUpDown.TabIndex = i + 1;
                numericUpDowns.Add(numericUpDown);
                lst.Add(numericUpDown);

                ExampleY += DistanceBetweenExamples;

                foreach(var control in lst)
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
        public void StartTheQuiz(object sender, EventArgs e)
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

        private bool CheckTheAnswer()
        {
            for (int i = 0; i < 4; i++)
            {
                if (Calculate(int.Parse(numberLabels[i][0].Text), int.Parse(numberLabels[i][1].Text), numericUpDowns[i].Value, operators[i]))
                {
                    continue;
                }
                else
                {
                    return false;
                }
                
            }
            return true;
        }

        private bool Calculate(int x, int y, decimal answer, string op)
        {
            switch (op)
            {
                case "+":
                    if (x + y == answer)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "-":
                    if (x - y == answer)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "×":
                    if (x * y == answer)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "÷":
                    if (x / y == answer)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            return false;
        }
        private decimal Calculate(int x, int y, string op)
        {
            switch (op)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "×":
                    return x * y;
                case "÷":
                    return x / y;
            }
            return 0;
        }


        private void timer1_Tick(object sender, EventArgs e)
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

    }
}