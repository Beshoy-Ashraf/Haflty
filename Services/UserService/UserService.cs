using Haflty.Core.Enum;
using Haflty.DTO.UserDto.Response;
using Haflty.Models.Entities;
using Haflty.Repository.InterFace;
using Haflty.Services.UserService.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Haflty.Services.UserService;

public class UserService(IBaseRepository<User> baseRepository) : IUserService
{
      private readonly IBaseRepository<User> data = baseRepository;

      public async Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken)
      {
            var userData = await data.GetByIdAsync(id, cancellationToken);
            var response = new UserDto(userData);
            return response;
      }
      public async Task<List<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
      {
            var userData = await data.GetAllAsync(cancellationToken);
            var listOfUsers = new List<UserDto> { };
            foreach (var item in userData)
            {
                  var response = new UserDto(item);

                  listOfUsers.Add(response);
            }

            return listOfUsers;
      }
      public async Task<List<UserDto>> AdminUsers(CancellationToken cancellationToken, string[]? includes = null)
      {
            var users = await data.Find(s => s.UserRole == UserRole.Admin, cancellationToken, includes);
            var listOfUsers = new List<UserDto>() { };

            foreach (var item in users)
            {
                  var response = new UserDto(item);

                  listOfUsers.Add(response);
            }
            return listOfUsers;
      }
      public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
      {
            await data.DeleteEntityAsync(id, cancellationToken);
            return new OkResult();
      }
}
