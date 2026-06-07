using Haflty.DTO.UserDto.Request;
using Haflty.DTO.UserDto.Response;

namespace Haflty.Services.AuthService.Interface;

public interface IAuthService
{
      Task<TokenResponseDto> GenerateUser(RegisterUserDto
 UserDto, CancellationToken cancellationToken);
      Task<TokenResponseDto> RefreshTokenAsync(string token, CancellationToken cancellationToken);
      Task<TokenResponseDto> UserLogin(UserLoginDto userData, CancellationToken cancellationToken);

}
