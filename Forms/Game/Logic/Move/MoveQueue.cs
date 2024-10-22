using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Forms.Game.Logic.Moves
{
    public class MoveQueue
    {
        public Game CurrentGame { get; set; }
        public List<string> CurrentMoves { get; set; }

        public void AddMove(string move)
        {
            this.CurrentMoves.Add(move);
        }
        public void AddMove(Move move)
        {
            this.CurrentMoves.Add(move.ToString());
        }

        public MoveQueue()
        {
            this.CurrentMoves = new List<string>();
        }

        public int? GetIndexOfMoveByPositionInList(int x, int y)
        {
            int index = -1;
            foreach(string move in CurrentMoves)
            {
                index++;
                Move convertedMove = (Move)move;
                if (convertedMove.Position[0] == x && convertedMove.Position[1] == y)
                {
                    return index;
                }
            }
            return null;
        }

        public override string ToString()
        {
            string movesConverted = "{";
            int index = -1;
            foreach(string move in CurrentMoves)
            {
                index++;
                movesConverted += $"{move.ToString()}{(index + 1 == CurrentMoves.Count ? "}" : ";")}";
            }
            return movesConverted;
        }
    }
}
