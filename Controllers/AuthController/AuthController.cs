using Haflty.DTO.UserDto.Request;
using Haflty.DTO.UserDto.Response;
using Haflty.Services.AuthService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Haflty.Controllers.AuthController;

[Route("api/authentication")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
      private readonly IAuthService _authService = authService;

      [Route("add-user")]
      [HttpPost]
      public async Task<ActionResult<TokenResponseDto>> RegisterUser(RegisterUserDto
 UserDto, CancellationToken cancellationToken)
      {
            try
            {
                  var response = await _authService.GenerateUser(UserDto, cancellationToken);
                  return Ok(response);
            }
            catch (ArgumentException ex)
            {
                  return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                  return StatusCode(500, ex.Message);
            }
      }

}
