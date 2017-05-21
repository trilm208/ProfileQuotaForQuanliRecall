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
    public partial class SummaryQC : ClientControl
    {
        public SummaryQC()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            base.Process();

            LoadQuota();
        }

        private bool IsFieldQuota(string field, DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                if (row["FieldName"].ToString().Trim().ToUpper() == field)
                    return true;
            }

            return false;
        }
        private string FindValue(DataTable tbl, string AnswerID, string inputColumn, string inputValue, string outputColumn)
        {
            foreach (DataRow item in tbl.Rows)
            {
                if (item["AnswerID"].ToString().ToUpper() == AnswerID.Trim().ToUpper() && item[inputColumn].ToString() == inputValue)
                {
                    return item[outputColumn].ToString();
                }
            }
            return "";
        }
        public string ProjectID { get; set; }
        public void LoadQuota()
        {

            var query = DataAccess.DataQuery.Create("Docs", "ws_DOC_QuotaControl_Get", new
            {
                ProjectID
            });
            //2
            query += DataAccess.DataQuery.Create("Docs", "ws_DOC_Answers_ListCalculateQuotaQC", new
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
            query += DataAccess.DataQuery.Create("Docs", "ws_DOC_Answers_CreateSummaryRecuitReport", new
            {
                ProjectID
            });

            query += DataAccess.DataQuery.Create("Docs", "ws_DOC_Answers_CreateSummaryQCReport", new
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

            LoadQuota(tblProfile, tblQuotaValue, tblQuotaField, tblExpression);

            gridControl1.Populate(ds.LastTable(1));
            gridControl2.Populate(ds.LastTable());

        }

        public void LoadQuota(DataTable tblProfile, DataTable tblQuotaValue, DataTable tblQuotaField, DataTable tblExpression)
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
                        row[i] = FindValue(tblQuotaValue, AnswerID, "FieldName", columnName, "FieldValue");
                    }

                }
            }

            foreach (DataRow row in tblExpression.Rows)
            {
                //var items = DependencyService.Get<IDataTableExtension>().SelectQuery(tblProfile, row["ConditionExpression"].ToString());
                try
                {
                    var items = tblProfile.Select(row["ConditionExpression"].ToString());
                    int currentCount = items.Length;
                    row["CurrentCount"] = currentCount;

                    int minValue = (int)row["MinValue"];
                    int maxValue = (int)row["MaxValue"];

                    if (currentCount <= minValue && minValue != maxValue)
                        row["NeedQty"] = String.Format("Còn {0}-{1}", minValue - currentCount, maxValue - currentCount);
                    ;
                    if (currentCount <= minValue && minValue == maxValue)
                        row["NeedQty"] = String.Format("Còn {0}", minValue - currentCount);
                    if (currentCount > minValue && minValue == maxValue)
                        row["NeedQty"] = String.Format("Dư {0}", currentCount - minValue);
                    if (currentCount > minValue && minValue != maxValue && currentCount <= maxValue)
                        row["NeedQty"] = String.Format("Đủ +{0}", maxValue - currentCount);

                    if (currentCount > minValue && minValue != maxValue && currentCount > maxValue)
                        row["NeedQty"] = String.Format("Dư +{0}", currentCount - maxValue);


                }
                catch (Exception ex)
                {
                    UI.ShowError(ex.Message);
                    return;
                }
            }
            tblExpression.AcceptChanges();
            gQuota.Populate(tblExpression);
            //gData.ItemsSource = tblExpression;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToFile();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl2.ExportToFile();
        }

        //private void btnFind_Click(object sender, EventArgs e)
        //{
        //    using (var frm = new frmDataRemain())
        //    {
        //        frm.Initialize(Services);
        //        frm.ProjectID = ProjectID;
        //        frm.Process();
        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {

        //        }
        //    }
        //}


    }
}
