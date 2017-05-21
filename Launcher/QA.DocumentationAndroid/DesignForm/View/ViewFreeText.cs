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
    public partial class ViewFreeText : ClientControl
    {
        public string Question_QuestionName {
            get { return questionControl1.QuestionName; }
            set { questionControl1.QuestionName = value; }
        }
        public string Answer_LimitText
        {
            get { return answerFreeText1.LimitText; }
            set { answerFreeText1.LimitText = value; }
        }

        public ViewFreeText()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            base.Process();


        }
    }
}
