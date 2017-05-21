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
    public partial class RuleControl : ClientControl
    {
        public RuleControl()
        {
            InitializeComponent();
        }
        public string QuestionID { get; set; }
        public string ProjectID { get; set; }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var frm = new QA.DocumentationAndroid.Rule.frmRuleBuilder())
            {
                frm.Initialize(Services);
                frm.QuestionID = this.QuestionID;
                frm.ProjectID = this.ProjectID;
                frm.Process();
                if (frm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
    }
}
