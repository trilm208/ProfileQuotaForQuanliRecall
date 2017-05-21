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
    public partial class FaceFaceSimilarUnit : UserControl
    {
        public Double Conf { set { txtConf.Text = String.Format("{0:0}", (value * 100)) + "%"; } }

        public string ProjectNameFullName { set { label2.Text = value; } }

        public Image ImageRespondent { set { FacePhoto.Image = value; } }

        
      

        public FaceFaceSimilarUnit()
        {
            InitializeComponent();

        }
    }
}
