using Haflty.Models.Modules.User;
using Microsoft.EntityFrameworkCore;

namespace Haflty.Models.Context;

public class AppDBContext(DbContextOptions options) : DbContext(options)
{

      public DbSet<User> Users { get; set; }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
      }
}
