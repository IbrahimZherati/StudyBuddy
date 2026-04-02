using Microsoft.Extensions.DependencyInjection;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Application.Services.GroupChats;
using StudyBuddy.Application.Services.GroupMessages;
using StudyBuddy.Application.Services.Majors;
using StudyBuddy.Application.Services.Messages;
using StudyBuddy.Application.Services.Shared.AutoGenerateSkills;
using StudyBuddy.Application.Services.Universities;
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
            services.AddScoped<IGroupMessageService, GroupMessageService>();
            services.AddScoped<IClientUserService, ClientUserService>();
            services.AddScoped<IAutoGenrateSkill, AutoGenerateSkill>();
            services.AddScoped<IMajorService, MajorService>();
            services.AddScoped<IUniversityService, UniversityService>();
            MapsterConfiguration.RegisterMappings();

            return services;
        }
    }
}
 