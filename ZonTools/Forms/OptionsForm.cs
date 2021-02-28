using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZonTools.Controllers;

namespace ZonTools.Forms
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private async void OptionsForm_Shown(object sender, EventArgs e)
        {
            this.listBox1.DataSource = await Program.ServiceProvider.GetService<IOptionsController>().GetServers();;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: Save Changes
            this.Close();
        }
    }
}
