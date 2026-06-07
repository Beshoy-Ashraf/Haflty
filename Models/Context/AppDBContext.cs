using Haflty.Models.Entities;
using Haflty.Models.Entities.Reservation;
using Microsoft.EntityFrameworkCore;

namespace Haflty.Models.Context;

public class AppDBContext(DbContextOptions options) : DbContext(options)
{

      public DbSet<User> Users { get; set; }
      public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
      public DbSet<Reservation> Reservations { get; set; }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
      }
}
