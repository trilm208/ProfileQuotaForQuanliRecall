using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QA;

namespace Shell
{
    public partial class FWProfile : ClientControl
    {
        public FWProfile()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            base.Process();
        }
        public string ProjectID { get; set; }

        private bool IsFieldQuota(string field, DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                if (row["FieldName"].ToString().Trim().ToUpper() == field)
                    return true;
            }

            return false;
        }
        private string FindValue(DataTable tbl, DataTable tblOptionValues, string AnswerID, string inputColumn, string inputValue, string outputColumn)
        {
            foreach (DataRow item in tbl.Rows)
            {
                if (item["AnswerID"].ToString().ToUpper() == AnswerID.Trim().ToUpper() && item[inputColumn].ToString() == inputValue)
                {
                    var code_value = item[outputColumn].ToString();
                    var index = tblOptionValues._FindIndex("FieldName", inputValue);
                    var optionValues = tblOptionValues.Rows[index].Item("OptionValues").JSONToDataTable();

                    var return_value = optionValues._FindValue("OptionFieldName", "OptionFieldValue", code_value);
                    return return_value;
                }
            }
            return "";
        }
      
        public void LoadQuota()
        {

            var query = DataAccess.DataQuery.Create("Docs", "ws_DOC_QuotaControl_Get", new
            {
                ProjectID
            });
            //2
            query += DataAccess.DataQuery.Create("Docs", "ws_DOC_Answers_CreateProfileReportQuanl", new
            {
                ProjectID
            });
            //3
            //data quota field name
            query += DataAccess.DataQuery.Create("Docs", "ws_DOC_QuotaControlValues_List", new
            {
                ProjectID
            });
            //4
            query += DataAccess.DataQuery.Create("Docs", "ws_DOC_QuotaConditionExpression_List", new
            {
                ProjectID
            });

            var ds = Services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            var tblQuotaField = ds.Tables[0];
            var tblProfile = ds.Tables[1];
            var tblQuotaValue = ds.Tables[2];
            var tblExpression = ds.Tables[3];

            LoadData(tblProfile, tblQuotaValue, tblQuotaField, tblExpression);
        }

        public void LoadData(DataTable tblProfile, DataTable tblQuotaValue, DataTable tblQuotaField, DataTable tblExpression)
        {
            foreach (DataRow row in tblQuotaField.Rows)
            {
                tblProfile.Columns.Add(row["FieldName"].ToString().ToUpper(), typeof(String));

            }

            foreach (DataRow row in tblProfile.Rows)
            {
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    string AnswerID = row["AnswerID"].ToString();
                    string columnName = row.Table.Columns[i].ColumnName.ToString().ToUpper();
                    if (IsFieldQuota(columnName, tblQuotaField) == true)
                    {
                        row[i] = FindValue(tblQuotaValue, tblQuotaField, AnswerID, "FieldName", columnName, "FieldValue");
                    }

                }
            }



            gProfile.Populate(tblProfile);

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView1.Columns)
            {

                for (int i = 0; i < col.Summary.Count; i++)
                {
                    col.Summary.RemoveAt(i);
                }

            }

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView1.Columns)
            {
                var _index = tblQuotaField._FindIndex("FieldName", col.FieldName);

                if (_index >= 0)
                {
                    col.Caption = tblQuotaField.Rows[_index].Item("Caption");
                }
                col.MaxWidth = 300;
                col.MinWidth = 120;

                if (col.FieldName == "AnswerID")
                {
                    col.VisibleIndex = -1;
                }
                if (col.FieldName.ToUpper() == "HỌ TÊN")
                {
                    col.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Tổng bài", "Count={0:n0}")});
                }

            }
            //gData.ItemsSource = tblExpression;
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            gProfile.ExportToFile();
        }
    }
}
