using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using SITS.BNS.Entities.Entity;
using SITS.BNS.Entities.Interfaces;

namespace SITS.BNS.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService, IConfiguration configuration) : DbContext(options)
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private IDbContextTransaction? _currentTransaction;
        protected readonly IConfiguration Configuration = configuration;

        public required string CurrentUserId { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<AidDetail> AidDetail { get; set; }
        public DbSet<FamilyMember> FamilyMember { get; set; }
        public DbSet<Family> Family { get; set; }
        public DbSet<GramaOffice> GramaOffice { get; set; }
        public DbSet<AuditLog> AuditLog { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>().Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Entity<AppUser>().ToTable($"UIA_{nameof(this.AppUsers)}");

            builder.Entity<AppRole>().Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Entity<AppRole>().ToTable($"UIA_{nameof(this.AppRoles)}");

            builder.Entity<AppUserRole>().Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Entity<AppUserRole>().ToTable($"UIA_{nameof(this.AppUserRoles)}");

            builder.Entity<RefreshToken>().Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Entity<RefreshToken>().ToTable($"UIA_{nameof(this.RefreshToken)}");

            builder.Entity<GramaOffice>().Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Entity<GramaOffice>().ToTable($"UIA_{nameof(this.GramaOffice)}");

            builder.Entity<Family>().Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Entity<Family>().ToTable($"UIA_{nameof(this.Family)}");

            builder.Entity<FamilyMember>().Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Entity<FamilyMember>().ToTable($"UIA_{nameof(this.FamilyMember)}");

            builder.Entity<AuditLog>().Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Entity<AuditLog>().ToTable($"UIA_{nameof(this.AuditLog)}");

            builder.Entity<AidDetail>().Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Entity<AidDetail>().ToTable($"UIA_{nameof(this.AidDetail)}");

            builder.Entity<FamilyMember>().HasOne(fm => fm.AidDetail).WithOne(ad => ad.FamilyMember).HasForeignKey<AidDetail>(ad => ad.MemberId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries<CommonEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedByName = _currentUserService.Name;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService.UserId;
                        entry.Entity.UpdatedDate = DateTime.Now;
                        entry.Entity.UpdatedByName = _currentUserService.Name;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
