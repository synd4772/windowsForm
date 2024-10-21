using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KolmRakendust.Forms.Game.Logic
{
    public partial class DataManagment
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public string UsersFilePath { get; set; } = @"../../../Forms/Game/users.txt";
        public string GamesFilePath { get; set; } = @"../../../Forms/Game/games.txt";
        public List<User> CurrentUsers { get; set; }
        public List<Game> CurrentGames { get; set; }

        public DataManagment()
        {
            this.CurrentUsers = GetCurrentUsersFromFile();
            this.CurrentGames = GetCurrentGamesFromFile();
            this.DeleteNotFinishedGames();
        }
    }
}
