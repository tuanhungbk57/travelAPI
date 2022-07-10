using System;
using System.Collections.Generic;

namespace NTH.Core.Models
{
    public partial class Vihomepage
    {
        public uint Id { get; set; }
        public string? Title { get; set; }
        public string? Banner { get; set; }
        public string? PlanTitle { get; set; }
        public string? PlanContent { get; set; }
        public string? BookingCondition { get; set; }
        public string? Trips { get; set; }
        public string? TreatYourself { get; set; }
        public string? TreatContent { get; set; }
        public string? BlockQuote { get; set; }
        public string? BlockQuoteAuthor { get; set; }
        public string? OurDestination { get; set; }
    }
}
