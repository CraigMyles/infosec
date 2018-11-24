using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace infosecQuiz
{
    public partial class quizLose : Form
    {
        public quizLose()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            home myFormHome = new home();
            this.Hide();
            myFormHome.ShowDialog();
        }
    }
}
