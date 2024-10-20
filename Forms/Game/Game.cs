using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KolmRakendust.Core.Interfaces;

namespace KolmRakendust
{
    public partial class Game : Form, IVorm
    {
        public TableLayoutPanel Tlp { get; set; } = new TableLayoutPanel();
        public List<List<Label>> ColumnsAndRows { get; set; } = [];
        private Random random { get; set; } = new Random();
        private readonly List<string> icons =
        [
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        ];
        public Label? firstClicked { get; set; } = null;
        public Label? secondClicked { get; set; } = null;
        public bool next { get; set; } = false;
        public string VormName { get;set; } = "Game";

        public Game()
        {
            this.Text = "Matching Game";
            this.Size = new Size(550, 550);

            timer1 = new System.Windows.Forms.Timer();

            Tlp = new TableLayoutPanel();
            Tlp.BackColor = Color.CornflowerBlue;
            Tlp.Dock = DockStyle.Fill;
            Tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            Tlp.RowCount = 4;
            Tlp.ColumnCount = 4;


            for (int i = 0; i < Tlp.RowCount; i++)
            {
                Tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                Tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }

            for (int i = 0; i < Tlp.RowCount; i++)
            {
                ColumnsAndRows.Add(new List<Label>());
                for (int j = 0; j < Tlp.ColumnCount; j++)
                {
                    int randInt = random.Next(0, icons.Count);
                    Label label = new Label();

                    label.AutoSize = false;
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Font = new Font("Webdings", 48, FontStyle.Bold);
                    label.Text = icons[randInt];
                    label.ForeColor = Color.CornflowerBlue;
                    label.Click += new EventHandler(label1_Click);

                    Tlp.Controls.Add(label);

                    icons.RemoveAt(randInt);
                    ColumnsAndRows[i].Add(label);
                }
            }

            this.Controls.Add(Tlp);

        }
        private void label1_Click(object? sender, EventArgs e)
        {
            if (sender == null) return;
            Label? clickedLabel = sender as Label;

            if (clickedLabel is null) return;


            if (this.next)
            {
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;
                this.next = false;
                firstClicked = null;
                secondClicked = null;
            }

            if (clickedLabel.ForeColor == Color.Black) return;

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            CheckForWinner();

            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
            {
                this.next = true;
            }
            timer1.Start();
        }

        private void timer1_Tick(object? sender, EventArgs e)
        {

            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in Tlp.Controls)
            {
                Label? iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }
    }
}
