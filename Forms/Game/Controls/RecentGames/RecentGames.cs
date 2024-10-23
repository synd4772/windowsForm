using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Forms.Game.Logic;

namespace KolmRakendust.Forms.Game.Controls
{
    public delegate void GameData(Logic.Game game);
    public partial class RecentGames : UserControl
    {
        public event GameData onGameClick;

        public Panel MainPanel { get; set; } = new Panel
        {
            AutoScroll = true,
            Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.FixedSingle
        };
        public List<Logic.Game> Games { get; set; }

        public RecentGames(List<Logic.Game> games)
        {
            this.ClientSize = new Size(450, 450);
            this.Games = games;
            int positionY = 0;
            foreach(GameCard gameCard in GetGameCards())
            {
                gameCard.Location = new Point(0, positionY);
                gameCard.Tag = gameCard.Game;
                gameCard.Click += (object? sender, EventArgs e) =>
                {
                    GameCard? gamecard = sender as GameCard;
                    if(gamecard is null) return;

                    if(onGameClick is not null) onGameClick(gamecard.Game);
                };
                
                positionY += gameCard.Height;
                this.MainPanel.Controls.Add(gameCard);
            }
            this.Controls.Add(MainPanel);
        }

        public List<GameCard> GetGameCards()
        {
            List<GameCard> returnList = new List<GameCard>();

            foreach(Logic.Game game in Games)
            {
                GameCard gameCard = new GameCard(game);
                gameCard.Game = game;
                gameCard.BorderStyle = BorderStyle.FixedSingle;
                returnList.Add(gameCard);
            }

            return returnList;
        }

    }
}
