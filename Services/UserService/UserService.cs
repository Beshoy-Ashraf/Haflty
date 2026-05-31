using Haflty.DTO.UserDto.Response;
using Haflty.Models.Entities;
using Haflty.Repository.InterFace;
using Haflty.Services.UserService.Interface;

namespace Haflty.Services.UserService;

public class UserService(IBaseRepository<User> baseRepository) : IUserService
{
      private readonly IBaseRepository<User> data = baseRepository;

      public async Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken)
      {
            var userData = await data.GetByIdAsync(id, cancellationToken);
            var response = new UserDto
            {
                  Id = userData.Id,
                  UserName = userData.UserName,
                  Address = userData.Address,
                  UserRole = userData.UserRole,
                  BirthDate = userData.BirthDate,
                  Name = userData.Name,
                  Email = userData.Email,
                  Phone = userData.Phone,
                  QRCode = userData.QRCode,
            };
            return response;
      }
      public async Task<List<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
      {
            var userData = await data.GetAllAsync(cancellationToken);
            var listOfUsers = new List<UserDto> { };
            foreach (var item in userData)
            {
                  var response = new UserDto
                  {
                        Id = item.Id,
                        UserName = item.UserName,
                        Address = item.Address,
                        UserRole = item.UserRole,
                        BirthDate = item.BirthDate,
                        Name = item.Name,
                        Email = item.Email,
                        Phone = item.Phone,
                        QRCode = item.QRCode,
                  };
                  listOfUsers.Add(response);
            }

            return listOfUsers;
      }
}
