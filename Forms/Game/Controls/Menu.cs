using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Forms.Game.Logic;
using gamePath = KolmRakendust.Forms.Game.Logic;

namespace KolmRakendust.Forms.Game.Controls
{
    public delegate void VoidDelegate();
    public partial class Menu: UserControl
    {
        public event VoidDelegate onStartGame;
        public event VoidDelegate onRecentGames;
        public Button SettingsButton { get; set; } = new Button
        {
            Name = "SettingsButton",
            Font = new Font("Arial", 18),
            Size = new Size(150, 50),
            Text = "Settings"
        };
        public Button StartGameButton { get; set; } = new Button
        {
            Name = "StartGame",
            Font = new Font("Arial", 18),
            Size = new Size(150, 50),
            Text = "Start game"
        };
        public Button RecentGames { get; set; } = new Button
        {
            Name = "RecentGames",
            Font = new Font("Arial", 18),
            Size = new Size(250, 50),
            Text = "Recent games"
        };
        public User CurrentUser { get; set; }

        public int FormWidth { get; } = 480;
        public int FormHeight { get; } = 450;
        public Menu(User user)
        {
            DataManagment dm = DataManagment.Instance;

            this.Size = new Size(FormWidth, FormHeight);

            SettingsButton.Location = new Point(FormWidth - SettingsButton.Size.Width, FormHeight - SettingsButton.Height);
            StartGameButton.Location = new Point(0, FormHeight - StartGameButton.Height);
            StartGameButton.Click += startGame_click;
            Label welcomeLabel = new Label
            {
                Text = $"Hi, {user.Username}!",
                Font = new Font("Arial", 18),
                Size = new Size(450, 50)
            };
            welcomeLabel.Location = new Point(0, 0);

            gamePath.Game? bestUserGame = dm.GetBestGame(user);

            Label scoreLabel = new Label
            {
                Text = $"Your best game: {(bestUserGame is not null ? $"{bestUserGame.Time} seconds, {bestUserGame.Mistakes} mistakes." : "nothing")}",
                Font = new Font("Arial", 18),
                Size = new Size(480, 50)
            };
            scoreLabel.Location = new Point(0, 50);

            RecentGames.Location = new Point(FormWidth - RecentGames.Size.Width, FormHeight - RecentGames.Height - RecentGames.Height - 50);
            RecentGames.Click += recentGames_click;
            
            foreach(Control control in new List<Control>() { SettingsButton, StartGameButton, RecentGames, welcomeLabel, scoreLabel})
            {
                this.Controls.Add(control);
            }
        }

        private void recentGames_click(object? sender, EventArgs e)
        {
            if(onRecentGames is not null)
            {
                onRecentGames();
            }
        }

        private void startGame_click(object? sender, EventArgs e)
        {
            if (onStartGame != null)
            {
                onStartGame();
            }
            
        }
    }
}
