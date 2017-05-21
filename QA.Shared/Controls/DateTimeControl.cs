using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.Controls
{
    public partial class DateTimeControl : UserControl
    {
        public DateTimeControl()
        {
            InitializeComponent();
        }

        public DateTime DateTime
        {
            get
            {
                string dttext;

                try
                {
                    if (textEdit1.Text.Trim().Length < 2)
                    {
                        textEdit1.Text = "0" + textEdit1.Text;
                    }
                    if (textEdit2.Text.Trim().Length < 2)
                    {
                        textEdit2.Text = "0" + textEdit2.Text;
                    }
                    if (textEdit3.Text.Trim().Length == 2)
                    {
                        textEdit3.Text = "20" + textEdit2.Text;
                    }
                    else
                    {
                        if (textEdit3.Text.Trim().Length == 3)
                        {
                            textEdit3.Text = "1" + textEdit3.Text;
                        }
                    }
                    dttext = textEdit1.Text + "/" + textEdit2.Text;
                    DateTime dte = new DateTime();
                    if (textEdit1.Text.Contains("/"))
                    {
                        return dte = DateTime.ParseExact(textEdit1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (textEdit1.Text.Contains("-"))
                        return dte = DateTime.ParseExact(textEdit1.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    return DateTime.MinValue;
                }
                catch
                {
                    return DateTime.MinValue;
                }

                return DateTime.MinValue;
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit1.Text.ToString().Length == 2)
            {
                textEdit2.Focus();
                textEdit2.SelectAll();
            }

            if (ConvertSafe.ToInt32(textEdit1.Text.Trim()) >= 4)
            {
                FormatNgay();
                textEdit2.Focus();
                textEdit2.SelectAll();
            }
            SetMaskThang();
        }

        private void FormatNgay()
        {
            if (textEdit1.Text.Trim().Length < 2)
            {
                textEdit1.Text = "0" + textEdit1.Text;
            }
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (textEdit1.Text.ToString().Length == 2)
            //{
            //    textEdit2.Focus();
            //    textEdit2.SelectAll();
            //}
        }

        private void SetMaskThang()
        {
            if (ConvertSafe.ToInt32(textEdit1.Text.Trim()) <= 29)
            {
                textEdit2.Properties.Mask.EditMask = "[1-9]|[1][0-2]|[0][1-9]";
            }
            else
            {
                if (ConvertSafe.ToInt32(textEdit1.Text.Trim()) == 30)
                {
                    textEdit2.Properties.Mask.EditMask = "[13456789]|[1][0-2]|[0][13456789]";
                }
                else if (ConvertSafe.ToInt32(textEdit1.Text.Trim()) == 31)
                {
                    textEdit2.Properties.Mask.EditMask = "[13578]|[1][02]|[0][13578]";
                }
                else
                {
                    textEdit2.Properties.Mask.EditMask = "[1-9]|[1][0-2]|[0][1-9]";
                }
            }
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit2.Text.ToString().Length == 2)
            {
                textEdit3.Focus();
                textEdit3.SelectAll();
            }

            if (ConvertSafe.ToInt32(textEdit2.Text.Trim()) >= 2)
            {
                FormatMonth();
                textEdit3.Focus();
                textEdit3.SelectAll();
            }
        }

        private void textEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                if (textEdit2.Text.IsEmpty())
                {
                    textEdit1.Focus();
                    textEdit1.SelectAll();
                }
            }
        }

        private void FormatMonth()
        {
            if (textEdit2.Text.Trim().Length == 1)
            {
                textEdit2.Text = "0" + textEdit2.Text;
            }
        }

        private void textEdit3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
            {
                if (textEdit3.Text.IsEmpty())
                {
                    textEdit2.Focus();
                    textEdit2.SelectAll();
                }
            }
        }

        private void textEdit3_Leave(object sender, EventArgs e)
        {
            if (textEdit3.Text.Trim().Length == 2)
            {
                textEdit3.Text = "20" + textEdit3.Text;
            }
            else
            {
                if (textEdit3.Text.Trim().Length == 3)
                {
                    textEdit3.Text = "1" + textEdit3.Text;
                }
            }
        }
    }
}