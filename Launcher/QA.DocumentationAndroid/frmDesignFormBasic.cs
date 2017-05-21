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
    public partial class frmDesignFormBasic : ClientForm
    {
        public frmDesignFormBasic()
        {
            InitializeComponent();
        }
        public override void Process()
        {
            base.Process();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
           
            if (keyData == (Keys.F1))
            {
                btnAddQuestion_Click(null, null);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            using (var frm = new frmQuestion())
            {
                frm.Initialize(Services);
                frm.Process();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
        }

        private void frmDesignFormBasic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                
            }
        }
    }
}
