using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace StudyBuddy.Infrastructure.Seeds
{
    public class Seed : ISeed
    {
        private readonly IRepo<Day> dayRepo;
        private readonly IRepo<Country> countryRepo;
        private readonly IRepo<Major> majorRepo;
        private readonly IRepo<Domain.Entities.NotificationType> notificationTypeRepo;
        private readonly UserManager<AppUser> userManager;
        private readonly IAuthService authService;

        #region Default
        public static string DefaultEmail = "admin@admin";
        public static string DefaultPassword = "123";
        #endregion

        public string rootPath { get; set; }
        public Seed(IRepo<Day> dayRepo,
                    IRepo<Country> countryRepo,
                    IRepo<Major> majorRepo,
                    IRepo<NotificationType> notificationTypeRepo,
                    UserManager<AppUser> userManager,
                    IAuthService authService)
        {
            this.dayRepo = dayRepo;
            this.countryRepo = countryRepo;
            this.majorRepo = majorRepo;
            this.notificationTypeRepo = notificationTypeRepo;
            this.userManager = userManager;
            this.authService = authService;
        }
        async Task ISeed.Seed(string root)
        {
            rootPath = root;
            await SeedUser();
            await SeedDays();
            await SeedCountriesAndCities();
            await SeedMajors();
            await SeedNotificationType();
        }

        public async Task SeedDays()
        {
            var days = await dayRepo.GetAllAsync();
            if (days.Any())
                return;
            foreach (var day in Enum.GetNames(typeof(Days)))
            {
                var newDay = Day.Create(day);
                await dayRepo.AddAsync(newDay);
            }
            await dayRepo.SaveAsync();

        }

        public async Task SeedCountriesAndCities()
        {
            var countries = await countryRepo.GetAllAsync();
            if (countries.Any())
            {
                return;
            }
            string baseDir = rootPath;
            string filePath = Path.Combine(baseDir, "data", "countries.min.json");

            if (!File.Exists(filePath))
            {
                return;
            }

            string jsonString = await File.ReadAllTextAsync(filePath);

            var data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString);

            if (data != null)
            {
                foreach (var entry in data)
                {
                    var country = Country.Create(entry.Key);
                    var cities = entry.Value.Select(c => City.Create(c)).ToList();
                    foreach (var city in cities)
                        country.AddCity(city);

                    await countryRepo.AddAsync(country);
                }

                await countryRepo.SaveAsync();
            }
        }

        public async Task SeedUser()
        {
            if (await userManager.Users.FirstOrDefaultAsync() == null)
            {
                var register = new RegisterDTO
                {
                    Email = Seed.DefaultEmail,
                    UserName = Seed.DefaultEmail,
                    Password = Seed.DefaultPassword,
                    PasswordConfirmation = Seed.DefaultPassword,
                };

                var result = await authService.Register(register);
                if (!result.IsSuccess)
                    return;
            }

        }

        public async Task SeedMajors()
        {
            var majors = await majorRepo.GetAllAsync();
            if (majors.Any())
                return;
            var path = Path.Combine(rootPath, "data", "majors.json");
            if (!File.Exists(path))
                return;
            var jsonString = await File.ReadAllTextAsync(path);
            var data = JsonSerializer.Deserialize<List<string>>(jsonString);
            if (data != null)
            {

                foreach (var entry in data)
                {
                    var major = Major.Create(entry.ToString());
                    await majorRepo.AddAsync(major);
                }
                await majorRepo.SaveAsync();
            }
        }

        public async Task SeedNotificationType()
        {
            var notificationTypes = await notificationTypeRepo.GetAllAsync();
            if (notificationTypes.Any())
                return;
            foreach(var notificationType in Enum.GetNames(typeof(NotificationTypes)))
            {
                var newType = NotificationType.Create(notificationType);
                await notificationTypeRepo.AddAsync(newType);

            }

            await notificationTypeRepo.SaveAsync();
        }
    }
}
