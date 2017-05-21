namespace Launcher
{
    partial class frmUpdate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdate));
            this.cmdContinue = new System.Windows.Forms.Button();
            this.listLog = new System.Windows.Forms.ListBox();
            this.txtTask = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtTitle = new System.Windows.Forms.Label();
            this.txtSubtitle = new System.Windows.Forms.Label();
            this.startTimer = new System.Windows.Forms.Timer(this.components);
            this.pictureSoftwareUpdate = new System.Windows.Forms.PictureBox();
            this.pictureSeparator = new System.Windows.Forms.PictureBox();
            this.pictureUpdateHeader = new System.Windows.Forms.PictureBox();
            this.finishTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureSoftwareUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSeparator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUpdateHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdContinue
            // 
            this.cmdContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdContinue.Enabled = false;
            this.cmdContinue.Location = new System.Drawing.Point(376, 296);
            this.cmdContinue.Name = "cmdContinue";
            this.cmdContinue.Size = new System.Drawing.Size(75, 23);
            this.cmdContinue.TabIndex = 0;
            this.cmdContinue.Text = "Continue";
            this.cmdContinue.UseVisualStyleBackColor = true;
            this.cmdContinue.Click += new System.EventHandler(this.cmdContinue_Click);
            // 
            // listLog
            // 
            this.listLog.FormattingEnabled = true;
            this.listLog.Location = new System.Drawing.Point(23, 117);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(414, 160);
            this.listLog.TabIndex = 5;
            // 
            // txtTask
            // 
            this.txtTask.AutoSize = true;
            this.txtTask.Location = new System.Drawing.Point(20, 74);
            this.txtTask.Name = "txtTask";
            this.txtTask.Size = new System.Drawing.Size(89, 13);
            this.txtTask.TabIndex = 3;
            this.txtTask.Text = "Updating: File.ext";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(23, 90);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(414, 18);
            this.progressBar1.TabIndex = 4;
            // 
            // txtTitle
            // 
            this.txtTitle.AutoSize = true;
            this.txtTitle.BackColor = System.Drawing.Color.White;
            this.txtTitle.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(20, 13);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(55, 14);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.Text = "Updating";
            // 
            // txtSubtitle
            // 
            this.txtSubtitle.AutoSize = true;
            this.txtSubtitle.BackColor = System.Drawing.Color.White;
            this.txtSubtitle.Location = new System.Drawing.Point(29, 29);
            this.txtSubtitle.Name = "txtSubtitle";
            this.txtSubtitle.Size = new System.Drawing.Size(201, 13);
            this.txtSubtitle.TabIndex = 2;
            this.txtSubtitle.Text = "Please wait while files are being updated.";
            // 
            // startTimer
            // 
            this.startTimer.Enabled = true;
            this.startTimer.Tick += new System.EventHandler(this.startTimer_Tick);
            // 
            // pictureSoftwareUpdate
            // 
            this.pictureSoftwareUpdate.BackColor = System.Drawing.Color.White;
            this.pictureSoftwareUpdate.Image = ((System.Drawing.Image)(resources.GetObject("pictureSoftwareUpdate.Image")));
            this.pictureSoftwareUpdate.Location = new System.Drawing.Point(389, 5);
            this.pictureSoftwareUpdate.Name = "pictureSoftwareUpdate";
            this.pictureSoftwareUpdate.Size = new System.Drawing.Size(48, 48);
            this.pictureSoftwareUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureSoftwareUpdate.TabIndex = 6;
            this.pictureSoftwareUpdate.TabStop = false;
            // 
            // pictureSeparator
            // 
            this.pictureSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureSeparator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureSeparator.BackgroundImage")));
            this.pictureSeparator.Location = new System.Drawing.Point(12, 288);
            this.pictureSeparator.Name = "pictureSeparator";
            this.pictureSeparator.Size = new System.Drawing.Size(439, 2);
            this.pictureSeparator.TabIndex = 1;
            this.pictureSeparator.TabStop = false;
            // 
            // pictureUpdateHeader
            // 
            this.pictureUpdateHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureUpdateHeader.BackgroundImage")));
            this.pictureUpdateHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureUpdateHeader.Location = new System.Drawing.Point(0, 0);
            this.pictureUpdateHeader.Name = "pictureUpdateHeader";
            this.pictureUpdateHeader.Size = new System.Drawing.Size(463, 59);
            this.pictureUpdateHeader.TabIndex = 0;
            this.pictureUpdateHeader.TabStop = false;
            // 
            // finishTimer
            // 
            this.finishTimer.Interval = 1000;
            this.finishTimer.Tick += new System.EventHandler(this.finishTimer_Tick);
            // 
            // frmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 331);
            this.Controls.Add(this.txtSubtitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.pictureSoftwareUpdate);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtTask);
            this.Controls.Add(this.listLog);
            this.Controls.Add(this.cmdContinue);
            this.Controls.Add(this.pictureSeparator);
            this.Controls.Add(this.pictureUpdateHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update";
            ((System.ComponentModel.ISupportInitialize)(this.pictureSoftwareUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSeparator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUpdateHeader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureUpdateHeader;
        private System.Windows.Forms.PictureBox pictureSeparator;
        private System.Windows.Forms.Button cmdContinue;
        private System.Windows.Forms.ListBox listLog;
        private System.Windows.Forms.Label txtTask;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pictureSoftwareUpdate;
        private System.Windows.Forms.Label txtTitle;
        private System.Windows.Forms.Label txtSubtitle;
        private System.Windows.Forms.Timer startTimer;
        private System.Windows.Forms.Timer finishTimer;
    }
}

