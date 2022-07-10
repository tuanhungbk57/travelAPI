using System;
using System.Collections.Generic;

namespace NTH.Core.Models
{
    /// <summary>
    /// Trang chủ
    /// </summary>
    public partial class Homepage
    {
        public int Id { get; set; }
        public string? Lang { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        /// <summary>
        /// Sơ lược về trang chủ du lịch
        /// </summary>
        public string? Quickview { get; set; }
        public string? QuickviewContent { get; set; }
        /// <summary>
        /// Tiêu đề các chuyến đi riêng lẻ
        /// </summary>
        public string? IndividualTripTitle { get; set; }
        /// <summary>
        /// Tiêu đề bổ sung các chuyến đi riêng lẻ
        /// </summary>
        public string? IndividualTripAdditional { get; set; }
        /// <summary>
        /// Nội dung mô tả các chuyến đi riêng lẻ
        /// </summary>
        public string? IndividualTripContent { get; set; }
        /// <summary>
        /// Tiêu đề các điểm đến
        /// </summary>
        public string? DestinationTitle { get; set; }
        /// <summary>
        /// Nội dung điểm đến
        /// </summary>
        public string? DestinationContent { get; set; }
        /// <summary>
        /// Tiêu đề bổ sung dẫn tới trang đăng ký
        /// </summary>
        public string? NewsletterContent { get; set; }
        public string? BtnRegister { get; set; }
        /// <summary>
        /// Ưu đãi độc quyền
        /// </summary>
        public string? ExclusiveOfferTitle { get; set; }
        /// <summary>
        /// Tiêu đề trước video
        /// </summary>
        public string? VideoTitle { get; set; }
        public string? VideoScript { get; set; }
        public string? PillarTravel { get; set; }
    }
}
