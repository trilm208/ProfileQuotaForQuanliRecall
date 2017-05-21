
using QA;
using System;
using System.Windows.Forms;
using DataAccess;

namespace Shell
{
    public partial class frmLogin : ClientForm
    {
        public string UserID;
        public string UserName;

        public frmLogin()
        {
            InitializeComponent();
        }

        public override void Process()
        {

        }

        private bool ValidateForm()
        {
            bool result = true;
            errorProvider1.Clear();

            if (txtUserName.Text.IsEmpty())
            {
                errorProvider1.SetError(txtUserName, Services.Localize("Required"));
                result = false;
            }

            if (txtPassword.Text.IsEmpty())
            {
                errorProvider1.SetError(txtPassword, Services.Localize("Required"));
                result = false;
            }

            return result;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                cmdContinue_Click(sender, EventArgs.Empty);
            }
        }

        private void cmdContinue_Click(object sender, EventArgs e)
        {
            if (ValidateForm() == false)
                return;

            var username = txtUserName.Text.Trim();
            var password = txtPassword.Text.Trim().GetMd5Hash();

            var query = DataQuery.Create("Security", "ws_Session_Authenticate", new { Username = username, PasswordHash = password, FacID = QAFunction.GetFacID(Services) });
            var ds = Services.Execute(query);
            if (ds == null)
            {
                MessageBox.Show(Services.LastError);
                return;
            }

            Services.SetInformation("UserID", ds.FirstRow().Item("UserID"));
            Services.SetInformation("Username", ds.FirstRow().Item("Username"));
            Services.SetInformation("SessionID", ds.FirstRow().Item("SessionID"));
            UserID = ds.FirstRow().Item("UserID");
            UserName = ds.FirstRow().Item("Username");

            //var UserID = Services.GetInformation("UserID");

            var result = ds.FirstValue() + "";
            if (result.ToUpper() != "OK")
            {
                // Xóa Session khia user logout hoac tat phan mem.
                query = DataQuery.Create("Security", "ws_Session_Delete");

                ds = Services.Execute(query);

                if (ds == null)
                {
                    MessageBox.Show(Services.LastError);
                    return;
                }

                MessageBox.Show(this, result, Services.Localize("Login", "ErrorCaption"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();



        }

    }
}