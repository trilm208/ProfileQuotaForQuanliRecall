using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Launcher
{
    public partial class frmUpdate : Form
    {
        InstallerFileInfo[] list;
        int finishCountDown = 5;


        public frmUpdate()
        {
            InitializeComponent();
        }


        public void Initialize()
        {
            this.list = Installer.GetChangedFiles();
        }


        private void finishTimer_Tick(object sender, EventArgs e)
        {
            if (finishCountDown <= 0)
            {
                finishTimer.Stop();
                cmdContinue_Click(null, null);
            }
            else
            {
                cmdContinue.Text = string.Format("Continue ({0})", finishCountDown);
                finishCountDown--;
            }
        }


        private void cmdContinue_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void startTimer_Tick(object sender, EventArgs e)
        {
            if (list != null)
            {
                startTimer.Stop();
                Thread updateThread = new Thread(UpdateThreadProc);
                updateThread.Start();
            }
        }


        private void UpdateThreadProc()
        {
            long complete = 0;
            long totalSize = list.Sum(v => v.Size);

            SetProgress(0, Convert.ToInt32(totalSize));

            foreach (var item in list)
            {
                BeginTask(item.Name);

                var data = Installer.GetFile(item.Path, item.Name);
                if (UpdateFile(item.Path, item.Name, data))
                {
                    CompleteTask(item.Name);
                }
                else
                {
                    FailedTask(item.Name);
                }

                complete += item.Size;
                SetProgress(Convert.ToInt32(complete), Convert.ToInt32(totalSize));
            }


            this.Invoke(() =>
            {
                try
                {
                    this.cmdContinue.Enabled = true;
                    this.cmdContinue.Focus();
                    this.finishTimer.Start();
                }
                catch
                {
                }
            });
        }


        private void SetProgress(int progress, int total)
        {
            this.Invoke(() =>
            {
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = total;
                this.progressBar1.Value = progress;
            });
        }


        private void BeginTask(string name)
        {
            this.Invoke(() =>
            {
                this.txtTask.Text = "Updating: " + name;
            });
        }


        private void FailedTask(string name)
        {
            this.Invoke(() =>
            {
                this.txtTask.Text = "Updating: done";
                int i = this.listLog.Items.Add("Failed: " + name);
                this.listLog.SelectedIndex = i;
            });
        }


        private void CompleteTask(string name)
        {
            this.Invoke(() =>
            {
                this.txtTask.Text = "Updating: done";
                int i = this.listLog.Items.Add("Updated: " + name);
                this.listLog.SelectedIndex = i;
            });
        }


        private void Invoke(Action action)
        {
            base.Invoke(action);
        }


        private bool UpdateFile(string folder, string fileName, byte[] data)
        {
            var path = Path.Combine(Globals.Destination, folder);
            var fullName = Path.Combine(path, fileName);
            var fileInfo = new FileInfo(fullName);

            try
            {
                if (fileInfo.Directory.Exists == false)
                {
                    fileInfo.Directory.Create();
                }

                File.WriteAllBytes(fullName, data);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
