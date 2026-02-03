using Microsoft.Extensions.DependencyInjection;
using StudyBuddy.Application.Services.Auth;
using System.Reflection;

namespace StudyBuddy.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {



            services.AddScoped<IAuthService, AuthService>();


            MapsterConfiguration.RegisterMappings();

            return services;
        }
    }
}
