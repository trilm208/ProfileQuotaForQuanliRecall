using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QA.DocumentationAndroid.GenericControls
{
    public partial class GenRadioGroup : GenControl
    {
        public override string FieldCaption
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }


        public override string Value
        {
            get 
            {
                var controls = panel1.Controls.OfType<CheckEdit>();
                foreach (var control in controls)
                {
                    if (control.Checked)
                        return control.Text; 
                    
                }

                return ""; 
            }
            set 
            {
                var controls = panel1.Controls.OfType<CheckEdit>();
                foreach (var control in controls)
                {
                    control.Checked = (control.Text.ToUpper() == value.ToUpper());                        
                }
            }
        }


        public GenRadioGroup()
        {
            InitializeComponent();
        }


        public override void Process()
        {
            panel1.Controls.Clear(); 

            var values = QA.Shared.Controls.ComboBox.LoadValues(Services, this.LookupSource);

            foreach (var value in values)
            {
                var radioBox = new CheckEdit();

                radioBox.Text = value;
                radioBox.Properties.AutoWidth = true;
                radioBox.Properties.Caption = value;
                radioBox.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
                radioBox.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
                radioBox.Properties.RadioGroupIndex = 1;

                panel1.Controls.Add(radioBox);
            }
        }
    }
}
