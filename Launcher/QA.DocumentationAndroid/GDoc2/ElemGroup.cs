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
    partial class ElemGroup : ElemBase
    {
        int _borderTop = 0;
        public int BorderTop { get { return _borderTop; } set { _borderTop = value; OnResize(EventArgs.Empty); } }
        int _borderLeft = 0;
        public int BorderLeft { get { return _borderLeft; } set { _borderLeft = value; OnResize(EventArgs.Empty); } }
        int _borderRight = 0;
        public int BorderRight { get { return _borderRight; } set { _borderRight = value; OnResize(EventArgs.Empty); } }
        int _borderBottom = 0;
        public int BorderBottom { get { return _borderBottom; } set { _borderBottom = value; OnResize(EventArgs.Empty); } }


        public int BorderAll
        {
            get
            {
                if (BorderTop == BorderLeft
                    && BorderLeft == BorderRight
                    && BorderRight == BorderBottom)
                    return BorderTop;
                return -1;
            }
            set
            {
                BorderTop = value;
                BorderLeft = value;
                BorderRight = value;
                BorderBottom = value;
            }
        }


        public ElemGroup()
        {
            InitializeComponent();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            int panel_x = BorderLeft;
            int panel_y = BorderTop;

            if (panel1.Location.X != panel_x || panel1.Location.Y != panel_y)
            {
                panel1.Location = new Point(panel_x, panel_y);
            }

            int panel_width = this.Width - BorderLeft - BorderRight;
            int panel_height = this.Height - BorderTop - BorderBottom;

            if (panel1.Size.Width != panel_width || panel1.Size.Height != panel_height)
            {
                panel1.Size = new Size(panel_width, panel_height);
            }
        }



        public ElemGroup FindGroup(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
                return this;

            var groups = this.panel1.Controls.OfType<ElemGroup>();
            foreach (var item in groups)
            {
                if ((item as IHaveValue).Value == groupName)
                    return item;
            }

            return null;
        }


        protected int last_x = 0;
        protected int row_y = 0;
        protected int row_height = 0;


        public void CreateControl(int height, int widthPersentage, string type, string name, string value, string options)
        {
            Control control = null;

            int width = (this.Width * widthPersentage) / 100;

            if (type == "label") control = new ElemLabel();
            if (type == "check") control = new ElemCheck();
            if (type == "input") control = new ElemInput();
            if (type == "group") control = new ElemGroup();

            control.Name = name;

            this.panel1.Controls.Add(control);


            if (type == "group" || type == "check")
            {
                control.Width = width;
                control.Height = height;
            }
            else
            {
                control.Width = width - 6;
                control.Height = height - 4;
            }

            if (control as IHaveValue != null)
                (control as IHaveValue).Value = value;

            float right = last_x + width;
            if (right > this.Width)
            {
                last_x = 0;
                row_y += row_height;
                row_height = 0;
            }

            if (type == "group" || type == "check")
            {
                control.Location = new System.Drawing.Point(last_x, row_y);
            }
            else
            {
                control.Location = new System.Drawing.Point(last_x + 3, row_y + 2);
            }

            last_x += width;
            row_height = Math.Max(row_height, height);

            var grop = new ElemOptions(options);
            if (type == "label")
            {
                grop.ApplyToLabel(control as ElemLabel);
            }
            else if (type == "group")
            {
                grop.ApplyToGroup(control as ElemGroup);
            }
            else
            {
                grop.ApplyToControl(control);
            }
        }


        public IEnumerable<KeyValuePair<string, string>> GetValues()
        {
            var controls = panel1.Controls.OfType<ElemBase>().ToArray();
            foreach (var control in controls)
            {
                if (control is ElemGroup)
                {
                    var items = (control as ElemGroup).GetValues();
                    foreach (var item in items)
                    {
                        yield return item;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(control.Name) == false)
                    {
                        yield return new KeyValuePair<string, string>(control.Name, control.Value);
                    }
                }
            }
        }
    }
}
