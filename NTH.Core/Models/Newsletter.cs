using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Models
{
    public class Newsletter
    {
        public int Id { get; set; }
        public string? Lang { get; set; }
        public string? BannerImage { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? RegisterTitle { get; set; }
        public string? RegisterContent { get; set; }
    }
}
