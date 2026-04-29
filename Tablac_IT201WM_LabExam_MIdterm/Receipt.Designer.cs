namespace Tablac_IT201WM_LabExam2_MIDTERM
{
    partial class Receipt
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label6 = new System.Windows.Forms.Label();
            this.printDisplayListbox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Sora ExtraBold", 10F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(34, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(337, 30);
            this.label6.TabIndex = 263;
            this.label6.Text = "4JEE ORDERING APPLICATION";
            // 
            // printDisplayListbox
            // 
            this.printDisplayListbox.FormattingEnabled = true;
            this.printDisplayListbox.ItemHeight = 20;
            this.printDisplayListbox.Location = new System.Drawing.Point(12, 66);
            this.printDisplayListbox.Name = "printDisplayListbox";
            this.printDisplayListbox.Size = new System.Drawing.Size(382, 364);
            this.printDisplayListbox.TabIndex = 390;
            // 
            // Receipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 450);
            this.Controls.Add(this.printDisplayListbox);
            this.Controls.Add(this.label6);
            this.Name = "Receipt";
            this.Text = "Receipt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox printDisplayListbox;
    }
}