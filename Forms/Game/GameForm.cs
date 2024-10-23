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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace KolmRakendust
{
    public partial class GameForm : Form, IVorm
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public DataManagment DM { get; set; }
        public Board? CurrentBoard { get; set; }
        public List<List<Label>> ColumnsAndRows { get; set; } = [];
        private Random random { get; set; } = new Random();

        private string[] icons =
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
            AllocConsole();
            this.DM = DataManagment.Instance;
            this.Text = "Login form";
            this.ClientSize = new Size(GameUtils.FormWidth, GameUtils.FormHeight);
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
            menu.onRecentGames += RecentGameRender; 
            menu.Location = new Point(this.ClientSize.Width / 2 - menu.Width / 2, this.ClientSize.Height / 2 - menu.Height / 2);
            this.Controls.Add(menu);
        }

        public void RecentGameRender()
        {
            this.Controls.Clear();
            RecentGames recentGames = new RecentGames(DM.FindGamesByUser(CurrentUser.Username));
            recentGames.Location = new Point(this.ClientSize.Width / 2 - recentGames.Width / 2, 0);
            recentGames.onGameClick += GameReplayRender;
            Button closeButton = new Button
            {
                Text = "Close",
                Font = new Font("Arial", 18),
                Size = new Size(100, 50)
                
            };
            closeButton.Location = new Point(this.ClientSize.Width / 2 - closeButton.Width / 2 , this.ClientSize.Height - closeButton.Height);
            closeButton.Click += (object? sender, EventArgs e) =>
            {
                this.Controls.Clear();
                MenuRender();
            };
            this.Controls.Add(closeButton);
            this.Controls.Add(recentGames);
        }

        public void GameReplayRender(Game game)
        {
            this.Controls.Clear();
            GameReplay gameReplay = new GameReplay(game);
            
            gameReplay.Location = new Point(this.ClientSize.Width / 2 - gameReplay.Width / 2, 0);

            this.Controls.Add(gameReplay);
        }

        public void StartGame()
        {
            
            this.Controls.Clear();
            this.Text = "Matching Game";
            
            this.DM = DataManagment.Instance;

            CurrentGame = new Game(CurrentUser);
            CurrentGame.GameId = DM.CurrentGames.Count;
            CurrentGame.OnMistakes += this.onMistake;
 
            this.Timer = new System.Windows.Forms.Timer();
            Timer.Interval = 1000;
            Timer.Start();
            Timer.Tick += timer_Tick;

            int rowCount = 4;
            int columnCount = 4;
            string[,] boardSymbols = GameUtils.GetRandomSymbols(icons, columnCount, rowCount);
            CurrentGame.BoardSymbols = boardSymbols;
            Board board = new Board(BoardType.Game, boardSymbols, label_Click, 400, 400);
            board.Location = new Point(this.ClientSize.Width / 2 - board.Width / 2, this.ClientSize.Height / 2 - board.Height / 2);
            this.CurrentBoard = board;

            this.Controls.Add(board);
            Label timeLabel = new Label
            {
                Text = $"Time: {CurrentTime}",
                Font = new Font("Arial", 17),
                Size = new Size(150, 50),
                Name = "TimerLabel"
            };
            this.Controls.Add(timeLabel);
            Label mistakesLabel = new Label()
            {
                Text = $"Mistakes: {CurrentGame.Mistakes}",
                Font = new Font("Arial", 17),
                Size = new Size(150, 50),
                Name = "MistakesLabel"
            };
            mistakesLabel.Location = new Point(timeLabel.Width + 20, 0);
            this.Controls.Add(mistakesLabel);
        }

        private void label_Click(object? sender, EventArgs e)
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
            Label? label = this.Controls.Find("MistakesLabel", false)[0] as Label;
            if (label is null) return;
            label.Text = $"Mistakes: {CurrentGame.Mistakes}";
        }
        private void timer_Tick(object? sender, EventArgs e)
        {
            this.CurrentTime++;
            CurrentGame.Time = this.CurrentTime;

            Label? label = this.Controls.Find("TimerLabel", false)[0] as Label;
            if (label is not null) label.Text = $"Time: {CurrentTime}";
        }

        private void CheckForWinner()
        {
            foreach (Control control in CurrentBoard.CurrentBoard.Controls)
            {
                Label? iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
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
