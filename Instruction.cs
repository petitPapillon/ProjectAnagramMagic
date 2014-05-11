using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using domasno.Properties;

namespace domasno
{
    public partial class Instruction : Form
    {
        public int width { get; set; }
        public int height { get; set; }
        public int gbWidth { get; set; }
        public int gbHeight { get; set; }
        public int lblWidth { get; set; }
        public int lblHeight { get; set; }
        Bitmap wallpaper;
        Bitmap letters;

        public Instruction()
        {
            InitializeComponent();
        
            StringBuilder sb = new StringBuilder();
            sb.Append("На почетокот на играта се избираат самогласки и согласки. \n");
            sb.Append("Доколку сте нов играч или сте избрале опција нова игра треба \n");
            sb.Append("да го пополните соодветното поле за име.Во текот на играњето \n");
            sb.Append("имате неколку копчиња на располагање.Next Round e за нова рунда, \n");
            sb.Append("рундата ја завршувате преку копчето IAMDONE\n");
            sb.Append(" или доколку ви истекло времето за погодување.\n");
            sb.Append("Се надеваме дека ќе ви биде забавно!\n");
            label1.Text = sb.ToString();
            label1.Font = new Font("SketchFlow Print", 14, FontStyle.Bold);
            label1.ForeColor = Color.MediumPurple;
            width = Width;
            height = Height;
            gbWidth = gbInstructions.Width;
            gbHeight = gbInstructions.Height;
            lblWidth = label1.Width;
            lblHeight = label1.Height;
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            int x = Width - width;
            int y = Height - height;
            gbInstructions.Width = gbWidth + x;
            gbInstructions.Height = gbHeight + y;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Instruction_Load(object sender, EventArgs e)
        {
            this.Location = new Point(270, 230);
        }
    }
}
