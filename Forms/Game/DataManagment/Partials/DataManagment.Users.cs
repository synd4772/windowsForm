using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Forms.Game.Logic
{
    public partial class DataManagment
    {
        public List<User> GetCurrentUsersFromFile()
        {
            List<User> users = new List<User>();
            using (StreamReader reader = new StreamReader(UsersFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if(line == "")
                    {
                        continue;
                    }
                    User user = (User)line.Trim();
                    users.Add(user);
                }
            }
            return users;
        }
        public void AddUser(User user)
        {
            using (StreamWriter writer = new StreamWriter(UsersFilePath, true))
            {
                writer.Write($"{(this.CurrentUsers.Count > 0 ?"\n":"")}{user.ToString()}");
            }
            this.CurrentUsers = GetCurrentUsersFromFile();
        }
        public void UpdateUsersFile()
        {
            int index = -1;
            using (StreamWriter writer = new StreamWriter(UsersFilePath))
            {
                foreach (User user in CurrentUsers)
                {
                    index++;
                    writer.Write($"{user}{(index + 1 != CurrentUsers.Count ? "\n" : "")}");
                }
            }
        }

        public bool FindUser(User fUser)
        {
            foreach (User user in CurrentUsers)
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
            foreach (User user in CurrentUsers)
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
            foreach (User user in CurrentUsers)
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
