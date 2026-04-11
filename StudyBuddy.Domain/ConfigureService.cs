using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyBuddy.Domain.Services.Articles;

namespace StudyBuddy.Domain
{
    public static class ConfigureService
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IArticleDomainService, ArticleDomainService>();

            return services;
        }
    }
}
