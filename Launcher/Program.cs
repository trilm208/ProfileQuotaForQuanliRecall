using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Launcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Initialize();

            if (Installer.UpdateNeeded())
            {
                var form = new frmUpdate();
                form.Initialize(); 
                Application.Run(form);

                Installer.UpdateCache();
            }

            try
            {
                var fileName = Path.Combine(Globals.Destination, AppConfig.Application);
                var info = new ProcessStartInfo();

                info.WorkingDirectory = Globals.Destination;
                info.FileName = fileName;

                Process.Start(info);
            }
            catch
            {
            }
        }


        static void Initialize()
        {
            if (AppConfig.UseLocalAppFolder)
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                path = Path.Combine(path, AppConfig.LocalAppFolder);

                Globals.Destination = path;
            }
            else
            {
                Globals.Destination = AppDomain.CurrentDomain.BaseDirectory;
            }

            if (Directory.Exists(Globals.Destination) == false)
            {
                Directory.CreateDirectory(Globals.Destination);
            }

            Globals.HttpDataServices = new HttpDataServices(AppConfig.WebService);

            Installer.Initialize(); 
        }
    }
}
