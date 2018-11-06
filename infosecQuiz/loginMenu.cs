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
    public partial class loginMenu : Form
    {
        public loginMenu(String teacherusername)
        {
            InitializeComponent();
            label1.Text = "Welcome, "+teacherusername+ ".";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //settings for machine authorisaiton
            authorisationMenu myForm = new authorisationMenu();
            this.Hide();
            myForm.ShowDialog();
            this.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
