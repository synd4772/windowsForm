using KolmRakendust.Core.Enums.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Forms.Game.Logic.Moves
{
    public class Move
    {
        
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public int[] Position { get; set; }
        public MoveType MoveType { get; set; }
        public SymbolType SymbolType { get; set; }

        public Move(int[] position, MoveType moveType, SymbolType symbolType)
        {
            this.Position = position;
            this.MoveType = moveType;
            this.SymbolType = symbolType;
        }
        public Move()
        {

        }
        public static explicit operator Move(string data)
        {
            
            string[] splitted = data.Split("-");
            string[] splittedPostion = splitted[0].Split(":");
            int[] postion = [int.Parse(splittedPostion[0]), int.Parse(splittedPostion[1])];
            
            return new Move(postion, (MoveType)Enum.Parse(typeof(MoveType), splitted[1]), (SymbolType)Enum.Parse(typeof(SymbolType), splitted[2]));
        }

        public override string ToString()
        {
            
            return $"{Position[0].ToString()}:{Position[1].ToString()}-{MoveType.ToString()}-{SymbolType.ToString()}"; 
            // example: "3:5-FirstMove-Spider"
        }
    }
}
