﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace infosecQuiz
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        public class API_Response
        {
            public bool IsError { get; set; }
            public string ErrorMessage { get; set; }
            public string ResponseData { get; set; }
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

        public class CPU_Request
        {
            public string cpuID { get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //open teacher menu 
            login myFormLogin = new login();
            this.Hide();
            myFormLogin.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Check if this computer is verified
            checkCPU();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkCPU()
        {
            //check there is a record of this computer in the database
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

            //CPU String is now stored as a variable
            //Now use API to check if this is in database of allowed computers

            //Check if username + passwords match on db
            //check database for entries
            string apiUrl = "https://craig.im/infosec.php";
            string apiMethod = "checkCPU";
            CPU_Request myCPU_Request = new CPU_Request()
            {
                cpuID = cpuInfo
            };

            // make http post request
            string response = Http.Post(apiUrl, new NameValueCollection()
            {
                { "api_method", apiMethod},
                { "api_data",   JsonConvert.SerializeObject(myCPU_Request) }
            });

            // decode json string to dto object
            //System.Diagnostics.Debug.WriteLine("finna crash here");
            API_Response r = JsonConvert.DeserializeObject<API_Response>(response);

            // check response
            if (!r.IsError && r.ResponseData == "SUCCESS")
            {
                //This computer is verified -> do this:
                quiz myQuiz = new quiz();
                this.Hide();
                myQuiz.ShowDialog();
            }
            else
            {
                MessageBox.Show("ERROR: " + r.ErrorMessage + "\n\n" + "Please ask your teacher to approve this machine.", "Computer Authorisation");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            scores myScores = new scores();
            this.Hide();
            myScores.ShowDialog();
        }

        private void home_Load(object sender, EventArgs e)
        {

        }
    }
}