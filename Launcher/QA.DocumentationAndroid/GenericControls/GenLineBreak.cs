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
    public partial class GenLineBreak : GenControl
    {
        public override bool IsReadOnly
        {
            get { return true; }
            set { }
        }


        public GenLineBreak()
        {
            InitializeComponent();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.SetWidth(1);
            this.SetHeight(1); 
        }
    }
}
