using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Models
{
    public class FolderImage
    {
        public uint Id { get; set; }
        public string? FolderName { get; set; }
        public string? ImageName { get; set; }
        public int? FolderId { get; set; }
        public int? ParentId { get; set; }
        public int? FolderType { get; set; }
    }
}
