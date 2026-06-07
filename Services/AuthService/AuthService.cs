using System.Security.Claims;
using System.Text;
using Haflty.DTO.UserDto.Request;
using Haflty.DTO.UserDto.Response;
using Haflty.Models.Context;
using Haflty.Models.Entities;
using Haflty.Services.AuthService.Interface;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

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
                  HashPassword = BCrypt.Net.BCrypt.HashPassword(UserDto.Password),
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
            var token = await GenerateAccessToken(user);
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return token;
      }
      public async Task<TokenResponseDto> GenerateAccessToken(User user)
      {
            var userRefreshToken = GenerateRefreshToken(user);
            _dbContext.UserRefreshTokens.Add(userRefreshToken);
            return new TokenResponseDto
            {
                  Token = GenerateToken(user),
                  RefreshToken = userRefreshToken.Token,
                  ExpireOne = DateTime.Now.AddMinutes(30),
                  UserId = user.Id.ToString(),

            };
      }
      public async Task<TokenResponseDto> UserLogin(UserLoginDto userData, CancellationToken cancellationToken)
      {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userData.Email, cancellationToken) ?? throw new KeyNotFoundException("User Not Found");
            if (!BCrypt.Net.BCrypt.Verify(userData.Password, user.HashPassword))
                  throw new KeyNotFoundException("Password Not Correct");

            var token = await GenerateAccessToken(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return token;
      }
      private string GenerateToken(User user)
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
      public async Task<TokenResponseDto> RefreshTokenAsync(string token, CancellationToken cancellationToken)
      {
            var refreshToken = await _dbContext.UserRefreshTokens.FirstOrDefaultAsync(x => x.Token == token, cancellationToken) ?? throw new InvalidOperationException("Refresh token not found.");
            if (refreshToken.ExpiresOn < DateTime.UtcNow)
                  throw new InvalidOperationException("Refresh token has expired.");
            if (refreshToken.RevokedOn != null)
                  throw new InvalidOperationException("Refresh token has been revoked.");

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == refreshToken.UserId, cancellationToken) ?? throw new InvalidOperationException("User not found.");

            refreshToken.RevokedOn = DateTime.Now;
            _dbContext.UserRefreshTokens.Update(refreshToken);
            var newToken = await GenerateAccessToken(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return newToken;
      }
      public UserRefreshToken GenerateRefreshToken(User user)
      {
            var RandomNumber = new byte[32];
            using var Generator = new RNGCryptoServiceProvider();
            Generator.GetBytes(RandomNumber);


            return new UserRefreshToken
            {
                  Token = Convert.ToBase64String(RandomNumber),
                  User = user,
                  UserId = user.Id,
                  CreatedOn = DateTime.UtcNow,
                  ExpiresOn = DateTime.UtcNow.AddDays(30),


            };
      }

}
