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

namespace infosecQuiz
{
    public partial class quiz : Form
    {
        //initalise vars for data holding
        List<String> question = new List<string>();
        List<String> answerA = new List<string>();
        List<String> answerB = new List<string>();
        List<String> correctAnswer = new List<string>();

        int currentScore = 0;
        
        //initalise current correct answer
        String currentCorrectAnswer = "";

        int currentQuestion = 0;
        int numQuestions;

        private System.Windows.Forms.Timer timer1;
        private int counter = 0;




        public quiz()
        {
            InitializeComponent();
        }

        public class API_Response
        {
            public bool IsError { get; set; }
            public string ErrorMessage { get; set; }
            public RootObject[] ResponseData { get; set; }
        }

        public class RootObject
        {
            public string question { get; set; }
            public string answerA { get; set; }
            public string answerB { get; set; }
            public string correctAnswer { get; set; }
        }

        public class GetQuestions
        {
            public string question { get; set; }
            public string answerA { get; set; }
            public string answerB { get; set; }
            public string correctAnswer { get; set; }
        }

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

        private void quiz_Load(object sender, EventArgs e)
        {
            //load data from database
            string data = getQuestions();
            //populate arrays
            populateArrays(data);
            //Get number of questions
            int numQuestions = question.Count;

            int currentQuestion = 0;

            //begin game
            beginTimer();
            loadQuestion();
        }

        private string getQuestions()
        {
            //GET DATA
            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "getQuestions";
            GetQuestions thisGetUsers = new GetQuestions()
            {

            };


            //Make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
            {
                {"api_method", apiMethod},
                {"api_data", JsonConvert.SerializeObject(thisGetUsers)}
            });

            return response;

        }

        public void populateArrays(string data)
        {
            //Deserialise
            API_Response r = JsonConvert.DeserializeObject<API_Response>(data);
            numQuestions = r.ResponseData.Length;
            //populate array
            for (int i = 0; i <= (r.ResponseData.Length - 1); i++)
            {
                question.Add(r.ResponseData[i].question.ToString());
                answerA.Add(r.ResponseData[i].answerA.ToString());
                answerB.Add(r.ResponseData[i].answerB.ToString());
                correctAnswer.Add(r.ResponseData[i].correctAnswer.ToString());
            }
        }

        public void loadQuestion()
        {
            if (currentQuestion == numQuestions)
            {
                //Do something because the user has answered all questions
                timer1.Stop();
                //Launch score screen.
                quizCompleted myform = new quizCompleted(currentScore, numQuestions);
                this.Hide();
                myform.ShowDialog();

                return;

            }
            else
            {
                //-> the user has not completed the game:
                questionNumber.Text = "Question " + (currentQuestion + 1) + "/" + numQuestions;
                questionLabel.Text = question[currentQuestion];
                answerAText.Text = answerA[currentQuestion];
                answerBText.Text = answerB[currentQuestion];

                currentCorrectAnswer = correctAnswer[currentQuestion];
            }


        }

        private void answerA_Click(object sender, EventArgs e)
        {
            if(String.Equals(currentCorrectAnswer, "A"))
            {
                correct();
            }
            else
            {
                incorrect();
            }
        }

        private void answerB_Click(object sender, EventArgs e)
        {
            
            if (String.Equals(currentCorrectAnswer, "B"))
            {
                correct();
            }
            else
            {
                incorrect();
            }
        }

        public void correct() //Run this when user selected the correct answer
        {
            
            //increase score
            currentScore++;

            resetThreat();
            currentQuestion++;
            loadQuestion();
            //progress bar management todo

        }

        public void incorrect() //Run this when user selected the incorrect answer
        {
            //progress bar management todo
            lowerReputation();
            currentQuestion++;
            loadQuestion();
        }

        public void resetThreat()
        {
            verticleProgressBar1.Value = 0;
            modifyVerticleProgressBar.SetState(verticleProgressBar1, 2);
        }

        public void lowerReputation()
        {
            verticleProgressBar2.Value = 0;
            modifyVerticleProgressBar.SetState(verticleProgressBar2, 1);
        }

        private void verticleProgressBar1_Click(object sender, EventArgs e)
        {
            verticleProgressBar1.Value = 0;
            verticleProgressBar1.Value = 100;
            modifyVerticleProgressBar.SetState(verticleProgressBar1, 2);

        }

        private void beginTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // 1 second
            verticleProgressBar1.Value = 0;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;

            if (verticleProgressBar1.Value == 100)
            {
                timer1.Stop();
                MessageBox.Show("GAME OVER!!");
                return;
            }
            verticleProgressBar1.Value = (verticleProgressBar1.Value+10);
            modifyVerticleProgressBar.SetState(verticleProgressBar1, 2);
        }
    }
}