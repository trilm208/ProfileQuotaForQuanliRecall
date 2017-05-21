namespace QA.DocumentationAndroid.DesignForm.Answers
{
    partial class AnswerMultiChoice
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
            this.gAnswers = new DevExpress.XtraGrid.GridControl();
            this.gvAnswers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnUp = new DevExpress.XtraEditors.SimpleButton();
            this.btnDown = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtAnswerText = new DevExpress.XtraEditors.MemoEdit();
            this.btnProperties = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gAnswers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAnswers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnswerText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gAnswers
            // 
            this.gAnswers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gAnswers.Location = new System.Drawing.Point(3, 97);
            this.gAnswers.MainView = this.gvAnswers;
            this.gAnswers.Name = "gAnswers";
            this.gAnswers.Size = new System.Drawing.Size(489, 200);
            this.gAnswers.TabIndex = 0;
            this.gAnswers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAnswers});
            // 
            // gvAnswers
            // 
            this.gvAnswers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gvAnswers.GridControl = this.gAnswers;
            this.gvAnswers.Name = "gvAnswers";
            this.gvAnswers.OptionsBehavior.Editable = false;
            this.gvAnswers.OptionsBehavior.ReadOnly = true;
            this.gvAnswers.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvAnswers.OptionsView.ShowGroupPanel = false;
            this.gvAnswers.OptionsView.ShowIndicator = false;
            this.gvAnswers.DoubleClick += new System.EventHandler(this.gvAnswers_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Text";
            this.gridColumn1.FieldName = "MultiChoice_View_Answer_AnswerText";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 341;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Index";
            this.gridColumn2.FieldName = "MultiChoice_View_Answer_AnswerIndex";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 97;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "MultiChoice_View_Answer_AnswerCodes_VariableName";
            this.gridColumn3.FieldName = "MultiChoice_View_Answer_AnswerCodes_VariableName";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "MultiChoice_View_Answer_AnswerCodes_UnCheckedCode";
            this.gridColumn4.FieldName = "MultiChoice_View_Answer_AnswerCodes_UnCheckedCode";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "MultiChoice_View_Answer_AnswerCodes_CheckedCode";
            this.gridColumn5.FieldName = "MultiChoice_View_Answer_AnswerCodes_CheckedCode";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "MultiChoice_View_Answer_AnswerCodes_OtherSpecify";
            this.gridColumn6.FieldName = "MultiChoice_View_Answer_AnswerCodes_OtherSpecify";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "MultiChoice_View_Answer_AnswerCodes_OtherSpecify_InType";
            this.gridColumn7.FieldName = "MultiChoice_View_Answer_AnswerCodes_OtherSpecify_InType";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "MultiChoice_View_Answer_AnswerCodes_OtherSpecify_VariableName";
            this.gridColumn8.FieldName = "MultiChoice_View_Answer_AnswerCodes_OtherSpecify_VariableName";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Location = new System.Drawing.Point(496, 97);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 23);
            this.btnUp.TabIndex = 1;
            this.btnUp.Text = "Up";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Location = new System.Drawing.Point(496, 126);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 23);
            this.btnDown.TabIndex = 2;
            this.btnDown.Text = "Down";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(496, 155);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(417, 68);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(4, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Answer Text";
            // 
            // txtAnswerText
            // 
            this.txtAnswerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAnswerText.Location = new System.Drawing.Point(71, 3);
            this.txtAnswerText.Name = "txtAnswerText";
            this.txtAnswerText.Size = new System.Drawing.Size(423, 59);
            this.txtAnswerText.TabIndex = 8;
            this.txtAnswerText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAnswerText_KeyPress);
            // 
            // btnProperties
            // 
            this.btnProperties.Location = new System.Drawing.Point(7, 68);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(75, 23);
            this.btnProperties.TabIndex = 9;
            this.btnProperties.Text = "Properties";
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // AnswerMultiChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnProperties);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.gAnswers);
            this.Controls.Add(this.txtAnswerText);
            this.Name = "AnswerMultiChoice";
            this.Size = new System.Drawing.Size(576, 300);
            ((System.ComponentModel.ISupportInitialize)(this.gAnswers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAnswers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnswerText.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gAnswers;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAnswers;
        private DevExpress.XtraEditors.SimpleButton btnUp;
        private DevExpress.XtraEditors.SimpleButton btnDown;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtAnswerText;
        private DevExpress.XtraEditors.SimpleButton btnProperties;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    }
}
