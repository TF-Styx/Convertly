using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertly.Model.Data
{
    class FileInformation
    {
        public string FileUri { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileResolution { get; set; }
        public string FileSize { get; set; }
        public string FileCreatTime { get; set;}
        public string FIleTimeOfChange { get; set; }
        public string FileOwner { get; set; }
    }
}
