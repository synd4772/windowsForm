using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class StartForm: Form
    {

        public PictureViewer Ev { get; set; } = new PictureViewer(800, 500);
        public MathQuiz Tv { get; set; } = new MathQuiz(500, 400);
        public Game Kv { get; set; } = new Game();

        private void AvaPictureViewer(object? sender, EventArgs e)
        {
            RadioButton? rb = sender as RadioButton;
            if(rb is null) { return; }

            if (rb.Checked)
            {
                CurrentVorm?.Close();
                Ev.Show();
                CurrentVorm = Ev;
            }
        }
        private void AvaMathQuiz(object? sender, EventArgs e)
        {
            RadioButton? rb = sender as RadioButton;
            if(rb is null) { return; }

            if (rb.Checked)
            {
                CurrentVorm?.Close();
                Tv.Show();
                CurrentVorm = Tv;

            }
        }
        private void AvaGame(object? sender, EventArgs e)
        {
            RadioButton? rb = sender as RadioButton;
            if(rb is null) { return; }

            if (rb.Checked)
            {
                CurrentVorm?.Close();
                Kv.Show();
                CurrentVorm = Kv;

            }
        }
    }

}
