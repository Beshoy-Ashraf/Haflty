using System.Security.Claims;
using System.Text;
using Haflty.Core.Enum;
using Haflty.DTO.UserModel.Request;
using Haflty.DTO.UserModel.Response;
using Haflty.Models.Context;
using Haflty.Models.Modules.User;
using Haflty.Services.AuthService.Interface;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Haflty.Services.AuthService;

public class AuthService(ILogger<AuthService> logger, AppDBContext appDBContext, IConfiguration configuration) : IAuthService
{

      private readonly ILogger<AuthService> _logger = logger;
      private readonly AppDBContext _dbContext = appDBContext;
      private readonly IConfiguration _confg = configuration;



      public async Task<TokenResponseModel> GenerateUser(RegisterUserModel userModel, CancellationToken cancellationToken)
      {
            if (_dbContext.Users.SingleOrDefault(x => x.Email == userModel.Email) != null)
            {
                  throw new InvalidOperationException("A user with this email already exists.");
            }
            if (_dbContext.Users.SingleOrDefault(x => x.UserName == userModel.UserName) != null)
            {
                  throw new InvalidOperationException("A user with this UserName already exists.");
            }
            var user = new User
            {
                  HashPassword = BCrypt.Net.BCrypt.HashPassword(userModel.HashPassword),
                  Name = userModel.Name,
                  Email = userModel.Email,
                  Phone = userModel.Phone,
                  UserName = userModel.UserName,
                  Address = userModel.Address,
                  UserRole = userModel.UserRole,
                  BirthDate = userModel.BirthDate,
                  QRCode = userModel.QRCode,
                  CreatedDate = DateTime.UtcNow,
                  UpdatedDate = DateTime.UtcNow,
            };
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return GenerateAccessToken(user);
      }
      public TokenResponseModel GenerateAccessToken(User user)
      {

            return new TokenResponseModel
            {
                  Token = GenerateToken(user),
                  RefreshToken = "",
                  UserId = user.Id.ToString(),

            };
      }
      public string GenerateToken(User user)
      {
            var secretKey = _confg["JwtConfig:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                  Subject = new ClaimsIdentity([
                        new Claim(ClaimTypes.Email ,user.Email),
                        new Claim(ClaimTypes.NameIdentifier ,user.Id.ToString()),
                        new Claim(ClaimTypes.Role ,user.UserRole.ToString()),


                  ]),
                  Expires = DateTime.Now.AddMinutes(30),
                  SigningCredentials = credentials,
                  Audience = _confg["JwtConfig:Audience"],
                  Issuer = _confg["JwtConfig:Issuer"],

            };
            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);

      }

}
