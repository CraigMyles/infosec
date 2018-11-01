using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace infosecQuiz
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Feature to be added soon.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Check if this computer is verified

            var url = "https://craig.im/infosec.php";
            var client = new WebClient();
            var response = client.DownloadString(new Uri(url));
            MessageBox.Show(response);

        }
    }
}
