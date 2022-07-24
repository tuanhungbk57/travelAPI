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
        public string? TripName { get; set; }
        public string? TripURL { get; set; }
        public string? ImageURL { get; set; }
        public string? DestinationURL { get; set; }
    }
    
}
