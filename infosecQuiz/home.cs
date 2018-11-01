using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            //MessageBox.Show("Feature to be added soon.");
            //Display login.cs and kill current menu
            login myform = new login();
            this.Hide();
            myform.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Check if this computer is verified

            var uri = "https://craig.im/infosec.php";
            var client = new WebClient();

            var data = new NameValueCollection();
            data["api_method"] = "getUsers";
            data["api_data"] = "a";

            var responseBytes = client.UploadValues(uri, "POST", data);

            string responseString = Encoding.Default.GetString(responseBytes);
            //var response = client.DownloadString(new Uri(url));
            MessageBox.Show("Response: "+responseString);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}