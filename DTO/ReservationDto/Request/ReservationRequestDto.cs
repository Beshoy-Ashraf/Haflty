using Haflty.Core.Enum;

namespace Haflty.DTO.ReservationDto.Request;

public class ReservationRequestDto
{

      public DateTime StartDateTime { get; set; }

      public DateTime EndDateTime { get; set; }

      public decimal TotalPrice { get; set; }

      public ReservationStatus Status { get; set; }
      public Guid UserId { get; set; }
    
}
