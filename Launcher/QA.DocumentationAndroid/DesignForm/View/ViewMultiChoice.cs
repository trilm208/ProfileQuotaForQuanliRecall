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
    public partial class ViewMultiChoice : ClientControl
    {
        public string Question_QuestionName {
            get { return questionControl1.QuestionName; }
            set { questionControl1.QuestionName = value; }
        }
        public int QuestionIndex { get; set; }
        public string Answer_AnswerList {
            get { return answer.AnswerList; }

            set { answer.AnswerList = value; }

        }
        public ViewMultiChoice()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            base.Process();

            answer.Initialize(Services);
            answer.QuestionIndex = this.QuestionIndex;
            answer.Process();
        }
    }
}
