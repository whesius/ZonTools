using System;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;
using ZonTools.Forms;
using ZonTools.UserControls;

namespace ZonTools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            tabPageServices.ContextMenuStrip = new ContextMenuStrip();
            var toolstripItem = new ToolStripMenuItem("Copy", null, (sender, e) =>
             {
                 // Copy this tabPage
                 var tabPage = new TabPage("Services");
                 var uc = new ServicesUserControl();
                 uc.Dock = DockStyle.Fill;
                 tabPage.Controls.Add(uc);
                 this.tabControl.TabPages.Add(tabPage);
            });

            tabPageServices.ContextMenuStrip.Items.Add(toolstripItem);

            tabPageAppPools.ContextMenuStrip = new ContextMenuStrip();
            toolstripItem = new ToolStripMenuItem("Copy", null, (sender, e) =>
            {
                // Copy this tabPage
                var tabPage = new TabPage("WebSites");
                var uc = new WebSiteUserControl();
                uc.Dock = DockStyle.Fill;
                tabPage.Controls.Add(uc);
                this.tabControl.TabPages.Add(tabPage);
            });

            tabPageAppPools.ContextMenuStrip.Items.Add(toolstripItem);
        }


        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new OptionsForm();

            form.ShowDialog();

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.toolStripStatusLabelUser.Text = WindowsIdentity.GetCurrent().Name;

            this.tabPageServices.Controls.Clear();
            var uc = new ServicesUserControl();
            this.tabPageServices.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;

            var uc1 = new WebSiteUserControl();
            this.tabPageAppPools.Controls.Add(uc1);
            uc1.Dock = DockStyle.Fill;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
            
                    
        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage CurrentTab = tabControl.TabPages[e.Index];
            Rectangle ItemRect = tabControl.GetTabRect(e.Index);
            SolidBrush FillBrush = new SolidBrush(Color.RoyalBlue);
            SolidBrush TextBrush = new SolidBrush(Color.White);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            //If we are currently painting the Selected TabItem we'll
            //change the brush colors and inflate the rectangle.
            if (System.Convert.ToBoolean(e.State & DrawItemState.Selected))
            {
                FillBrush.Color = Color.White;
            }

            //If we are currently painting the Selected TabItem we'll
            //change the brush colors and inflate the rectangle.
            if (System.Convert.ToBoolean(e.State & DrawItemState.Selected))
            {
                FillBrush.Color = Color.White;
                TextBrush.Color = Color.Black;
                ItemRect.Inflate(2, 2);
            }

            //Set up rotation for left and right aligned tabs
            if (tabControl.Alignment == TabAlignment.Left || tabControl.Alignment == TabAlignment.Right)
            {
                float RotateAngle = 90;
                if (tabControl.Alignment == TabAlignment.Left)
                    RotateAngle = 270;
                PointF cp = new PointF(ItemRect.Left + (ItemRect.Width / 2), ItemRect.Top + (ItemRect.Height / 2));
                e.Graphics.TranslateTransform(cp.X, cp.Y);
                e.Graphics.RotateTransform(RotateAngle);
                ItemRect = new Rectangle(-(ItemRect.Height / 2), -(ItemRect.Width / 2), ItemRect.Height, ItemRect.Width);
            }

            //Next we'll paint the TabItem with our Fill Brush
            e.Graphics.FillRectangle(FillBrush, ItemRect);

            //Now draw the text.
            e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, (RectangleF)ItemRect, sf);

            //Reset any Graphics rotation
            e.Graphics.ResetTransform();

            //Finally, we should Dispose of our brushes.
            FillBrush.Dispose();
            TextBrush.Dispose();
        }

        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TCHITTESTINFO HTI = new TCHITTESTINFO(e.X, e.Y);
                TabPage hotTab = tabControl.TabPages[SendMessage(tabControl.Handle, TCM_HITTEST, IntPtr.Zero, ref HTI)];
                tabControl.ContextMenuStrip = hotTab.ContextMenuStrip;
            }
        }

        [Flags()]
        private enum TCHITTESTFLAGS
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }

        private const int TCM_HITTEST = 0x130D;

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct TCHITTESTINFO
        {
            public Point pt;
            public TCHITTESTFLAGS flags;
            public TCHITTESTINFO(int x, int y)
            {
                pt = new Point(x, y);
                flags = TCHITTESTFLAGS.TCHT_ONITEM;
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, ref TCHITTESTINFO lParam);

        private void tabControl_MouseUp(object sender, MouseEventArgs e)
        {
            tabControl.ContextMenuStrip = null;
        }
    }
}
