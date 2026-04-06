using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudyBuddy.Infrastructure.Seeds
{
    public class Seed : ISeed
    {
        private readonly IRepo<Day> dayRepo;

        public Seed(IRepo<Day> dayRepo)
        {
            this.dayRepo = dayRepo;
           
        }
        async Task ISeed.Seed()
        {
            await SeedDays();
        }

        public async Task SeedDays()
        {
            var days = await dayRepo.GetAllAsync();
            if (days == null || days.Count() == 0)
            {
                foreach(var day in Enum.GetNames(typeof(Days)))
                {
                    await dayRepo.AddAsync(new Day
                    {
                        Name = day
                    });
                }
                await dayRepo.SaveAsync();
            }
        }

    }
}
