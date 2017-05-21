using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.DocumentationAndroid
{
    public partial class frmNewDocumentSelect : ClientForm
    {
        public DataTable LDocumentationTypes { get; set; }
        public string DocType { get; set; }

        
        public frmNewDocumentSelect()
        {
            InitializeComponent();
        }


        public override void Process()
        {
            gridControl1.DataSource = LDocumentationTypes; 
        }


        private void cmdCreate_Click(object sender, EventArgs e)
        {
        }
    }
}
