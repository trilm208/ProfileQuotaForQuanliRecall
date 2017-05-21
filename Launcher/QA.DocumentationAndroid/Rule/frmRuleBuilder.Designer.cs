namespace QA.DocumentationAndroid.Rule
{
    partial class frmRuleBuilder
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbQuestion = new System.Windows.Forms.ComboBox();
            this.cmdCreate = new DevExpress.XtraEditors.SimpleButton();
            this.cbQuestionCode = new System.Windows.Forms.ComboBox();
            this.cbRule = new System.Windows.Forms.ComboBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtValue = new DevExpress.XtraEditors.TextEdit();
            this.txtExpressionValidate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressionValidate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(7, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(131, 16);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Display question only if";
            // 
            // cbQuestion
            // 
            this.cbQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbQuestion.DisplayMember = "View_Question_QuestionName";
            this.cbQuestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuestion.FormattingEnabled = true;
            this.cbQuestion.Location = new System.Drawing.Point(169, 7);
            this.cbQuestion.Name = "cbQuestion";
            this.cbQuestion.Size = new System.Drawing.Size(269, 21);
            this.cbQuestion.TabIndex = 3;
            this.cbQuestion.ValueMember = "QuestionID";
            this.cbQuestion.SelectedIndexChanged += new System.EventHandler(this.cbQuestion_SelectedIndexChanged);
            // 
            // cmdCreate
            // 
            this.cmdCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCreate.Location = new System.Drawing.Point(363, 108);
            this.cmdCreate.Name = "cmdCreate";
            this.cmdCreate.Size = new System.Drawing.Size(75, 23);
            this.cmdCreate.TabIndex = 6;
            this.cmdCreate.Text = "Create";
            // 
            // cbQuestionCode
            // 
            this.cbQuestionCode.DisplayMember = "FieldName";
            this.cbQuestionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuestionCode.FormattingEnabled = true;
            this.cbQuestionCode.Location = new System.Drawing.Point(7, 58);
            this.cbQuestionCode.Name = "cbQuestionCode";
            this.cbQuestionCode.Size = new System.Drawing.Size(126, 21);
            this.cbQuestionCode.TabIndex = 5;
            this.cbQuestionCode.ValueMember = "FieldName";
            this.cbQuestionCode.SelectedIndexChanged += new System.EventHandler(this.cbQuestionCode_SelectedIndexChanged);
            // 
            // cbRule
            // 
            this.cbRule.DisplayMember = "RuleTypeName";
            this.cbRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRule.FormattingEnabled = true;
            this.cbRule.Location = new System.Drawing.Point(139, 58);
            this.cbRule.Name = "cbRule";
            this.cbRule.Size = new System.Drawing.Size(126, 21);
            this.cbRule.TabIndex = 7;
            this.cbRule.ValueMember = "RuleTypeCode";
            this.cbRule.SelectedIndexChanged += new System.EventHandler(this.cbRule_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(7, 33);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(83, 16);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Question Code";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(139, 36);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(25, 16);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "Rule";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(271, 36);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(32, 16);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "Value";
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(271, 58);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(167, 20);
            this.txtValue.TabIndex = 14;
            this.txtValue.EditValueChanged += new System.EventHandler(this.txtValue_EditValueChanged);
            // 
            // txtExpressionValidate
            // 
            this.txtExpressionValidate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpressionValidate.Location = new System.Drawing.Point(139, 84);
            this.txtExpressionValidate.Name = "txtExpressionValidate";
            this.txtExpressionValidate.Size = new System.Drawing.Size(299, 20);
            this.txtExpressionValidate.TabIndex = 16;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(7, 85);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(111, 16);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "Expression Validate";
            // 
            // frmRuleBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 137);
            this.Controls.Add(this.txtExpressionValidate);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cbRule);
            this.Controls.Add(this.cmdCreate);
            this.Controls.Add(this.cbQuestionCode);
            this.Controls.Add(this.cbQuestion);
            this.Controls.Add(this.labelControl1);
            this.Name = "frmRuleBuilder";
            this.Text = "Rule Builder";
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressionValidate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox cbQuestion;
        private DevExpress.XtraEditors.SimpleButton cmdCreate;
        private System.Windows.Forms.ComboBox cbQuestionCode;
        private System.Windows.Forms.ComboBox cbRule;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtValue;
        private DevExpress.XtraEditors.TextEdit txtExpressionValidate;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}