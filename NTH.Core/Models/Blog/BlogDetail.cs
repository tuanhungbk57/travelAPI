using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Models.Blog
{
    public class BlogDetail
    {
        public int Id { get; set; }
        public string? BlogDetailName { get; set; }
        public string? BlogDetailURL { get; set; }
        public string? ImageURL { get; set; }
        public string? BlogURL { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
