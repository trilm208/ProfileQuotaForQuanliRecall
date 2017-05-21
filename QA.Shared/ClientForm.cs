using System.Data;
using System.Windows.Forms;

namespace QA
{
    public partial class ClientForm : Form
    {
        public IClientServices Services { get; set; }

        public ClientForm()
        {
            InitializeComponent();
        }

        public virtual void Initialize(IClientServices services)
        {
            this.Services = services;
        }

        public void Process(object args)
        {
            ClientControlHelper.Process(this, args);

            Process();
        }

        public virtual void Process()
        {
            this.Controls.SetControlColor();
        }

        protected bool ValidateSaveResult(DataSet ds)
        {
            return ClientControlHelper.ValidateSaveResult(ds);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
          

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}