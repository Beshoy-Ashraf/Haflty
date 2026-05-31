using System.Security.Claims;
using System.Text;
using Haflty.DTO.UserDto.Request;
using Haflty.DTO.UserDto.Response;
using Haflty.Models.Context;
using Haflty.Models.Entities;
using Haflty.Services.AuthService.Interface;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Haflty.Services.AuthService;

public class AuthService(ILogger<AuthService> logger, AppDBContext appDBContext, IConfiguration configuration) : IAuthService
{

      private readonly ILogger<AuthService> _logger = logger;
      private readonly AppDBContext _dbContext = appDBContext;
      private readonly IConfiguration _confg = configuration;



      public async Task<TokenResponseDto> GenerateUser(RegisterUserDto UserDto, CancellationToken cancellationToken)
      {
            if (_dbContext.Users.SingleOrDefault(x => x.Email == UserDto.Email) != null)
            {
                  throw new InvalidOperationException("A user with this email already exists.");
            }
            if (_dbContext.Users.SingleOrDefault(x => x.UserName == UserDto.UserName) != null)
            {
                  throw new InvalidOperationException("A user with this UserName already exists.");
            }
            var user = new User
            {
                  HashPassword = BCrypt.Net.BCrypt.HashPassword(UserDto.HashPassword),
                  Name = UserDto.Name,
                  Email = UserDto.Email,
                  Phone = UserDto.Phone,
                  UserName = UserDto.UserName,
                  Address = UserDto.Address,
                  UserRole = UserDto.UserRole,
                  BirthDate = UserDto.BirthDate,
                  QRCode = UserDto.QRCode,
                  CreatedDate = DateTime.UtcNow,
                  UpdatedDate = DateTime.UtcNow,
            };
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return GenerateAccessToken(user);
      }
      public TokenResponseDto GenerateAccessToken(User user)
      {

            return new TokenResponseDto
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
