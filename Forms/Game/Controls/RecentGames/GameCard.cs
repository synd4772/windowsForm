using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Core.Enums;

namespace KolmRakendust.Forms.Game.Controls
{
    public partial class GameCard: UserControl
    {
        public Board Board {get; set;}
        public Logic.Game Game { get; set; }
        public GameCard(Logic.Game game)
        {
            this.ClientSize = new Size(445, 100);
            Board = new Board(BoardType.Visual, game, 100, 100);

            this.Controls.Add(Board);
            Label timeLabel = new Label
            {
                Font = new Font("Arial", 15),
                Text = $"Seconds: {game.Time}",
                Size = new Size(300, 20)
            };
            Label mistakesLabel = new Label
            {
                Font = new Font("Arial", 15),
                Text = $"Mistakes: {game.Mistakes}",
                Size = new Size(300, 20)

            };
            Label startedAtLabel = new Label
            {
                Font = new Font("Arial", 11),
                Size = new Size(200, 20),
                Text = game.CurrentTime
            };

            timeLabel.Location = new Point(Board.Width + 10, 10);
            mistakesLabel.Location = new Point(Board.Width + 10, timeLabel.Height + 20);
            startedAtLabel.Location = new Point(this.ClientSize.Width - startedAtLabel.Width - 10,
                this.ClientSize.Height - startedAtLabel.Height - 10);

            this.Controls.Add(Board);
            this.Controls.Add(timeLabel);
            this.Controls.Add(mistakesLabel);
            this.Controls.Add(startedAtLabel);

        }
    }
}
