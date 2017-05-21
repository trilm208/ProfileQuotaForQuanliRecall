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
    public partial class GenComboBox : GenControl
    {
        public override string FieldCaption
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }


        public override string LookupSource
        {
            get { return comboBox1.ComboName; }
            set { comboBox1.ComboName = value; }
        }

       
        public override string Value
        {
            get { return comboBox1.Text; }
            set { comboBox1.Text = value; }
        }


        public GenComboBox()
        {
            InitializeComponent();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (comboBox1 != null)
                this.SetHeight(comboBox1.Bottom + 1);
        }


        public override void Process()
        {
            comboBox1.Initialize(Services);
            comboBox1.Process(); 
        }
    }
}
