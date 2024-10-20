using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KolmRakendust.Core.Interfaces;

namespace KolmRakendust
{
    public partial class StartForm
    {
        private void NuppSelect()
        {
            Btn.Text = "Vajuta siia";
            Btn.Height = 50;
            Btn.Width = 70;
            Btn.Location = new Point(150, getFreeY(10));
            Btn.Click += Btn_Click;

            Controls.Add(Btn);
        }
        private void SiltSelect()
        {
            Lbl.Text = "Aknade elemendid c# abil";
            Lbl.Font = new Font("Arial", 30, FontStyle.Underline);
            Lbl.Size = new Size(200, 50);
            Lbl.Location = new Point(150, getFreeY(10));
            Lbl.MouseHover += Lbl_MouseHover;
            Lbl.MouseLeave += Lbl_MouseLeave;

            Controls.Add(Lbl);
        }
        private void PiltSelect()
        {
            Pbox.Size = new Size(160, 160);
            Pbox.Location = new Point(150, getFreeY(10));
            Pbox.SizeMode = PictureBoxSizeMode.Zoom;
            Pbox.TabIndex = 0;
            Pbox.Image = Image.FromFile(@"..\..\..\images\zxc.jpg");
            Pbox.DoubleClick += Pbox_DoubleClick;
            Controls.Add(Pbox);
        }
        private void MarkeruutSelect(TreeViewEventArgs e)
        {
            if(e.Node is null) return;

            Chk1.Checked = false;
            Chk1.Text = e.Node.Text;
            Chk1.Size = new Size(Chk1.Text.Length * 10, Chk1.Size.Height);
            Chk1.Location = new Point(150, getFreeY(10));

            Chk2.Checked = false;
            // Chk2.Image = Image.FromFile(@"..\..\..\zxc.png");
            Chk2.BackgroundImage = Image.FromFile(@"..\..\..\zxc.jpg");
            Chk2.BackgroundImageLayout = ImageLayout.Zoom;
            Chk2.Size = new Size(100, 100);
            Chk2.Location = new Point(150, getFreeY(10));
            Chk2.CheckedChanged += new EventHandler(Chk_CheckedChanged);
            Controls.Add(Chk1);
            Controls.Add(Chk2);
        }
        private void RaadionuppSelect()
        {
            int startY = 250;
            int spacing = 30;

            for (int i = 0; i < Rbtn_list.Count; i++)
            {
                Rbtn.Checked = false;
                Rbtn.Text = Rbtn_list[i];
                Rbtn.Size = new Size(100, 40);
                Rbtn.Location = new Point(350, startY + (i * spacing));
                Rbtn.CheckedChanged += new EventHandler(Btn_CheckedChanged);

                this.Controls.Add(Rbtn);
            }
        }
        private void TekstikastSelect()
        {
            Console.WriteLine(getFreeY(10));
            Txt.Location = new Point(150, getFreeY(10));
            Txt.Font = new Font("Arial", 26);
            Txt.Width = 200;
            Txt.TextChanged += new EventHandler(Txt_TextChanged);
            Controls.Add(Txt);
        }
        private void LoeteluSelect()
        {
            List<string> Rbtn_list = new List<string> { "Ьks", "Kaks", "Kolm" };
            foreach (string item in Rbtn_list)
            {
                Lb.Items.Add(item);
            }

            Lb.Location = new Point(160 + Btn.Width + Txt.Width, getFreeY(10));
            Lb.SelectedIndexChanged += Lb_SelectedIndexChanged;
            Controls.Add(Lb);
        }
        private void DialoogiAknadSelect()
        {
            MessageBox.Show("Dialogue", "This is a simple window");
            var vastus = MessageBox.Show("Insert data", "Do you want to use the InputBox?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (vastus == DialogResult.Yes)
            {
                string text = Interaction.InputBox("Write something here", "Data insertion");
                MessageBox.Show("You wrote: " + text, text);
            }
        }
        private void DataGridViewSelect()
        {
            DS = new DataSet("XML file");
            DS.ReadXml(@"..\..\..\plant_catalog.xml");
             
            DGV.Location = new Point(500, 400);
            DGV.DataSource = DS;
            DGV.DataMember = "PLANT";
            DGV.Click += DGV_Click;
            Controls.Add(DGV);
        }

        private void VormidSelect()
        {
            List<IVorm> vormid = new List<IVorm>()
            {
                 this.Ev, this.Tv, this.Kv
            };

            GroupBox vormidNuppid = new GroupBox();
            vormidNuppid.Text = "Vormid";

            int y = 20;
            foreach (IVorm vorm in vormid)
            {
                RadioButton rdb = new RadioButton();

                rdb.CheckedChanged += new EventHandler((object? sender, EventArgs e) =>
                {
                    Form? localVorm = vorm as Form;
                    if(localVorm is null) return;

                    RadioButton? rb = sender as RadioButton;
                    if(rb is null) { return; }

                    if (rb.Checked)
                    {
                        CurrentVorm?.Close();
                        Console.WriteLine((localVorm as IVorm).VormName);
                        localVorm.Show();
                        CurrentVorm = localVorm;
                    }
                });
                
                rdb.Text = vorm.VormName;
                rdb.Location = new Point(0, y);
                vormidNuppid.Controls.Add(rdb);
                y += 20;
            }
            vormidNuppid.Location = new Point(150, getFreeY());
            Controls.Add(vormidNuppid);
        }

    }
}