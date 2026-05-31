using System.Net.Quic;
using Haflty.Core.Enum;

namespace Haflty.DTO.UserDto.Request;

public class RegisterUserDto
{
      public string Name { get; set; } = "";
      public string Email { get; set; } = "";
      public string Phone { get; set; } = "";
      public string UserName { get; set; } = "";
      public string Address { get; set; } = "";
      public UserRole UserRole { get; set; }
      public string HashPassword { get; set; } = "";
      public DateTime BirthDate { get; set; }
      public string QRCode { get; set; } = "";

}
