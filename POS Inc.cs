using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tablac_IT201WM_LabExam_MIdterm
{
    public partial class POS_Admin : Form
    {
        int currentSlot = 1;

        public POS_Admin()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbStock.SelectedItem == null || cbPrice.SelectedItem == null || cbImage.SelectedItem == null)
            {
                MessageBox.Show("All IDs (Stock, Price, and Image) are REQUIRED.", "Validation Error");
                return;
            }

            string sID = cbStock.SelectedItem.ToString();
            string pID = cbPrice.SelectedItem.ToString();
            string iID = cbImage.SelectedItem.ToString();

            foreach (var entry in POSData.ProductData.Values)
            {
                if (entry[3].ToString() == pID) { MessageBox.Show($"Price ID {pID} is already assigned!"); return; }
                if (entry[4].ToString() == iID) { MessageBox.Show($"Image ID {iID} is already assigned!"); return; }
            }

            if (POSData.ProductData.ContainsKey(sID))
            {
                MessageBox.Show("This Stock ID is already in use. Use UPDATE to modify it.", "Save Error");
                return;
            }

            if (currentSlot > 15) { MessageBox.Show("All slots are full!"); return; }

            ProcessSaveOrUpdate(sID, pID, iID);
            MessageBox.Show($"Product {currentSlot} Saved Successfully!");


            ResetIDs();
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            if (cbStock.SelectedItem == null) return;
            string sID = cbStock.SelectedItem.ToString();

            if (POSData.ProductData.ContainsKey(sID))
            {
                string pID = cbPrice.SelectedItem?.ToString() ?? "N/A";
                string iID = cbImage.SelectedItem?.ToString() ?? "N/A";

                ProcessSaveOrUpdate(sID, pID, iID);
                MessageBox.Show($"Product {sID} Updated Successfully!");
            }
            else { MessageBox.Show("ID not found. Use SAVE to create this product first."); }
        }

        private void ProcessSaveOrUpdate(string sID, string pID, string iID)
        {
            Control[] n = this.Controls.Find("nameTxt" + currentSlot, true);
            Control[] p = this.Controls.Find("price" + currentSlot, true);
            Control[] img = this.Controls.Find("prod" + currentSlot, true);

            if (n.Length > 0 && p.Length > 0 && img.Length > 0)
            {
                POSData.ProductData[sID] = new object[] {
                    n[0].Text,
                    p[0].Text,
                    ((PictureBox)img[0]).Image,
                    pID,
                    iID
                };

                RefreshDashboardDisplay();

                NotifyOrderingForm();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cbStock.SelectedItem == null) return;
            string searchID = cbStock.SelectedItem.ToString();

            if (POSData.ProductData.ContainsKey(searchID))
            {
                object[] data = POSData.ProductData[searchID];

                cbPrice.SelectedItem = data[3].ToString();
                cbImage.SelectedItem = data[4].ToString();

                MessageBox.Show($"Details:\nStock ID: {searchID}\nPrice ID: {data[3]}\nImage ID: {data[4]}\nProduct: {data[0]}\nPrice: {data[1]}");
            }
            else { MessageBox.Show("No saved data found for this Stock ID."); }
        }

        private void RefreshDashboardDisplay()
        {
            for (int i = 0; i < cbStock.Items.Count; i++)
            {
                string id = cbStock.Items[i].ToString();
                if (POSData.ProductData.ContainsKey(id))
                {
                    object[] saved = POSData.ProductData[id];
                    FillSlot(i + 1, saved[0].ToString(), saved[1].ToString(), saved[2] as System.Drawing.Image);
                }
            }
        }

        private void FillSlot(int slot, string name, string price, System.Drawing.Image img)
        {
            Control[] n = this.Controls.Find("nameTxt" + slot, true);
            Control[] p = this.Controls.Find("price" + slot, true);
            Control[] pb = this.Controls.Find("prod" + slot, true);

            if (n.Length > 0) n[0].Text = name;
            if (p.Length > 0) p[0].Text = price;
            if (pb.Length > 0 && pb[0] is PictureBox pic)
            {
                pic.Image = img;
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        public void HandleImageClick(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pb.Image = new Bitmap(ofd.FileName);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (cbStock.SelectedItem == null) return;
            string idToDelete = cbStock.SelectedItem.ToString();

            if (MessageBox.Show($"Delete {idToDelete}?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (POSData.ProductData.Remove(idToDelete))
                {
                    ClearDashboardOnly();
                    RefreshDashboardDisplay();
                    NotifyOrderingForm();
                    MessageBox.Show("Record Deleted.");
                }
            }
        }

        private void NotifyOrderingForm()
        {
            foreach (System.Windows.Forms.Form f in System.Windows.Forms.Application.OpenForms)
            {
                if (f is Ordering_POS orderingForm)
                {
                    orderingForm.RefreshDashboard();
                }
            }
        }

        private void ResetIDs()
        {
            cbStock.SelectedIndex = -1;
            cbPrice.SelectedIndex = -1;
            cbImage.SelectedIndex = -1;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            currentSlot = 1;
            POSData.ProductData.Clear();
            ClearProductSlots();
            NotifyOrderingForm();
            MessageBox.Show("All Data Reset.");
        }

        private void btnExit_Click(object sender, EventArgs e) => this.Close();

        private void ClearProductSlots()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox t) t.Clear();
                if (c is PictureBox p) p.Image = null;
                if (c is ComboBox cb) cb.SelectedIndex = -1;
            }
        }

        private void ClearDashboardOnly()
        {
            for (int i = 1; i <= 15; i++)
            {
                Control[] n = this.Controls.Find("nameTxt" + i, true);
                Control[] p = this.Controls.Find("price" + i, true);
                Control[] pb = this.Controls.Find("prod" + i, true);
                if (n.Length > 0) ((TextBox)n[0]).Clear();
                if (p.Length > 0) ((TextBox)p[0]).Clear();
                if (pb.Length > 0) ((PictureBox)pb[0]).Image = null;
            }
        }
    }
}