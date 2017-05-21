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
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();

            txtSubscriptionKey.Text = Properties.Settings.Default.SubscriptionKey;

            txtConfidence.Text = Properties.Settings.Default.Confidence.ToString() ;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SubscriptionKey = txtSubscriptionKey.Text.Trim();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Confidence = Convert.ToDouble(txtConfidence.Text);

            Properties.Settings.Default.Save();

            MessageBox.Show("Saved");
        }
    }
}
