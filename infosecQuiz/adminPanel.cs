using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace infosecQuiz
{
    public partial class adminPanel : Form
    {
        public adminPanel()
        {
            InitializeComponent();
        }

        public class RootObject
        {
            public string userID { get; set; }
            public string username { get; set; }

            public string questionID { get; set; }
            public string question { get; set; }
            public string answerA { get; set; }
            public string answerB { get; set; }
            public string correctAnswer { get; set; }
        }

        public class API_Response
        {
            public bool IsError { get; set; }
            public string ErrorMessage { get; set; }
            public RootObject[] ResponseData { get; set; }
        }

        public class GetUsers
        {
            public string userID { get; set; }
            public string username { get; set; }
        }

        public class AddUsers
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class RemoveUsers
        {
            public string userID { get; set; }
        }

        public class AddQuestions
        {
            public string question { get; set; }
            public string answerA { get; set; }
            public string answerB { get; set; }
            public string correctAnswer { get; set; }

        }

        public class RemoveQuestions
        {
            public string questionID { get; set; }
        }

        public class GetQuestions
        {
            public string questionID { get; set; }
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

        private void adminPanel_Load(object sender, EventArgs e)
        {
            getUserList();
            getQuestionList();
        }

        private void getQuestionList()
        {
            //FOR LISTING AUTHORISED MACHINES
            Console.WriteLine("clearing all view list data");
            listView1.Clear();


            //GET DATA
            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "getQuestions";
            GetQuestions thisGetQuesitons = new GetQuestions()
            {

            };


            //Make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
            {
                { "api_method", apiMethod},
                { "api_data",   JsonConvert.SerializeObject(thisGetQuesitons) }
            });

            //Deserialise
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            //MessageBox.Show("" + r.ResponseData[0].commonName);

            // Set to details view.
            listView1.View = View.Details;
            // Add both columns.
            listView1.Columns.Add("ID", 25, HorizontalAlignment.Left);
            listView1.Columns.Add("Question", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Answer A", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("Answer B", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("Correct Answer", 85, HorizontalAlignment.Left);

            //fill with data
            int lim = r.ResponseData.Length;

            for (int i = 0; i <= (lim - 1); i++)
            {
                Console.WriteLine("This is loop number " + i);
                string[] row1 = { r.ResponseData[i].questionID, r.ResponseData[i].question, r.ResponseData[i].answerA, r.ResponseData[i].answerB, r.ResponseData[i].correctAnswer };
                var listViewItem = new ListViewItem(row1);
                listView1.Items.Add(listViewItem);
                row1 = null;
            }
        }

        private void getUserList()
        {
            //FOR LISTING AUTHORISED MACHINES
            Console.WriteLine("clearing all view list data");
            listView2.Clear();


            //GET DATA
            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "getUsers";
            GetUsers thisGetUsers = new GetUsers()
            {

            };


            //Make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
                {
                    { "api_method", apiMethod},
                    { "api_data",   JsonConvert.SerializeObject(thisGetUsers) }
                });

            //Deserialise
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            //MessageBox.Show("" + r.ResponseData[0].commonName);

            // Set to details view.
            listView2.View = View.Details;
            // Add both columns.
            listView2.Columns.Add("User ID", 50, HorizontalAlignment.Left);
            listView2.Columns.Add("Username", 330, HorizontalAlignment.Left);

            //fill with data
            int lim = r.ResponseData.Length;

            for (int i = 0; i <= (lim-1); i++)
            {
                Console.WriteLine("This is loop number " + i);
                string[] row = { r.ResponseData[i].userID, r.ResponseData[i].username };
                var listViewItem = new ListViewItem(row);
                listView2.Items.Add(listViewItem);
                row = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //REFRESH USERS
            getUserList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //BACK BUTTON
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //CREATE ACCOUNT BUTTON

            //check all fields are not empty
            if (!username.Text.Equals("") && !password.Text.Equals("") && !passwordConfirm.Text.Equals(""))
            {
                if (password.Text == passwordConfirm.Text)
                {
                    //Check if username + passwords match on db
                    //check database for entries
                    string apiUrl = "https://craig.im/infosec.php";
                    string apiMethod = "addUsers";
                    AddUsers addUser_Request = new AddUsers()
                    {
                        username = username.Text,
                        password = password.Text
                    };

                    // make http post request
                    string response = Http.Post(apiUrl, new NameValueCollection()
                    {
                        { "api_method", apiMethod},
                        { "api_data",   JsonConvert.SerializeObject(addUser_Request) }
                    });

                    // decode json string to object
                    API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

                    // check response
                    if (!r.IsError)
                    {
                        //There was no errors when adding the account -> do this:
                        MessageBox.Show("Account added successfully.");
                        //update lits
                        getUserList();
                    }
                    else
                    {
                        MessageBox.Show("ERROR: " + r.ErrorMessage);
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match.");
                }
            }

            else if (username.Text.Equals("") || password.Text.Equals(""))
            {
                MessageBox.Show("You cannot leave the fields blank.");
            }
            else
            {
                MessageBox.Show("An error has occured.");
            }
        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //REMOVE USER BY ID BUTTON

            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "removeUsers";
            RemoveUsers removeUsers_Request = new RemoveUsers()
            {
                userID = userID.Text
            };

            // make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
                    {
                        { "api_method", apiMethod},
                        { "api_data",   JsonConvert.SerializeObject(removeUsers_Request) }
                    });

            // decode json string to object
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            // check response
            if (!r.IsError)
            {
                //There was no errors when adding the account -> do this:
                MessageBox.Show("Account Successfully removed.");
                //clear text box
                userID.Text = String.Empty;
                //update lits
                getUserList();
            }
            else
            {
                MessageBox.Show("ERROR: " + r.ErrorMessage);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //ADD QUESTION BUTTON

            //get combo box selection as string
            string selected = this.correctAnswer.GetItemText(this.correctAnswer.SelectedItem);
            //Select last char from this string e.g. A or B
            selected = selected.Substring(selected.Length - 1);


            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "addQuestions";
            AddQuestions addQuestions_Request = new AddQuestions()
            {
                question = question.Text,
                answerA = answerA.Text,
                answerB = answerB.Text,
                correctAnswer = selected

            };

            // make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
                    {
                        { "api_method", apiMethod},
                        { "api_data",   JsonConvert.SerializeObject(addQuestions_Request) }
                    });

            // decode json string to object
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            // check response
            if (!r.IsError)
            {
                //There was no errors when adding the account -> do this:
                MessageBox.Show("Question successfully added.");
                //clear text box
                userID.Text = String.Empty;
                //update lits
                getUserList();
            }
            else
            {
                MessageBox.Show("ERROR: " + r.ErrorMessage);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Remove Question Button

            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "removeQuestions";
            RemoveQuestions removeQuestions_Request = new RemoveQuestions()
            {
                questionID = questionID.Text
            };

            // make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
            {
                { "api_method", apiMethod},
                { "api_data",   JsonConvert.SerializeObject(removeQuestions_Request) }
            });

            // decode json string to object
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            // check response
            if (!r.IsError)
            {
                //There was no errors -> do this:
                MessageBox.Show("Account Successfully removed.");
                //clear text box
                questionID.Text = String.Empty;
                //update lits
                getQuestionList();
            }
            else
            {
                MessageBox.Show("ERROR: " + r.ErrorMessage);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Refresh Questions button
            getQuestionList();
        }


    }
}
