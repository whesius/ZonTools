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
    public partial class ServicesUserControl : UserControl
    {
        public ServicesUserControl()
        {
            InitializeComponent();

            this.dataGridViewServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewServices.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dataGridViewServices.CellMouseClick += DataGridViewServices_CellMouseClick;
        }

        private void DataGridViewServices_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {              
                var cells = this.dataGridViewServices.SelectedCells;
                if (cells.Count > 0)
                {
                    Rectangle r = dataGridViewServices.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    Point p = new Point(r.X + r.Width, r.Y);

                    this.contextMenuStrip.Show(this.dataGridViewServices, p) ;
                }
            }
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
            var collection = await Program.ServiceProvider.GetService<IServicesController>().Pull(this.textBoxServer.Text);
            this.dataGridViewServices.DataSource = new SortableBindingList<WindowsService>(collection.ToArray());
        }

        private async void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var selectedCells = this.dataGridViewServices.SelectedCells;

            var selectedServices = new List<WindowsService>();

            foreach (DataGridViewCell selectedCell in selectedCells)
            {
                var windowsService = (WindowsService)selectedCell.OwningRow.DataBoundItem;
                selectedServices.Add(windowsService);
            }

            if (e.ClickedItem == toolStripMenuItemStart)
            {
                
                var collection = await Program.ServiceProvider
                    .GetService<IServicesController>()
                    .Start(this.textBoxServer.Text, selectedServices);
                this.dataGridViewServices.DataSource = new SortableBindingList<WindowsService>(collection.ToList());
            }

            if (e.ClickedItem == toolStripMenuItemStop)
            {             

                var collection = await Program.ServiceProvider
                    .GetService<IServicesController>()
                    .Stop(this.textBoxServer.Text, selectedServices);
                this.dataGridViewServices.DataSource = new SortableBindingList<WindowsService>(collection.ToList());
            }
        }
    }
}
