using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace QA.DocumentationAndroid.DesignForm
{
    public partial class frmDesignFreeTextControl : ClientForm
    {
        public string QuestionID { get; set; }
        public string ProjectID { get;  set; }
        public frmDesignFreeTextControl()
        {
            InitializeComponent();
        }
        public override void Process()
        {
            base.Process();

            rule.Initialize(Services);
            rule.QuestionID = this.QuestionID;
            rule.ProjectID = this.ProjectID;
            rule.Process();

            view.Initialize(Services);
            view.Process();


        }
        public DataRow SelectedRow { get; internal set; }
        public string View_Question_QuestionName { get { return view.Question_QuestionName; } set { view.Question_QuestionName = value; } }
        public string FreeText_View_Answer_LimitText { get { return view.Answer_LimitText; }  set { view.Answer_LimitText = value; } }
        public string FreeText_Variables_VariablesName
        {
            get { return variables.VariablesName; }
            set { variables.VariablesName = value; }
        }

        public string QuestionTypeID { get;  set; }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var query = DataAccess.DataQuery.Create("Docs", "ws_DOC_GenericFormQuestion_Save", new
            {
                QuestionID,
                QuestionTypeID,
                ProjectID,
                View_Question_QuestionName = View_Question_QuestionName,
                FreeText_View_Answer_LimitText = FreeText_View_Answer_LimitText,
                FreeText_Variables_VariablesName = FreeText_Variables_VariablesName,
            });
            var ds = Services.Execute(query);
            if(ds==null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
