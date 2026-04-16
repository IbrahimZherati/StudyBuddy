using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyBuddy.Domain.Services.Articles;
using StudyBuddy.Domain.Services.ArticleTypes;
using StudyBuddy.Domain.Services.Cities;
using StudyBuddy.Domain.Services.ClientFiles;
using StudyBuddy.Domain.Services.ClientUsers;
using StudyBuddy.Domain.Services.Countries;
using StudyBuddy.Domain.Services.Days;
using StudyBuddy.Domain.Services.Events;
using StudyBuddy.Domain.Services.FeedReplays;
using StudyBuddy.Domain.Services.Feeds;
using StudyBuddy.Domain.Services.GroupChats;
using StudyBuddy.Domain.Services.GroupMessages;
using StudyBuddy.Domain.Services.Majors;
using StudyBuddy.Domain.Services.Messages;
using StudyBuddy.Domain.Services.Notes;
using StudyBuddy.Domain.Services.Notifications;
using StudyBuddy.Domain.Services.PostReplays;
using StudyBuddy.Domain.Services.Posts;
using StudyBuddy.Domain.Services.Universities;

namespace StudyBuddy.Domain
{
    public static class ConfigureService
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IArticleDomainService, ArticleDomainService>();
            services.AddScoped<ICityDomainService, CityDomainService>();
            services.AddScoped<ICountryDomainService, CountryDomainService>();
            services.AddScoped<IDayDomainService, DayDomainService>();
            services.AddScoped<IGroupMessageDomainService, GroupMessageDomainService>();
            services.AddScoped<IMajorDomainService, MajorDomainService>();
            services.AddScoped<IMessageDomainService , MessageDomainService>(); 
            services.AddScoped<INotificationDomainService , NotificationDomainService>(); 
            services.AddScoped<IUniversityDomainService , UniversityDomainService>();
            services.AddScoped<IClientUserDomainService, ClientUserDomainService>();
            services.AddScoped<IGroupChatDomainService, GroupChatDomainService>();
            services.AddScoped<IArticleTypeDomainService, ArticleTypeDomainService>();
            services.AddScoped<IClientFileDomainService, ClientFileDomainService>();
            services.AddScoped<IEventDomainService, EventDomainService>();
            services.AddScoped<IFeedDomainService, FeedDomainService>();
            services.AddScoped<IFeedReplayDomainService, FeedReplayDomainService>();
            services.AddScoped<INoteDomainService, NoteDomainService>();
            services.AddScoped<IPostDomainService, PostDomainService>();
            services.AddScoped<IPostReplayDomainService, PostReplayDomainService>();
            return services;
        }
    }
}
