using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using DataAccess;

namespace QA.DocumentationAndroid.Rule
{
    public partial class frmRuleBuilder : ClientForm
    {
        public frmRuleBuilder()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            base.Process();
            var query = DataQuery.Create("Docs", "ws_DOC_GenericFormQuestion_ListForCreateRule", new
            {
                QuestionID,
                ProjectID
            });
            query += DataQuery.Create("Docs", "ws_L_RuleType_List");
            var ds = Services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            cbRule.DataSource = ds.LastTable();
            //cbRule.SelectedIndex = -1;
            cbQuestion.DataSource = ds.FirstTable();
            cbQuestion.SelectedIndex = -1;
            //cbQuestionCode.DataSource = null;

        }

        public string ProjectID { get; set; }

        public string QuestionID { get; set; }

        private void cbQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbQuestion.SelectedValue == null)
            {
                cbQuestionCode.SelectedIndex = -1;
                return;
            }

            DataTable data = (cbQuestion.DataSource as DataTable);
            var row = data.Rows[cbQuestion.SelectedIndex];
            if (row == null)
            {
                cbRule.SelectedIndex = -1;
                return;
            }

            var dt = new DataTable();
            dt.Columns.Add("QuestionID", typeof(string));
            dt.Columns.Add("FieldName", typeof(string));
            dt.Columns.Add("FieldValue", typeof(string));
            if (row.Item("QuestionTypeName") == "Free Text")
            {
                var new_row = dt.NewRow();
                new_row["QuestionID"] = row.Item("QuestionID");
                new_row["FieldName"] = row.Item("FreeText_Variables_VariablesName");
                new_row["FieldValue"] = "";

                dt.Rows.Add(new_row);
            }
            if (row.Item("QuestionTypeName") == "Single Choice")
            {

                var answer_list = row.Item("SingleChoice_Answer_AnswerList").JSONToDataTable();
                foreach (DataRow r in answer_list.Rows)
                {
                    var new_row = dt.NewRow();
                    new_row["QuestionID"] = row.Item("QuestionID");
                    new_row["FieldName"] = r.Item("SingleChoice_View_Answer_AnswerCodes_VariableName");
                    new_row["FieldValue"] = "";
                    dt.Rows.Add(new_row);

                    if (ConvertSafe.ToBoolean(r.Item("SingleChoice_View_Answer_AnswerCodes_OtherSpecify")) == true)
                    {
                        var c_new_row = dt.NewRow();
                        c_new_row["QuestionID"] = row.Item("QuestionID");
                        c_new_row["FieldName"] = r.Item("SingleChoice_View_Answer_AnswerCodes_OtherSpecify_VariableName");
                        c_new_row["FieldValue"] = "";
                        dt.Rows.Add(c_new_row);
                    }

                }
            }

            if (row.Item("QuestionTypeName") == "Multi Choice")
            {
                var answer_list = row.Item("MultiChoice_Answer_AnswerList").JSONToDataTable();
                foreach (DataRow r in answer_list.Rows)
                {
                    var new_row = dt.NewRow();
                    new_row["QuestionID"] = row.Item("QuestionID");
                    new_row["FieldName"] = r.Item("MultiChoice_View_Answer_AnswerCodes_VariableName");
                    new_row["FieldValue"] = "";
                    dt.Rows.Add(new_row);

                    if (ConvertSafe.ToBoolean(r.Item("MultiChoice_View_Answer_AnswerCodes_OtherSpecify")) == true)
                    {
                        var c_new_row = dt.NewRow();
                        c_new_row["QuestionID"] = row.Item("QuestionID");
                        c_new_row["FieldName"] = r.Item("MultiChoice_View_Answer_AnswerCodes_OtherSpecify_VariableName");
                        c_new_row["FieldValue"] = "";
                        dt.Rows.Add(c_new_row);
                    }


                }
            }
            cbQuestionCode.DataSource = dt;
        }

        private void cbQuestionCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbQuestion.SelectedValue == null || cbRule.SelectedValue == null)
            {
                txtExpressionValidate.Text = string.Empty;
                return;
            }
            txtExpressionValidate.Text = cbRule.SelectedValue.ToString().Replace("FieldName", cbQuestionCode.SelectedValue.ToString()).Replace("[Value]", txtValue.Text.Trim());
        }

        private void cbRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbQuestion.SelectedValue == null || cbRule.SelectedValue == null)
            {
                txtExpressionValidate.Text = string.Empty;
                return;
            }

            txtExpressionValidate.Text = cbRule.SelectedValue.ToString().Replace("FieldName", cbQuestionCode.SelectedValue.ToString()).Replace("[Value]", txtValue.Text.Trim());
           
        }

        private void txtValue_EditValueChanged(object sender, EventArgs e)
        {
            if (cbQuestion.SelectedValue == null || cbRule.SelectedValue == null)
            {
                txtExpressionValidate.Text = string.Empty;
                return;
            }
            txtExpressionValidate.Text = cbRule.SelectedValue.ToString().Replace("FieldName", cbQuestionCode.SelectedValue.ToString()).Replace("[Value]", txtValue.Text.Trim());
        }
    }
}