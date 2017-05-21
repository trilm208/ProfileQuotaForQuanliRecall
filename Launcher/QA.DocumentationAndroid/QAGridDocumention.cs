using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.Documentation
{
    public partial class QAGridDocumention : ClientControl
    {
        public DataRow _row { get; set; }
        public DataTable DataSource { get; set; }
        public event EventHandler gvDoubleClick;
        public event EventHandler gvFocusRowChanged; 
        public QAGridDocumention()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            base.Process();
            this.gridControl1.DataSource = DataSource;
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this.RaiseEvent(gvDoubleClick);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this._row = this.gridView1.GetDataRow(gridView1.FocusedRowHandle);
            this.RaiseEvent(gvFocusRowChanged);
        }

        
    }
}
