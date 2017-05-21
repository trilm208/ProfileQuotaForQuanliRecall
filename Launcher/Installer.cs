using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Launcher
{
    class Installer
    {
        static InstallerCache cache;
        static InstallerFileInfo[] fileList = null;


        public static void Initialize()
        {
            cache = new InstallerCache();
            cache.Initialize(); 
        }

        public static InstallerFileInfo[] GetChangedFiles()
        {
            var list = new List<InstallerFileInfo>();
            var files = GetAllFiles();

            foreach (var file_src in files)
            {
                var file_dest = cache.GetFileInfo(file_src.Path, file_src.Name);

                if (file_src.Hash != file_dest.Hash)
                {
                    list.Add(file_src);
                }
            }

            return list.ToArray();
        }


        public static InstallerFileInfo[] GetAllFiles()
        {
            if (fileList == null)
            {
                var query = DataQuery.Create("Application", "ws_InstallerProgram_List",new
                {
                    SolutionName=AppConfig.SolutionName
                });
                var ds = Globals.Execute(query);
                var table = ds.FirstTable(); 
                if (table == null)
                {
                    fileList = new InstallerFileInfo[0];
                }
                else
                {
                    var list = new List<InstallerFileInfo>();
                    var rows = table.Select();
                    foreach (var row in rows)
                    {
                        var item = new InstallerFileInfo();
                        item.Path = row["Path"] + "";
                        item.Name = row["Name"] + "";
                        item.Extension = row["Extension"] + "";
                        item.Hash = row["Hash"] + "";
                        item.Version = row["Version"] + "";
                        item.Size = Convert.ToInt64(row["Size"]);
                        item.Description = row["Description"] + "";
                        item.ModifiedOn = Convert.ToDateTime(row["ModifiedOn"]);
                        list.Add(item); 
                    }
                    fileList = list.ToArray(); 
                }
            }

            return fileList;
        }


        public static byte[] GetFile(string path, string name)
        {
            var query = DataQuery.Create("Application", "ws_InstallerProgram_File", new {SolutionName=AppConfig.SolutionName, path, name });
            var ds = Globals.Execute(query);
            var data = ds.FirstValue();

            if (string.IsNullOrEmpty(data) == false)
            {
                return Convert.FromBase64String(data);
            }

            return new byte[0]; 
        }


        public static bool UpdateNeeded()
        {
            if (GetChangedFiles().Length > 0)
                return true;

            return false;
        }


        public static void UpdateCache()
        {
            cache.UpdateCache();
        }
    }
}
