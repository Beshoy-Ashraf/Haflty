using Haflty.Models.Context;
using Haflty.Services.AuthService.Interface;

namespace Haflty.Services.AuthService;

public class AuthService(ILogger<AuthService> logger, AppDBContext appDBContext) : IAuthService
{

      private readonly ILogger<AuthService> _logger = logger;
      private readonly AppDBContext _dbContext = appDBContext;


}
