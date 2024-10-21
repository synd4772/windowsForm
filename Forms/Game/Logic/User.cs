using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Forms.Game.Logic
{
    public class User
    {
        public List<Game> Games { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public User(string username, string password, List<Game> score)
        {
            this.Username = username;
            this.Password = password;
            this.Games = score;
        }
        public override string ToString()
        {
            return $"{this.Username}:{this.Password};"; 
        }
        public static explicit operator User(string data)
        {
            string[] splited = data.Split("|");
            string[] nameAndPassword = splited[0].Split(":");


            return new User(nameAndPassword[0], nameAndPassword[1].Split(";")[0]);
        }
    }
}
