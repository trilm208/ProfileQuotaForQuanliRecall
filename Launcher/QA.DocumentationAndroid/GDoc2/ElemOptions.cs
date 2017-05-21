using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.DocumentationAndroid.GDoc2
{
    class ElemOptions
    {
        public bool CanGrow { get; set; }
        public bool IsTitle { get; set; }
        public System.Drawing.ContentAlignment TextAlignment { get; set; }
        public bool IsBold { get; set; }
        public bool IsStrikeout { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderline { get; set; }

        public bool BorderTop { get; set; }
        public bool BorderBottom { get; set; }
        public bool BorderLeft { get; set; }
        public bool BorderRight { get; set; }


        public ElemOptions(string options)
        {
            var all = options.ToLower().Split(' ');

            this.CanGrow = all.Contains("grow") || all.Contains("cangrow");
            this.IsTitle = all.Contains("title");

            this.IsBold = all.Contains("bold");
            this.IsStrikeout = all.Contains("strikeout");
            this.IsItalic = all.Contains("italic");
            this.IsUnderline = all.Contains("underline");

            this.BorderTop = all.Contains("border-all") || all.Contains("border-top");
            this.BorderBottom = all.Contains("border-all") || all.Contains("border-bottom");
            this.BorderLeft = all.Contains("border-all") || all.Contains("border-left");
            this.BorderRight = all.Contains("border-all") || all.Contains("border-right");

            this.TextAlignment = System.Drawing.ContentAlignment.TopLeft;

            if (all.Contains("topleft")) this.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            if (all.Contains("topcenter")) this.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            if (all.Contains("topright")) this.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            if (all.Contains("middleleft")) this.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            if (all.Contains("middlecenter")) this.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            if (all.Contains("middleright")) this.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            if (all.Contains("bottomleft")) this.TextAlignment = System.Drawing.ContentAlignment.BottomLeft;
            if (all.Contains("bottomcenter")) this.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            if (all.Contains("bottomright")) this.TextAlignment = System.Drawing.ContentAlignment.BottomRight;
        }


        public void ApplyToControl(Control control)
        {
            var fontName = "Vardana";
            var fontStyle = System.Drawing.FontStyle.Regular;
            var fontSize = 6.75f * 1.2f;

            if (this.IsTitle)
            {
                fontName = "Tahoma";
                fontSize = 10F * 1.2f;
                this.IsBold = true;
            }

            if (this.IsBold) fontStyle |= System.Drawing.FontStyle.Bold;
            if (this.IsStrikeout) fontStyle |= System.Drawing.FontStyle.Strikeout;
            if (this.IsItalic) fontStyle |= System.Drawing.FontStyle.Italic;
            if (this.IsUnderline) fontStyle |= System.Drawing.FontStyle.Underline;

            control.Font = new System.Drawing.Font(fontName, fontSize, fontStyle);
        }


        public void ApplyToGroup(ElemGroup control)
        {
            //ApplyToControl(control);

            control.BorderAll = 0;

            if (this.BorderTop) control.BorderTop = 1;
            if (this.BorderBottom) control.BorderBottom = 1;
            if (this.BorderLeft) control.BorderLeft = 1;
            if (this.BorderRight) control.BorderRight = 1;

            control.Invalidate();
        }


        public void ApplyToLabel(ElemLabel control)
        {
            ApplyToControl(control);

            control.TextAlignment = this.TextAlignment;
        }
    }
}
