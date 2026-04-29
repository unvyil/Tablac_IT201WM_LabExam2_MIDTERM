using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tablac_IT201WM_LabExam_MIdterm
{
    public partial class Ordering_POS : Form
    {
        public Ordering_POS()
        {
            InitializeComponent();
        }

        private void Ordering_POS_Load(object sender, EventArgs e)
        {
            txtItem.Enabled = false;
            txtPrice.Enabled = false;
            txtAmt.Enabled = false;
            txtDAmt.Enabled = false;
            sumItem.Enabled = false;
            sumTotalGiven.Enabled = false;
            sumTotalAmt.Enabled = false;

            RefreshDashboard();

            WireProductClicks();
            WireKeypad();
            WireCheckboxes();

            btnCalc.Click += btnCalc_Click;
            btnNew.Click += (s, ev) => ClearTransaction();
            btnCancel.Click += (s, ev) => ClearTransaction();
            btnExit.Click += (s, ev) => this.Close();
        }

        public void RefreshDashboard()
        {
            for (int i = 1; i <= 15; i++)
            {
                GetSlotControls(i, out TextBox nBox, out TextBox pBox, out PictureBox picBox);
                if (nBox != null) nBox.Text = "";
                if (pBox != null) pBox.Text = "";
                if (picBox != null) picBox.Image = null;
            }

            int slot = 1;
            if (POSData.ProductData != null)
            {
                foreach (var entry in POSData.ProductData)
                {
                    if (slot > 15) break;

                    object[] saved = entry.Value;
                    GetSlotControls(slot, out TextBox nBox, out TextBox pBox, out PictureBox picBox);

                    if (nBox != null) nBox.Text = saved[0].ToString();
                    if (pBox != null) pBox.Text = saved[1].ToString();
                    if (picBox != null)
                    {
                        picBox.Image = saved[2] as Image;
                        picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    slot++;
                }
            }
        }

        private void GetSlotControls(int slot, out TextBox nBox, out TextBox pBox, out PictureBox picBox)
        {
            nBox = null; pBox = null; picBox = null;
            switch (slot)
            {
                case 1: nBox = nameTxt1; pBox = price1; picBox = prod1; break;
                case 2: nBox = nameTxt2; pBox = price2; picBox = prod2; break;
                case 3: nBox = nameTxt3; pBox = price3; picBox = prod3; break;
                case 4: nBox = nameTxt4; pBox = price4; picBox = prod4; break;
                case 5: nBox = nameTxt5; pBox = price5; picBox = prod5; break;
                case 6: nBox = textBox2; pBox = textBox1; picBox = pictureBox1; break;
                case 7: nBox = textBox4; pBox = textBox3; picBox = pictureBox2; break;
                case 8: nBox = textBox6; pBox = textBox5; picBox = pictureBox3; break;
                case 9: nBox = textBox8; pBox = textBox7; picBox = pictureBox4; break;
                case 10: nBox = textBox10; pBox = textBox9; picBox = pictureBox5; break;
                case 11: nBox = textBox12; pBox = textBox11; picBox = pictureBox6; break;
                case 12: nBox = textBox14; pBox = textBox13; picBox = pictureBox7; break;
                case 13: nBox = textBox16; pBox = textBox15; picBox = pictureBox8; break;
                case 14: nBox = textBox18; pBox = textBox17; picBox = pictureBox9; break;
                case 15: nBox = textBox20; pBox = textBox19; picBox = pictureBox10; break;
            }
        }

        private void WireProductClicks()
        {
            for (int i = 1; i <= 15; i++)
            {
                GetSlotControls(i, out TextBox nBox, out TextBox pBox, out PictureBox picBox);

                if (nBox != null && pBox != null)
                {
                    EventHandler clickHandler = (s, e) => SelectProduct(nBox.Text, pBox.Text);

                    if (picBox != null) picBox.Click += clickHandler;
                    nBox.Click += clickHandler;
                    pBox.Click += clickHandler;

                    if (picBox != null) picBox.Cursor = Cursors.Hand;
                    nBox.Cursor = Cursors.Hand;
                    pBox.Cursor = Cursors.Hand;
                }
            }
        }

        private void SelectProduct(string name, string price)
        {
            if (string.IsNullOrWhiteSpace(name)) return;

            txtItem.Text = name;
            txtPrice.Text = price;
            numericUpDown1.Value = 1;

            cashRen.Enabled = true;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtPrice.Text, out double price))
            {
                int qty = (int)numericUpDown1.Value;

                double subtotal = price * qty;
                double discountRate = 0.0;

                if (senCit.Checked || discCard.Checked) { discountRate = 0.20; }
                else if (empDisc.Checked) { discountRate = 0.10; }

                double discountAmt = subtotal * discountRate;
                double finalAmt = subtotal - discountAmt;

                txtAmt.Text = discountAmt.ToString("N2");
                txtDAmt.Text = finalAmt.ToString("N2");

                sumItem.Text = qty.ToString();
                sumTotalGiven.Text = discountAmt.ToString("N2");
                sumTotalAmt.Text = finalAmt.ToString("N2");

                CalculateChange();
            }
            else
            {
                MessageBox.Show("Please select an item first.");
            }
        }

        private void CalculateChange()
        {
            if (double.TryParse(sumTotalAmt.Text, out double total) && double.TryParse(cashRen.Text, out double cash))
            {
                double changeAmt = cash - total;
                change.Text = (changeAmt >= 0) ? changeAmt.ToString("N2") : "Insufficient";
            }
        }

        private void WireKeypad()
        {
            Button[] numButtons = { button5, button6, button7, button8, button9, button10, button11, button12, button13, button17, button18 };

            foreach (Button b in numButtons)
            {
                b.Click += (s, e) => {
                    cashRen.Text += b.Text;
                };
            }

            enter.Click += (s, e) => CalculateChange();

            add.Click += (s, e) => cashRen.Clear();
            subtract.Click += (s, e) => cashRen.Clear();
            multiply.Click += (s, e) => cashRen.Clear();
            divide.Click += (s, e) => cashRen.Clear();
        }

        private void WireCheckboxes()
        {
            senCit.CheckedChanged += EnforceSingleDiscount;
            discCard.CheckedChanged += EnforceSingleDiscount;
            empDisc.CheckedChanged += EnforceSingleDiscount;
            noDisc.CheckedChanged += EnforceSingleDiscount;
        }

        private void EnforceSingleDiscount(object sender, EventArgs e)
        {
            CheckBox active = sender as CheckBox;
            if (active != null && active.Checked)
            {
                if (active != senCit) senCit.Checked = false;
                if (active != discCard) discCard.Checked = false;
                if (active != empDisc) empDisc.Checked = false;
                if (active != noDisc) noDisc.Checked = false;
            }
        }

        private void ClearTransaction()
        {
            txtItem.Clear(); txtPrice.Clear(); txtAmt.Clear(); txtDAmt.Clear();
            numericUpDown1.Value = 1;

            sumItem.Clear(); sumTotalGiven.Clear(); sumTotalAmt.Clear();
            cashRen.Clear(); change.Clear();

            senCit.Checked = false; discCard.Checked = false; empDisc.Checked = false; noDisc.Checked = true;
            cashRen.Enabled = false;
        }

        private void label31_Click(object sender, EventArgs e) { }
        private void label32_Click(object sender, EventArgs e) { }
    }
}