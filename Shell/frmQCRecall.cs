using QA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shell
{
    public partial class frmQCRecall : ClientForm
    {
        public frmQCRecall()
        {
            InitializeComponent();


        }

        public string ProjectID { get; set; }
        public string ProjectNo { get; internal set; }

        public override void Process()
        {
            base.Process();
            respondentProfileInformation1.Initialize(Services);
            respondentProfileInformation1.ProjectID = this.ProjectID;
            respondentProfileInformation1.ProjectNo = this.ProjectNo;
            respondentProfileInformation1.Process();

          
            summarySupRecuit1.Initialize(Services);
            summarySupRecuit1.ProjectID = this.ProjectID;
            summarySupRecuit1.Process();


          


            fwProfile1.Initialize(Services);
            fwProfile1.ProjectID = this.ProjectID;
            fwProfile1.Process();
        }
        private void TabCollection_Click(object sender, EventArgs e)
        {
           
          
            if (TabCollection.SelectedTabPage.Name == "TabSummaryRecuit")
            {
                summarySupRecuit1.LoadQuota();


            }
          
            if (TabCollection.SelectedTabPage.Name == "TabReport")
            {

                fwProfile1.LoadQuota();
            }

        }

        private void xtraTabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
