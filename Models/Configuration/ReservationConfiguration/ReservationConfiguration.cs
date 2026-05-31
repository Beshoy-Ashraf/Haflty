using Haflty.Models.Entities.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Haflty.Models.Configuration.ReservationConfiguration;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
      public void Configure(EntityTypeBuilder<Reservation> builder)
      {
            builder.HasKey(i => i.Id);
            builder.HasIndex(i => i.UserId).IsUnique();

            builder.HasOne(x => x.User)
            .WithMany(i => i.Reservations)
            .HasForeignKey(f => f.UserId);
      }
}
