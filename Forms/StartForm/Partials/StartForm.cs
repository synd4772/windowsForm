
using Microsoft.VisualBasic;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KolmRakendust
{
    public partial class StartForm : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        static List<string> treeNodeNames = new List<string>() { 
            "Nupp", "Silt", "Pilt", "Marketuur", "Raadionupp", "Tekstikast", "DataGridView", "Loetelu", "Vormid"
            };
        
        public StartForm()
        {
            ClickCount = 0;
            DoubleClickVar = 0;
            AllocConsole();
            this.Height = 500;
            this.Width = 700;
            this.Text = "Vorm elementidega";

            Tree.Dock = DockStyle.Left;

            TreeNode tn = new TreeNode("Elemendid:");
            foreach(string name in treeNodeNames)
            {
                tn.Nodes.Add(new TreeNode(name));
            }
            Tree.AfterSelect += Tree_AfterSelect;
            Tree.Nodes.Add(tn);

            this.Controls.Add(Tree);

        }
        
        private int getFreeY(int plus)
        {
            List<int?> ints = new List<int?>() {
            Btn?.Height, Lbl?.Height, Chk1?.Height, Pbox?.Height, Chk2?.Height, Rdb1?.Height, Rdb2?.Height
            };
            int sum = 0;
            foreach (int? i in ints)
            {
                if (i is not null)
                {
                    sum += (int)i;
                }
            }
            return sum != 0 ? sum + plus : sum;
        }

        private int getFreeY()
        {
            List<int?> ints = new List<int?>() {
            Btn?.Height, Lbl?.Height, Chk1?.Height, Pbox?.Height, Chk2?.Height, Rdb1?.Height, Rdb2?.Height
            };
            int sum = 0;
            foreach (int? i in ints)
            {
                if (i is not null)
                {
                    sum += (int)i;
                }
            }
            return sum;
        }

        public void Pbox_DoubleClick(object? sender, EventArgs e)
        {
            string path = @"images\";
            string[] pildid = { "esimene.png", "teine.jpg", "kolmas.png" };
            string fail = pildid[DoubleClickVar];
            Pbox.Image = Image.FromFile(@"..\..\..\" + path + fail);
            DoubleClickVar++;
            if (DoubleClickVar == 3) { DoubleClickVar = 0; }
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            ClickCount++;
            if (ClickCount % 2 == 0)
            {
                Btn.BackColor = Color.Red;
            }
            else
            {
                Btn.BackColor = Color.Green;
            }
        }


        private void Tree_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node is null) { return; }
            switch (e?.Node.Text)
            {
                case "Nupp":
                    this.NuppSelect();
                    break;
                case "Silt":
                    this.SiltSelect();
                    break;
                case "Pilt":
                    this.PiltSelect();
                    break;
                case "Markeruut":
                    this.MarkeruutSelect(e);
                    break;
                case "Raadionupp":
                    this.RaadionuppSelect();
                    break;
                case "Tekstikast":
                    this.TekstikastSelect();
                    break;
                case "DataGridView":
                    this.DataGridViewSelect();
                    break;
                case "Loetelu":
                    this.LoeteluSelect();
                    break;
                case "Vormid":
                    this.VormidSelect();
                    break;

                default:
                    break;
            }
        }
        private void Btn_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton? rb = sender as RadioButton;
            if (rb is null || Lbl is null) { return; }
            Lbl.Text = rb.Text;
        }

        private void Lb_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (Tree is null || Lb is null) { return;}
            switch (Lb.SelectedIndex)
            {
                case 0: Tree.BackColor = Color.Red; break;
                case 1: Tree.BackColor = Color.Chocolate; break;
                case 2: Tree.BackColor = Color.Purple; break;
                case 3: Tree.BackColor = Color.PaleTurquoise; break;
            }
        }

        private void Txt_TextChanged(object? sender, EventArgs e)
        {
            if (Lbl is null || Txt is null) { return; }
            Lbl.Text = Txt.Text;
        }

        private void Lbl_MouseLeave(object? sender, EventArgs e)
        {
            if (Lbl is null) { return; }
            Lbl.Font = new Font("Arial", 30, FontStyle.Underline);
        }
        private void Lbl_MouseHover(object? sender, EventArgs e)
        {
            if (Lbl is null) { return; }
            Lbl.Font = new Font("Arial", 32, FontStyle.Underline);
            Lbl.ForeColor = Color.FromArgb(70, 50, 150, 200);

        }

        private void Rdb2_CheckedChanged(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            if (Lbl is null || Chk1 is null || Chk2 is null || Pbox is null) { return;}
            if (Chk1.Checked && Chk2.Checked)
            {
                Lbl.BorderStyle = BorderStyle.Fixed3D;
                Pbox.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (Chk1.Checked)
            {
                Lbl.BorderStyle = BorderStyle.Fixed3D;
                Pbox.BorderStyle = BorderStyle.None;
            }
            else if (Chk2.Checked)
            {
                Pbox.BorderStyle = BorderStyle.Fixed3D;
                Lbl.BorderStyle = BorderStyle.None;
            }
            else
            {
                Lbl.BorderStyle = BorderStyle.None;
                Pbox.BorderStyle = BorderStyle.None;
            }
        }
        private void DGV_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(DGV.SelectedCells[0].Value.ToString());
        }
    }
}
