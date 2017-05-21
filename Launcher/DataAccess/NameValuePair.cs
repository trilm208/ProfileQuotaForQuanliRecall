using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Launcher
{
    public class NameValuePair
    {
        public bool IsNull { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }


        public NameValuePair()
        {
            this.IsNull = true;
            this.Name = "";
            this.Value = "";
        }


        public NameValuePair(string name, object value)
        {
            this.Name = name;

            if (value == null)
            {
                this.IsNull = true;
                this.Value = "";
            }
            else
            {
                this.IsNull = false;

                if (value is Boolean)
                {
                    this.Value = (bool)value ? "1" : "0";
                }
                else
                {
                    this.Value = value.ToString();
                }
            }
        }


        public override string ToString()
        {
            if (IsNull)
            {
                return Name + " = null";
            }
            else
            {
                return Name + " = '" + Value + "'";
            }
        }
    }
}
