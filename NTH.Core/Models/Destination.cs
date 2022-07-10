using System;
using System.Collections.Generic;

namespace NTH.Core.Models
{
    /// <summary>
    /// Các điểm đến (các quốc gia)
    /// </summary>
    public partial class Destination
    {
        public int Id { get; set; }
        public string? Lang { get; set; }
        public string? Country { get; set; }
        public string? BannerImage { get; set; }
        public string? BannerText { get; set; }
        public string? Thumbnail { get; set; }
        public string? Quickview { get; set; }
        public string? QuickviewContent { get; set; }
        public string? IndividualTripTitle { get; set; }
        public string? IndividualTripAdditional { get; set; }
        public string? IndividualTripContent { get; set; }
        public string? TripTitle { get; set; }
    }
}
