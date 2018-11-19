using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace infosecQuiz
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        public class API_Response
        {
            public bool IsError { get; set; }
            public string ErrorMessage { get; set; }
            public string ResponseData { get; set; }
        }

        public class Login_Request
        {
            public string username { get; set; }
            public string password { get; set; }
        }


        private void loginButton_Click(object sender, EventArgs e)
        {
            if (!username.Text.Equals("") && !password.Text.Equals(""))
            {

                //Check if username + passwords match on db
                //check database for entries
                string apiUrl = "https://craig.im/infosec.php";
                string apiMethod = "userLogin";
                Login_Request myLogin_Request = new Login_Request()
                {
                    username = username.Text,
                    password = password.Text
                };

                // make http post request
                string response = Http.Post(apiUrl, new NameValueCollection()
                {
                    { "api_method", apiMethod},
                    { "api_data",   JsonConvert.SerializeObject(myLogin_Request) }
                });

                // decode json string to object
                API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

                // check response
                if (!r.IsError && r.ResponseData == "SUCCESS")
                {
                    string teacherUsername = username.Text;
                    loginMenu myForm = new loginMenu(teacherUsername);
                    this.Hide();
                    myForm.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("ERROR: " + r.ErrorMessage);
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

        private void usernameLabel_Click(object sender, EventArgs e)
        {

        }

        private void passwordLabel_Click(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


