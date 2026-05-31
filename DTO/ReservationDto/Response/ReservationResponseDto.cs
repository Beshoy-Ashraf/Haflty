using Haflty.Core.Enum;

namespace Haflty.DTO.ReservationDto.Response;

public class ReservationResponseDto
{
      public Guid UserId { get; set; }

      public DateTime StartDateTime { get; set; }

      public DateTime EndDateTime { get; set; }

      public decimal TotalPrice { get; set; }

      public ReservationStatus Status { get; set; }
      public string QRCode { get; set; } = "";
      public string UserFullName { get; set; } = "";
      public string UserEmail { get; set; } = "";
      public string UserPhone { get; set; } = "";

}
