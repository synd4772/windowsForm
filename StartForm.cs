
using System.Runtime.InteropServices;
namespace WinFormsApp3
{
    public partial class StartForm: Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        int clickCount;
        TreeView tree;
        Button btn;
        Label lbl;
        PictureBox pbox;
        public StartForm()
        {
            clickCount = 0;

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
            pbox.Size = new Size(60, 60);
            pbox.Location = new Point(150, btn.Height + lbl.Height + 5);
            pbox.SizeMode = PictureBoxSizeMode.Zoom;
            pbox.TabIndex = 0;
            pbox.Image = Image.FromFile("");

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
        }
    }
}
