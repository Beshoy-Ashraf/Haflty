using Haflty.DTO.UserModel.Request;
using Haflty.DTO.UserModel.Response;

namespace Haflty.Services.AuthService.Interface;

public interface IAuthService
{
      Task<TokenResponseModel> GenerateUser(RegisterUserModel userModel, CancellationToken cancellationToken);
}
