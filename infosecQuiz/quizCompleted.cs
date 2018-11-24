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
    public partial class quizCompleted : Form
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

        public class RootObject
        {
            public string score { get; set; }
            public string totalQuestions { get; set; }
            public string username { get; set; }
        }

        public class API_Response
        {
            public bool IsError { get; set; }
            public string ErrorMessage { get; set; }
            public RootObject[] ResponseData { get; set; }
        }

        public class AddScore
        {
            public string score { get; set; }
            public string totalQuestions { get; set; }
            public string username { get; set; }
        }

        public quizCompleted(int currentScore, int numQuestions)
        {
            InitializeComponent();

            int score = currentScore;
            int totalQuestions = numQuestions;
            
            displayUsername(getUsername());
            displayScore(score, totalQuestions);

            addScore(score, totalQuestions);

        }

        public string getUsername()
        {
            return Environment.UserName;
        }

        public void displayUsername(string username)
        {
            usernameLabel.Text = username;
        }

        public void displayScore(int score, int totalQuestions)
        {
            label6.Text = (score+"/"+totalQuestions);
        }

        public void addScore(int score, int totalQuestions)
        {
            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "addScores";
            AddScore addScore_Request = new AddScore()
            {
                score = score.ToString(),
                totalQuestions = totalQuestions.ToString(),
                username = Environment.UserName,

            };

            // make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
            {
                { "api_method", apiMethod},
                { "api_data",   JsonConvert.SerializeObject(addScore_Request) }
            });

            // decode json string to object
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            // check response
            if (!r.IsError)
            {
                //The data was succefully updated to the SQL database.
            }
            else
            {
                MessageBox.Show("ERROR: " + r.ErrorMessage);
            }
        }








        private void button3_Click(object sender, EventArgs e)
        {
            home myform = new home();
            this.Hide();
            myform.ShowDialog();
        }

        private void quizCompleted_Load(object sender, EventArgs e)
        {

        }
    }
}
