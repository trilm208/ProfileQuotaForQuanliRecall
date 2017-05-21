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
    partial class ElemBase : UserControl, IHaveValue
    {
        public virtual string Value { get; set; }

        public ElemBase()
        {
            InitializeComponent();
        }
    }
}
