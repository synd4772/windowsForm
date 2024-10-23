using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KolmRakendust.Forms.Game.Logic
{
    public partial class DataManagment
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public List<Game> GetCurrentGamesFromFile()
        {
            List<Game> games = new List<Game>();
            using (StreamReader reader = new StreamReader(GamesFilePath))
            {

                string line;
                bool gameDetected = false;
                string[]? data = null;
                List<string> tempDataList = new List<string>();
                while ((line = reader.ReadLine()) != null)
                {
   
                    if (line == ">>>>")
                    {
                        if (gameDetected)
                        {
                            gameDetected = false;
                            data = new string[tempDataList.Count];
                            int index = -1;
                            foreach(string tempData in tempDataList)
                            {
                                index++;
                                data[index] = tempData;
                            }
                            Game game = (Game)data;
                            data = null;
                            tempDataList = new List<string>();
                            games.Add(game);
                            continue;
                        }
                        gameDetected = true;
                        continue;
                    }
                    else if (gameDetected)
                    {
                        tempDataList.Add(line.Trim());
                    }
                }
            }
            return games;
        }
        public void AddGame(Game game)
        {
            using (StreamWriter writer = new StreamWriter(GamesFilePath, true))
            {
                writer.Write($"\n{game.ToString()}");
            }
            this.CurrentGames = GetCurrentGamesFromFile();
        }

        public void UpdateGamesFile()
        {
            int index = -1;
            using (StreamWriter writer = new StreamWriter(GamesFilePath))
            {
                
                foreach (Game game in CurrentGames)
                {
                    
                    index++;
                    writer.Write($"{game}{(index + 1 != CurrentGames.Count ? "\n" : "")}");
                }
            }
            this.CurrentGames = GetCurrentGamesFromFile();
        }

        public bool FindGame(Game fGame)
        {
            foreach (Game game in CurrentGames)
            {
                if (fGame == game)
                {
                    return true;
                }
            }
            return false;
        }

        public Game? GetGame(Game fGame)
        {
            foreach(Game game in CurrentGames)
            {
                if(game.CurrentUserName == fGame.CurrentUserName && game.Mistakes == fGame.Mistakes && game.Time == fGame.Time && game.CurrentTime == fGame.CurrentTime)
                {
                    return game;
                }
            }
            return null;
        }

        public List<Game> FindGamesByUser(string name)
        {
            List<Game> userGames = new List<Game>();
            foreach (Game game in CurrentGames)
            {
                if (game.CurrentUserName == name)
                {
                    userGames.Add(game);
                }
            }
            return userGames;
        }
        public List<Game> FindGamesByUser(User user)
        {
            List<Game> userGames = new List<Game>();
            this.CurrentGames = GetCurrentGamesFromFile();
            foreach (Game game in CurrentGames)
            {
                if (game.CurrentUserName == user.Username)
                {
                    userGames.Add(game);
                }
            }
            return userGames;
        }

        public Game? GetBestGame(User user)
        {
            List<Game> games = this.FindGamesByUser(user);
            
            Game? bestGame = null;
            foreach (Game game in games)
            {
                if (bestGame is null )
                {
                    
                    bestGame = game;
                    continue;
                }
                if (bestGame.Time > game.Time)
                {
                    bestGame = game;
                }
            }

            if (bestGame != null) return bestGame;
            else return null;

        }
        public Game? GetGameByTime(string time)
        {
            foreach (Game game in CurrentGames)
            {
                if(game.CurrentTime.Split(";")[0] == time.Split(";")[0])
                {
                    return game;
                }

            }
            return null;

        }

        public List<int> GetUserGamesId(string username)
        {
            List<int> returnList = new List<int>();

            

            return returnList;
        }
    }
}
