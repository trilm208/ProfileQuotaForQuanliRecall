
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccess;

namespace QA.DocumentationAndroid
{
    public partial class DocGenericForm2 : ClientForm, IDocument
    {
        public string PatientID { get; set; }
        public string FacAdmissionID { get; set; }
        public string PhysicianAdmissionID { get; set; }
        public string DocInstanceID { get; set; }
        public string DocType { get; set; }
        public DataRow LDocumentationType { get; set; }


        public string GetProc { get; set; }
        public string SaveProc { get; set; }
        public string DeleteProc { get; set; }


        public DataTable FormData { get; set; }


        public DateTime DocDate
        {
            get { return txtDate.DateTime; }
            set { txtDate.DateTime = value; }
        }


        public DocGenericForm2()
        {
            InitializeComponent();
        }


        public override void Process()
        {
            this.GetProc = LDocumentationType.Item("GetProc");
            this.SaveProc = LDocumentationType.Item("SaveProc");
            this.DeleteProc = LDocumentationType.Item("DeleteProc");

            var query = DataQuery.Create("Docs", GetProc, new
            {
                DocInstanceID = this.DocInstanceID,
                PatientID = this.PatientID,
                FacAdmissionID = this.FacAdmissionID,
                PhysicianAdmissionID = this.PhysicianAdmissionID,
                DocType = this.DocType
            });

            DataSet ds = Services.Execute(query);
            if (ds == null)
            {
                MessageBox.Show(Services.LastError);
                return;
            }

            this.FormData = ds.Tables(0);
            document1.Populate(this.FormData);
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            var query = DataQuery.Create("Docs", "ws_CN_ClinicalSessionDocuments_Save", new
            {
                PatientID = this.PatientID,
                FacAdmissionID = this.FacAdmissionID,
                PhysicianAdmissionID = this.PhysicianAdmissionID,
                DocInstanceID = this.DocInstanceID,
                DocType = this.DocType,
                DocDate = this.DocDate.ToString("yyyy/MM/dd"),
                IsComplete = true
            });


            var namedValues = document1.GetValues();


            foreach (var item in namedValues)
            {
                query += DataQuery.Create("Docs", SaveProc, new
                {
                    DocInstanceID = this.DocInstanceID,
                    FieldName = item.Key,
                    FieldValue = item.Value,
                });
            }

            DataSet ds = Services.Execute(query);
            if (ds == null)
            {
                MessageBox.Show(Services.LastError);
                return;
            }

            MessageBox.Show("Information Saved");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
