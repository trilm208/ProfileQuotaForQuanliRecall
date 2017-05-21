using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.DocumentationAndroid.GenericControls
{
    public partial class GenCheckBox : GenControl
    {
        public override string FieldCaption
        {
            get { return checkEdit1.Text; }
            set { checkEdit1.Text = value; }
        }


        public override string Value 
        {
            get { return checkEdit1.Checked ? "Y" : "N"; }
            set { checkEdit1.Checked = value == "Y"; }
        }


        public GenCheckBox()
        {
            InitializeComponent();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (checkEdit1 != null)
                this.SetHeight(checkEdit1.Bottom + 1); 
        }
    }
}
