using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using DataAccess;

namespace QA.DocumentationAndroid
{
    public partial class frmAssignUser : ClientForm
    {
        public frmAssignUser()
        {
            InitializeComponent();
        }
        public override void Process()
        {
            base.Process();
            var query =DataQuery.Create("Docs", "ws_L_ProjectUser_Get", new
            {
                ProjectID
            });
            var ds = Services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            gridControl1.Populate(ds.FirstTable());
        }

        public string ProjectID { get; set; }

        public string ProjectNo { set { this.Text = "Assign user for project : " + value; } }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var query = DataAccess.DataQuery.Create("Docs", "ws_L_ProjectUser_Save", new
            {
                ProjectID,
                UserIDs=(gridControl1.DataSource as DataTable).GetSplit("ID",",","checked='True'")
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
    }
}
