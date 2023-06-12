using Microsoft.EntityFrameworkCore;
using SportNation2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNation2.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionEvent> CompetitionEvents { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Sport> Sports { get; set; }


        public AppDbContext(DbContextOptions opt) : base(opt)
        {
            try
            {
                Database.EnsureCreated();
                if (Database.GetPendingMigrations().Count() > 0)
                {
                    Database.Migrate();
                }
            }
            catch (Exception)
            {
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRole>();

            builder.Entity<Role>()
                .HasData(
                new Role { Id = 1, RoleName = "Admin" },
                new Role { Id = 2, RoleName = "Basic" }
                );
            builder.Entity<User>()
                .HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@sportnation.com",
                    HashedPassword = "F2D81A260DEA8A100DD517984E53C56A7523D96942A834B9CDC249BD4E8C7AA9"
                }
                );
            builder.Entity<UserRole>()
                .HasData(
                new UserRole { Id = 1, UserId = 1, RoleId = 1 },
                new UserRole { Id = 2, UserId = 1, RoleId = 2 }
                );

            builder.Entity<Sport>()
                .HasData(
                new Sport("Boxe") { Id = 1 },
                new Sport("Judo") { Id = 2 },
                new Sport("Lutte") { Id = 3 },
                new Sport("Natation") { Id = 4 },
                new Sport("Triathlon") { Id = 5 },
                new Sport("Badminton") { Id = 6 },
                new Sport("Plongeon") { Id = 7 },
                new Sport("Tennis") { Id = 8 },
                new Sport("Tennis de table") { Id = 9 },
                new Sport("Cyclisme") { Id = 10 }
                );
        }

    }
}
