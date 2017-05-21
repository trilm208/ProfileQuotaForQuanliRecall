using System.Collections.Generic;
using System.Drawing;

namespace System.Windows.Forms
{
    public static class ControlExtentions
    {
        public static void RaiseEvent(this UserControl control, EventHandler @event)
        {
            if (@event != null)
                @event(control, EventArgs.Empty);
        }

        public static IEnumerable<Control> GetChildren(this Control control)
        {
            List<Control> result = new List<Control>();

            foreach (Control item in control.Controls)
            {
                result.Add(item);
                result.AddRange(item.GetChildren());
            }

            return result;
        }

        public static void SetLocation(this Control control, Point value)
        {
            if (control == null) return;
            if (control.Location.X == value.X && control.Location.Y == value.Y) return;

            control.Location = value;
        }

        public static void SetLocation(this Control control, int x, int y)
        {
            if (control == null) return;
            if (control.Location.X == x && control.Location.Y == y) return;

            control.Location = new Point(x, y);
        }

        public static void SetSize(this Control control, int width, int height)
        {
            if (control == null) return;
            if (control.Size.Height == height && control.Size.Width == width) return;

            control.Size = new Size(width, height);
        }

        public static void SetSize(this Control control, Size size)
        {
            if (control == null) return;
            if (control.Size.Height == size.Height && control.Size.Width == size.Width) return;

            control.Size = size;
        }

        public static void SetHeight(this Control control, int height)
        {
            if (control == null) return;
            if (control.Size.Height == height) return;

            control.Size = new Size(control.Width, height);
        }

        public static void SetWidth(this Control control, int width)
        {
            if (control == null) return;
            if (control.Size.Width == width) return;

            control.Size = new Size(width, control.Height);
        }

        public static void SetSize(this Control control, double width, double height)
        {
            control.SetSize((int)width, (int)height);
        }

        public static void SetHeight(this Control control, double height)
        {
            control.SetHeight((int)height);
        }

        public static void SetWidth(this Control control, double width)
        {
            control.SetWidth((int)width);
        }
    }
}