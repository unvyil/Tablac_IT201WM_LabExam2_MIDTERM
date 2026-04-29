using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tablac_IT201WM_LabExam2_MIDTERM
{
    public partial class Receipt : Form
    {

    private ListBox lstReceipt;


public Receipt(ListBox.ObjectCollection receiptItems)
        {
            InitializeComponent();
            GenerateReceipt(receiptItems);
        }

        private void GenerateReceipt(ListBox.ObjectCollection items)
        {
            printDisplayListbox.Items.Clear();

            printDisplayListbox.Items.Add("====================================");
            printDisplayListbox.Items.Add("          4JEE FOODS INC.           ");
            printDisplayListbox.Items.Add("          OFFICIAL RECEIPT          ");
            printDisplayListbox.Items.Add("====================================");
            printDisplayListbox.Items.Add($"Date: {DateTime.Now.ToString("g")}");
            printDisplayListbox.Items.Add("");

            foreach (var item in items)
            {
                printDisplayListbox.Items.Add(item);
            }

            printDisplayListbox.Items.Add("");
            printDisplayListbox.Items.Add("     Thank you for your order!      ");
            printDisplayListbox.Items.Add("====================================");
        }
    }
}