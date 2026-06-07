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
      [Route("login")]
      [HttpPost]
      public async Task<ActionResult<TokenResponseDto>> LoginUser(UserLoginDto
UserDto, CancellationToken cancellationToken)
      {
            try
            {
                  var response = await _authService.UserLogin(UserDto, cancellationToken);
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
      [HttpGet("refresh-token")]
      public async Task<ActionResult<TokenResponseDto>> RefreshToken(CancellationToken cancellationToken)
      {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null)
                  return Unauthorized("Refresh token is required");

            try
            {
                  var response = _authService.RefreshTokenAsync(refreshToken, cancellationToken);
                  return Ok(response);
            }
            catch
            {
                  return NotFound("Refresh token not found");
            }


      }

      private void SetRefreshTokenInCookies(string refreshToken, DateTime expire)
      {
            var Cookies = new CookieOptions
            {
                  HttpOnly = true,
                  Expires = expire.ToLocalTime(),
                  Secure = true,
                  IsEssential = true,
                  SameSite = SameSiteMode.None

            };
            Response.Cookies.Append("refreshToken", refreshToken, Cookies);
      }

}
