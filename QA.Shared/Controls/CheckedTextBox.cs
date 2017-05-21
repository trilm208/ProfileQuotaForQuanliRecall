using System;
using System.ComponentModel;

namespace QA.Controls
{
    public partial class CheckedTextBox : ClientControl
    {
        [Browsable(true)]
        public override string Text
        {
            get
            {
                return textEdit1.Text;
            }
            set
            {
                textEdit1.Text = value;
                checkEdit1.Checked = value.IsNotEmpty();
                checkEdit1_CheckedChanged(null, null);
            }
        }

        public string Caption
        {
            get { return checkEdit1.Text; }
            set { checkEdit1.Text = value; }
        }

        public string Value
        {
            get { return textEdit1.Text.Trim(); }
            set { textEdit1.Text = value; }
        }

        public bool Checked
        {
            get { return checkEdit1.Checked; }
            set { checkEdit1.Checked = value; }
        }

        public CheckedTextBox()
        {
            InitializeComponent();
            this.Text = "";
        }

        public override void Initialize(IClientServices services)
        {
            base.Initialize(services);
        }

        public override void Process()
        {
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (textEdit1 != null)
            {
                if (this.Height != textEdit1.Height)
                    this.Height = textEdit1.Height;

                textEdit1.Left = textEdit1.Width;
                textEdit1.Width = this.Width - textEdit1.Left;
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                textEdit1.Enabled = true;
                textEdit1.Focus();
            }
            else
            {
                textEdit1.Enabled = false;
                textEdit1.Text = String.Empty;
            }
        }
    }
}