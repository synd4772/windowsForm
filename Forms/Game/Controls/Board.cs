using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Core.Enums;
using KolmRakendust.Core.Enums.Game;
using KolmRakendust.Forms.Game.Logic;
using KolmRakendust.Forms.Game.Logic.Moves;

namespace KolmRakendust.Forms.Game.Controls
{

    public partial class Board: UserControl
    {
        public TableLayoutPanel CurrentBoard { get; set; }
        public EventHandler? LabelClick { get; set; }
        public BoardType BoardType { get; set; }
        public string[,] Symbols { get; set; }

        public Board(BoardType boardType, Logic.Game game, int width, int height)
        {
            Symbols = game.BoardSymbols;
            BoardType = boardType;

            this.ClientSize = new Size(width, height);
            Render();
        }

        public Board(BoardType boardType, string[,] symbols, EventHandler? labelClick, int width, int height)
        {
            BoardType = boardType;
            Symbols = symbols;
            LabelClick = labelClick;

            this.ClientSize = new Size(width, height);
            Render();
        }
        public Board() { }

        public Control GetLabelByPosition(int[] position)
        {
            int index = position[0] * 4 + position[1];
            return CurrentBoard.Controls[index];
        }


        public void Render()
        {
            CurrentBoard = new TableLayoutPanel
            {
                BackColor = Color.CornflowerBlue,
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset,
                RowCount = 4,
                ColumnCount = 4
            };

            for (int i = 0; i < CurrentBoard.RowCount; i++)
            {
                CurrentBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                CurrentBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }

            for(int i = 0; i < Symbols.GetLength(0); i++)
            {
                for(int j = 0; j < Symbols.GetLength(1); j++)
                {
                    Label label = new Label 
                    { 
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Webdings", BoardType == BoardType.Game ? 39 : (BoardType == BoardType.Replay ? 19 : 12), FontStyle.Bold),
                        ForeColor = BoardType == BoardType.Game || BoardType == BoardType.Replay ? Color.CornflowerBlue : Color.Black,
                        Text = Symbols[i, j],
                        
                    };

                    if(BoardType == BoardType.Game)
                    {
                        if(LabelClick is not null) label.Click += LabelClick;
                        SymbolType? smtp = GameUtils.GetValue(Symbols[i, j]);
                        label.Tag = new GameLabelTag([i, j], smtp.Value);
                    }
                    CurrentBoard.Controls.Add(label);
                }
            }
            this.Controls.Add(CurrentBoard);
        }
        
    }
}
