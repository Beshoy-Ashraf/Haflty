using Haflty.Services.AuthService;
using Haflty.Services.AuthService.Interface;

namespace Haflty;

public static class ServicesRegistrationExtension
{
      public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
      {
            services.AddScoped<IAuthService, AuthService>();
            return services;
      }
}
