namespace QA.DocumentationAndroid
{
    partial class frmDesignFormBasic
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
            this.gQuestions = new DevExpress.XtraGrid.GridControl();
            this.gvQuestions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAddQuestion = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gQuestions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuestions)).BeginInit();
            this.SuspendLayout();
            // 
            // gQuestions
            // 
            this.gQuestions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gQuestions.Location = new System.Drawing.Point(-1, 4);
            this.gQuestions.MainView = this.gvQuestions;
            this.gQuestions.Name = "gQuestions";
            this.gQuestions.Size = new System.Drawing.Size(925, 399);
            this.gQuestions.TabIndex = 1;
            this.gQuestions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvQuestions});
            // 
            // gvQuestions
            // 
            this.gvQuestions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gvQuestions.GridControl = this.gQuestions;
            this.gvQuestions.Name = "gvQuestions";
            this.gvQuestions.OptionsBehavior.Editable = false;
            this.gvQuestions.OptionsBehavior.ReadOnly = true;
            this.gvQuestions.OptionsView.ShowGroupPanel = false;
            this.gvQuestions.OptionsView.ShowIndicator = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Main chapter";
            this.gridColumn1.FieldName = "View_Question_QuestionName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // btnAddQuestion
            // 
            this.btnAddQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddQuestion.Location = new System.Drawing.Point(3, 409);
            this.btnAddQuestion.Name = "btnAddQuestion";
            this.btnAddQuestion.Size = new System.Drawing.Size(142, 23);
            this.btnAddQuestion.TabIndex = 2;
            this.btnAddQuestion.Text = "Add Question [F1]";
            this.btnAddQuestion.Click += new System.EventHandler(this.btnAddQuestion_Click);
            // 
            // frmDesignFormBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 436);
            this.Controls.Add(this.btnAddQuestion);
            this.Controls.Add(this.gQuestions);
            this.Name = "frmDesignFormBasic";
            this.Text = "Design Form";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmDesignFormBasic_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.gQuestions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuestions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gQuestions;
        private DevExpress.XtraGrid.Views.Grid.GridView gvQuestions;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.SimpleButton btnAddQuestion;
    }
}