
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

            tree.Nodes.Add(tn);
            this.Controls.Add(tree);
            //nupp-button
            btn = new Button();
            btn.Text = "Vajuta siia";
            btn.Height = 50;
            btn.Width = 70;
            btn.Location = new Point(150, 50);
            btn.Click += Btn_Click;

            //silt_label
            lbl = new Label();
            lbl.Text = "Aknade elemendid c# abil";
            lbl.Font = new Font("Arial", 30, FontStyle.Underline);
            lbl.Size = new Size(200, 50);
            lbl.Location = new Point(150, 0);
            lbl.MouseHover += Lbl_MouseHover;
            lbl.MouseLeave += Lbl_MouseLeave;

            pbox = new PictureBox();
            pbox.Size = new Size(160, 160);
            pbox.Location = new Point(150, btn.Height + lbl.Height + 5);
            pbox.SizeMode = PictureBoxSizeMode.Zoom;
            pbox.TabIndex = 0;
            pbox.Image = Image.FromFile(@"..\..\..\zxc.jpg");
            pbox.DoubleClick += Pbox_DoubleClick;

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
                Controls.Add(btn);
            }
            else if (e.Node.Text == "Silt")
            {
                Controls.Add(lbl);
            }
            else if (e.Node.Text == "Pilt")
            {
                Controls.Add(pbox);
            }
            else if (e.Node.Text == "Märkeruut")
            {
                chk1 = new CheckBox();
                chk1.Checked = false;
                chk1.Text = e.Node.Text;
                chk1.Size = new Size(chk1.Text.Length + 10, chk1.Size.Height);
                chk1.Location = new Point(100, btn.Height + lbl.Height + pbox.Height + 10);

                chk2 = new CheckBox();
                chk2.Checked = false;
                // chk2.Image = Image.FromFile(@"..\..\..\zxc.png");
                chk2.BackgroundImage = Image.FromFile(@"..\..\..\zxc.png");
                chk2.BackgroundImageLayout = ImageLayout.Zoom;
                chk2.Size = new Size(100, 100);
                chk2.Location = new Point(150, btn.Height + lbl.Height + pbox.Height + chk1.Height + 15);
                chk2.CheckedChanged += new EventHandler(Chk_CheckedChanged);
                Controls.Add(chk1);
                Controls.Add(chk2);

            }
        }

        private void Chk_CheckedChanged(object? senderk, EventArgs e)
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
