using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using NTH.Core.Models;

namespace NTH.Core.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
       
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        private readonly IConfiguration? _configuration;
        private readonly string? _connectionString;
        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnStr");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=12345678@Abc;database=travel", ServerVersion.AutoDetect("server=localhost;port=3306;user=root;password=12345678@Abc;database=travel"));
            }
        }

        public virtual DbSet<Aspnetrole> Aspnetroles { get; set; } = null!;
        public virtual DbSet<Aspnetroleclaim> Aspnetroleclaims { get; set; } = null!;
        public virtual DbSet<Aspnetuser> Aspnetusers { get; set; } = null!;
        public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; } = null!;
        public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; } = null!;
        public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; } = null!;
        public virtual DbSet<Companyinfo> Companyinfos { get; set; } = null!;
        public virtual DbSet<Companyoverview> Companyoverviews { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Destination> Destinations { get; set; } = null!;
        public virtual DbSet<DestinationTrip> DestinationTrips { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<Homepage> Homepages { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public virtual DbSet<Subscribe> Subscribes { get; set; } = null!;
        public virtual DbSet<Trip> Trips { get; set; } = null!;
        public virtual DbSet<Tripdetail> Tripdetails { get; set; } = null!;
        public virtual DbSet<Tripitem> Tripitems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Folder> Folders { get; set; } = null!;
        public virtual DbSet<FolderImage> FolderImages { get; set; } = null!;
        public virtual DbSet<Vihomepage> Vihomepages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Aspnetrole>(entity =>
            {
                entity.ToTable("aspnetroles");

                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<Aspnetroleclaim>(entity =>
            {
                entity.ToTable("aspnetroleclaims");

                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Aspnetroleclaims)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
            });

            modelBuilder.Entity<Aspnetuser>(entity =>
            {
                entity.ToTable("aspnetusers");

                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEnd).HasMaxLength(6);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "Aspnetuserrole",
                        l => l.HasOne<Aspnetrole>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                        r => r.HasOne<Aspnetuser>().WithMany().HasForeignKey("UserId").HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("aspnetuserroles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<Aspnetuserclaim>(entity =>
            {
                entity.ToTable("aspnetuserclaims");

                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Aspnetuserclaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
            });

            modelBuilder.Entity<Aspnetuserlogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("aspnetuserlogins");

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Aspnetuserlogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
            });

            modelBuilder.Entity<Aspnetusertoken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("aspnetusertokens");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Aspnetusertokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
            });

            modelBuilder.Entity<Companyinfo>(entity =>
            {
                entity.ToTable("companyinfo");

                entity.HasComment("Thông tin công ty");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Ceo)
                    .HasMaxLength(255)
                    .HasComment("Giám đốc điều hành")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);
            });

            modelBuilder.Entity<Companyoverview>(entity =>
            {
                entity.ToTable("companyoverview");

                entity.HasComment("Thông tin đa ngôn ngữ tổng quát của công ty");

                entity.Property(e => e.BlockQuote)
                    .HasColumnType("text")
                    .HasComment("Câu quote của công ty")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.CeoAvatar).HasMaxLength(255);

                entity.Property(e => e.ContactMe)
                    .HasColumnType("text")
                    .HasComment("Tiêu đề mời liên hệ")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.CustomizeTour)
                    .HasColumnType("text")
                    .HasComment("Du lịch tùy chỉnh")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.CustomizeTourContent)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Lang).HasMaxLength(255);

                entity.Property(e => e.MoreInfo)
                    .HasMaxLength(255)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.NewsletterContent)
                    .HasColumnType("text")
                    .HasComment("Bản tin chân trang")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.PersonalService)
                    .HasColumnType("text")
                    .HasComment("Dịch vụ cá nhân")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.PersonalServiceContent)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Phone).HasColumnType("text");

                entity.Property(e => e.Position)
                    .HasColumnType("text")
                    .HasComment("Chức vụ")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.ResponsibleTravel)
                    .HasColumnType("text")
                    .HasComment("Du lịch có trách nhiệm")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.ResponsibleTravelContent)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Specialism)
                    .HasColumnType("text")
                    .HasComment("Chuyên đề")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.SpecialismContent)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.WorkTime)
                    .HasColumnType("text")
                    .HasComment("Giờ làm việc")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("contact");

                entity.HasComment("Thông tin người đăng ký du lịch");

                entity.Property(e => e.ContactRequest)
                    .HasMaxLength(255)
                    .HasComment("Yêu cầu của người booking");

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .HasComment("Vị trí người đặt");

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.Postcode)
                    .HasMaxLength(255)
                    .HasComment("Mã bưu điện");

                entity.Property(e => e.PotentialChannel)
                    .HasMaxLength(255)
                    .HasComment("Kênh tiềm năng thu hút người dùng");
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.ToTable("destination");

                entity.HasComment("Các điểm đến (các quốc gia)");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BannerImage).HasMaxLength(255);

                entity.Property(e => e.BannerText)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Country)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.IndividualTripAdditional).HasColumnType("text");

                entity.Property(e => e.IndividualTripContent).HasColumnType("text");

                entity.Property(e => e.IndividualTripTitle)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Lang).HasMaxLength(255);

                entity.Property(e => e.Quickview)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.QuickviewContent)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Thumbnail).HasMaxLength(255);

                entity.Property(e => e.TripTitle).HasColumnType("text");
            });

            modelBuilder.Entity<DestinationTrip>(entity =>
            {
                entity.ToTable("destination_trip");

                entity.HasComment("Bảng quan hệ của Destiantion và Trip");

                entity.Property(e => e.DesId).HasMaxLength(255);

                entity.Property(e => e.TripId).HasMaxLength(255);
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Homepage>(entity =>
            {
                entity.ToTable("homepage");

                entity.HasComment("Trang chủ");

                entity.Property(e => e.BtnRegister)
                    .HasMaxLength(200)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.DestinationContent)
                    .HasColumnType("text")
                    .HasComment("Nội dung điểm đến")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.DestinationTitle)
                    .HasColumnType("text")
                    .HasComment("Tiêu đề các điểm đến")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.ExclusiveOfferTitle)
                    .HasColumnType("text")
                    .HasComment("Ưu đãi độc quyền")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(300)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.IndividualTripAdditional)
                    .HasColumnType("text")
                    .HasComment("Tiêu đề bổ sung các chuyến đi riêng lẻ")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.IndividualTripContent)
                    .HasColumnType("text")
                    .HasComment("Nội dung mô tả các chuyến đi riêng lẻ")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.IndividualTripTitle)
                    .HasColumnType("text")
                    .HasComment("Tiêu đề các chuyến đi riêng lẻ")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Lang).HasMaxLength(255);

                entity.Property(e => e.NewsletterContent)
                    .HasColumnType("text")
                    .HasComment("Tiêu đề bổ sung dẫn tới trang đăng ký")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.PillarTravel)
                    .HasMaxLength(255)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Quickview)
                    .HasColumnType("text")
                    .HasComment("Sơ lược về trang chủ du lịch")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.QuickviewContent)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Title)
                    .HasMaxLength(500)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.VideoScript)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.VideoTitle)
                    .HasColumnType("text")
                    .HasComment("Tiêu đề trước video")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("refresh_token");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.TokenHash)
                    .HasMaxLength(1000)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.TokenSalt)
                    .HasMaxLength(45)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Ts)
                    .HasColumnType("datetime")
                    .HasColumnName("TS");

                entity.Property(e => e.UserId)
                    .HasMaxLength(45)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");
            });

            modelBuilder.Entity<Subscribe>(entity =>
            {
                entity.ToTable("subscribe");

                entity.HasComment("Người đăng ký nhận bản tin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("trip");

                entity.HasComment("Các chuyến đi");

                entity.Property(e => e.BannerImage).HasColumnType("text");

                entity.Property(e => e.BannerText).HasColumnType("text");

                entity.Property(e => e.IndividualTripTitle).HasColumnType("text");

                entity.Property(e => e.Lang).HasMaxLength(255);

                entity.Property(e => e.Thumbnail).HasColumnType("text");

                entity.Property(e => e.TripName).HasColumnType("text");
            });

            modelBuilder.Entity<Tripdetail>(entity =>
            {
                entity.ToTable("tripdetail");

                entity.HasComment("Chi tiết chuyến đi");

                entity.Property(e => e.BannerImage).HasMaxLength(555);

                entity.Property(e => e.BannerText).HasColumnType("text");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Thumbnail).HasMaxLength(255);

                entity.Property(e => e.TripDetailName).HasColumnType("text");
            });

            modelBuilder.Entity<Tripitem>(entity =>
            {
                entity.ToTable("tripitem");

                entity.HasComment("Bảng chi tiết chuyến đi");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(555)
                    .HasColumnName("ImageURL");
            });

            modelBuilder.Entity<Folder>(entity => {
                entity.ToTable("folder");
            });
            modelBuilder.Entity<FolderImage>(entity => {
                entity.ToTable("folderimage");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .HasMaxLength(45)
                    .HasColumnName("userId");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .HasColumnName("email")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasColumnName("fullName")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.IsActived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("isActived");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(255)
                    .HasColumnName("passwordSalt")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Ts)
                    .HasColumnType("datetime")
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<Vihomepage>(entity =>
            {
                entity.ToTable("vihomepage");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned zerofill")
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8mb3");
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
