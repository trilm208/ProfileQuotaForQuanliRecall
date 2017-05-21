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
    public partial class GenGroupControl : GenControl
    {
        public string GroupName
        {
            get { return groupControl1.Text; }
            set { groupControl1.Text = value; }
        }


        public FlowLayoutPanel PanelControl
        {
            get { return panel1; }
        }


        public GenGroupControl()
        {
            InitializeComponent();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            try
            {
                int height = panel1.Top;
                height += panel1.Controls.Cast<Control>().Max(v => v.Bottom);
                height += 20;

                this.SetHeight(height); 
            }
            catch
            {
            }
        }


        private void panel1_ControlAdded(object sender, ControlEventArgs e)
        {
            OnResize(EventArgs.Empty); 
        }
    }
}
