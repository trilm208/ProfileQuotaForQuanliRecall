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
    public partial class GenLabel : GenControl
    {
        public override string FieldCaption
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }


        public override bool IsReadOnly
        {
            get { return true; }
            set { }
        }


        public GenLabel()
        {
            InitializeComponent();
        }
    }
}
