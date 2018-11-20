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


            //Make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
                {
                    { "api_method", apiMethod},
                    { "api_data", "" }
                });

            //Deserialise
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            //MessageBox.Show("" + r.ResponseData[0].commonName);

            // Set to details view.
            listView2.View = View.Details;
            // Add both columns.
            listView2.Columns.Add("User ID", 200, HorizontalAlignment.Left);
            listView2.Columns.Add("Username", 200, HorizontalAlignment.Left);

            //fill with data
            MessageBox.Show("" + r.ResponseData);
            //int lim = r.ResponseData.Length;


            //Console.WriteLine("Beginning loop for data fill.");
            //for (int i = 0; i <= (lim); i++)
            //{
            //    Console.WriteLine("This is loop number " + i);
            //    string[] row = { r.ResponseData[i].userID, r.ResponseData[i].username };
            //    var listViewItem = new ListViewItem(row);
            //    listView2.Items.Add(listViewItem);
            //    row = null;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //REFRESH USERS
            getUserList();
        }
    }
}
