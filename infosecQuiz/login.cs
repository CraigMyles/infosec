using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace infosecQuiz
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (!username.Text.Equals("") && !password.Text.Equals(""))
            {
                //Check if username + passwords match on db
            }
            else if (username.Text.Equals("") || password.Text.Equals(""))
            {
                MessageBox.Show("You cannot leave the fields blank.");
            }
            else
            {
                MessageBox.Show("An error has occured.");
            }

            ////testing CPU number and username

            //string cpuInfo = string.Empty;
            //ManagementClass mc = new ManagementClass("win32_processor");
            //ManagementObjectCollection moc = mc.GetInstances();

            //foreach (ManagementObject mo in moc)
            //{
            //    if (cpuInfo == "")
            //    {
            //        //Get only the first CPU's ID
            //        cpuInfo = mo.Properties["processorID"].Value.ToString();
            //        break;
            //    }
            //}
            //MessageBox.Show("Environment Username = "+Environment.UserName+"\nProcessorID ="+ cpuInfo);
        }

        private void usernameLabel_Click(object sender, EventArgs e)
        {

        }

        private void passwordLabel_Click(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
