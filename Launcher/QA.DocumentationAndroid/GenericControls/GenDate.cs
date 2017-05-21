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
    public partial class GenDate : GenControl
    {
        public override string FieldCaption
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public override string Value
        {
            get { return dateEdit1.Text; }
            set { dateEdit1.Text = value; }
        }

        public override bool IsReadOnly
        {
            get { return dateEdit1.Properties.ReadOnly; }
            set { dateEdit1.Properties.ReadOnly = value; }
        }

        public GenDate()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (dateEdit1 != null)
                this.SetHeight(dateEdit1.Bottom + 1);
        }
    }
}