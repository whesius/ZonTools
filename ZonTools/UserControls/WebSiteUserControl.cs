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
using Macchiator;
using ZonTools.Shared;

namespace ZonTools.UserControls
{
    public partial class WebSiteUserControl : UserControl
    {
        public WebSiteUserControl()
        {
            InitializeComponent();

            this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }
              

        private async void ServicesUserControl_Load(object sender, EventArgs e)
        {
            var servers = await Program.ServiceProvider
                .GetService<IOptionsController>()
                .GetServers();

            var server = servers.FirstOrDefault();

            if (server != null)
            {
                this.comboBoxServers.DisplayMember = nameof(server.Name);
                this.comboBoxServers.DataSource = servers;
            }
            else
            {
                this.comboBoxServers.DataSource = new string[] {"No Servers!"};
            }

            this.comboBoxServers.SelectedIndexChanged += (sender, e) => this.textBoxServer.Text = $"{ ((WindowsServer) (((ComboBox)sender).SelectedItem)).Name}";

        }

        private async void buttonFindServices_Click(object sender, EventArgs e)
        {           
            var collection = await Program.ServiceProvider.GetService<IWebSiteController>().Pull(this.textBoxServer.Text);
            this.dataGridView.DataSource = new SortableBindingList<WebSite>(collection.ToArray());
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
    }
}
