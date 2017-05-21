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
    public partial class frmQuestion : ClientForm
    {
        public frmQuestion()
        {
            InitializeComponent();
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1))
            {
                btnSave_Click(null, null);
                return true;
            }

            if (keyData == (Keys.F2))
            {
                checkEdit1.Checked = !checkEdit1.Checked;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public override void Process()
        {
            base.Process();
        }
        public string QuestionCode
        {
            set
            {
                txtQuestionCode.Text = value;
            }
        }

        public string OpenEndedRecode
        {
            set
            {
                if (value.IsNotEmpty() && value != "null")
                {

                    gOpenEndedCode.DataSource = value.JSONToDataTable();
                }
            }
        }
        public string ValueRangeNumber
        {
            set
            {
                if (value.IsNotEmpty() && value != "null")
                {
                    gAnswers.DataSource = value.JSONToDataTable();
                }
            }
        }
        public string ValueType
        {
            set { radioGroup1.EditValue = ConvertSafe.ToInt32(value); }
        }
        public string IsRequired { set { checkEdit1.Checked = ConvertSafe.ToBoolean(value); } }

       
        public string QuestionName {
            set
            {
                txtQuestionName.Text = value;
            }
            get
            {
                return txtQuestionName.Text.Trim().ToUpper();
            }
        }


      

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gAnswers.DataSource == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Value", typeof(string));
                    dt.Columns.Add("DQA", typeof(string));
                    dt.Columns.Add("AutoAssign", typeof(string));
                    gAnswers.DataSource = dt;

                }
                gvAnswers.AddNewRow();

                int rowHandle = gvAnswers.GetRowHandle(gvAnswers.DataRowCount);
                if (gvAnswers.IsNewItemRow(rowHandle))
                {
                    gvAnswers.SetRowCellValue(rowHandle, "Value", txtValue.Text.Trim());

                    txtValue.Text = string.Empty;
                }
                gvAnswers.UpdateCurrentRow();
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = gvAnswers.GetDataRow(gvAnswers.FocusedRowHandle);
            if (row == null)
            {
                return;
            }
            gvAnswers.DeleteRow(gvAnswers.FocusedRowHandle);
            gvAnswers.UpdateCurrentRow();


        }

        public string SkipLogic
        {
            set
            {
                if (value.IsEmpty() || value == "null")
                    return;
                try
                {
                    gSkipLogic.DataSource = value.JSONToDataTable();
                }
                catch
                {
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            gvAnswers.UpdateCurrentRow();
            gvOpenEndedCode.UpdateCurrentRow();
            gvSkipLogic.UpdateCurrentRow();
            QuestionID=QuestionID == null ? QAFunction.NewGuid() : QuestionID;
            var IsRevivewCode = (gvOpenEndedCode.DataRowCount == 0) ? 0 : 1;
            var query = DataAccess.DataQuery.Create("Docs", "ws_DOC_GenericFormQuestion_Save", new
            {
                QuestionID = QuestionID ,
                QuestionCode = txtQuestionCode.Text.Trim().ToUpper(),
                QuestionName,
                ValueType = radioGroup1.EditValue,
                ValueRangeNumber = (gAnswers.DataSource as DataTable).DataTableToJSON(),
                SkipLogic=(gSkipLogic.DataSource as DataTable).DataTableToJSON(),
                OpenEndedRecode = (gOpenEndedCode.DataSource as DataTable).GetJSONString(),
                ProjectID,
                IsRequired = checkEdit1.Checked,
                IsMainRotation=0,
                IsSubRotation=0,
                IsRevivewCode = IsRevivewCode
            });
            var ds = Services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            UI.InformationSaved();
            DialogResult = DialogResult.OK;


        }

        public string QuestionID { get; set; }

        public string ProjectID { get; set; }

        private void btnSkipLogic_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSkipLogic())
            {

                frm.Initialize(Services);
                frm.Index = gvSkipLogic.DataRowCount + 1;
                frm.ProjectID = this.ProjectID;
                frm.Process();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (gSkipLogic.DataSource == null)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Index", typeof(string));
                        dt.Columns.Add("SkipType", typeof(string));
                        dt.Columns.Add("SkipTo", typeof(string));
                        dt.Columns.Add("EvaluatesExpression", typeof(string));
                        dt.Columns.Add("MessageError", typeof(string));
                        gSkipLogic.DataSource = dt;

                    }

                    gvSkipLogic.AddNewRow();

                    int rowHandle = gvSkipLogic.GetRowHandle(gvSkipLogic.DataRowCount);

                    if (gvSkipLogic.IsNewItemRow(rowHandle))
                    {
                        gvSkipLogic.SetRowCellValue(rowHandle, "Index", frm.Index);
                        gvSkipLogic.SetRowCellValue(rowHandle, "SkipType", frm.SkipType);
                        gvSkipLogic.SetRowCellValue(rowHandle, "SkipTo", frm.SkipTo);
                        gvSkipLogic.SetRowCellValue(rowHandle, "EvaluatesExpression", frm.EvaluatesExpression);
                       
                    }
                    gvSkipLogic.UpdateCurrentRow();

                }
            }
        }

        private void gvSkipLogic_DoubleClick(object sender, EventArgs e)
        {
            var row = gvSkipLogic.GetDataRow(gvSkipLogic.FocusedRowHandle);
            if (row == null) return;
            using (var frm = new frmSkipLogic())
            {

                frm.Initialize(Services);
                frm.ProjectID = this.ProjectID;
                frm.Process(row);
                frm.SkipTo = row.Item("SkipTo");
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    gvSkipLogic.SetRowCellValue(gvSkipLogic.FocusedRowHandle, "SkipType", frm.SkipType);
                    gvSkipLogic.SetRowCellValue(gvSkipLogic.FocusedRowHandle, "SkipTo", frm.SkipTo);
                    gvSkipLogic.SetRowCellValue(gvSkipLogic.FocusedRowHandle, "EvaluatesExpression", frm.EvaluatesExpression);
                }
                gvSkipLogic.UpdateCurrentRow();

            }
        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            gvSkipLogic.DeleteSelectedRows();
            gvSkipLogic.UpdateCurrentRow();
        }

        private void txtOpenEndedRecode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtOpenEndedRecode.Text.Trim().IsEmpty())
                    return;
                if (gOpenEndedCode.DataSource == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Code", typeof(string));
                    dt.Columns.Add("Value", typeof(string));
                   
                    gOpenEndedCode.DataSource = dt;
                    

                }
                gvOpenEndedCode.AddNewRow();

                int rowHandle = gvOpenEndedCode.GetRowHandle(gvOpenEndedCode.DataRowCount);
                if (gvOpenEndedCode.IsNewItemRow(rowHandle))
                {
                    gvOpenEndedCode.SetRowCellValue(rowHandle, "Value", txtOpenEndedRecode.Text.Trim());
                    gvOpenEndedCode.SetRowCellValue(rowHandle, "Code", txtCode.Text.Trim());

                   txtCode.Text= txtOpenEndedRecode.Text = string.Empty;
                   txtCode.Focus();
                }
                gvOpenEndedCode.UpdateCurrentRow();
            }
        }

        private void repositoryItemButtonEdit4_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            gvOpenEndedCode.DeleteRow(gvOpenEndedCode.FocusedRowHandle);
            gvOpenEndedCode.UpdateCurrentRow();
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void gvAnswers_DoubleClick(object sender, EventArgs e)
        {
            using (var frm =new frmAutoAssignValue())
            {
                frm.Initialize(Services);
                frm.Value = gvAnswers.GetDataRow(gvAnswers.FocusedRowHandle).Item("AutoAssign");
                frm.ProjectID = this.ProjectID;
                frm.Process();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    gvAnswers.SetRowCellValue(gvAnswers.FocusedRowHandle, "AutoAssign", frm.Value);
                    gvAnswers.UpdateCurrentRow();
                }
            }
        }

        private void txtQuestionCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==13)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void txtQuestionName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void radioGroup1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
            }
        }
    }
}