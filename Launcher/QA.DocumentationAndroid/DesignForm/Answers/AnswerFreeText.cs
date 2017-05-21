using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace QA.DocumentationAndroid.DesignForm.Answers
{
    public partial class AnswerFreeText : ClientControl
    {
        public string LimitText {
            get { return txtLimitText.Text.Trim(); }
            set { txtLimitText.Text = value; }
        }
        public AnswerFreeText()
        {
            InitializeComponent();
        }
        public override void Process()
        {
            base.Process();
        }
    }
}
