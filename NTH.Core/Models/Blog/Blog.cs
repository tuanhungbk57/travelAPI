using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Models.Blog
{
    public class Blogg
    {
        public int Id { get; set; }
        public string? Lang { get; set; }
        public string? BlogName { get; set; }
        public string? BannerImage { get; set; }
        public int MasterId { get; set; }
        public string? BlogURL { get; set; }

    }
}
