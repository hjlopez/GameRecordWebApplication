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
        }
    }
}