
using System.Runtime.InteropServices;
namespace WinFormsApp3
{
    public partial class StartForm: Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        int clickCount;
        int tt;
        TreeView tree;
        Button btn;
        Label lbl;
        PictureBox pbox;
        CheckBox chk1;
        CheckBox chk2;
        RadioButton rdb1;
        RadioButton rdb2;
        RadioButton rbtn;
        List<RadioButton> rbtn_list;
        TextBox txt;

        public StartForm()
        {
            clickCount = 0;
            tt = 0;
            this.Height = 500;
            this.Width = 700;

            this.Text = "Vorm elementidega";
            tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect;
            TreeNode tn = new TreeNode("Elemendid:");
            tn.Nodes.Add(new TreeNode("Nupp"));
            tn.Nodes.Add(new TreeNode("Silt"));
            tn.Nodes.Add(new TreeNode("Pilt"));
            tn.Nodes.Add(new TreeNode("Märkeruut"));
            tn.Nodes.Add(new TreeNode("Raadionupp"));

            tree.Nodes.Add(tn);
            this.Controls.Add(tree);




        }
        public void Pbox_DoubleClick(object? sender, EventArgs e)
        {
            string[] pildid = { "esimene.png", "teine.jpg", "kolmas.png" };
            string fail = pildid[tt];
            pbox.Image = Image.FromFile(@"..\..\..\" + fail);
            tt++;
            if (tt == 3) { tt = 0; }
        }

        public void Lbl_MouseLeave(object? sender, EventArgs e)
        {
            lbl.BackColor = Color.Purple;
        }
        public void Lbl_MouseHover(object? sender, EventArgs e)
        {
            lbl.BackColor = Color.Red;
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            
            clickCount++;
            if(clickCount % 2 == 0)
            {
                btn.BackColor = Color.Red;
            }
            else
            {
                btn.BackColor = Color.Green;
            }
        }
        private void Tree_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            AllocConsole();
            Console.WriteLine($"{e.Node.Text == "Nupp"}");
            if (e.Node.Text == "Nupp")
            {
                //nupp-button
                btn = new Button();
                btn.Text = "Vajuta siia";
                btn.Height = 50;
                btn.Width = 70;
                btn.Location = new Point(150, getFreeY(10));
                btn.Click += Btn_Click;
                Controls.Add(btn);
            }
            else if (e.Node.Text == "Silt")
            {
                //silt_label
                lbl = new Label();
                lbl.Text = "Aknade elemendid c# abil";
                lbl.Font = new Font("Arial", 30, FontStyle.Underline);
                lbl.Size = new Size(200, 50);
                lbl.Location = new Point(150, getFreeY(10));
                lbl.MouseHover += Lbl_MouseHover;
                lbl.MouseLeave += Lbl_MouseLeave;
                Controls.Add(lbl);
            }
            else if (e.Node.Text == "Pilt")
            {

                pbox = new PictureBox();
                pbox.Size = new Size(160, 160);
                pbox.Location = new Point(150, getFreeY(10));
                pbox.SizeMode = PictureBoxSizeMode.Zoom;
                pbox.TabIndex = 0;
                pbox.Image = Image.FromFile(@"..\..\..\zxc.jpg");
                pbox.DoubleClick += Pbox_DoubleClick;
                Controls.Add(pbox);
            }
            else if (e.Node.Text == "Märkeruut")
            {
                chk1 = new CheckBox();
                chk1.Checked = false;
                chk1.Text = e.Node.Text;
                chk1.Size = new Size(chk1.Text.Length * 10, chk1.Size.Height);
                chk1.Location = new Point(150, getFreeY(10));

                chk2 = new CheckBox();
                chk2.Checked = false;
                // chk2.Image = Image.FromFile(@"..\..\..\zxc.png");
                chk2.BackgroundImage = Image.FromFile(@"..\..\..\zxc.jpg");
                chk2.BackgroundImageLayout = ImageLayout.Zoom;
                chk2.Size = new Size(100, 100);
                chk2.Location = new Point(150, getFreeY(10));
                chk2.CheckedChanged += new EventHandler(Chk_CheckedChanged);
                Controls.Add(chk1);
                Controls.Add(chk2);

            }
            else if (e.Node.Text == "Raadionupp")
            {
                rdb1 = new RadioButton();
                rdb1.Checked = false;
                rdb1.Text = "Must teema";
                rdb1.Location = new Point(150, getFreeY(10));

                rdb2 = new RadioButton();
                rdb2.Text = "Valge teema";
                rdb2.Location = new Point(150, getFreeY(10));

                Controls.Add(rdb1);
                Controls.Add(rdb2);
                rbtn_list.Add(rdb1);
                rbtn_list.Add(rdb2);

                //int x = 20; 
                //for (int i = 0; i < rbtn_list.Count; i++)
                //{
                //    rbtn = new RadioButton();
                //    rbtn.Checked = false;
                //    rbtn.Text = rbtn_list[i].Text;
                //    rbtn.Height = x;
                //    x += 20;
                    
                //}
            }
            else if (e.Node.Text == "Tekstikast")
            {
                txt = new TextBox();
                txt.Location = new Point(150, getFreeY(10));
                txt.Font = new Font("Arial", 26);
                txt.Width = 100;
                txt.TextChanged += Txt_TextChanged;
                Controls.Add(txt);
            }
        }

        private void Txt_TextChanged(object? sender, EventArgs e)
        {

        }

        private void Rdb2_CheckedChanged(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private int getFreeY(int plus)
        {
            List<int?> ints = new List<int?>() { 
            btn?.Height, lbl?.Height, chk1?.Height, pbox?.Height, chk2?.Height, rdb1?.Height, rdb2?.Height
            };
            int sum = 0;
            foreach (int? i in ints)
            {
                if( i is not null)
                {
                    sum += (int)i;
                }
            }
            Console.WriteLine(sum);
            return sum != 0 ? sum + plus : sum;
        }

        private int getFreeY()
        {
            List<int?> ints = new List<int?>() {
            btn?.Height, lbl?.Height, chk1?.Height, pbox?.Height, chk2?.Height, rdb1?.Height, rdb2?.Height
            };
            int sum = 0;
            foreach (int? i in ints)
            {
                if (i is not null)
                {
                    sum += (int)i;
                }
            }
            Console.WriteLine(sum);
            return sum;
        }

        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            if (chk1.Checked && chk2.Checked)
            {
                lbl.BorderStyle = BorderStyle.Fixed3D;
                pbox.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (chk1.Checked)
            {
                lbl.BorderStyle = BorderStyle.Fixed3D;
                pbox.BorderStyle = BorderStyle.None;
            }
            else if (chk2.Checked)
            {
                pbox.BorderStyle = BorderStyle.Fixed3D;
                lbl.BorderStyle = BorderStyle.None; 
            }
            else
            {
                lbl.BorderStyle = BorderStyle.None;
                pbox.BorderStyle = BorderStyle.None;
            }
        }
    }
}
