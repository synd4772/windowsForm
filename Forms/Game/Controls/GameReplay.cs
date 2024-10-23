using KolmRakendust.Forms.Game.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Core.Enums;
using KolmRakendust.Core.Enums.Game;
using KolmRakendust.Forms.Game.Logic.Moves;
using System.Runtime.InteropServices;


namespace KolmRakendust.Forms.Game.Controls
{
    public partial class GameReplay: UserControl
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public Board Board { get; set; }
        public Logic.Game Game { get; set; }
        private int MoveCount { get; set; }
        public List<Control> wrongMoveList = new List<Control>();
        public List<Control> rightMoveList = new List<Control>();

        public GameReplay(Logic.Game game)
        {
            AllocConsole();
            Game = game;
            this.ClientSize = new Size(400, 400 + 100);

            Board = new Board(BoardType.Replay, game.BoardSymbols, null, 400, 400);
            Board.Location = new Point(this.ClientSize.Width / 2 - Board.Width / 2, 0);
            this.Controls.Add(Board);
           
            Button nextButton = new Button
            {
                Font = new Font("Arial", 15),
                Text = "Next",
                Size = new Size(100, 40)
            };
            Button prevButton = new Button
            {
                Font = new Font("Arial", 15),
                Text = "Previous",
                Size = new Size(100, 40)
            };
            nextButton.Click += nextButton_click;
            prevButton.Click += prevButton_click;
            nextButton.Location = new Point(0, this.ClientSize.Height - nextButton.Height);
            prevButton.Location = new Point(this.ClientSize.Width - prevButton.Width, this.ClientSize.Height - prevButton.Height );
            this.Controls.Add(nextButton);
            this.Controls.Add(prevButton);
        }


        public void nextButton_click(object? sender, EventArgs e)
        {
            Move move = (Move)Game.MoveQueue.CurrentMoves[MoveCount];
            Control control = Board.GetLabelByPosition(move.Position);

            Move? prevMove = null;
            Control? prevControl = null;
            

            if(MoveCount - 1 >= 0) 
            { 
                prevMove = (Move)Game.MoveQueue.CurrentMoves[MoveCount - 1];
                prevControl = Board.GetLabelByPosition(prevMove.Position);
            }
   
            switch (move.MoveType)
            {
                case MoveType.FirstMove:
                    {
                        if(wrongMoveList.Count != 0)
                        {
                            foreach(Control wrControl in wrongMoveList)
                            {
                                wrControl.ForeColor = Color.CornflowerBlue;
                            }
                            wrongMoveList.Clear();
                        }
                        control.ForeColor = Color.Yellow;
                        break;
                    }
                case MoveType.RightMove:
                    {
                        if(rightMoveList.Count != 0)
                        {
                            foreach(Control rmControl in rightMoveList)
                            {
                                rmControl.ForeColor = Color.Black;
                            }
                            rightMoveList.Clear();
                        }

                        prevControl.ForeColor = Color.Green;
                        control.ForeColor = Color.Green;
                        rightMoveList.Add(prevControl);
                        rightMoveList.Add(control);
                        break;
                    }
                case MoveType.WrongMove:
                    {
                        prevControl.ForeColor = Color.Red;
                        control.ForeColor = Color.Red;

                        wrongMoveList.Add(prevControl);
                        wrongMoveList.Add(control);
                        break;
                    }
            }
            MoveCount++;
        }

        public void prevButton_click(object? sender, EventArgs e)
        {

            MoveCount--;
        }
    }
}
