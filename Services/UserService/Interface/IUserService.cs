using Haflty.DTO.UserDto.Response;

namespace Haflty.Services.UserService.Interface;

public interface IUserService
{
      Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken);
      Task<List<UserDto>> GetUsersAsync(CancellationToken cancellationToken);
}
