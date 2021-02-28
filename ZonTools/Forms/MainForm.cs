using Macchiator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZonTools.Controllers;
using ZonTools.Forms;
using ZonTools.Shared;

namespace ZonTools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
            

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new OptionsForm();

            form.ShowDialog();

        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            this.toolStripStatusLabelUser.Text = WindowsIdentity.GetCurrent().Name;

            this.comboBoxServers.DataSource = await Program.ServiceProvider.GetService<IOptionsController>().GetServers();

            this.comboBoxServers.SelectedIndexChanged += (sender, e) => this.textBoxServer.Text = $"{ ((ComboBox)sender).SelectedValue}";
        }

        private void exitToolStripMenuItem_Click (object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBoxServer.Text = $"{ ((ComboBox)sender).SelectedValue}";
        }

        private async void buttonFind_Click(object sender, EventArgs e)
        {
            var collection = await Program.ServiceProvider.GetService<IServicesController>().Get(this.textBoxServer.Text);
            this.dataGridViewServices.DataSource = new SortableBindingList<WindowsService>(collection.ToList());
            
        }

        private void dataGridViewServices_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach(DataGridViewColumn column in dataGridViewServices.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
    }
}
