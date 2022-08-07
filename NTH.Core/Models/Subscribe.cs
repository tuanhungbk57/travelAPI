using System;
using System.Collections.Generic;

namespace NTH.Core.Models
{
    /// <summary>
    /// Người đăng ký nhận bản tin
    /// </summary>
    public partial class Subscribe
    {
        public int? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
        public string? Phone { get; set; }
        public bool? IsSubscribe { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
