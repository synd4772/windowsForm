using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Forms.Game.Logic
{
    public delegate void OnMistake();
    public class Game
    {
        public event OnMistake OnMistakes;
        private int _mistakes = -1;
        public int Time { get; set; } = -1;
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

        public override string ToString()
        {
            return $"{CurrentUserName}:{Time}:{Mistakes}+{CurrentTime}";
        }
        public static explicit operator Game(string data)
        {

            string[] splited = data.Split("+");

            string[] userGameData = splited[0].Split(":");
            string score = splited[1];

 
            return new Game(userGameData[0], int.Parse(userGameData[1]), int.Parse(userGameData[2]), score.Split(";")[0]);
        }
    }
}
