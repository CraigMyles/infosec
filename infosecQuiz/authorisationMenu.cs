using Newtonsoft.Json;
using System;
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
        public string ResponseData { get; private set; }

        public authorisationMenu()
        {
            InitializeComponent();
        }

        public class API_Response
        {
            public bool IsError { get; set; }
            public string ErrorMessage { get; set; }
            public dynamic ResponseData { get; set; }
        }

        public class Computer_Authorise
        {
            public string commonName { get; internal set; }
            public string processorID { get; internal set; }
        }

        private void authorisationMenu_Load(object sender, EventArgs e)
        {

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
            if (!r.IsError && r.ResponseData == "SUCCESS")
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
                MessageBox.Show(""+r.ResponseData);
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
            DataTable dt = new DataTable();

            dt.Columns.Add("Common Name");
            dt.Columns.Add("Processor");

            //GET DATA
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
            //List<r> r = r.ResponseData.ToObject<List<r>>();

            //var result = JsonConvert.DeserializeObject<API_Response>(r.ResponseData);
            dt.Rows.Add(new Object[] r.ResponseData);



            dataGridView1.DataSource = dt;
        }
    }
}
