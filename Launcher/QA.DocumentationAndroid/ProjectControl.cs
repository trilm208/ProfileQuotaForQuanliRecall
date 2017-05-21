using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using DataAccess;
using DevExpress.XtraGrid.Views.Grid;

namespace QA.DocumentationAndroid
{
    public partial class ProjectControl : ClientControl
    {
        public ProjectControl()
        {
            InitializeComponent();
        }
       
        public override void Process()
        {
            base.Process();
            var query =DataQuery.Create("Docs", "ws_L_Project_List");
            var ds = Services.Execute(query);
            if(ds==null)
            {
                UI.ShowError(Services.LastError);
                return;
            }
            gridControl1.DataSource = ds.FirstTable();
        }

        private void gvProjects_DoubleClick(object sender, EventArgs e)
        {
            var row = gvProjects.GetDataRow(gvProjects.FocusedRowHandle);
            if(row==null)
            {
                return;
            }
            using (var frm = new frmDesignForm())
            {
                frm.Initialize(Services);
                frm.Process(row);
                if(frm.ShowDialog()==DialogResult.OK)
                {
                    Process();
                }

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var frm = new frmProject())
            {
                frm.Initialize(Services);
                frm.Process();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Process();
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var row = gvProjects.GetDataRow(gvProjects.FocusedRowHandle);
            if (row == null)
                return;
            using (var frm = new frmProject())
            {
                frm.Initialize(Services);
                frm.Process(row);
                frm.ProjectTypeID = row.Item("ProjectType");
                
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Process();
                }
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var row = gvProjects.GetDataRow(gvProjects.FocusedRowHandle);
            if (row == null)
                return;
            using (var frm = new frmAssignUser())
            {
                frm.Initialize(Services);
                frm.Process(row);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Process();
                }
            }
        }
        int count;
        private void gvProjects_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
            {
                count = 0;
            }
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
            {
                if (ConvertSafe.ToBoolean(View.GetDataRow(e.RowHandle).Item("ProjectIsActived")) == true)
                {
                    count = count + 1;
                }
                e.TotalValue = count;
            }
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
              
            }
        }

        private void gvProjects_HiddenEditor(object sender, EventArgs e)
        {
            gvProjects.UpdateCurrentRow();
            gvProjects.UpdateTotalSummary();

            gvProjects.UpdateSummary();
        }

        private void gvProjects_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                gvProjects.PostEditor();
                gvProjects.UpdateTotalSummary();
            }));
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            gvProjects_DoubleClick(null, null);
        }

       
       
    }
}
