using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace QA.DocumentationAndroid.DesignForm.Answers.Properties
{
    public partial class frmPropertiesMultiChoice : ClientForm
    {
        public frmPropertiesMultiChoice()
        {
            InitializeComponent();
        }

        public string MultiChoice_View_Answer_AnswerText { get { return txtAnswerText.Text.Trim(); }
            set {
                txtAnswerText.Text = value;
            }
        }
        public string MultiChoice_View_Answer_AnswerIndex
        {
            get { return ConvertSafe.ToString(lblAnswerIndex.Text); }
            set { lblAnswerIndex.Text = value; }
        }
        public string MultiChoice_View_Answer_AnswerCodes_VariableName {
            get { return txtAnswerCodesVariablesName.Text.Trim(); }
            set {
                txtAnswerCodesVariablesName.Text = value; }
        }
        public string MultiChoice_View_Answer_AnswerCodes_UnCheckedCode {
            get { return ConvertSafe.ToString(txtAnswerCodesUnCheckedCode.Text.Trim()); } set
            {
                txtAnswerCodesUnCheckedCode.Value = ConvertSafe.ToInt32(value);
            }
        }
        public string MultiChoice_View_Answer_AnswerCodes_CheckedCode
        {
            get { return ConvertSafe.ToString(txtAnswerCodesCheckedCode.Text.Trim()); }
            set {
                txtAnswerCodesCheckedCode.Value = ConvertSafe.ToInt32(value);
            }
        }
        public string MultiChoice_View_Answer_AnswerCodes_OtherSpecify {
            get { return chkOther.Checked.ToString(); }
            set { chkOther.Checked =ConvertSafe.ToBoolean( value); }
        }
        public string MultiChoice_View_Answer_AnswerCodes_OtherSpecify_VariableName
        {
            get { return txtOtherVariableName.Text.Trim(); }
            set { txtOtherVariableName.Text = value; }
        }
        public string MultiChoice_View_Answer_AnswerCodes_OtherSpecify_InType
        {
            get { return cbInputType.Text.Trim(); }
            set { cbInputType.Text = value; }
        }
     
        public override void Process()
        {
            base.Process();

            chkOther_CheckedChanged(null, null);
        }

        private void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            txtOtherVariableName.Enabled = cbInputType.Enabled = chkOther.Checked;
        }
    }
}
