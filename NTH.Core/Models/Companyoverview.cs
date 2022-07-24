using System;
using System.Collections.Generic;

namespace NTH.Core.Models
{
    /// <summary>
    /// Thông tin đa ngôn ngữ tổng quát của công ty
    /// </summary>
    public partial class Companyoverview
    {
        public int Id { get; set; }
        public string? Lang { get; set; }
        /// <summary>
        /// Câu quote của công ty
        /// </summary>
        public string? BlockQuote { get; set; }
        /// <summary>
        /// Chức vụ
        /// </summary>
        public string? Position { get; set; }
        /// <summary>
        /// Chuyên đề
        /// </summary>
        public string? Specialism { get; set; }
        public string? specialismImage { get; set; }
        public string? SpecialismContent { get; set; }
        /// <summary>
        /// Dịch vụ cá nhân
        /// </summary>
        public string? PersonalService { get; set; }
        public string? PersonalServiceImage { get; set; }
        public string? PersonalServiceContent { get; set; }
        /// <summary>
        /// Du lịch tùy chỉnh
        /// </summary>
        public string? CustomizeTour { get; set; }
        public string? CustomizeTourImage { get; set; }
        public string? CustomizeTourContent { get; set; }
        /// <summary>
        /// Du lịch có trách nhiệm
        /// </summary>
        public string? ResponsibleTravel { get; set; }
        public string? ResponsibleTravelImage { get; set; }
        public string? ResponsibleTravelContent { get; set; }
        /// <summary>
        /// Tiêu đề mời liên hệ
        /// </summary>
        public string? ContactMe { get; set; }
        public string? Phone { get; set; }
        /// <summary>
        /// Giờ làm việc
        /// </summary>
        public string? WorkTime { get; set; }
        /// <summary>
        /// Bản tin chân trang
        /// </summary>
        public string? NewsletterContent { get; set; }
        public string? MoreInfo { get; set; }
        public string? EndTitle { get; set; }
        public string? EndContent { get; set; }
        public int? Status { get; set; }

    }
}
