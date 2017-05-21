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
    public partial class GenMemo : GenControl
    {
        public override string FieldCaption
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }


        public override string LookupSource
        {
            get { return memoEdit1.Category; }
            set { memoEdit1.Category = value; }
        }


        public override string Value
        {
            get { return memoEdit1.Text; }
            set { memoEdit1.Text = value; }
        }


        public GenMemo()
        {
            InitializeComponent();
        }


        public override void Process()
        {
            memoEdit1.Initialize(Services);
            memoEdit1.Process();
        }
    }
}
