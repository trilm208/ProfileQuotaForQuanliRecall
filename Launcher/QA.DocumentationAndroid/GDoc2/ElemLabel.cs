using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.DocumentationAndroid.GDoc2
{
    partial class ElemLabel : ElemBase
    {
        public override string Value
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }


        public System.Drawing.ContentAlignment TextAlignment
        {
            get { return label1.TextAlign; }
            set { label1.TextAlign = value; }
        }


        public ElemLabel()
        {
            InitializeComponent();
        }
    }
}
