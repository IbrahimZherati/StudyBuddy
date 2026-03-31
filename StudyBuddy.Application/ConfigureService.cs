using Microsoft.Extensions.DependencyInjection;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.GroupChats;
using StudyBuddy.Application.Services.Messages;
using System.Reflection;

namespace StudyBuddy.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {



            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IGroupChatService, GroupChatService>();

            MapsterConfiguration.RegisterMappings();

            return services;
        }
    }
}
