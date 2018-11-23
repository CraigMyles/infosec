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

        //initalise current correct answer
        String currentCorrectAnswer = "";

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
            loadQuestion(currentQuestion, numQuestions);
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
            //populate array
            for (int i = 0; i <= (r.ResponseData.Length - 1); i++)
            {
                question.Add(r.ResponseData[i].question.ToString());
                answerA.Add(r.ResponseData[i].answerA.ToString());
                answerB.Add(r.ResponseData[i].answerB.ToString());
                correctAnswer.Add(r.ResponseData[i].correctAnswer.ToString());
            }
        }

        public String loadQuestion(int currentQuestion, int numQuestions)
        {
            int i = currentQuestion;
            int lim = numQuestions;


            if (i == lim)
            {
                //Do something because the user has finished the game

            }
            //-> the user has not completed the game:

            questionNumber.Text = "Question " + (i+1) + "/" + lim;
            questionLabel.Text = question[i];
            answerAText.Text = answerA[i];
            answerBText.Text = answerB[i];

            return currentCorrectAnswer = correctAnswer[i];
            


            //^^ THIS MIGHT NOT WORK BECAUSE OF THE I?


        }

        private void answerA_Click(object sender, EventArgs e)
        {
            if(String.Equals(currentCorrectAnswer, "A"))
            {
                MessageBox.Show("Correct!!!");
            }
            else
            {
                MessageBox.Show("INCORRECT!!!, correct current answer:"+currentCorrectAnswer);
            }
        }

        private void answerB_Click(object sender, EventArgs e)
        {
            if (String.Equals(currentCorrectAnswer, "B"))
            {
                MessageBox.Show("Correct!!!");
            }
            else
            {
                MessageBox.Show("INCORRECT!!! ,correct current answer: "+currentCorrectAnswer);
            }
        }
    }
}