using System.Drawing;

namespace System
{
    internal static class SizeExtensions
    {
        public static Size Adjust(this Size size, double value)
        {
            double width = size.Width * (1 + value);
            double height = size.Height * (1 + value);

            return new Size(Convert.ToInt32(width), Convert.ToInt32(height));
        }
    }
}