using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class MathQuizForm
    {
        public void MainMenu()
        {
            Label header = new Label();
            header.Font = new Font("Arial", 22);
            header.Text = "Math Quiz";
            header.TextAlign = ContentAlignment.MiddleCenter;
            header.Size = new Size(250, 50);
            header.Location = new Point((this.Width / 2) - (header.Width / 2) - 10, (this.Height / 2) - 150 );

            foreach(Button button in new List<Button>() { this.DefaultModeButton, this.InfinityModeButton })
            {
                button.Size = new Size(150, 50);
                button.Font = new Font("Arial", 18);
                button.ForeColor = Color.White;
                button.Click += new EventHandler(ChooseForm);

                this.Controls.Add(button);
            }

            this.DefaultModeButton.Text = "Default mode";
            this.DefaultModeButton.Name = "DefaultMode";
            this.DefaultModeButton.BackColor = Color.Green;
            this.DefaultModeButton.Location = new Point((this.Width / 2) - (this.DefaultModeButton.Width / 2) - 100, (this.Height / 2) + 20);

            this.InfinityModeButton.Text = "Infinity mode";
            this.InfinityModeButton.Name = "InfinityMode";
            this.InfinityModeButton.BackColor = Color.Red;
            this.InfinityModeButton.Location = new Point((this.Width / 2) - (this.InfinityModeButton.Width / 2) + 100, (this.Height / 2) + 20);

            this.Controls.Add(header);
        }
        public void ChooseForm(object? sender, EventArgs e)
        {
            Button? button = sender as Button;
            if (button is null) return;
            
            if (button.Name == "DefaultMode")
            {
                this.Controls.Clear();
                this.DefaultMode();
            }
            else
            {
                this.Controls.Clear();
                this.InfinityMode();
            }
        }
    }
}
