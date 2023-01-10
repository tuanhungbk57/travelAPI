using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Models.Blog
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string? Lang { get; set; }
        public string? BlogPostName { get; set; }
        public string? ImageURL { get; set; }
        public string? BlogDetailURL { get; set; }
        public int BlogDetailID { get; set; }
        public string? Content { get; set; }
        public string? QuickViewContent { get; set; }
        public string? QuickView { get; set; }


    }
}
