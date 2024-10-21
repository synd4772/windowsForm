using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using KolmRakendust.MathQuiz.Logic;
using KolmRakendust.Core.Interfaces;
using System.Runtime.InteropServices;

namespace KolmRakendust
{

    public partial class MathQuizForm : Form, IVorm, IMathQuiz
    {
        public MathQuizForm(int x, int y)
        {
            this.Width = x;
            this.Height = y;
            this.Text = "Math Quiz";
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

            this.MainMenu();
        }
    }
}
