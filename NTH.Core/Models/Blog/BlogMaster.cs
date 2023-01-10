using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Models.Blog
{
    public class BlogMaster
    {
        public int Id { get; set; }
        public string? ImageURL { get; set; }
        public string? BlogName { get; set; }
        public string? BlogURL { get; set; }
    }
}
