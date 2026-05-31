using Haflty.Repository;
using Haflty.Repository.InterFace;
using Haflty.Services.AuthService;
using Haflty.Services.AuthService.Interface;
using Haflty.Services.UserService;
using Haflty.Services.UserService.Interface;

namespace Haflty;

public static class ServicesRegistrationExtension
{
      public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
      {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            return services;
      }
}
