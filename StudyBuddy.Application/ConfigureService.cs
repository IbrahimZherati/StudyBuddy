using Microsoft.Extensions.DependencyInjection;
using StudyBuddy.Application.Services;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Application.Services.GroupChats;
using StudyBuddy.Application.Services.GroupMessages;
using StudyBuddy.Application.Services.Messages;
using StudyBuddy.Application.Services.Notifications;
using StudyBuddy.Application.Services.Shared.AutoGenerateSkills;

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
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IDayService, DayService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IArticleTypeService, ArticleTypeService>();
            services.AddScoped<IClientFileService, ClientFileService>();
            services.AddScoped<IEventService, EventService>(); 
            MapsterConfiguration.RegisterMappings();

            return services;
        }
    }
}
 