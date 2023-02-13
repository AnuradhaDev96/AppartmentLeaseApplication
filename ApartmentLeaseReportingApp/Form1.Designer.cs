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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnManageBuildings = new System.Windows.Forms.Button();
            this.btnManageApartments = new System.Windows.Forms.Button();
            this.btnApartmentClasses = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnApproveExtensions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnShowReport
            // 
            this.BtnShowReport.Location = new System.Drawing.Point(48, 143);
            this.BtnShowReport.Name = "BtnShowReport";
            this.BtnShowReport.Size = new System.Drawing.Size(265, 34);
            this.BtnShowReport.TabIndex = 0;
            this.BtnShowReport.Text = "Show Apartments Report";
            this.BtnShowReport.UseVisualStyleBackColor = true;
            this.BtnShowReport.Click += new System.EventHandler(this.BtnShowReport_Click);
            // 
            // BtnShowBuildingsReport
            // 
            this.BtnShowBuildingsReport.Location = new System.Drawing.Point(48, 197);
            this.BtnShowBuildingsReport.Name = "BtnShowBuildingsReport";
            this.BtnShowBuildingsReport.Size = new System.Drawing.Size(265, 34);
            this.BtnShowBuildingsReport.TabIndex = 1;
            this.BtnShowBuildingsReport.Text = "Show Buildings Report";
            this.BtnShowBuildingsReport.UseVisualStyleBackColor = true;
            this.BtnShowBuildingsReport.Click += new System.EventHandler(this.BtnShowBuildingsReport_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(298, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Show Apartment Classes Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(48, 302);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(265, 34);
            this.button2.TabIndex = 3;
            this.button2.Text = "Show Parking Spaces Report";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Stencil", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(598, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "ADMIN PORTAL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(81, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "Report Management";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(492, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 28);
            this.label3.TabIndex = 6;
            this.label3.Text = "Asset Management";
            // 
            // btnManageBuildings
            // 
            this.btnManageBuildings.Location = new System.Drawing.Point(461, 143);
            this.btnManageBuildings.Name = "btnManageBuildings";
            this.btnManageBuildings.Size = new System.Drawing.Size(265, 34);
            this.btnManageBuildings.TabIndex = 7;
            this.btnManageBuildings.Text = "Manage Buildings";
            this.btnManageBuildings.UseVisualStyleBackColor = true;
            this.btnManageBuildings.Click += new System.EventHandler(this.btnManageBuildings_Click);
            // 
            // btnManageApartments
            // 
            this.btnManageApartments.Location = new System.Drawing.Point(461, 197);
            this.btnManageApartments.Name = "btnManageApartments";
            this.btnManageApartments.Size = new System.Drawing.Size(265, 34);
            this.btnManageApartments.TabIndex = 8;
            this.btnManageApartments.Text = "Manage Apartments";
            this.btnManageApartments.UseVisualStyleBackColor = true;
            // 
            // btnApartmentClasses
            // 
            this.btnApartmentClasses.Location = new System.Drawing.Point(461, 246);
            this.btnApartmentClasses.Name = "btnApartmentClasses";
            this.btnApartmentClasses.Size = new System.Drawing.Size(265, 34);
            this.btnApartmentClasses.TabIndex = 9;
            this.btnApartmentClasses.Text = "Manage Apartment Classes";
            this.btnApartmentClasses.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Stencil", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(35, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(307, 47);
            this.label4.TabIndex = 10;
            this.label4.Text = "E-Apartments";
            // 
            // btnApproveExtensions
            // 
            this.btnApproveExtensions.Location = new System.Drawing.Point(461, 302);
            this.btnApproveExtensions.Name = "btnApproveExtensions";
            this.btnApproveExtensions.Size = new System.Drawing.Size(265, 64);
            this.btnApproveExtensions.TabIndex = 11;
            this.btnApproveExtensions.Text = "Approve/Decline Extention Requests";
            this.btnApproveExtensions.UseVisualStyleBackColor = true;
            this.btnApproveExtensions.Click += new System.EventHandler(this.btnApproveExtensions_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnApproveExtensions);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnApartmentClasses);
            this.Controls.Add(this.btnManageApartments);
            this.Controls.Add(this.btnManageBuildings);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnShowBuildingsReport);
            this.Controls.Add(this.BtnShowReport);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button BtnShowReport;
        private Button BtnShowBuildingsReport;
        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnManageBuildings;
        private Button btnManageApartments;
        private Button btnApartmentClasses;
        private Label label4;
        private Button btnApproveExtensions;
    }
}