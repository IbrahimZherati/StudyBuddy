using Bogus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.Json;
using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.DTOs.UniversityDTO;
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
        private readonly IRepo<NotificationType> notificationTypeRepo;
        private readonly IRepo<University> universityRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly UserManager<AppUser> userManager;
        private readonly IAuthService authService;
        private readonly IPostService postService;

        #region Default
        public static string DefaultEmail = "admin@admin";
        public static string DefaultPassword = "123";
        #endregion

        public string rootPath { get; set; }
        public Seed(IRepo<Day> dayRepo,
                    IRepo<Country> countryRepo,
                    IRepo<Major> majorRepo,
                    IRepo<NotificationType> notificationTypeRepo,
                    IRepo<University> universityRepo,
                    IRepo<ClientUser> clientUserRepo,
                    UserManager<AppUser> userManager,
                    IAuthService authService,
                    IPostService postService)
        {
            this.dayRepo = dayRepo;
            this.countryRepo = countryRepo;
            this.majorRepo = majorRepo;
            this.notificationTypeRepo = notificationTypeRepo;
            this.universityRepo = universityRepo;
            this.clientUserRepo = clientUserRepo;
            this.userManager = userManager;
            this.authService = authService;
            this.postService = postService;
        }
        async Task ISeed.Seed(string root)
        {
            rootPath = root;
            await SeedDays();
            await SeedCountriesAndCities();
            await SeedMajors();
            await SeedNotificationType();
            await SeedUser();
            await SeedUniversities();
            //await SeedFakeDataForTest();
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
                var anyMajor = await majorRepo.GetQuery().FirstOrDefaultAsync();
                if (anyMajor == null)
                    throw new Exception("Majors is Empty");
                var register = new RegisterDTO
                {
                    Email = DefaultEmail,
                    UserName = DefaultEmail,
                    Password = DefaultPassword,
                    PasswordConfirmation = DefaultPassword,
                    MajorId = anyMajor.Id,
                };

                var result = await authService.Register(register, rootPath);
                if (!result.IsSuccess)
                    throw new Exception("admin not seed");
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
            foreach (var notificationType in Enum.GetNames(typeof(NotificationTypes)))
            {
                var newType = NotificationType.Create(notificationType);
                await notificationTypeRepo.AddAsync(newType);

            }

            await notificationTypeRepo.SaveAsync();
        }

        public async Task SeedUniversities()
        {
            var universities = await universityRepo.GetAllAsync();
            if (universities.Any())
                return;
            var path = Path.Combine(rootPath, "data", "universities.json");
            if (!File.Exists(path))
                return;
            var jsonString = await File.ReadAllTextAsync(path);
            var data = JsonSerializer.Deserialize<List<UniversityJsonDTO>>(jsonString);

            foreach (var entry in data)
            {
                var newUniversity = University.Create(new CreateUniversityDTO
                {
                    Name = entry.name
                });
                if (newUniversity.Value != null)
                    await universityRepo.AddAsync(newUniversity.Value);
            }

            await universityRepo.SaveAsync();
        }

        public async Task SeedFakeDataForTest()
        {

            // Create the faker for RegisterDTO
            var registerFaker = new Faker<RegisterDTO>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.UserName, f => f.Internet.UserName())
                .RuleFor(r => r.Password, f => "1234")
                .RuleFor(r => r.PasswordConfirmation, (f, r) => r.Password) // Match the password
                .RuleFor(r => r.MajorId, f => f.Random.Number(1, 50)); // Random MajorId between 1-50


            var registerDtos = registerFaker.Generate(100);
            foreach (var register in registerDtos)
            {
                var registerResult = await authService.Register(register, rootPath);

                if (!registerResult.IsSuccess)
                    continue;

                var postFaker = new Faker<CreatePostDTO>()
                    .RuleFor(p => p.Text, f => f.Lorem.Lines())
                    .RuleFor(p => p.Title, f => f.Lorem.Text());
                var posts = postFaker.Generate(100);
                var user = await userManager.FindByEmailAsync(register.Email);
                var client = await clientUserRepo.GetQuery()
                    .Where(c => c.UserId == user.Id)
                    .FirstOrDefaultAsync();
                foreach (var post in posts)
                {
                    var createPostResult = await postService.Create(client.Id, post);

                    if(!createPostResult.IsSuccess)
                        continue;
                }
            }

        }
    }
}
