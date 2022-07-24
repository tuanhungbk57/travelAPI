using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Models
{
    public class Tourdetail
    {
        public int Id { get; set; }
        public string? Lang { get; set; }
        public string? TourName { get; set; }
        public string? TripPath { get; set; }
        public string? DesPath { get; set; }
        public string? ImageURL { get; set; }
        public string? TourPath { get; set; }
        public string? Content { get; set; }
        public string? QuickView { get; set; }
        public string? QuickViewContent { get; set; }
        public int TourMasterId { get; set; }
    }
}
