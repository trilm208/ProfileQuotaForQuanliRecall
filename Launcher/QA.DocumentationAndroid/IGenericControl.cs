using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QA.DocumentationAndroid
{
    interface IGenericControl
    {
        string FieldName { get; set; }
        string FieldCaption { get; set; }
        int HeightPixel { get; set; }
        int WidthPercentage { get; set; }
        bool IsRequired { get; set; }
        bool IsReadOnly { get; set; }
        string LookupSource { get; set; }
        
        string Value { get; set; }
    }
}