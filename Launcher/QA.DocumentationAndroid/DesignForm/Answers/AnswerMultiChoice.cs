using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace QA.DocumentationAndroid.DesignForm.Answers
{
    public partial class AnswerMultiChoice : ClientControl
    {
        public int QuestionIndex { get; set; }
        public AnswerMultiChoice()
        {
            InitializeComponent();
        }

        public string AnswerList
        {
            get
            {

                return (gAnswers.DataSource as DataTable).GetJSONString();
            }
            set
            {
                if (value.IsNotEmpty() && value != "null")
                  gAnswers.DataSource = value.JSONToDataTable();
                else
                {

                }
            }
        }

        public override void Process()
        {
            base.Process();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(gAnswers.DataSource==null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MultiChoice_View_Answer_AnswerText",typeof(string));
                dt.Columns.Add("MultiChoice_View_Answer_AnswerIndex", typeof(int));
                dt.Columns.Add("MultiChoice_View_Answer_AnswerCodes_VariableName", typeof(string));
                dt.Columns.Add("MultiChoice_View_Answer_AnswerCodes_UnCheckedCode", typeof(int));
                dt.Columns.Add("MultiChoice_View_Answer_AnswerCodes_CheckedCode", typeof(int));
                dt.Columns.Add("MultiChoice_View_Answer_AnswerCodes_OtherSpecify", typeof(Boolean));
                dt.Columns.Add("MultiChoice_View_Answer_AnswerCodes_OtherSpecify_VariableName", typeof(string));              
                dt.Columns.Add("MultiChoice_View_Answer_AnswerCodes_OtherSpecify_InType", typeof(string));
               
                gAnswers.DataSource = dt;
                
            }
             gvAnswers.AddNewRow();

            int rowHandle = gvAnswers.GetRowHandle(gvAnswers.DataRowCount);
            if(gvAnswers.IsNewItemRow(rowHandle))
            {
                gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerText", txtAnswerText.Text.Trim());
                gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerIndex", gvAnswers.DataRowCount + 1);
                gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_VariableName", "A_"+QuestionIndex.ToString()+"_"+(gvAnswers.DataRowCount+1).ToString());
                gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_UnCheckedCode", 0);
                gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_CheckedCode", 1);
                gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_OtherSpecify", false);
                gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_OtherSpecify_VariableName", "S_" + QuestionIndex.ToString() + "_" + (gvAnswers.DataRowCount + 1).ToString());
                gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_OtherSpecify_InType", "Normal TextBox");
                txtAnswerText.Text = String.Empty;
            }
            gvAnswers.UpdateCurrentRow();
      

        }

        private void txtAnswerText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                btnAdd_Click(null, null);
                e.Handled = true;
            }
        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            var row = gvAnswers.GetDataRow(gvAnswers.FocusedRowHandle);
            if(row==null)
            {
                return;
            }
            using (var frm = new Properties.frmPropertiesMultiChoice())
            {
                frm.Initialize(Services);
                frm.Process(row);
                if(frm.ShowDialog()==DialogResult.OK)
                {
                    //update lai table
                    var rowHandle = ConvertSafe.ToInt32(frm.MultiChoice_View_Answer_AnswerIndex)-1;
                    gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerText", frm.MultiChoice_View_Answer_AnswerText);
                    gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_VariableName", frm.MultiChoice_View_Answer_AnswerCodes_VariableName);
                    gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_UnCheckedCode", ConvertSafe.ToInt32(frm.MultiChoice_View_Answer_AnswerCodes_UnCheckedCode));
                    gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_CheckedCode", ConvertSafe.ToInt32(frm.MultiChoice_View_Answer_AnswerCodes_CheckedCode));
                    gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_OtherSpecify", ConvertSafe.ToBoolean(frm.MultiChoice_View_Answer_AnswerCodes_OtherSpecify));
                    gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_OtherSpecify_VariableName",frm.MultiChoice_View_Answer_AnswerCodes_OtherSpecify_VariableName);
                    gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerCodes_OtherSpecify_InType", frm.MultiChoice_View_Answer_AnswerCodes_OtherSpecify_InType);
                    gvAnswers.UpdateCurrentRow();
                    
                }
            }

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            //swap
            int rowHandle = gvAnswers.FocusedRowHandle;
            int rowSwapHandle = rowHandle - 1;
            if (rowSwapHandle < 0)
                return;

            string rowHandle_value = gvAnswers.GetDataRow(rowHandle).Item("MultiChoice_View_Answer_AnswerText");
            string rowSwapHandle_value = gvAnswers.GetDataRow(rowSwapHandle).Item("MultiChoice_View_Answer_AnswerText");

            gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerText", rowSwapHandle_value);
            gvAnswers.SetRowCellValue(rowSwapHandle, "MultiChoice_View_Answer_AnswerText", rowHandle_value);
            gvAnswers.UpdateCurrentRow();
            gvAnswers.FocusedRowHandle = rowSwapHandle;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int rowHandle = gvAnswers.FocusedRowHandle;
            int rowSwapHandle = rowHandle + 1;
            if (rowSwapHandle == gvAnswers.DataRowCount)
                return;

            string rowHandle_value = gvAnswers.GetDataRow(rowHandle).Item("MultiChoice_View_Answer_AnswerText");
            string rowSwapHandle_value = gvAnswers.GetDataRow(rowSwapHandle).Item("MultiChoice_View_Answer_AnswerText");

            gvAnswers.SetRowCellValue(rowHandle, "MultiChoice_View_Answer_AnswerText", rowSwapHandle_value);
            gvAnswers.SetRowCellValue(rowSwapHandle, "MultiChoice_View_Answer_AnswerText", rowHandle_value);
            gvAnswers.UpdateCurrentRow();
            gvAnswers.FocusedRowHandle = rowSwapHandle;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = gvAnswers.GetDataRow(gvAnswers.FocusedRowHandle);
            if (row == null)
            {
                return;
            }
            gvAnswers.DeleteRow(gvAnswers.FocusedRowHandle);
            gvAnswers.UpdateCurrentRow();

            //update index
            for (int i = 0; i < gvAnswers.DataRowCount; i++)
            {
                gvAnswers.SetRowCellValue(i, "MultiChoice_View_Answer_AnswerIndex", (i + 1).ToString());
            }

        }

        private void gvAnswers_DoubleClick(object sender, EventArgs e)
        {
            btnProperties_Click(null, null);
        }
    }
}
