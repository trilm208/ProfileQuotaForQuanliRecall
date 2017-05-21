using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.DocumentationAndroid.GenericControls
{
    public partial class GenTextBox : GenControl
    {
        public override string FieldCaption
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public override string Value
        {
            get { return textEdit1.Text; }
            set { textEdit1.Text = value; }
        }

        public override bool IsReadOnly
        {
            get { return textEdit1.Properties.ReadOnly; }
            set { textEdit1.Properties.ReadOnly = value; }
        }

        public GenTextBox()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (textEdit1 != null)
                this.SetHeight(textEdit1.Bottom + 1);
        }
    }
}