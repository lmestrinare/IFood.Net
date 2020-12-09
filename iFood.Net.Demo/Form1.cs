using iFood.Net.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iFood.Net.Demo
{
    public partial class Form1 : Form
    {
        string _ClientId = "";
        string _ClientSecret = "";
        string _Username = "";
        string _Password = "";

        public Form1()
        {
            InitializeComponent();
            _ClientId = ConfigurationManager.AppSettings.Get("ClientID");
            _ClientSecret = ConfigurationManager.AppSettings.Get("ClientSecret");
            _Username = ConfigurationManager.AppSettings.Get("Username");
            _Password = ConfigurationManager.AppSettings.Get("Password");
        }

        private async void btnToken_Click(object sender, EventArgs e)
        {
            var service = new iFoodService();
            var oauth = await service.OathToken(_ClientId, _ClientSecret, _Username, _Password);
            if (oauth.Success)
            {
                string output = JsonConvert.SerializeObject(oauth.Result, Formatting.Indented);
                txtResults.Text = output;
            }
            else
            {
                txtResults.Text = oauth.Message;
                return;
            }

        }
    }
}
