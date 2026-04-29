using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tablac_IT201WM_LabExam2_MIDTERM;

namespace Tablac_IT201WM_LabExam1_Midterm
{
    public partial class Bundles : Form
    {
        private const double BundleAPrice = 750.00;
        private const double BundleBPrice = 1200.00;

        public Bundles()
        {
            InitializeComponent();


            rb_bundA.CheckedChanged += Bundle_CheckedChanged;
            rb_bundB.CheckedChanged += Bundle_CheckedChanged;

            rb_bundA.CheckedChanged += UpdateOrderImage;
            rb_bundB.CheckedChanged += UpdateOrderImage;
        }

        private void Bundle_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_bundA.Checked)
            {
                txtPrice.Text = BundleAPrice.ToString("0.00");
            }
            else if (rb_bundB.Checked)
            {
                txtPrice.Text = BundleBPrice.ToString("0.00");
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double price = string.IsNullOrEmpty(txtPrice.Text) ? 0 : Convert.ToDouble(txtPrice.Text);
                int qty = string.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToInt32(txtQty.Text);
                double cashGiven = string.IsNullOrEmpty(txtCash.Text) ? 0 : Convert.ToDouble(txtCash.Text);

                double subtotal = price * qty;

                double discountRate = 0.0;
                if (senCit.Checked || discCard.Checked)
                {
                    discountRate = 0.20;
                }
                else if (empDisc.Checked)
                {
                    discountRate = 0.10;
                }

                double discountAmount = subtotal * discountRate;
                double totalBills = subtotal - discountAmount;

                txtAmt.Text = subtotal.ToString("0.00");
                txtDiscAmt.Text = discountAmount.ToString("0.00");
                txtBills.Text = totalBills.ToString("0.00");
                txtTotQty.Text = qty.ToString();

                if (cashGiven > 0)
                {
                    if (cashGiven >= totalBills)
                    {
                        double change = cashGiven - totalBills;
                        txtChange.Text = change.ToString("0.00");

                        string itemName = "";
                        if (rb_bundA.Checked) itemName = "Food Bundle A";
                        else if (rb_bundB.Checked) itemName = "Food Bundle B";
                        else itemName = "Custom Pizza Order";

                        string cartItem = $"{qty}x {itemName} | Price: ₱{price:0.00} | Total: ₱{totalBills:0.00}";

                        displaylistBox1.Items.Add(cartItem);
                    }
                    else
                    {
                        MessageBox.Show("Insufficient cash provided!", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtChange.Clear();
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (displaylistBox1.Items.Count > 0)
            {
                Receipt receiptWindow = new Receipt(displaylistBox1.Items);
                receiptWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("No items to print.", "Empty Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (displaylistBox1.SelectedIndex != -1)
            {
                displaylistBox1.Items.RemoveAt(displaylistBox1.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Please select an item to remove.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            rb_bundA.Checked = false;
            rb_bundB.Checked = false;
            noDisc.Checked = true;

            txtPrice.Clear();
            txtQty.Clear();
            txtAmt.Clear();
            txtDiscAmt.Clear();
            txtBills.Clear();
            txtTotQty.Clear();
            txtCash.Clear();
            txtChange.Clear();

            displaylistBox1.Items.Clear();
        }

        private void UpdateOrderImage(object sender, EventArgs e)
        {
            pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;

            if (rb_bundA.Checked)
            {
                pictureBox11.Image = prod1.Image;
            }
            else if (rb_bundB.Checked)
            {
                pictureBox11.Image = prod2.Image;
            }
            else
            {
                pictureBox11.Image = null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}