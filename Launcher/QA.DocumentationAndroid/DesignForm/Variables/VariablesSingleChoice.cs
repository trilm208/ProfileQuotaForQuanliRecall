using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace QA.DocumentationAndroid.DesignForm.Variables
{
    public partial class VariablesSingleChoice : ClientControl
    {
        public string VariablesName
        {
            set { txtVariablesName.Text = value; }
            get { return txtVariablesName.Text.Trim(); }
        }
        public VariablesSingleChoice()
        {
            InitializeComponent();
        }
    }
}
