using KolmRakendust.Core.Enums.MathQuiz;
using KolmRakendust.MathQuiz.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class MathQuizForm
    {
        // MathQuiz.cs
        public string VormName { get; set; } = "Math quiz"; // IVorm
        public Mode? CurrentMode { get; set; }
        public List<MathExample> CurrentExamples { get; set; } = new List<MathExample>();
        private System.Windows.Forms.Timer? Timer { get; set; }
        public Label TimeLeftLabel { get; set; } = new Label();

        public int TimeLeft { get; set; } = 0;
          
        // MathQuiz.MainMenu.cs
        public Button InfinityModeButton { get; set; } = new Button();
        public Button DefaultModeButton { get; set; } = new Button();

        // MathQuiz.DefaultMode.cs
        public int BetweenControlsX { get; set; } = 40; // IMathQuiz
        public int BetweenExamplesY { get; set; } = 40; // IMathQuiz
        public int DMStartExampleY { get; set; } = 80; // IMathQuiz , DM - DefaultMode
        public int StartExampleX { get; set; } = 100; // IMathQuiz


        public int DMTimerDuration { get; private set; } = 50; // DM - DefaultMode


        // MathQuiz.InfinityMode.cs

        public int IMStartExampleY { get; set; } = 150;

    }
}


