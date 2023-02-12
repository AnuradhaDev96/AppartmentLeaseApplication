namespace ApartmentLeaseReportingApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnShowReport = new System.Windows.Forms.Button();
            this.BtnShowBuildingsReport = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnShowReport
            // 
            this.BtnShowReport.Location = new System.Drawing.Point(289, 95);
            this.BtnShowReport.Name = "BtnShowReport";
            this.BtnShowReport.Size = new System.Drawing.Size(265, 34);
            this.BtnShowReport.TabIndex = 0;
            this.BtnShowReport.Text = "Show Apartments Report";
            this.BtnShowReport.UseVisualStyleBackColor = true;
            this.BtnShowReport.Click += new System.EventHandler(this.BtnShowReport_Click);
            // 
            // BtnShowBuildingsReport
            // 
            this.BtnShowBuildingsReport.Location = new System.Drawing.Point(289, 149);
            this.BtnShowBuildingsReport.Name = "BtnShowBuildingsReport";
            this.BtnShowBuildingsReport.Size = new System.Drawing.Size(265, 34);
            this.BtnShowBuildingsReport.TabIndex = 1;
            this.BtnShowBuildingsReport.Text = "Show Buildings Report";
            this.BtnShowBuildingsReport.UseVisualStyleBackColor = true;
            this.BtnShowBuildingsReport.Click += new System.EventHandler(this.BtnShowBuildingsReport_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(276, 198);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(298, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Show Apartment Classes Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(289, 254);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(265, 34);
            this.button2.TabIndex = 3;
            this.button2.Text = "Show Parking Spaces Report";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnShowBuildingsReport);
            this.Controls.Add(this.BtnShowReport);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private Button BtnShowReport;
        private Button BtnShowBuildingsReport;
        private Button button1;
        private Button button2;
    }
}