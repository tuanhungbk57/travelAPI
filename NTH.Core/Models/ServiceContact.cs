using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Models
{
    public class ServiceContact
    {
        public int Id { get; set; }
        public string? Lang { get; set; }
        public string? Title { get; set; }
        public string? BannerImage { get; set; }
        public string? Content { get; set; }
        public string? CommunicationTitle { get; set; }
        public string? CommunicationImage { get; set; }
        public string? OurTeamTitle { get; set; }
        public string? OurTeamImage { get; set; }
        public string? WorktimeTitle { get; set; }
        public string? WorktimeImage { get; set; }
        public string? VisaTitle { get; set; }
        public string? VisaImage { get; set; }
        public string? QuestionTitle { get; set; }
        public string? QuestionImage { get; set; }
        public string? NewsLetterTitle { get; set; }
        public string? NewsLetterImage { get; set; }
    }
}
