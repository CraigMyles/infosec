﻿using Newtonsoft.Json;
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
        }

        private void getQuestionList()
        {
            
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
    }
}
