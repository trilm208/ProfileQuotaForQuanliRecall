using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.DocumentationAndroid.GenericControls
{
    public partial class GenControl : ClientControl, IGenericControl
    {
        public virtual string FieldName { get; set; }
        public virtual string FieldCaption { get; set; }
        public virtual string LookupSource { get; set; }
        public virtual int HeightPixel { get; set; }
        public virtual int WidthPercentage { get; set; }
        public virtual bool IsRequired { get; set; }
        public virtual bool IsReadOnly { get; set; }

        public virtual string Value { get; set; }


        public GenControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
    }
}
