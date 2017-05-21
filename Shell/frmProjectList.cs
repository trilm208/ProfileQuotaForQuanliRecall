using QA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shell
{
    public partial class frmProjectList : ClientForm
    {
        public frmProjectList()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            base.Process();

        }

        public string ProjectID { get; set; }

        public string SelectedProjectNo { get; set; }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            var row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row == null)
                return;
            SelectedProjectNo = row.Item("ProjectNo");
            SelectedProjectID = row.Item("ProjectID");
            DialogResult = DialogResult.OK;
        }

        public string SelectedProjectID { get; set; }

        public DataTable InitializeDataSource
        {
            set
            {
                gridControl1.DataSource = value;
            }
        }
    }
}
