using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QA.Controls
{
    public partial class RichEditor : UserControl
    {
        private RichTextBoxHelper utils;

        public string RTFString { get { return textBox.Rtf; } set { textBox.Rtf = value; } }

        public bool AcceptsTab { get { return textBox.AcceptsTab; } set { textBox.AcceptsTab = value; } }

        public bool IsReadOnly { get { return textBox.ReadOnly; } set { textBox.ReadOnly = value; } }

        public string Category { get; set; }

        private string temp;

        private class Area
        {
            public int FirstIndex;
            public int LastIndex;

            public int Length
            {
                get { return LastIndex - FirstIndex + 1; }
            }
        }

        public Color _borderColor = Color.Black;

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; this.Invalidate(); }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; textBox_TextChanged(null, null); }
        }

        public RichEditor()
        {
            InitializeComponent();

            utils = new RichTextBoxHelper(textBox);

            this.BorderColor = Color.LightGray;
            textBox.Location = new Point(1, 1);
            OnResize(EventArgs.Empty);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.Clear(_borderColor);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            textBox.Size = new Size(this.Width - 2, this.Height - 2);
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            DoColoring();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            var areas = FindAllAreas();
            if (areas.Count() == 0)
            {
                return;
            }

            if (e.KeyCode == Keys.ControlKey)
            {
                var next = areas.Where(v => v.FirstIndex > textBox.SelectionStart);
                if (next.Count() > 0)
                {
                    textBox.SelectionStart = next.Min(v => v.FirstIndex) + 1;
                }
                else
                {
                    textBox.SelectionStart = areas.Min(v => v.FirstIndex) + 1;
                }
                e.SuppressKeyPress = true;
                DoHighlighting();
            }
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            DoHighlighting();
        }

        private void textBox_MouseDown(object sender, MouseEventArgs e)
        {
            DoHighlighting();
        }

        private bool _coloring = false;

        private void DoColoring()
        {
            // changing colot triggers text change event
            // this makes sure there is no recursions
            if (_coloring)
                return;

            _coloring = true;
            utils.LockWindowUpdate(true);

            // save selection
            int priorSelectionStart = textBox.SelectionStart;
            int priorSelectionLength = textBox.SelectionLength;

            // clear colors
            // set all text to ForeColor (black)
            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length;
            textBox.SelectionColor = this.ForeColor;
            Font fontAll = new Font("Times New Roman", 10f);
            textBox.SelectionFont = new Font(fontAll, FontStyle.Regular);

            var areas = FindAllAreas();
            foreach (var area in areas)
            {
                // set area to red
                textBox.SelectionStart = area.FirstIndex;
                textBox.SelectionLength = area.Length;
                textBox.SelectionColor = Color.Red;
            }

            var boldAreas = FindBoldAreas();
            foreach (var area in boldAreas)
            {
                // set area to bold
                textBox.SelectionStart = area.FirstIndex;
                textBox.SelectionLength = area.Length;
                Font font = new Font("Times New Roman", 12.0f);
                textBox.SelectionFont = new Font(font, FontStyle.Bold);
            }

            //var ItalicAreas = FindItalicAreas();
            //foreach (var area in ItalicAreas)
            //{
            //    // set area to bold
            //    textBox.SelectionStart = area.FirstIndex;
            //    textBox.SelectionLength = area.Length;
            //    textBox.SelectionFont = new Font(this.Font, FontStyle.Italic);
            //}

            // restore selection
            textBox.SelectionStart = priorSelectionStart;
            textBox.SelectionLength = priorSelectionLength;

            utils.LockWindowUpdate(false);
            _coloring = false;
        }

        private bool DoHighlighting()
        {
            var areas = FindAllAreas();
            var matches = areas.Where(v => textBox.SelectionStart > v.FirstIndex && textBox.SelectionStart <= v.LastIndex);
            var match = matches.FirstOrDefault();
            if (match != null)
            {
                textBox.SelectionStart = match.FirstIndex;
                textBox.SelectionLength = match.Length;
                return true;
            }

            return false;
        }

        private IEnumerable<Area> FindAllAreas()
        {
            return FindAllAreas(textBox.Text, "(enter", ")")
                .Concat(FindAllAreas(textBox.Text, "(nhập", ")"))
                .Concat(FindAllAreas(textBox.Text, "*", ":"))
                .ToArray();
        }

        private IEnumerable<Area> FindBoldAreas()
        {
            return FindAllAreas(textBox.Text, "---", ":").ToArray();
        }

        private IEnumerable<Area> FindItalicAreas()
        {
            return FindAllAreas(textBox.Text, "+", ":").ToArray();
        }

        private IEnumerable<Area> FindAllAreas(string text, string open, string close)
        {
            text = text.ToLower();
            open = open.ToLower();
            close = close.ToLower();

            for (int i = 0; i < text.Length - open.Length; i++)
            {
                if (text.Substring(i, open.Length) == open)
                {
                    for (int n = i + open.Length; n <= text.Length - close.Length; n++)
                    {
                        if (text.Substring(n, close.Length) == close)
                        {
                            int firstIndex = i;
                            int lastIndex = n + close.Length - 1;

                            i = lastIndex;

                            yield return new Area()
                            {
                                FirstIndex = firstIndex,
                                LastIndex = lastIndex
                            };

                            break;
                        }
                    }
                }
            }
        }
    }
}