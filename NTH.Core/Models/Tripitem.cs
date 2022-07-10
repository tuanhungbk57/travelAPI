using System;
using System.Collections.Generic;

namespace NTH.Core.Models
{
    /// <summary>
    /// Bảng chi tiết chuyến đi
    /// </summary>
    public partial class Tripitem
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
    }
}
