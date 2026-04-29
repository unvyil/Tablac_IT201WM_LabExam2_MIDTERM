using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tablac_IT201WM_LabExam_MIdterm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void jeePOSIncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms) { if (f is POS_Inc) { f.Focus(); return; } }
            POS_Inc child = new POS_Inc(); child.MdiParent = this; child.Show();
        }

        private void jeePOSOrderingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms) { if (f is OrderingPOS) { f.Focus(); return; } }
            OrderingPOS child = new OrderingPOS(); child.MdiParent = this; child.Show();
        }

        private void simplePOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms) { if (f is SimplePOS) { f.Focus(); return; } }
            SimplePOS child = new SimplePOS(); child.MdiParent = this; child.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close dashboard?", "Logout", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }
