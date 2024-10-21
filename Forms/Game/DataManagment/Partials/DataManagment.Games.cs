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
        public List<Game> GetCurrentGamesFromFile()
        {
            List<Game> games = new List<Game>();
            using (StreamReader reader = new StreamReader(GamesFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] splited = line.Split("|");
                    string[] nameAndPassword = splited[0].Split(":");
                    Game game = (Game)line.Trim();
                    games.Add(game);
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
        public void DeleteNotFinishedGames()
        {
            List<Game> gamesForDelete = new List<Game>();
            foreach(Game game in CurrentGames)
            {
                if(game.Time == -1)
                {
                    gamesForDelete.Add(game);
                }
            }
            foreach(Game game in gamesForDelete)
            {
                CurrentGames.Remove(game);
            }
            this.UpdateGamesFile();
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

                if (game.Time == -1) continue;
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
                Console.WriteLine($"123, {game.CurrentTime}, {time}");
                if(game.CurrentTime.Split(";")[0] == time.Split(";")[0])
                {
                    return game;
                }

            }
            return null;

        }
    }
}
