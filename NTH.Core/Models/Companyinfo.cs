using System;
using System.Collections.Generic;

namespace NTH.Core.Models
{
    /// <summary>
    /// Thông tin công ty
    /// </summary>
    public partial class Companyinfo
    {
        public int Id { get; set; }
        /// <summary>
        /// Giám đốc điều hành
        /// </summary>
        public string? Ceo { get; set; }
        public string? CeoAvatar { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? CompanyName { get; set; }
        public string? Logo { get; set; }
    }
}
