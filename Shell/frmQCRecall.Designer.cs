namespace Shell
{
    partial class frmQCRecall
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
            this.TabCollection = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.respondentProfileInformation1 = new Shell.RespondentProfileInformation();
            this.TabSummaryRecuit = new DevExpress.XtraTab.XtraTabPage();
            this.summarySupRecuit1 = new Shell.SummarySupRecuit();
            this.TabReport = new DevExpress.XtraTab.XtraTabPage();
            this.fwProfile1 = new Shell.FWProfile();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.settings1 = new Shell.Settings();
            ((System.ComponentModel.ISupportInitialize)(this.TabCollection)).BeginInit();
            this.TabCollection.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.TabSummaryRecuit.SuspendLayout();
            this.TabReport.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabCollection
            // 
            this.TabCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCollection.Location = new System.Drawing.Point(0, 0);
            this.TabCollection.Name = "TabCollection";
            this.TabCollection.SelectedTabPage = this.xtraTabPage1;
            this.TabCollection.Size = new System.Drawing.Size(1126, 812);
            this.TabCollection.TabIndex = 0;
            this.TabCollection.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.TabSummaryRecuit,
            this.TabReport,
            this.xtraTabPage4});
            this.TabCollection.Click += new System.EventHandler(this.TabCollection_Click);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.respondentProfileInformation1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1120, 784);
            this.xtraTabPage1.Text = "Respondent Detail";
            this.xtraTabPage1.Click += new System.EventHandler(this.xtraTabPage1_Click);
            // 
            // respondentProfileInformation1
            // 
            this.respondentProfileInformation1.AnswerID = null;
            this.respondentProfileInformation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.respondentProfileInformation1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respondentProfileInformation1.Location = new System.Drawing.Point(0, 0);
            this.respondentProfileInformation1.Name = "respondentProfileInformation1";
            this.respondentProfileInformation1.ProjectID = null;
            this.respondentProfileInformation1.Services = null;
            this.respondentProfileInformation1.Size = new System.Drawing.Size(1120, 784);
            this.respondentProfileInformation1.TabIndex = 0;
            // 
            // TabSummaryRecuit
            // 
            this.TabSummaryRecuit.Controls.Add(this.summarySupRecuit1);
            this.TabSummaryRecuit.Name = "TabSummaryRecuit";
            this.TabSummaryRecuit.Size = new System.Drawing.Size(1120, 784);
            this.TabSummaryRecuit.Text = "Summary";
            // 
            // summarySupRecuit1
            // 
            this.summarySupRecuit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summarySupRecuit1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.summarySupRecuit1.Location = new System.Drawing.Point(0, 0);
            this.summarySupRecuit1.Name = "summarySupRecuit1";
            this.summarySupRecuit1.ProjectID = null;
            this.summarySupRecuit1.Services = null;
            this.summarySupRecuit1.Size = new System.Drawing.Size(1120, 784);
            this.summarySupRecuit1.TabIndex = 0;
            // 
            // TabReport
            // 
            this.TabReport.Controls.Add(this.fwProfile1);
            this.TabReport.Name = "TabReport";
            this.TabReport.Size = new System.Drawing.Size(1120, 784);
            this.TabReport.Text = "Respondent Profile";
            // 
            // fwProfile1
            // 
            this.fwProfile1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fwProfile1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fwProfile1.Location = new System.Drawing.Point(0, 0);
            this.fwProfile1.Name = "fwProfile1";
            this.fwProfile1.ProjectID = null;
            this.fwProfile1.Services = null;
            this.fwProfile1.Size = new System.Drawing.Size(1120, 784);
            this.fwProfile1.TabIndex = 0;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.settings1);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1120, 784);
            this.xtraTabPage4.Text = "Cài đặt";
            // 
            // settings1
            // 
            this.settings1.Location = new System.Drawing.Point(3, 3);
            this.settings1.Name = "settings1";
            this.settings1.Size = new System.Drawing.Size(387, 160);
            this.settings1.TabIndex = 0;
            // 
            // frmQCRecall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 812);
            this.Controls.Add(this.TabCollection);
            this.MaximumSize = new System.Drawing.Size(1700, 850);
            this.MinimumSize = new System.Drawing.Size(1000, 850);
            this.Name = "frmQCRecall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QC Recall";
            ((System.ComponentModel.ISupportInitialize)(this.TabCollection)).EndInit();
            this.TabCollection.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.TabSummaryRecuit.ResumeLayout(false);
            this.TabReport.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl TabCollection;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private RespondentProfileInformation respondentProfileInformation1;
        private Settings settings1;
        private DevExpress.XtraTab.XtraTabPage TabSummaryRecuit;
        private DevExpress.XtraTab.XtraTabPage TabReport;
        private SummarySupRecuit summarySupRecuit1;
        private FWProfile fwProfile1;
    }
}