using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Management;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace infosecQuiz
{
    public partial class authorisationMenu : Form
    {
        public class RootObject
        {
            public string commonName { get; set; }
            public string processorID { get; set; }
        }

        public authorisationMenu()
        {
            InitializeComponent();
        }

        public class API_Response
        {
            public bool IsError { get; set; }
            public string ErrorMessage { get; set; }
            public RootObject[] ResponseData { get; set; }
        }

        public class Computer_Authorise
        {
            public string commonName { get; set; }
            public string processorID { get; set; }
        }

        private void authorisationMenu_Load(object sender, EventArgs e)
        {
            // Attach Subitems to the ListView
            //listView1.Columns.Add("Title", 30, HorizontalAlignment.Left);
            //listView1.Columns.Add("ID", 20, HorizontalAlignment.Left);
            //listView1.Columns.Add("Price", 40, HorizontalAlignment.Left);
            //listView1.Columns.Add("Publi-Date", 50, HorizontalAlignment.Left);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //get CPI ID
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }


            //Add this computer to the auth list

            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "addCPU";
            Computer_Authorise thisComputer_Authorise = new Computer_Authorise()
            {
                commonName = commonName.Text,
                processorID = cpuInfo
            };

            // make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
                {
                    { "api_method", apiMethod},
                    { "api_data",   JsonConvert.SerializeObject(thisComputer_Authorise) }
                });

            // decode json string to object
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            // check response
            if (!r.IsError)
            {
                MessageBox.Show("This machine has been added successfully.");
            }
            else
            {
                MessageBox.Show("ERROR: " + r.ErrorMessage);
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Add this computer to the auth list

            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "listCPU";
            Computer_Authorise thisComputer_Authorise = new Computer_Authorise()
            {

            };

            // make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
                {
                    { "api_method", apiMethod},
                    { "api_data",   JsonConvert.SerializeObject(thisComputer_Authorise) }
                });

            // decode json string
            //API_Response r = JsonConvert.DeserializeObject<API_Response>(response);
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            // check response
            if (!r.IsError)
            {
                MessageBox.Show("" + r.ResponseData);
            }
            else
            {
                MessageBox.Show("ERROR: " + r.ErrorMessage);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //FOR LISTING AUTHORISED MACHINES

            Console.WriteLine("clearing all view list data");
            listView1.Clear();
            

            //GET DATA
            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "listCPU";
            Computer_Authorise thisComputer_Authorise = new Computer_Authorise()
            {

            };

            //Make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
                {
                    { "api_method", apiMethod},
                    { "api_data",   JsonConvert.SerializeObject(thisComputer_Authorise) }
                });

            //Deserialise
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            //MessageBox.Show("" + r.ResponseData[0].commonName);

            // Set to details view.
            listView1.View = View.Details;
            // Add both columns.
            listView1.Columns.Add("CommonName", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("ComputerID", 200, HorizontalAlignment.Left);

            //fill with data
            int lim = r.ResponseData.Length;

            Console.WriteLine("Beginning loop for data fill.");
            for (int i = 0; i <= (lim-1); i++)
            {
                Console.WriteLine("This is loop number "+ i);
                string[] row = {r.ResponseData[i].commonName, r.ResponseData[i].processorID};
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
                row = null;


            }

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
