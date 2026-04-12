using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using StudyBuddy.Application.Services.Shared.Interfaces;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Interfaces.AppUsers;
using StudyBuddy.Infrastructure.Context;
using StudyBuddy.Infrastructure.Repositories;
using StudyBuddy.Infrastructure.Seeds;
using StudyBuddy.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfratructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region AppDbContext
            services.AddDbContext<AppDbContext>(options =>
             options.UseSqlite(configuration.GetConnectionString("SQLite")));
            #endregion

            #region IdentityContext
            services.AddDbContext<AppIdentityDbContext>(options =>
             options.UseSqlite(configuration.GetConnectionString("SQLite")));

            services.AddIdentity<AppUser, IdentityRole<Guid>>()
             .AddEntityFrameworkStores<AppIdentityDbContext>()
             .AddDefaultTokenProviders()
             .AddClaimsPrincipalFactory<AppClaimsFactory>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = false;
            });
            #endregion

            #region Repositories
            services.AddScoped<IAppUserRepository, AppUserRepository>();

            services.AddScoped(typeof(IRepo<,>), typeof(Repo<,>));

            services.AddScoped(typeof(IRepo<>), typeof(Repo<>));


            #endregion

            #region Services

            services.AddHttpClient<IAiService, GeminiAiService>();
            services.AddScoped<ISeed, Seed>();
            #endregion

          

            return services;
        }
    }
}
