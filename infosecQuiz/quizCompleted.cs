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
    public partial class quizCompleted : Form
    {

        public quizCompleted(int currentScore, int numQuestions)
        {
            InitializeComponent();

            int score = currentScore;
            int totalQuestions = numQuestions;
            string username = "";
            
            displayUsername(getUsername());
            displayScore(score, totalQuestions);

        }

        public string getUsername()
        {
            //return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return Environment.UserName;
        }

        public void displayUsername(string username)
        {
            label4.Text = username;
        }

        public void displayScore(int score, int totalQuestions)
        {
            label6.Text = (score+"/"+totalQuestions);
        }










        private void button3_Click(object sender, EventArgs e)
        {
            home myform = new home();
            this.Hide();
            myform.ShowDialog();
        }
    }
}
