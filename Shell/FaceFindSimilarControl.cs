using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shell
{
    public partial class FaceFindSimilarControl : FlowLayoutPanel
    {
        public FaceFindSimilarControl()
        {
            InitializeComponent();
        }

        internal void Add(FaceFaceSimilarUnit faceRespondent)
        {

            faceRespondent.Size = new System.Drawing.Size(155, 179);
            faceRespondent.Width = this.Width-10;
            faceRespondent.Height = 50;
            flowLayoutPanel1.Controls.Add(faceRespondent);
        }
    }
}
