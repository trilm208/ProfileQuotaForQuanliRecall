using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QA.Controls
{
    internal class RichTextBoxHelper
    {
        private RichTextBox Control;
        private PaintControl Paint;

        private class PaintControl : NativeWindow
        {
            public bool Suppress { get; set; }

            public PaintControl(Control control)
            {
                try
                {
                    this.AssignHandle(control.Handle);
                }
                catch
                {
                }
            }

            protected override void WndProc(ref Message m)
            {
                try
                {
                    if (Suppress && m.Msg == WM_PAINT)
                    {
                        m.Result = IntPtr.Zero;
                        return;
                    }

                    if (Suppress && m.Msg == WM_DESTROY)
                    {
                        this.ReleaseHandle();
                    }

                    base.WndProc(ref m);
                }
                catch
                {
                }
            }
        }

        public RichTextBoxHelper(RichTextBox control)
        {
            this.Control = control;
            this.Paint = new PaintControl(control);
        }

        // =====================================================================================================
        // Avoid flickering problem
        // =====================================================================================================

        private const int WM_SETREDRAW = 0x000B;
        private const int WM_PAINT = 0x000F;
        private const int WM_DESTROY = 0x0002;
        private const int WM_USER = 0x400;
        private const int EM_GETEVENTMASK = (WM_USER + 59);
        private const int EM_SETEVENTMASK = (WM_USER + 69);

        [DllImport("user32", CharSet = CharSet.Auto)]
        private extern static IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        private IntPtr lockWindowUpdateEventMask = IntPtr.Zero;
        private bool lockWindowUpdateFlag = false;

        public void LockWindowUpdate(bool flag)
        {
            if (flag == lockWindowUpdateFlag)
                return;

            if (flag)
            {
                Paint.Suppress = true;

                // Stop redrawing:
                SendMessage(Control.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
                // Stop sending of events:
                lockWindowUpdateEventMask = SendMessage(Control.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero);

                lockWindowUpdateFlag = true;
            }
            else
            {
                Paint.Suppress = false;

                // turn on events
                SendMessage(Control.Handle, EM_SETEVENTMASK, 0, lockWindowUpdateEventMask);
                // turn on redrawing
                SendMessage(Control.Handle, WM_SETREDRAW, 1, IntPtr.Zero);

                lockWindowUpdateFlag = false;

                Control.Invalidate();
            }
        }

        // =====================================================================================================
        // Determine scrollbar visibility by comparing this.ClientRectangle and this.Size.
        // =====================================================================================================

        public bool HScrollVisible
        {
            get
            {
                var clientRectangle = Control.ClientRectangle;
                var size = Control.Size;
                return (size.Height - clientRectangle.Height) >= SystemInformation.HorizontalScrollBarHeight;
            }
        }

        public bool VScrollVisible
        {
            get
            {
                var clientRectangle = Control.ClientRectangle;
                var size = Control.Size;
                return (size.Width - clientRectangle.Width) >= SystemInformation.VerticalScrollBarWidth;
            }
        }

        // =====================================================================================================
        // Get/Set the ScrollBars positions
        // =====================================================================================================

        private const int SB_HORZ = 0x0;
        private const int SB_VERT = 0x1;
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int SB_THUMBPOSITION = 4;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetScrollPos(int hWnd, int nBar);

        [DllImport("user32.dll")]
        private static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll")]
        private static extern bool PostMessageA(IntPtr hWnd, int nBar, int wParam, int lParam);

        public int HScrollPos
        {
            private get { return GetScrollPos((int)Control.Handle, SB_HORZ); }
            set
            {
                SetScrollPos((IntPtr)Control.Handle, SB_HORZ, value, true);
                PostMessageA((IntPtr)Control.Handle, WM_HSCROLL, SB_THUMBPOSITION + 0x10000 * value, 0);
            }
        }

        public int VScrollPos
        {
            private get { return GetScrollPos((int)Control.Handle, SB_VERT); }
            set
            {
                SetScrollPos((IntPtr)Control.Handle, SB_VERT, value, true);
                PostMessageA((IntPtr)Control.Handle, WM_VSCROLL, SB_THUMBPOSITION + 0x10000 * value, 0);
            }
        }
    }
}