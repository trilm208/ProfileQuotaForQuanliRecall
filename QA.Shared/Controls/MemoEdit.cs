using System;
using System.Windows.Forms;

namespace QA.Shared.Controls
{
    public partial class MemoEdit : ClientControl
    {
        public string Category { get; set; }

        public override string Text
        {
            get { return memoEdit1.Text; }
            set { memoEdit1.Text = value; }
        }

        public MemoEdit()
        {
            InitializeComponent();
        }

        private void cmdTemplate_Click(object sender, EventArgs e)
        {
           
        }
    }
}