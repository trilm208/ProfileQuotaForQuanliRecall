using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.Documentation
{
    public partial class QAGridCase : ClientControl
    {
        public DataTable table;

        public QAGridCase()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            base.Process();
            gridControl.DataSource = table;
        }
        

        private void cmdQueueOpen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            MessageBox.Show("aaaaa");
        }
    }
}