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
    public partial class frmRangeNumberTemplate : ClientForm
    {
        public frmRangeNumberTemplate()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            base.Process();
            if (Services.GetInformation("RangeTemplate") == null)
            {
                var query = DataAccess.DataQuery.Create("Docs", "ws_L_RangeNumberTemplate_List");
              
                var ds = Services.Execute(query);
                if (ds == null)
                {
                    UI.ShowError(Services.LastError);
                    return;
                }
                Services.SetInformation("RangeTemplate", ds.FirstTable());
            }
            gridControl1.DataSource = Services.GetInformation("RangeTemplate") as DataTable;
        }

        private void gvProjects_DoubleClick(object sender, EventArgs e)
        {
            var row = gvProjects.GetDataRow(gvProjects.FocusedRowHandle);
            if (row == null)
                return;
            RangeValue = row.Item("Value");
            DialogResult = DialogResult.OK;
        }

        public string RangeValue { get; set; }
    }
}
