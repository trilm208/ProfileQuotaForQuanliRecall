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
    public partial class frmAutoAssignValue : ClientForm
    {
        public frmAutoAssignValue()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            base.Process();
            var query = DataQuery.Create("Docs", "ws_DOC_GenericFormQuestion_ListForCreateRule", new
            {

                ProjectID
            });
            var ds = Services.Execute(query);
            if (ds == null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            cbQuestionCode.DataSource = ds.FirstTable();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            DialogResult = DialogResult.OK;
        }

        public string Value {
            set
            {
                try
                {
                    cbQuestionCode.SelectedValue = value.Split('=')[0].Split('[')[1].Split(']')[0];
                    textEdit1.Text = value.Split('=')[1].Split('{')[1].Split('}')[0];
                }
                catch
                {

                }
            }
            get
            {
                return "["+cbQuestionCode.SelectedValue.ToString()+"]" + "={" + textEdit1.Text.Trim() + "}";
            }
        }

        public object ProjectID { get; set; }
    }
}
