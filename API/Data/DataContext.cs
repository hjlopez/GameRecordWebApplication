using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Photo> Photo { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<GameTypes> GameTypes { get; set; }
        public DbSet<Tournament> Tournament { get; set; }
        public DbSet<TournamentMembers> TournamentMembers { get; set; }
        public DbSet<BilliardsMatchType> BilliardsMatchTypes { get; set; }
        public DbSet<TournamentMatchType> TournamentMatchTypes { get; set; }
        public DbSet<BilliardsMode> BilliardsModes { get; set; }
        public DbSet<TournamentMode> TournamentModes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // creating user entity
            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            
            // creating role entity
            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            
            // games entity
            builder.Entity<Games>()
                .HasKey(k => k.Id);
            
            builder.Entity<Games>()
                .HasOne(gt => gt.GameTypes)
                .WithOne(g => g.Games)
                .HasForeignKey<GameTypes>(gt => gt.Id)
                .IsRequired();
                
            // games type entity
            builder.Entity<GameTypes>()
                .HasKey(k => k.Id);

            builder.Entity<Tournament>()
                .HasKey(k => k.Id);

            builder.Entity<Tournament>()
                .HasMany(tm => tm.TournamentMembers)
                .WithOne(t => t.Tournament)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TournamentMembers>()
                .HasKey(k => k.Id);

            builder.Entity<BilliardsMatchType>()
                .HasKey(b => b.Id);

            builder.Entity<TournamentMatchType>()
                .HasKey(b => b.Id);
                
            builder.Entity<TournamentMatchType>()
                .HasOne(s => s.BilliardsMatchTypes)
                .WithMany(t => t.TournamentMatchType)
                .HasForeignKey(s => s.MatchTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TournamentMatchType>()
                .HasOne(s => s.Tournament)
                .WithMany(t => t.TournamentMatchTypes)
                .HasForeignKey(s => s.TournamentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Entity<BilliardsMode>()
                .HasKey(b => b.Id);

            builder.Entity<TournamentMode>()
                .HasKey(b => b.Id);
                
            builder.Entity<TournamentMode>()
                .HasIndex(x => x.Order)
                .IsUnique();
            
            builder.Entity<TournamentMode>()
                .HasIndex(x => x.HighestRank)
                .IsUnique();
            
            builder.Entity<TournamentMode>()
                .HasOne(s => s.BilliardsMode)
                .WithMany(t => t.TournamentMode)
                .HasForeignKey(s => s.ModeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}