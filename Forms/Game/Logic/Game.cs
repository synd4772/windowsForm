using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Core.Enums.Game;
using KolmRakendust.Forms.Game.Logic.Moves;

namespace KolmRakendust.Forms.Game.Logic
{

    public delegate void OnMistake();
    public class Game
    {
        public int GameId { get; set; }
        public event OnMistake OnMistakes;
        private int _mistakes = 0;
        public int Time { get; set; } = -1;
        public MoveQueue MoveQueue { get; set;} = new MoveQueue();
        public string[,] BoardSymbols { get; set; } = new string[4,4];
        public int Mistakes
        {
            get
            {
                return _mistakes;
            }
            set
            {
                _mistakes = value;
                if(OnMistakes != null)
                {
                    OnMistakes();
                }

            }
        }
        public string CurrentUserName { get; set; }
        public string CurrentTime { get; set; } = DateTime.Now.ToString("dd-MM-yyyy|HH:mm:ss;");

        public Game(string name) 
        {
            this.CurrentUserName = name;
        }
        public Game(User user)
        {
            this.CurrentUserName = user.Username;
        }
        public Game(string name, int time, int mistakes, DateTime currentTime)
        {
            Time = time;
            Mistakes = mistakes;
            CurrentUserName = name;
            CurrentTime = currentTime.ToString("dd-MM-yyyy|HH:mm:ss;");
        }
        public Game(string name, int time, int mistakes, string currentTime)
        {
            Time = time;
            Mistakes = mistakes;
            CurrentUserName = name;
            CurrentTime = currentTime;
        }
        public Game() { }
        public override string ToString()
        {
            string returnString = ">>>>\n";
            string gameData = $"{GameId}:{CurrentUserName}:{Time}:{Mistakes}\n";
            returnString += gameData;
            string timeString = $"{CurrentTime}\n";
            returnString += timeString;
           
            returnString += "{\n";
            for(int i = 0; i < BoardSymbols.GetLength(0); i++)
            {
                string result = "";
                
                for(int j = 0; j < BoardSymbols.GetLength(1); j++)
                {
                    result += $"{BoardSymbols[i, j]}{(j + 1 == BoardSymbols.GetLength(1) ? "\n" : ",")}";
                }
                returnString += $"{result}";
            }
            returnString += "}\n{\n";

            foreach(string move in MoveQueue.CurrentMoves)
            {
                returnString += $"{move}\n";
            }
            returnString += "}\n>>>>";


            return returnString;
            // example:
//>>>>
//1:john: 33:9
//21 - 10 - 2024 | 16:16:08
//{
//                !,v,b,s
//                !,v,b,s
//                q, w, e, r
//q,w,e,r
//}
//            {
//                3:5 - FirstMove - Spider
//            3:5 - FirstMove - Spider
//            3:5 - FirstMove - Spider
//            3:5 - FirstMove - Spider
//            }
//>>>>
        }
        public static explicit operator Game(string[] data)
        {
            MoveQueue moveQueue = new MoveQueue();
            bool boardSymbolsCompleted = false;
            bool moveCompleted = false;

            string[,]? boardSymbols = null;
            int? boardSymbolsX = null;
            int boardSymbolsY = 0;
            

            List<string[]> tempStringList = new List<string[]>();

            bool figure = false;
            for(int i = 0; i < data.Length; i++)
            {
                if (data[i] == "{")
                {
                    figure = true;
                    continue;
                }
                else if (data[i] == "}")
                {
                    figure = false;
                    if (!boardSymbolsCompleted)
                    {
                        boardSymbols = new string[boardSymbolsX.Value, boardSymbolsY];
                        for(int j = 0; j < boardSymbolsY; j++)
                        {
                            for(int x = 0; x < boardSymbolsX.Value; x++)
                            {
                                boardSymbols[j, x] = tempStringList[j][x];
                            }
                        }
                        boardSymbolsCompleted = true;
                    }
                    else if (!moveCompleted)
                    {
                        break;
                    }
                    continue;
                }
                if (figure)
                {
                    if (!boardSymbolsCompleted)
                    {
                        boardSymbolsY++;
                        string[] row = data[i].Split(",");
                        tempStringList.Add(row);
                        if (boardSymbolsX is null)
                        {
                            boardSymbolsX = row.Length;
                        }
                    }
                    else if (!moveCompleted)
                    {
                        string[] splittedMoveData = data[i].Split("-");
                        int[] movePosition = [ int.Parse(splittedMoveData[0].Split(":")[0]), int.Parse(splittedMoveData[0].Split(":")[1])];
                        Move move = new Move(movePosition, (MoveType)Enum.Parse(typeof(MoveType), splittedMoveData[1]), (SymbolType)Enum.Parse(typeof(SymbolType), splittedMoveData[2]));
                        moveQueue.AddMove(move);
                    }
                }
            }

            string[] splitedGameData = data[0].Split(":");
            

            Game game = new Game
            {
                GameId = int.Parse(splitedGameData[0]),
                CurrentUserName = splitedGameData[1],
                Time = int.Parse(splitedGameData[2]),
                Mistakes = int.Parse(splitedGameData[3]),
                CurrentTime = data[1],
                MoveQueue = moveQueue,
                BoardSymbols = boardSymbols
            };
            return game;
        }
    }
}
