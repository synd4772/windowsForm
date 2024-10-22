using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KolmRakendust.Core.Enums;
using KolmRakendust.Core.Enums.Game;
using KolmRakendust.Core.Interfaces;
using KolmRakendust.Forms.Game.Controls;
using KolmRakendust.Forms.Game.Logic;
using KolmRakendust.Forms.Game.Logic.Moves;

namespace KolmRakendust
{
    public partial class GameForm : Form, IVorm
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public DataManagment DM { get; set; }
        public TableLayoutPanel Tlp { get; set; } = new TableLayoutPanel();
        public List<List<Label>> ColumnsAndRows { get; set; } = [];
        private Random random { get; set; } = new Random();

        private List<string> icons =
        [
            "!", "!", "N", "N", "n", "n", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        ];

        public FormType FormType { get; set; } = FormType.Game;
        public Label? firstClicked { get; set; } = null;
        public Label? secondClicked { get; set; } = null;
        public User? CurrentUser { get; set; }
        public bool next { get; set; } = false;
        public string VormName { get;set; } = "Game";
        public int CurrentTime { get; set; } = 0;
        public Game CurrentGame { get; set; }
        public System.Windows.Forms.Timer Timer { get; set; } = new System.Windows.Forms.Timer();
        public GameForm()
        {
            this.DM = DataManagment.Instance;

            this.Text = "Login form";
            this.ClientSize = new Size(550, 550);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            Login loginForm = new Login();
            loginForm.OnSuccesfulSubmit += LoggedIn;
            loginForm.Location = new Point(this.ClientSize.Width / 2 - loginForm.Width / 2, this.ClientSize.Height / 2 - loginForm.Height / 2);
            this.Controls.Add(loginForm);
            //AllocConsole();
        }
        public void LoggedIn(User user)
        {
            this.Controls.Clear();
            CurrentUser = user;
            MenuRender();
        }

        public void MenuRender()
        {
            Menu menu = new Menu(CurrentUser);
            this.Text = "General";
            menu.onStartGame += StartGame;
            menu.Location = new Point(this.ClientSize.Width / 2 - menu.Width / 2, this.ClientSize.Height / 2 - menu.Height / 2);
            this.Controls.Add(menu);
        }

        public void StartGame()
        {
            this.Controls.Clear();
            this.Text = "Matching Game";
            AllocConsole();
            this.DM = DataManagment.Instance;

            CurrentGame = new Game(CurrentUser);
            Console.WriteLine(CurrentGame.CurrentUserName);
            CurrentGame.GameId = DM.CurrentGames.Count;
            CurrentGame.OnMistakes += this.onMistake;
 


            this.Timer = new System.Windows.Forms.Timer();
            Timer.Interval = 1000;
            Timer.Start();
            Timer.Tick += timer1_Tick;
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
            List<string> iconsCopy = new List<string>(icons);
            

            for (int i = 1; i < Tlp.RowCount; i++)
            {
                
                ColumnsAndRows.Add(new List<Label>());
                for (int j = 0; j < Tlp.ColumnCount; j++)
                {
                    int randInt = random.Next(0, iconsCopy.Count);
                    Label label = new Label();
                    
                    label.AutoSize = false;
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Font = new Font("Webdings", 48, FontStyle.Bold);

                    

                    label.ForeColor = Color.CornflowerBlue;
                    label.Click += new EventHandler(label1_Click);
                    //Console.WriteLine($"i: {i -1}, j: {j}");
                    Tlp.Controls.Add(label);
                    
                    label.Text = iconsCopy[randInt];
                    
                    CurrentGame.BoardSymbols[i - 1, j] = iconsCopy[randInt];
                    SymbolType? smtp = GameUtils.GetValue(iconsCopy[randInt]);
                    if(smtp is null) return;

                    label.Tag = new GameLabelTag([i - 1, j], smtp.Value);
                    iconsCopy.RemoveAt(randInt);
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

            GameLabelTag glt = clickedLabel.Tag as GameLabelTag;
            if (glt is null) return;
            Move currentMove = new Move
            {
                Position = glt.Position,
                SymbolType = glt.SymbolType
            };
            

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

                currentMove.MoveType = Core.Enums.Game.MoveType.FirstMove;
                CurrentGame.MoveQueue.AddMove(currentMove);
                return;
            }

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            CheckForWinner();

            if (firstClicked.Text == secondClicked.Text)
            {
                currentMove.MoveType = Core.Enums.Game.MoveType.RightMove;
                firstClicked = null;
                secondClicked = null;
            }
            else
            {
                currentMove.MoveType = Core.Enums.Game.MoveType.WrongMove;
                CurrentGame.Mistakes++;
                this.next = true;
            }
            CurrentGame.MoveQueue.AddMove(currentMove);

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
            Timer.Stop();
            this.Controls.Clear();
            DM.AddGame(CurrentGame);
            this.MenuRender();
            
            MessageBox.Show($"You matched all the icons! Your time is {CurrentTime}!", "Congratulations");
            this.CurrentTime = 0;

        }
    }
}
