using Haflty.Services.AuthService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Haflty.Controllers.AuthController;

[Route("api/authentication")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
      private readonly IAuthService _authService = authService;

}
