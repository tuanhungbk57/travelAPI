using System;
using System.Collections.Generic;

namespace NTH.Core.Entities
{
    public partial class User
    {
        public string UserId { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PasswordSalt { get; set; }
        public string? FullName { get; set; }
        public DateTime? Ts { get; set; }
        public ulong? IsActived { get; set; }
    }
}
