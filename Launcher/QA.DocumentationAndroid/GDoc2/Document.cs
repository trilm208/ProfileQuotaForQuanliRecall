using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.DocumentationAndroid.GDoc2
{
    partial class Document : ElemGroup
    {
        public Document()
        {
            InitializeComponent();
        }


        public void Populate(DataTable table)
        {
            last_x = 0;
            row_y = 0;
            row_height = 0;

            foreach (DataRow row in table.Rows)
            {
                var group = row.Item("Group");
                var name = row.Item("Name");
                var height = ConvertSafe.ToInt32(row.Item("Height"));
                var width = ConvertSafe.ToInt32(row.Item("Width"));
                var type = row.Item("Type");
                var value = row.Item("Value");
                var options = row.Item("Options");

                var groupControl = this.FindGroup(group);
                groupControl.CreateControl((int)(height * 1.2), width, type, name, value, options);
            }

            this.OnResize(EventArgs.Empty);  
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.panel1 == null)
                return;

            if (panel1.Controls.Count == 0)
                return;

            int width = panel1.Controls.Cast<Control>().Max(c => c.Right) + this.BorderRight + this.BorderLeft;
            int height = panel1.Controls.Cast<Control>().Max(c => c.Bottom) + this.BorderTop + this.BorderBottom;

            if (this.Height < height)
                this.Height = height;
        }
    }
}
