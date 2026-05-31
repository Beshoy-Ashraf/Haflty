using Haflty.DTO.UserDto.Response;
using Haflty.Services.UserService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Haflty.Controllers.UserController;

[Route("api/user")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
      private readonly IUserService service = userService;

      [HttpGet("{id:guid}")]
      public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
      {
            try
            {
                  var result = await service.GetUserAsync(id, cancellationToken);
                  return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                  return NotFound();
            }
      }
      [HttpGet]
      public async Task<List<UserDto>> GetUsers(CancellationToken cancellationToken)
      {
            return await service.GetUsersAsync(cancellationToken);
      }


}
