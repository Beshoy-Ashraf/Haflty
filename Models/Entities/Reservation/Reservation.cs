using Haflty.Core.Enum;

namespace Haflty.Models.Entities.Reservation;

public class Reservation
{
      public Guid Id { get; set; }
      public Guid UserId { get; set; }
      public User User { get; set; } = new User { };

      public DateTime StartDateTime { get; set; }

      public DateTime EndDateTime { get; set; }

      public decimal TotalPrice { get; set; }

      public ReservationStatus Status { get; set; }
}
