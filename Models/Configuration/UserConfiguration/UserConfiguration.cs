using Haflty.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Haflty.Models.Configuration.UserConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
      public void Configure(EntityTypeBuilder<User> builder)
      {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.UserName).IsRequired();
            builder.HasIndex(s => s.UserName).IsUnique();
            builder.Property(s => s.HashPassword).IsRequired();
      }
}
