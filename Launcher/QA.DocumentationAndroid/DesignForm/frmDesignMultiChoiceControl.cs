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
    public partial class frmDesignMultiChoiceControl : ClientForm
    {
        public string QuestionID { get; set; }
        public string ProjectID { get;  set; }

        public int QuestionIndex { get; set; }

        public frmDesignMultiChoiceControl()
        {
            InitializeComponent();
        }
        public override void Process()
        {
            base.Process();
            view.QuestionIndex = this.QuestionIndex;

            view.Initialize(Services);
            view.Process();
        }
        public DataRow SelectedRow { get; internal set; }
        public string View_Question_QuestionName { get { return view.Question_QuestionName; } set { view.Question_QuestionName = value; } }
    
        public string MultiChoice_Variables_VariablesName
        {
            get { return variables.VariablesName; }
            set { variables.VariablesName = value; }
        }
     
        public string MultiChoice_Answer_AnswerList
        {
           
            
            get { return view.Answer_AnswerList; }
            set { view.Answer_AnswerList = value; }
        }

        public string QuestionTypeID { get;  set; }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var query = DataAccess.DataQuery.Create("Docs", "ws_DOC_GenericFormQuestion_Save", new
            {
                QuestionID,
                QuestionTypeID,
                ProjectID,
                View_Question_QuestionName = View_Question_QuestionName ,                       
                MultiChoice_Answer_AnswerList=view.Answer_AnswerList,
                MultiChoice_Variables_VariablesName =variables.VariablesName

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
