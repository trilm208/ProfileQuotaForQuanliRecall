using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Shell
{
    class ReportUtility
    {
        public static void ClearReports()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Reports");

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            var files = Directory.GetFiles(path, "*.pdf");
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                }
            }
        }


        public static void CreateAndOpen(string fileData, string fileExtension)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Reports");
           // var fileName = path + newguid + "." + fileExtension;
            var fileName = path + Guid.NewGuid().ToString() + "." + fileExtension;

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            var bytes = Convert.FromBase64String(fileData);
            File.WriteAllBytes(fileName, bytes);

            Process.Start(fileName);
        }

        public static void Create(string fileData, string fileExtension, string newguid)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Reports");
            var fileName = path + newguid + "." + fileExtension;
            //var fileName = path + Guid.NewGuid().ToString() + "." + fileExtension;

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            var bytes = Convert.FromBase64String(fileData);
            File.WriteAllBytes(fileName, bytes);

            //Process.Start(fileName);
        }
    }
}
