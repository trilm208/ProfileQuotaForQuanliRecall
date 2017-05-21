using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Launcher
{
    class InstallerCache
    {
        private List<InstallerFileInfo> files = new List<InstallerFileInfo>();


        public IEnumerable<InstallerFileInfo> Files
        {
            get { return files; }
        }


        public void Initialize()
        {
            var fileName = Path.Combine(Globals.Destination, "Installer.dat");
            if (File.Exists(fileName))
            {
                var binary = File.ReadAllBytes(fileName);
                files = Serializer.FromBinary<List<InstallerFileInfo>>(binary);  
            }
        }


        public InstallerFileInfo GetFileInfo(string folder, string fileName)
        {
            var path = Path.Combine(Globals.Destination, folder);
            fileName = Path.Combine(path, fileName);

            var info = files.Where(v => v.Name == fileName).FirstOrDefault();
            if (info == null)
            {
                info = new InstallerFileInfo();
                info.Name = fileName;

                var fileInfo = new FileInfo(fileName);
                if (fileInfo.Exists)
                {
                    info.Hash = ComputeHash(fileName);
                    info.ModifiedOn = fileInfo.LastWriteTimeUtc;
                    info.Size = fileInfo.Length;
                }
                else
                {
                    info.Hash = string.Empty;
                    info.ModifiedOn = DateTime.MinValue;
                    info.Size = 0;
                }

                files.Add(info);
            }

            return info;
        }


        public void UpdateCache()
        {
            var all = GetFiles(Globals.Destination);

            files.Clear();
            foreach (var fileName in all)
            {
                var info = new FileInfo(fileName);  
                var file = new InstallerFileInfo();

                file.Path = GetLocalPath(info.DirectoryName);
                file.Name = info.Name;
                file.Size = info.Length;
                file.ModifiedOn = info.LastWriteTimeUtc;
                file.Hash = ComputeHash(fileName);

                files.Add(file);
            }

            var binary = Serializer.ToBinary(files);
            File.WriteAllBytes(Path.Combine(Globals.Destination, "Installer.dat"), binary);
        }


        private string GetLocalPath(string path)
        {
            var localPath = path.Substring(Globals.Destination.Length - 1);
            return localPath;
        }


        IEnumerable<string> GetFiles(string path)
        {
            var files = Directory.GetFiles(path);
            var folders = Directory.GetDirectories(path);

            foreach (var item in files)
                yield return item;

            foreach (var item in folders)
            {
                var files2 = GetFiles(Path.Combine(path, item));
                foreach (var file in files2)
                    yield return file;
            }
        }


        private string ComputeHash(string fileName)
        {
            var raw = File.ReadAllBytes(fileName);
            return GetMd5Hash(raw);
        }


        private string GetMd5Hash(byte[] input)
        {
            var md5 = MD5.Create();
            var sb = new StringBuilder();

            byte[] hash = md5.ComputeHash(input);
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

    }
}
