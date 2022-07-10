using System;
using System.Collections.Generic;

namespace NTH.Core.Models
{
    /// <summary>
    /// Bảng quan hệ của Destiantion và Trip
    /// </summary>
    public partial class DestinationTrip
    {
        public int Id { get; set; }
        public string? DesId { get; set; }
        public string? TripId { get; set; }
    }
}
