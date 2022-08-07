﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTH.Core.Models
{
    [Table("aspnetusers")]
    public partial class Aspnetuser
    {
        public Aspnetuser()
        {
            Aspnetuserclaims = new HashSet<Aspnetuserclaim>();
            Aspnetuserlogins = new HashSet<Aspnetuserlogin>();
            Aspnetusertokens = new HashSet<Aspnetusertoken>();
            Roles = new HashSet<Aspnetrole>();
        }

        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public sbyte EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public sbyte PhoneNumberConfirmed { get; set; }
        public sbyte TwoFactorEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public sbyte LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<Aspnetuserclaim> Aspnetuserclaims { get; set; }
        public virtual ICollection<Aspnetuserlogin> Aspnetuserlogins { get; set; }
        public virtual ICollection<Aspnetusertoken> Aspnetusertokens { get; set; }

        public virtual ICollection<Aspnetrole> Roles { get; set; }
    }
}
