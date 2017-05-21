using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.Controls
{
    public partial class QACombo : ComboBox
    {
        public QACombo()
        {
            InitializeComponent();
            this.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        public string _GetValueFromField(string outputField, string inputField, string inputValue)
        {
            DataTable table = this.DataSource as DataTable;
            foreach (DataRow row in table.Rows)
            {
                if (row.Item(inputField) == inputValue)
                {
                    return row.Item(outputField);
                }
            }
            return String.Empty;
        }
    }
}