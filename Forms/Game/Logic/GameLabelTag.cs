using KolmRakendust.Core.Enums.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust.Forms.Game.Logic
{
    public class GameLabelTag
    {
        public int[] Position { get; set; }
        public SymbolType SymbolType { get; set; }
        public GameLabelTag(int[] position, SymbolType symbolType)
        {
            this.Position = position;
            this.SymbolType = symbolType;
        }
    }
}
