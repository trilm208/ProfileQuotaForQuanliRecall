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
    partial class ElemInput : ElemBase
    {
        public override string Value
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }


        public ElemInput()
        {
            InitializeComponent();
        }
    }
}
