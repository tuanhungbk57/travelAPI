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
        public int TripMasterId { get; set; }
        
        public string? TripDetailName { get; set; }
        public string? ImageURL { get; set; }
        public string? DestinationURL { get; set; }
        public string? TripURL { get; set; }
        public string? QuickView { get; set; }
        public string? QuickViewContent { get; set; }
        public string? Lang { get; set; }
    }
}
