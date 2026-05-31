using Haflty.Core.Enum;
using Haflty.Migrations;
using Haflty.Models.Entities;

namespace Haflty.DTO.UserDto.Response;

public class UserDto(User user)
{
      public Guid Id { get; set; } = user.Id;

      public string Name { get; set; } = user.Name;
      public string Email { get; set; } = user.Email;
      public string Phone { get; set; } = user.Phone;
      public string UserName { get; set; } = user.UserName;
      public string Address { get; set; } = user.Address;
      public UserRole UserRole { get; set; } = user.UserRole;
      public DateTime BirthDate { get; set; } = user.BirthDate;
      public string QRCode { get; set; } = user.QRCode;



}
