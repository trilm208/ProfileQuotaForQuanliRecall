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
    public partial class frmSkipLogic : ClientForm
    {
        public frmSkipLogic()
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
            radioGroup1_EditValueChanged(null, null);
        }
        public string SkipType
        {
            get { return radioGroup1.EditValue.ToString(); }
            set { radioGroup1.EditValue = value; }
        }
    
        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public int Index { get; set; }

        private void radioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            if (radioGroup1.EditValue == null)
                return;
            if (radioGroup1.EditValue.ToString() == "Always Skip")
            {
                textEdit1.Enabled = false;
            }
            else
            {
                textEdit1.Enabled = true;
            }
          
        }

        public string EvaluatesExpression
        {
            get
            {
                if (radioGroup1.EditValue.ToString() == "1")
                {
                    return "";
                }
                else
                {
                    return textEdit1.Text.Trim();
                }
            }
            set
            {
                textEdit1.Text = value;
            }
        }

        public string SkipTo { get {return cbQuestionCode.SelectedValue.ToString(); } set { cbQuestionCode.SelectedValue = value; } }

        public string ProjectID { get; set; }
    }
}
