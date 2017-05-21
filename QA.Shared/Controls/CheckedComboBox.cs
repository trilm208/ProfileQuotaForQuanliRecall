using System;
using System.ComponentModel;

namespace QA.Controls
{
    public partial class CheckedComboBox : ClientControl
    {
        [Browsable(true)]
        public override string Text
        {
            get
            {
                return comboBox1.Text;
            }
            set
            {
                comboBox1.Text = value;
                checkEdit1.Checked = value.IsNotEmpty();
                checkEdit1_CheckedChanged(null, null);
            }
        }

        public string Caption
        {
            get { return checkEdit1.Text; }
            set { checkEdit1.Text = value; }
        }

        public string ComboName
        {
            get { return comboBox1.ComboName; }
            set { comboBox1.ComboName = value; }
        }

        public CheckedComboBox()
        {
            InitializeComponent();
            this.Text = "";
        }

        public override void Initialize(IClientServices services)
        {
            base.Initialize(services);
            comboBox1.Initialize(services);
        }

        public override void Process()
        {
            comboBox1.Process();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (comboBox1 != null)
            {
                if (this.Height != comboBox1.Height)
                    this.Height = comboBox1.Height;

                comboBox1.Left = checkEdit1.Width;
                comboBox1.Width = this.Width - comboBox1.Left;
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.Text = "";
            }
        }
    }
}