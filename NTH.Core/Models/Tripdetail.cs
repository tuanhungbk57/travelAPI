using System;
using System.Collections.Generic;

namespace NTH.Core.Models
{
    /// <summary>
    /// Chi tiết chuyến đi
    /// </summary>
    public partial class Tripdetail
    {
        public int Id { get; set; }
        public string? TripDetailName { get; set; }
        public string? BannerText { get; set; }
        public string? BannerImage { get; set; }
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
    }
}
