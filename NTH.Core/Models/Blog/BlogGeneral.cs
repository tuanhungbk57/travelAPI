using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Models.Blog
{
    public class BlogGeneral
    {
        public int Id { get; set; }
        public string? Lang { get; set; }
        public string? TitleInfo { get; set; }
        public string? AdditionalInfor { get; set; }
        public string? ContentInfo { get; set; }
        public string? ImageURL { get; set; }
    }
}
