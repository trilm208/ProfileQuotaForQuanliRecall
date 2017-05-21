namespace QA.DocumentationAndroid.DesignForm.Answers
{
    partial class AnswerFreeText
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtLimitText = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtLimitText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(0, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Limit Text Size To";
            // 
            // txtLimitText
            // 
            this.txtLimitText.Location = new System.Drawing.Point(89, 3);
            this.txtLimitText.Name = "txtLimitText";
            this.txtLimitText.Properties.Mask.EditMask = "[0-9]+";
            this.txtLimitText.Properties.Mask.IgnoreMaskBlank = false;
            this.txtLimitText.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtLimitText.Properties.Mask.ShowPlaceHolders = false;
            this.txtLimitText.Size = new System.Drawing.Size(102, 20);
            this.txtLimitText.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(197, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Characters";
            // 
            // AnswerFreeText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtLimitText);
            this.Controls.Add(this.labelControl1);
            this.Name = "AnswerFreeText";
            this.Size = new System.Drawing.Size(535, 278);
            ((System.ComponentModel.ISupportInitialize)(this.txtLimitText.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtLimitText;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
