
namespace ZonTools.UserControls
{
    partial class WebSiteUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.buttonFind = new System.Windows.Forms.Button();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.comboBoxServers = new System.Windows.Forms.ComboBox();
            this.labelServer = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.buttonFind);
            this.panelHeader.Controls.Add(this.textBoxServer);
            this.panelHeader.Controls.Add(this.comboBoxServers);
            this.panelHeader.Controls.Add(this.labelServer);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(5, 5);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.panelHeader.Size = new System.Drawing.Size(926, 50);
            this.panelHeader.TabIndex = 0;
            // 
            // buttonFind
            // 
            this.buttonFind.Location = new System.Drawing.Point(502, 5);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(94, 29);
            this.buttonFind.TabIndex = 3;
            this.buttonFind.Text = "Find";
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFindServices_Click);
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(281, 5);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(214, 27);
            this.textBoxServer.TabIndex = 2;
            // 
            // comboBoxServers
            // 
            this.comboBoxServers.FormattingEnabled = true;
            this.comboBoxServers.Location = new System.Drawing.Point(61, 5);
            this.comboBoxServers.Name = "comboBoxServers";
            this.comboBoxServers.Size = new System.Drawing.Size(214, 28);
            this.comboBoxServers.TabIndex = 1;
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelServer.Location = new System.Drawing.Point(5, 10);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(50, 20);
            this.labelServer.TabIndex = 0;
            this.labelServer.Text = "Server";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(5, 55);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 29;
            this.dataGridView.Size = new System.Drawing.Size(926, 437);
            this.dataGridView.TabIndex = 1;
            // 
            // WebSiteUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panelHeader);
            this.Name = "WebSiteUserControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(936, 497);
            this.Load += new System.EventHandler(this.ServicesUserControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.ComboBox comboBoxServers;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}
