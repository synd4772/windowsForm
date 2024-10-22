using KolmRakendust.Core.Enums.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Forms.Game.Logic.Moves
{
    public static class GameUtils
    {
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
    }
}

