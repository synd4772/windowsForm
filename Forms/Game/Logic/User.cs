using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Forms.Game.Logic
{
    public class User
    {
        public int Score { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public User(string username, string password, int score)
        {
            this.Username = username;
            this.Password = password;
            this.Score = score;
        }
        public override string ToString()
        {
            return $"{this.Username}:{this.Password}|{this.Score.ToString()};"; 
        }
        public static explicit operator User(string data)
        {
            string[] splited = data.Split("|");
            string[] nameAndPassword = splited[0].Split(":");
            string score = splited[1].Split(";")[0];

            return new User(nameAndPassword[0], nameAndPassword[1], int.Parse(score));
        }
    }
}
