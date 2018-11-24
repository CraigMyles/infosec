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
    public partial class scores : Form
    {
        public scores()
        {
            InitializeComponent();
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

        public class RootObject
        {
            public string scoreID { get; set; }
            public string player { get; set; }
            public string percentageScore { get; set; }
            public dynamic playDate { get; set; }
        }

        public class API_Response
        {
            public bool IsError { get; set; }
            public string ErrorMessage { get; set; }
            public RootObject[] ResponseData { get; set; }
        }

        public class getScores
        {
            public string scoreID { get; set; }
            public string player { get; set; }
            public string percentageScore { get; set; }
            public dynamic playDate { get; set; }
        }

        private void scores_Load(object sender, EventArgs e)
        {
            getScoreList();
        }

        private void getScoreList()
        {
            listView1.Clear();
            //GET DATA
            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "getScores";
            getScores thisGetUsers = new getScores()
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
            

            //fill with data
            int lim = r.ResponseData.Length;
            
            // Set to details view.
            listView1.View = View.Details;
            // Add both columns.
            listView1.Columns.Add("Player", 222, HorizontalAlignment.Left);
            listView1.Columns.Add("Score", 152, HorizontalAlignment.Center);
            listView1.Columns.Add("Date", 322, HorizontalAlignment.Center);

            for (int i = 0; i <= (lim - 1); i++)
            {
                //Console.WriteLine("This is loop number " + i);
                string[] row = { r.ResponseData[i].player, ""+r.ResponseData[i].percentageScore+"%", r.ResponseData[i].playDate };
                //string[] row = { "test1", "test2", "test3" };
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
                row = null;
            }
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            home myform = new home();
            this.Hide();
            myform.ShowDialog();
            this.Close();
        }
    }
}
