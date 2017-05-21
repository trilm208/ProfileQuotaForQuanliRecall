using System;
using System.Windows.Forms;

namespace QA
{
    public class UI
    {
        public static void EmailSent()
        {
            string message = "Email Sent";
            MessageBox.Show(message, "Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void InformationSaved()
        {
            string message = "Đã lưu";
            MessageBox.Show(message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void InformationDeleted()
        {
            string message = "Đã xóa";
            MessageBox.Show(message, "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowException(Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

        public static bool ConfirmDelete()
        {
            string message = "Are you really delete the item?";
            message += "\nYou CANNOT undo deleted item.";

            var res = MessageBox.Show(message, "Delete", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// hungnq 2014-10-03
        /// </summary>
        /// <returns></returns>
        public static bool Confirm(string mess)
        {
            var res = MessageBox.Show(mess, "Confirm", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        public static bool Confirm(string confirm, string mess)
        {
            var res = MessageBox.Show(mess, confirm, MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        public static bool ConfirmDelete(string itemName)
        {
            if (itemName.Trim().Length == 0)
            {
                return ConfirmDelete();
            }

            string message = string.Format("Bạn muốn xóa '{0}'?", itemName);
            message += "\nBạn sẽ KHÔNG thể khôi phục lại dữ liệu đã xóa.";

            var res = MessageBox.Show(message, "Delete", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        public static void WebSaveInformation()
        {
        }
    }
}