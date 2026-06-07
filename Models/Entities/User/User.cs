using Haflty.Core.Enum;

namespace Haflty.Models.Entities;

public class User
{
      public Guid Id { get; set; }

      public string Name { get; set; } = "";
      public string Email { get; set; } = "";
      public string Phone { get; set; } = "";
      public string UserName { get; set; } = "";
      public string Address { get; set; } = "";
      public UserRole UserRole { get; set; }
      public string HashPassword { get; set; } = "";
      public DateTime BirthDate { get; set; }
      public string QRCode { get; set; } = "";
      public List<Reservation.Reservation> Reservations { get; set; } = [];
      public List<UserRefreshToken> UserRefreshTokens { get; set; } = [];

      public DateTime CreatedDate { get; set; }
      public DateTime UpdatedDate { get; set; }
      public DateTime DeletedDate { get; set; }

}
