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
        public string VormName { get; set; } = "Math quiz";
        public Mode? CurrentMode { get; set; }

        // MathQuiz.MainMenu.cs
        public Button InfinityModeButton { get; set; } = new Button();
        public Button DefaultModeButton { get; set; } = new Button();

        // MathQuiz.DefaultMode.cs

        public Label TimeLeft { get; set; } = new Label();
        public int TimerDuration { get; private set; } = 300;

        // MathQuiz.InfinityMode.cs

    }
}


