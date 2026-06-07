namespace Haflty.Models.Entities;

public class UserRefreshToken
{
      public Guid Id { get; set; }

      public required string Token { get; set; }
      public DateTime ExpiresOn { get; set; }
      public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
      public DateTime CreatedOn { get; set; }
      public DateTime? RevokedOn { get; set; }
      public bool IsActive => RevokedOn != null && !IsExpired;
      public Guid UserId { get; set; }
      public User? User { get; set; } // Navigation property

}
