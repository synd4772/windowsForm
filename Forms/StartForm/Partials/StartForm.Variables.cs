using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolmRakendust
{
    public partial class StartForm
    {

        public int ClickCount { get; set; } = 0;
        public int DoubleClickVar { get; set; } = 0;
        public TreeView Tree { get; set; } = new TreeView();
        public Button Btn { get; set; } = new Button();
        public Label Lbl { get; set; } = new Label();
        public PictureBox Pbox { get; set; } = new PictureBox();
        public CheckBox Chk1 { get; set; } = new CheckBox();
        public CheckBox Chk2 { get; set; } =  new CheckBox();
        public RadioButton Rdb1 { get; set; } = new RadioButton();
        public RadioButton Rdb2 { get; set; } = new RadioButton();
        public RadioButton Rbtn { get; set; } = new RadioButton();
        public List<string> Rbtn_list { get; set; } = new List<string>();
        public TextBox Txt { get; set; } = new TextBox();
        public ListBox Lb { get; set; } = new ListBox();
        public DataGridView DGV { get; set; } = new DataGridView();
        public DataSet DS { get; set; } = new DataSet();
        public Form? CurrentVorm { get; set; }
        public Random Rand{ get; set; } = new Random();

        public PictureViewer Ev { get; set; } = new PictureViewer(800, 500);
        public MathQuizForm Tv { get; set; } = new MathQuizForm(500, 400);
        public Game Kv { get; set; } = new Game();
    }

}
