using Haflty.DTO.UserModel.Request;
using Haflty.DTO.UserModel.Response;
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
      public async Task<ActionResult<TokenResponseModel>> RegisterUser(RegisterUserModel userModel, CancellationToken cancellationToken)
      {
            try
            {
                  var response = await _authService.GenerateUser(userModel, cancellationToken);
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
