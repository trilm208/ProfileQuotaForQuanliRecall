namespace Shell
{
    partial class FaceFaceSimilarUnit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtConf = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FacePhoto = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.FacePhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // txtConf
            // 
            this.txtConf.AutoSize = true;
            this.txtConf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConf.ForeColor = System.Drawing.Color.Red;
            this.txtConf.Location = new System.Drawing.Point(4, 9);
            this.txtConf.Name = "txtConf";
            this.txtConf.Size = new System.Drawing.Size(32, 15);
            this.txtConf.TabIndex = 1;
            this.txtConf.Text = "Conf";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(45, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "txtProjectName";
            // 
            // FacePhoto
            // 
            this.FacePhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FacePhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FacePhoto.Location = new System.Drawing.Point(0, 39);
            this.FacePhoto.Name = "FacePhoto";
            this.FacePhoto.Size = new System.Drawing.Size(179, 161);
            this.FacePhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FacePhoto.TabIndex = 213;
            this.FacePhoto.TabStop = false;
            // 
            // FaceFaceSimilarUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FacePhoto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConf);
            this.Name = "FaceFaceSimilarUnit";
            this.Size = new System.Drawing.Size(182, 205);
            ((System.ComponentModel.ISupportInitialize)(this.FacePhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label txtConf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox FacePhoto;
    }
}
