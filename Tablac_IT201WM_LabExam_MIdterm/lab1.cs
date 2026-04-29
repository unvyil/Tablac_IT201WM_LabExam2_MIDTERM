using System;
using System.Windows.Forms;
using Tablac_IT201WM_LabExam1_Midterm;

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
            foreach (Form f in Application.OpenForms) { if (f is POS_Admin) { f.Focus(); return; } }
            POS_Admin child = new POS_Admin();
            child.MdiParent = this;
            child.Show();
        }

        private void jeePOSOrderingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms) { if (f is Ordering_POS) { f.Focus(); return; } }
            Ordering_POS child = new Ordering_POS();
            child.MdiParent = this;
            child.Show();
        }

        private void simplePOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is Bundles)
                {
                    f.Focus();
                    return;
                }
            }

            Bundles simplePosChild = new Bundles();
            simplePosChild.MdiParent = this;
            simplePosChild.Show();
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close dashboard?", "Logout", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}