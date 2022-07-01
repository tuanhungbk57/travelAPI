﻿using System;
using System.Collections.Generic;

namespace NTH.Core.Entities
{
    public partial class RefreshToken
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? TokenHash { get; set; }
        public string? TokenSalt { get; set; }
        public DateTime? Ts { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
