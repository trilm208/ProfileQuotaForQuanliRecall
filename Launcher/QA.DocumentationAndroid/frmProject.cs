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
    public partial class frmProject : ClientForm
    {
        public frmProject()
        {
            InitializeComponent();
        }

        public string ProjectID { get; set; }

        public string ProjectNo { get { return txtProjectCode.Text.Trim(); }
            set
            {
                txtProjectCode.Text = value;
            }
        }

        public string ProjectName {
            get
            {
                return txtProjectName.Text.Trim();
            }
            set
            {
                txtProjectName.Text = value;
            }
        }
        public string ProjectDesciption
        {
            get
            {
                return txtDescription.Text.Trim();
            }
            set
            {
                txtDescription.Text = value;
            }
        }

        public string ProjectIsActived
        {
            set
            {
                chkActive.Checked = ConvertSafe.ToBoolean(value);
            }
            get
            {
                return chkActive.Checked==true ? "1" :  "0";
            }
        }

        public override void Process()
        {
            base.Process();
            // lay danh sach loai Project
            var query = DataAccess.DataQuery.Create("Docs", "ws_L_ProjectType_List");
            var ds = Services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            cbProjectType.DataSource = ds.FirstTable();
            cbProjectType.SelectedValue = ProjectTypeID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            var query = DataAccess.DataQuery.Create("Docs", "ws_L_Project_Save", new
            {
                ProjectID=ProjectID==null ? QAFunction.NewGuid() : ProjectID,
                ProjectNo,
                ProjectName,
                ProjectDesciption,
                ProjectIsActived,
                ProjectTypeID=cbProjectType.SelectedValue==null ? "" : cbProjectType.SelectedValue,
                UserID=Services.GetInformation("UserID")
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

        private void txtProjectCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void txtProjectName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        private void chkActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
        }

        public string ProjectTypeID { get; set; }
    }
}
