using KolmRakendust.Core.Enums.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Forms.Game.Logic { 
    public static class GameUtils
    {
        public static int FormWidth { get; } = 550;
        public static int FormHeight { get;} = 550;

        public static Dictionary<SymbolType, string> Symbols = new Dictionary<SymbolType, string>() {
            { SymbolType.Spider, "!" },
            { SymbolType.Eye, "N" },
            { SymbolType.Pepper, "n"},
            { SymbolType.Companion, "k" },
            { SymbolType.Bicycle, "b" },
            { SymbolType.Bus, "v" },
            { SymbolType.Flag, "w" },
            { SymbolType.Smoking, "z" }
            };

        public static string? GetValue(SymbolType key)
        {
            foreach (KeyValuePair<SymbolType, string> kvp in Symbols)
            {
                if (kvp.Key == key)
                {
                    return kvp.Value;
                }
            }
            return null;
        }
        public static SymbolType? GetValue(string value)
        {
            foreach (KeyValuePair<SymbolType, string> kvp in Symbols)
            {
                if (kvp.Value == value)
                {
                    return kvp.Key;
                }
            }
            return null;
        }
        public static string[,] GetRandomSymbols(string[] currentSymbols, int x, int y)
        {
            string[,] returnSymbols = new string[x,y];
            List<string> symbolsCopy = new List<string>(currentSymbols);

            Random random = new Random();

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    int randInt = random.Next(0, symbolsCopy.Count);
                    returnSymbols[i, j] = symbolsCopy[randInt];
                    symbolsCopy.RemoveAt(randInt);
                }
            }

            return returnSymbols;
        }
    }
}

