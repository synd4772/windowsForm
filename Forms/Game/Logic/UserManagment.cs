using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KolmRakendust.Forms.Game.Logic
{
    public class UserManagment
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public string FilePath { get; set; } = @"../../../Forms/Game/users.txt";
        public List<User> CurrentUsers { get; set; }
        public UserManagment()
        {
            this.CurrentUsers = GetCurrentUsersFromFile();
        }
        public List<User> GetCurrentUsersFromFile()
        {
            List<User> users = new List<User>();
            using(StreamReader reader = new StreamReader(FilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {

                    User user = (User)line.Trim();
                    users.Add(user);
                }
            }
            return users;
        }
        public void AddUser(User user)
        {
            using(StreamWriter writer = new StreamWriter(FilePath, true))
            {
                writer.Write($"\n{user.ToString()}");
            }
            this.CurrentUsers = GetCurrentUsersFromFile();
        }
        public void UpdateFile()
        {
            int index = -1;
            using(StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach(User user in CurrentUsers)
                {
                    index++;
                    Console.WriteLine($"{user}{(index + 1 == CurrentUsers.Count ? "\n" : "")}");
                    writer.Write($"{user}{(index + 1 != CurrentUsers.Count ? "\n" : "")}");
                }
            }
        }

        public bool FindUser(User fUser)
        {
            foreach(User user in CurrentUsers)
            {
                if (fUser == user)
                {
                    return true;
                }
            }
            return false;
        }
        public bool FindUser(string name)
        {
            foreach(User user in CurrentUsers)
            {
                if (user.Username == name)
                {
                    return true;
                }
            }
            return false;
        }
        public User? GetUserByName(string name)
        {
            foreach(User user in CurrentUsers)
            {
                if (user.Username == name)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
