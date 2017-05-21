using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Shell
{
    public partial class QuotaGroup : UserControl
    {
        private DataRow row;

     
        public string QuotaTitle { set { labelControl1.Text = value.Trim(); } }



        public string QuotaFieldValue { get { return this.radioGroup1.EditValue == null ? null : this.radioGroup1.EditValue.ToString(); } set { radioGroup1.EditValue = value; } }

        public string QuotaOptions
        {
            set
            {
                var table = (DataTable)JsonConvert.DeserializeObject(value, (typeof(DataTable)));
                DevExpress.XtraEditors.Controls.RadioGroupItem[] items=new DevExpress.XtraEditors.Controls.RadioGroupItem[table.Rows.Count];
                foreach (DataRow row in table.Rows)
                {
                    HeightResize += 40;
                    var item =new  DevExpress.XtraEditors.Controls.RadioGroupItem();
                    item.Description =row.Item("OptionFieldValue")+". "+ row.Item("OptionFieldName");
                    item.Value = row.Item("OptionFieldValue");
                    this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {          
                             item});
                }

              
            }
        }

        public QuotaGroup()
        {
            InitializeComponent();
        }

        public QuotaGroup(DataRow row)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            HeightResize = 0;
            this.row = row;

            QuotaTitle = row.Item("Caption");
            QuotaOptions = row.Item("OptionValues");
            QuotaFieldName = row.Item("FieldName");
        }

        public int HeightResize { get; set; }

        public string QuotaFieldName { get; set; }
    }
}
