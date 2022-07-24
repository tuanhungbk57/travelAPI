using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TripPath { get; set; }
        public string? DesPath { get; set; }
        public string? ImageURL { get; set; }
        public string? TourPath { get; set; }
        public int TripId { get; set; }
    }
}
