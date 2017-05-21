namespace QA.DocumentationAndroid
{
    partial class frmDesignForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDesignForm));
            this.gQuestions = new DevExpress.XtraGrid.GridControl();
            this.gvQuestions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAddSingleChoice = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddMultiChoice = new DevExpress.XtraEditors.SimpleButton();
            this.btnUp = new DevExpress.XtraEditors.SimpleButton();
            this.btnDown = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gQuestions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuestions)).BeginInit();
            this.SuspendLayout();
            // 
            // gQuestions
            // 
            this.gQuestions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gQuestions.Location = new System.Drawing.Point(2, 2);
            this.gQuestions.MainView = this.gvQuestions;
            this.gQuestions.Name = "gQuestions";
            this.gQuestions.Size = new System.Drawing.Size(610, 664);
            this.gQuestions.TabIndex = 0;
            this.gQuestions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvQuestions});
            this.gQuestions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gQuestions_KeyPress);
            // 
            // gvQuestions
            // 
            this.gvQuestions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gvQuestions.GridControl = this.gQuestions;
            this.gvQuestions.Name = "gvQuestions";
            this.gvQuestions.OptionsBehavior.Editable = false;
            this.gvQuestions.OptionsBehavior.ReadOnly = true;
            this.gvQuestions.OptionsView.ShowGroupPanel = false;
            this.gvQuestions.OptionsView.ShowIndicator = false;
            this.gvQuestions.DoubleClick += new System.EventHandler(this.gvQuestions_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Question";
            this.gridColumn1.FieldName = "QuestionName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // btnAddSingleChoice
            // 
            this.btnAddSingleChoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddSingleChoice.Location = new System.Drawing.Point(2, 672);
            this.btnAddSingleChoice.Name = "btnAddSingleChoice";
            this.btnAddSingleChoice.Size = new System.Drawing.Size(168, 23);
            this.btnAddSingleChoice.TabIndex = 2;
            this.btnAddSingleChoice.Text = "Add SA";
            this.btnAddSingleChoice.Click += new System.EventHandler(this.btnAddSingleChoice_Click);
            // 
            // btnAddMultiChoice
            // 
            this.btnAddMultiChoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddMultiChoice.Location = new System.Drawing.Point(176, 672);
            this.btnAddMultiChoice.Name = "btnAddMultiChoice";
            this.btnAddMultiChoice.Size = new System.Drawing.Size(168, 23);
            this.btnAddMultiChoice.TabIndex = 3;
            this.btnAddMultiChoice.Text = "Add MA";
            this.btnAddMultiChoice.Click += new System.EventHandler(this.btnAddMultiChoice_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnUp.Location = new System.Drawing.Point(615, 39);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(34, 41);
            this.btnUp.TabIndex = 4;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDown.Location = new System.Drawing.Point(615, 86);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(34, 41);
            this.btnDown.TabIndex = 5;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDelete.Location = new System.Drawing.Point(615, 133);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(34, 33);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton1.Location = new System.Drawing.Point(350, 672);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(168, 23);
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "Add FreeText";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Variables Name";
            this.gridColumn2.FieldName = "Variables_VariablesName";
            this.gridColumn2.MaxWidth = 100;
            this.gridColumn2.MinWidth = 100;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 100;
            // 
            // frmDesignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 700);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnAddMultiChoice);
            this.Controls.Add(this.btnAddSingleChoice);
            this.Controls.Add(this.gQuestions);
            this.Name = "frmDesignForm";
            this.Text = "Design Form";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmDesignForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.gQuestions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuestions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gQuestions;
        private DevExpress.XtraGrid.Views.Grid.GridView gvQuestions;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.SimpleButton btnAddSingleChoice;
        private DevExpress.XtraEditors.SimpleButton btnAddMultiChoice;
        private DevExpress.XtraEditors.SimpleButton btnUp;
        private DevExpress.XtraEditors.SimpleButton btnDown;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}