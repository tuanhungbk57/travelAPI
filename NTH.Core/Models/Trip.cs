using System;
using System.Collections.Generic;

namespace NTH.Core.Models
{
    /// <summary>
    /// Các chuyến đi
    /// </summary>
    public partial class Trip
    {
        public int Id { get; set; }
        public string? Lang { get; set; }
        public string? TripName { get; set; }
        public string? BannerText { get; set; }
        public string? BannerImage { get; set; }
        public string? Thumbnail { get; set; }
        public string? IndividualTripTitle { get; set; }
    }
}
