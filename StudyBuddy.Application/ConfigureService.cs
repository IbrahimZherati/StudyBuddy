using Microsoft.Extensions.DependencyInjection;
using StudyBuddy.Application.Services;
using StudyBuddy.Application.Services.App;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Application.Services.GroupChats;
using StudyBuddy.Application.Services.GroupMessages;
using StudyBuddy.Application.Services.Messages;
using StudyBuddy.Application.Services.Notifications;
using StudyBuddy.Application.Services.Shared.AutoGenerateSkills;
using StudyBuddy.Application.Services.Shared.GetTagsFromMajors;
using MailKitSimplified.Sender;
using StudyBuddy.Application.Services.Shared.Emails;
namespace StudyBuddy.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {


            services.AddScoped<IEmailService, EmailService>();
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
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostReplyService, PostReplyService>();
            services.AddScoped<ITagsService , TagsService>();
            services.AddScoped<IAppService, AppService>();
            services.AddScoped<IReviewService, ReviewService>();
            MapsterConfiguration.RegisterMappings();

            return services;
        }
    }
}
 