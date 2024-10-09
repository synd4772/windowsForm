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
    public partial class teineVorm : Form
    {
        Label lbl;
        Label lbl2;
        Label lbl3;
        Label lbl4;
        Label lbl5;
        public teineVorm(int x, int y)
        {
            this.Width = x;
            this.Height = y;
            this.Text = "Math Quiz";
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

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

            lbl3 = new Label();
            lbl3.Text = "?";
            lbl3.AutoSize = false;
            lbl3.Size = new Size(60, 50);
            lbl3.Font = new Font("Arial", 18);
            lbl3.TextAlign = ContentAlignment.MiddleCenter;
            lbl3.Location = new Point(50, 75);
            lbl3.Name = "plusLeftLabel";

            lbl4 = new Label();
            lbl4.Text = "+";
            lbl4.AutoSize = false;
            lbl4.Size = new Size(60, 50);
            lbl4.Font = new Font("Arial", 18);
            lbl4.TextAlign = ContentAlignment.MiddleCenter;
            lbl4.Location = new Point(50, 75);
            lbl4.Name = "plusRightLabel";

            lbl4 = new Label();
            lbl4.Text = "=";
            lbl4.AutoSize = false;
            lbl4.Size = new Size(60, 50);
            lbl4.Font = new Font("Arial", 18);
            lbl4.TextAlign = ContentAlignment.MiddleCenter;
            lbl4.Location = new Point(50, 75);
            lbl4.Name = "plusRightLabel";

            this.Controls.Add(lbl);
            this.Controls.Add(lbl2);

        }
    }
}
