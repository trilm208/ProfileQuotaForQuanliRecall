using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace QA.DocumentationAndroid
{
    public partial class frmDesignForm : ClientForm
    {
        public frmDesignForm()
        {
            InitializeComponent();
        }
        public string DocTypeID { get; set; }
        public string DocTypeName { get; set; }
        public string ProjectID { get; set; }
        public override void Process()
        {
            base.Process();

            var query = DataAccess.DataQuery.Create("Docs", "ws_DOC_GenericFormQuestionAndroid_List", new
            {
                ProjectID
            });
            var ds = Services.Execute(query);
            if(ds==null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            gQuestions.DataSource = ds.FirstTable();

        }

        private void gvQuestions_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = gvQuestions.GetDataRow(gvQuestions.FocusedRowHandle);
            if(row==null)
            {
                return;
            }

        }

        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            using (var frm = new frmQuestion())
            {
                frm.Initialize(Services);
                //frm.FreeText_View_Answer_LimitText = "9999";
                //frm.View_Question_QuestionName = "Enter your question!";
                //frm.FreeText_Variables_VariablesName = "Q_" +(gvQuestions.DataRowCount+1).ToString() ;
                //frm.ProjectID = this.ProjectID;
                //frm.QuestionID = QAFunction.NewGuid();
                //frm.QuestionTypeID = "84319E50-5A59-4DFE-9BBD-2793F0CCC478";
                frm.ProjectID = this.ProjectID;
                frm.Process();
                if(frm.ShowDialog()==DialogResult.OK)
                {
                    Process();
                    gvQuestions.SelectARowInGridView("QuestionID", frm.QuestionID);
                }
                //gvQuestions.SelectARowInGridView("QuestionID",frm.QuestionID);
            }
        }

        private void gvQuestions_DoubleClick(object sender, EventArgs e)
        {
            var row = gvQuestions.GetDataRow(gvQuestions.FocusedRowHandle);
            if(row==null)
            {
                return;
            }

            using (var frm = new frmQuestion())
            {
                frm.Initialize(Services);
                frm.Process(row);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Process();
                    gvQuestions.SelectARowInGridView("QuestionID", row.Item("QuestionID"));
                }
            }

            return;
            if (row.Item("QuestionTypeID").ToUpper() == "84319E50-5A59-4DFE-9BBD-2793F0CCC478")
            {
                using (var frm = new DesignForm.frmDesignFreeTextControl())
                {
                    frm.Initialize(Services);
                   
                    frm.Process(row);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        Process();
                    }
                }
            }

            if (row.Item("QuestionTypeID").ToUpper() == "24D310A9-E80C-4684-923E-A86F66B66279")
            {
                using (var frm = new DesignForm.frmDesignSingleChoiceControl())
                {
                    frm.Initialize(Services);

                    frm.Process(row);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        Process();
                    }
                }
            }

            if (row.Item("QuestionTypeID").ToUpper() == "79A06333-E2F1-4A3C-B576-F7235BAD1577")
            {
                using (var frm = new DesignForm.frmDesignMultiChoiceControl())
                {
                    frm.Initialize(Services);

                    frm.Process(row);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        Process();
                        gvQuestions.SelectARowInGridView("QuestionID", frm.QuestionID);
                    }
                }
            }
            

        }

        private void btnAddSingleChoice_Click(object sender, EventArgs e)
        {
            using (var frm = new DesignForm.frmDesignSingleChoiceControl())
            {
                frm.Initialize(Services);
             
                frm.QuestionName = "Enter your question!";
                frm.Variables_VariablesName = "Q_" + (gvQuestions.DataRowCount + 1).ToString();
                frm.SingleChoice_Answer_AnswerList = "";
                frm.QuestionIndex = gvQuestions.DataRowCount + 1;
                frm.ProjectID = this.ProjectID;
                frm.QuestionID = QAFunction.NewGuid();
                frm.QuestionTypeID = "1";
                frm.Process();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Process();
                    gvQuestions.SelectARowInGridView("QuestionID", frm.QuestionID);
                }
            }
        }

        private void btnAddMultiChoice_Click(object sender, EventArgs e)
        {
            using (var frm = new DesignForm.frmDesignMultiChoiceControl())
            {
                frm.Initialize(Services);

                frm.View_Question_QuestionName = "Enter your question!";
                frm.MultiChoice_Variables_VariablesName = "Q_" + (gvQuestions.DataRowCount + 1).ToString();
                frm.MultiChoice_Answer_AnswerList = "";
                frm.QuestionIndex = gvQuestions.DataRowCount + 1;
                frm.ProjectID = this.ProjectID;
                frm.QuestionID = QAFunction.NewGuid();
                frm.QuestionTypeID = "79A06333-E2F1-4A3C-B576-F7235BAD1577";
                frm.Process();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Process();
                    gvQuestions.SelectARowInGridView("QuestionID", frm.QuestionID);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (UI.ConfirmDelete() == false)
                return;
            var row = gvQuestions.GetDataRow(gvQuestions.FocusedRowHandle);
            if (row == null)
            {
                return;
            }
            var query = DataAccess.DataQuery.Create("Docs", "ws_DOC_GenericFormQuestionAndroid_Delete", new
            {
                QuestionID=row.Item("QuestionID")
            });
            query += DataAccess.DataQuery.Create("Docs", "ws_DOC_GenericFormQuestionAndroid_List", new
            {
                ProjectID
            });
            var ds = Services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            gQuestions.DataSource = ds.LastTable();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            var row = gvQuestions.GetDataRow(gvQuestions.FocusedRowHandle);
            if (row == null)
            {
                return;
            }

            var query = DataAccess.DataQuery.Create("Docs", "ws_DOC_GenericFormQuestion_SwapUp", new
            {
                QuestionID = row.Item("QuestionID"),
                ProjectID = row.Item("ProjectID"),
                currentIndex = row.Item("OrderIndex")
            });
            query += DataAccess.DataQuery.Create("Docs", "ws_DOC_GenericFormQuestion_List", new
            {
                ProjectID
            });
            var ds = Services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            gQuestions.DataSource = ds.LastTable();

            gvQuestions.SelectARowInGridView("QuestionID", row.Item("QuestionID"));
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1))
            {
                btnAddQuestion_Click(null, null);
                return true;
            }

          

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            var row = gvQuestions.GetDataRow(gvQuestions.FocusedRowHandle);
            if (row == null)
            {
                return;
            }
            
            var query = DataAccess.DataQuery.Create("Docs", "ws_DOC_GenericFormQuestion_SwapDown", new
            {
                QuestionID = row.Item("QuestionID"),
                ProjectID = row.Item("ProjectID"),
                currentIndex=row.Item("OrderIndex")
            });
            query += DataAccess.DataQuery.Create("Docs", "ws_DOC_GenericFormQuestion_List", new
            {
                ProjectID
            });
            var ds = Services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            gQuestions.DataSource = ds.LastTable();
            gvQuestions.SelectARowInGridView("QuestionID", row.Item("QuestionID"));
        }

        private void frmDesignForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void gQuestions_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                gvQuestions_DoubleClick(null, null);
            }
        }

       
    }
}
