using System;
using System.Collections.Generic;
using System.Data;

namespace QA.Shared.Controls
{
    public partial class ComboBox : ClientControl
    {
        public string ComboName { get; set; }

        public override string Text
        {
            get
            {
                return comboBoxEdit1.Text;
            }
            set
            {
                comboBoxEdit1.Text = value;
            }
        }

        public ComboBox()
        {
            InitializeComponent();
        }

        public override void Process()
        {
            var comboValues = LoadValues(Services, this.ComboName);

            var items = comboBoxEdit1.Properties.Items;
            items.Clear();

            foreach (var value in comboValues)
            {
                items.Add(value);
            }
        }

        public static IEnumerable<string> LoadValues(IClientServices services, string comboName)
        {
            return null;
            //var comboValues = services.GetInformation("ComboValues") as DataTable;
            //if (comboValues == null)
            //{
            //    var query = DataAccess.DataQuery.Create("QAHosGenericDB", "ws_DOC_ComboValues_List", new
            //    {
            //        FacID = QAFunction.GetFacID(services)
            //    });
            //    var ds = services.Execute(query);
            //    comboValues = ds.FirstTable();
            //    services.SetInformation("ComboValues", comboValues);
            //}

            //var rows = comboValues.Select("ComboName='" + comboName + "'");
            //foreach (var row in rows)
            //{
            //    yield return row.Item("ComboValue");
            //}
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (comboBoxEdit1 != null)
            {
                if (this.Height != comboBoxEdit1.Height)
                    this.Height = comboBoxEdit1.Height;
            }
        }
    }
}