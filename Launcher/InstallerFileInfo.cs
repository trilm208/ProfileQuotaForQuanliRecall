using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Launcher
{
    [Serializable] 
    class InstallerFileInfo
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Hash { get; set; }
        public string Version { get; set; }
        public long Size { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
