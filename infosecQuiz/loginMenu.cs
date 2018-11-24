using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace infosecQuiz
{
    public partial class loginMenu : Form
    {
        public static class Http
        {
            public static String Post(string uri, NameValueCollection pairs)
            {
                byte[] response = null;
                using (WebClient client = new WebClient())
                {
                    response = client.UploadValues(uri, pairs);
                }
                return Encoding.Default.GetString(response);
            }
        }

        public class API_Response
        {
            public bool IsError { get; set; }
            public string ErrorMessage { get; set; }
            public RootObject[] ResponseData { get; set; }
        }

        public class RootObject
        {
            public string response { get; set; }
        }

        public class ResetScoreList
        {
            public string response { get; set; }
        }

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
            //ADMINISTRATIVE MENU BUTTON

            //open admin panel
            adminPanel myForm = new adminPanel();
            this.Hide();
            myForm.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Main menu button
            home myFormHome = new home();
            this.Hide();
            myFormHome.ShowDialog();
            this.Show();
        }

        private void resetScoresButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to reset ALL scores?"+"\n"+ "Note: This function is irreversible.",
                "Reset Scores", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                ResetScores();
            }
            else if(dialog == DialogResult.No)
            {
                return;
            }
        }

        private void ResetScores()
        {
            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "resetScores";
            ResetScoreList thisResetScores = new ResetScoreList()
            {

            };


            //Make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
            {
                { "api_method", apiMethod},
                { "api_data",   JsonConvert.SerializeObject(thisResetScores) }
            });

            //Deserialise
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            if (!r.IsError)
            {
                //The data was succefully updated to the SQL database.
                MessageBox.Show("Scores were successfully reset.");
            }
            else
            {
                MessageBox.Show("there was an error resetting the scores.");
            }


        }
    }
}
