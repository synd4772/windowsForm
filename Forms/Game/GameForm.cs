
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KolmRakendust.Core.Interfaces;
using KolmRakendust.Forms.Game.Controls;
using KolmRakendust.Forms.Game.Logic;

namespace KolmRakendust
{
    public partial class GameForm : Form, IVorm
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public DataManagment UM { get; set; } = new DataManagment();
        public TableLayoutPanel Tlp { get; set; } = new TableLayoutPanel();
        public List<List<Label>> ColumnsAndRows { get; set; } = [];
        private Random random { get; set; } = new Random();
        private List<string> icons =
        [
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        ];
        public Label? firstClicked { get; set; } = null;
        public Label? secondClicked { get; set; } = null;
        public User? CurrentUser { get; set; }
        public bool next { get; set; } = false;
        public string VormName { get;set; } = "Game";
        public int CurrentTime { get; set; } = 0;
        public Game CurrentGame { get; set; }

        public GameForm()
        {
            this.Text = "Login form";
            this.ClientSize = new Size(550, 550);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            Login loginForm = new Login(UM);
            loginForm.OnSuccesfulSubmit += LoggedIn;
            loginForm.Location = new Point(this.ClientSize.Width / 2 - loginForm.Width / 2, this.ClientSize.Height / 2 - loginForm.Height / 2);
            this.Controls.Add(loginForm);

        }
        public void LoggedIn(User user)
        {
            this.Controls.Clear();
            CurrentUser = user;
            MenuRender();
        }

        public void MenuRender()
        {
            Menu menu = new Menu(CurrentUser, UM);
            this.Text = "General";
            menu.onStartGame += StartGame;
            menu.Location = new Point(this.ClientSize.Width / 2 - menu.Width / 2, this.ClientSize.Height / 2 - menu.Height / 2);
            this.Controls.Add(menu);
        }

        public void StartGame()
        {
            this.Controls.Clear();
            this.Text = "Matching Game";

            CurrentGame = new Game(CurrentUser);
            CurrentGame.OnMistakes += this.onMistake;
            UM.AddGame(CurrentGame);
            CurrentGame = UM.GetGameByTime(CurrentGame.CurrentTime);


            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Start();
            timer1.Tick += timer1_Tick;
            Tlp = new TableLayoutPanel();
            Tlp.BackColor = Color.CornflowerBlue;
            Tlp.Dock = DockStyle.Fill;
            Tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            Tlp.RowCount = 5;
            Tlp.ColumnCount = 4;


            for (int i = 0; i < Tlp.RowCount; i++)
            {
                Tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                Tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }

            for (int i = 1; i < Tlp.RowCount; i++)
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
                    ColumnsAndRows[i - 1].Add(label);
                }
            }
            Label lbl = new Label
            {
                Text = $"Time: {CurrentTime}",
                Font = new Font("Arial", 17),
                Size = new Size(150, 50),
                Name = "TimerLabel"
            };
            Tlp.Controls.Add(lbl);
            Label lbl2 = new Label()
            {
                Text = $"Mistakes: {CurrentGame.Mistakes}",
                Font = new Font("Arial", 17),
                Size = new Size(150, 50),
                Name = "MistakesLabel"
            };
            Tlp.Controls.Add(lbl2);

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
                CurrentGame.Mistakes++;
                this.next = true;
            }


        }
        private void onMistake()
        {
            Label? label = Tlp.Controls.Find("MistakesLabel", false)[0] as Label;
            if (label is null) return;
            label.Text = $"Mistakes: {CurrentGame.Mistakes}";
        }
        private void timer1_Tick(object? sender, EventArgs e)
        {
            this.CurrentTime++;
            CurrentGame.Time = this.CurrentTime;

            Label? label = Tlp.Controls.Find("TimerLabel", false)[0] as Label;
            if (label is not null) label.Text = $"Time: {CurrentTime}";
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

            Tlp = new TableLayoutPanel();
            ColumnsAndRows  = [];
            icons =
            [
                "!", "!", "N", "N", ",", ",", "k", "k",
                "b", "b", "v", "v", "w", "w", "z", "z"
            ];
            timer1.Stop();
            this.Controls.Clear();
            this.MenuRender();
            UM.UpdateGamesFile();
            MessageBox.Show($"You matched all the icons! Your time is {CurrentTime}!", "Congratulations");
            this.CurrentTime = 0;

        }
    }
}
