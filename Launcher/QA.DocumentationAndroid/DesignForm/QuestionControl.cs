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
    public partial class QuestionControl : ClientControl
    {
        public string QuestionName
        {
            get { return txtQuestion.Text.Trim(); }
            set { txtQuestion.Text = value; }
        }
        public QuestionControl()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            base.Process();
        }
    }
}
