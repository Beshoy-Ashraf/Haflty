using Haflty.DTO.UserDto.Response;
using Microsoft.AspNetCore.Mvc;

namespace Haflty.Services.UserService.Interface;

public interface IUserService
{
      Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken);
      Task<List<UserDto>> GetUsersAsync(CancellationToken cancellationToken);
      Task<List<UserDto>> AdminUsers(CancellationToken cancellationToken, string[]? includes = null);
      Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken);
}
