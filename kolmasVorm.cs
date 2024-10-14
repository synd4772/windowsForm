using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class kolmasVorm : Form
    {
        TableLayoutPanel tlp;
        List<List<Label>> columnsAndRows;
        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };
        Label firstClicked = null;
        Label secondClicked = null;
        bool next = false;

        public kolmasVorm()
        {
            timer1 = new System.Windows.Forms.Timer();
            this.Text = "Matching Game";
            this.Size = new Size(550, 550);
            tlp = new TableLayoutPanel();
            tlp.BackColor = Color.CornflowerBlue;
            tlp.Dock = DockStyle.Fill;
            tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tlp.RowCount = 4;
            tlp.ColumnCount = 4;
            columnsAndRows = new List<List<Label>>();
            for (int i = 0; i < tlp.RowCount; i++)
            {
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }

            for (int i = 0; i < tlp.RowCount; i++)
            {
                Console.WriteLine($"i = {i}");
                columnsAndRows.Add(new List<Label>());
                for (int j = 0; j < tlp.ColumnCount; j++)
                {
                    Label label = new Label();
                    label.AutoSize = false;
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Font = new Font("Webdings", 48, FontStyle.Bold);
                    int randInt = random.Next(0, icons.Count);
                    label.Text = icons[randInt];
                    icons.RemoveAt(randInt);
                    label.ForeColor = Color.CornflowerBlue;
                    label.Click += new EventHandler(label1_Click);
                    
                    tlp.Controls.Add(label);
                    columnsAndRows[i].Add(label);
                }
            }

            this.Controls.Add(tlp);

        }
        private void label1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked");
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (this.next)
                {
                    firstClicked.ForeColor = firstClicked.BackColor;
                    secondClicked.ForeColor = secondClicked.BackColor;
                    this.next = false;
                    firstClicked = null;
                    secondClicked = null;
                }
                if (clickedLabel.ForeColor == Color.Black)
                    return;

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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tlp.Controls)
            {
                Label iconLabel = control as Label;

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
