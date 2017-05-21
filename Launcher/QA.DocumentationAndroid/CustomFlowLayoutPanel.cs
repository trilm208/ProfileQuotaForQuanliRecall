using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QA.DocumentationAndroid
{
    class CustomFlowLayoutPanel : FlowLayoutPanel
    {
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            this.OnResize(EventArgs.Empty);
        }


        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            var genControls = this.Controls.OfType<IGenericControl>();
            foreach (var genControl in genControls)
            {
                var control = genControl as Control;

                int height = control.Height;
                int width = control.Width;

                if (genControl.HeightPixel > 0)
                    height = genControl.HeightPixel;

                if (genControl.WidthPercentage > 0)
                    width = ((this.Width - 22 /* scroll bar */) * genControl.WidthPercentage) / 100;

                if (control.Height != height || control.Width != width)
                {
                    control.Size = new System.Drawing.Size(width, height);
                }
            }
        }
    }
}
