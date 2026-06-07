using System.Text.Json.Serialization;

namespace Haflty.DTO.UserDto.Response;

public class TokenResponseDto
{
      public string Token { get; set; } = "";
      public string UserId { get; set; } = "";
      public DateTime ExpireOne { get; set; }
      [JsonIgnore]
      public string? RefreshToken { get; set; }


}
