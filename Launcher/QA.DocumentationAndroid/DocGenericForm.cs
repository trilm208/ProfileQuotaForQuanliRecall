

using QA.DocumentationAndroid.GenericControls;
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
    public partial class DocGenericForm : ClientForm, IDocument
    {
        private DataTable FormFields;
        private DataTable FormValues;

        public string PatientID { get; set; }

        public string FacAdmissionID { get; set; }

        public string PhysicianAdmissionID { get; set; }

        public string DocInstanceID { get; set; }

        public string DocType { get; set; }

        public DataRow LDocumentationType { get; set; }

        public DateTime DocDate
        {
            get { return txtDate.DateTime; }
            set { txtDate.DateTime = value; }
        }

        private List<IGenericControl> GenControls = new List<IGenericControl>();

        public bool IsComplete
        {
            get
            {
                foreach (var item in GenControls)
                {
                    if (item.IsRequired && string.IsNullOrEmpty(item.Value))
                        return false;
                }

                return true;
            }
        }

        public DocGenericForm()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            var query = DataQuery.Create("Docs", "ws_DOC_GenericFormFields_List", new { DocTypeName = this.DocType });
            query += DataQuery.Create("Docs", "ws_DOC_GenericFormValues_List", new
            {
                DocInstanceID = this.DocInstanceID,
                PatientID = this.PatientID,
                FacAdmissionID = "00000000-0000-0000-0000-000000000000",
                PhysicianAdmissionID = "00000000-0000-0000-0000-000000000000",
            });

            DataSet ds = Services.Execute(query);
            if (ds == null)
            {
                MessageBox.Show(Services.LastError);
                return;
            }

            this.FormFields = ds.Tables(0);
            this.FormValues = ds.Tables(1);

            CreateControls();
            PopulateControls();
        }

        private void CreateControls()
        {
            this.GenControls.Clear();
            this.panel1.Controls.Clear();

            foreach (DataRow row in FormFields.Rows)
            {
                var groupName = row.Item("GroupName");
                var controlType = row.Item("ControlType");

                var control = CreateGenControl(groupName, controlType);

                control.Margin = new System.Windows.Forms.Padding(0);
                control.FieldName = row.Item("FieldName");
                control.FieldCaption = row.Item("FieldCaption");
                control.LookupSource = row.Item("LookupSource");
                control.WidthPercentage = ConvertSafe.ToInt32(row.Item("WidthPercentage"));
                control.HeightPixel = ConvertSafe.ToInt32(row.Item("HeightPixel"));
                control.IsRequired = ConvertSafe.ToBoolean(row.Item("IsRequired"));
                control.IsReadOnly = ConvertSafe.ToBoolean(row.Item("IsReadOnly"));

                control.Initialize(Services);
                control.Process();
            }
        }

        private GenControl CreateGenControl(string groupName, string controlType)
        {
            var panel = FindPanel(groupName);
            GenControl control = null;
            bool flowbreak = false;

            if (controlType.ToUpper() == "COMBOBOX")
            {
                control = new GenComboBox();
            }

            if (controlType.ToUpper() == "DATE")
            {
                control = new GenDate();
            }

            if (controlType.ToUpper() == "LINEBREAK")
            {
                control = new GenLineBreak();
                flowbreak = true;
            }

            if (controlType.ToUpper() == "TEXTBOX")
            {
                control = new GenTextBox();
            }

            if (controlType.ToUpper() == "LABEL")
            {
                control = new GenLabel();
            }

            if (controlType.ToUpper() == "MEMO")
            {
                control = new GenMemo();
            }

            if (controlType.ToUpper() == "CHECKBOX")
            {
                control = new GenCheckBox();
            }

            if (controlType.ToUpper() == "RADIOGROUP")
            {
                control = new GenRadioGroup();
            }

            panel.Controls.Add(control);
            panel.SetFlowBreak(control, flowbreak);
            this.GenControls.Add(control);

            return control;
        }

        private FlowLayoutPanel FindPanel(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
                return this.panel1;

            var genGroups = this.panel1.Controls.OfType<GenGroupControl>();
            foreach (var control in genGroups)
            {
                if (control.GroupName == groupName)
                    return control.PanelControl;
            }

            var newGroup = new GenGroupControl();
            newGroup.Margin = new Padding(0);
            newGroup.GroupName = groupName;
            newGroup.WidthPercentage = 100;

            this.panel1.Controls.Add(newGroup);
            this.panel1.SetFlowBreak(newGroup, true);

            return newGroup.PanelControl;
        }

        private void PopulateControls()
        {
            
            foreach (DataRow row in FormValues.Rows)
            {
                var fieldName = row.Item("FieldName");
                var fieldValue = row.Item("FieldValue");

                var control = GenControls.Where(v => v.FieldName == fieldName).FirstOrDefault();

                if (control != null)
                {
                    control.Value = fieldValue;
                }
            }
            //base.Process();
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
                DocDate = this.DocDate,
                IsComplete = this.IsComplete
            });

            foreach (var item in GenControls)
            {
                if (item.IsReadOnly)
                    continue;

                query += DataQuery.Create("Docs", "ws_DOC_GenericFormValues_Save", new
                {
                    DocInstanceID = this.DocInstanceID,
                    FieldName = item.FieldName,
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