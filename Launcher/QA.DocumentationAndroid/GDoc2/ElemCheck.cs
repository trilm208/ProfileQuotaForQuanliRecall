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
    partial class ElemCheck  : ElemBase
    {
        public override string Value
        {
            get { return checkEdit1.EditValue + ""; }
            set { checkEdit1.EditValue = value; }
        }


        public ElemCheck()
        {
            InitializeComponent();
        }
    }
}
