using System;
using System.Collections.Generic;

namespace NTH.Core.Models
{
    /// <summary>
    /// Thông tin người đăng ký du lịch
    /// </summary>
    public partial class Contact
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        /// <summary>
        /// Yêu cầu của người booking
        /// </summary>
        public string? ContactRequest { get; set; }
        /// <summary>
        /// Mã bưu điện
        /// </summary>
        public string? Postcode { get; set; }
        /// <summary>
        /// Vị trí người đặt
        /// </summary>
        public string? Location { get; set; }
        public string? Country { get; set; }
        /// <summary>
        /// Kênh tiềm năng thu hút người dùng
        /// </summary>
        public string? PotentialChannel { get; set; }
        public string? CreatedDate { get; set; }

    }
}
