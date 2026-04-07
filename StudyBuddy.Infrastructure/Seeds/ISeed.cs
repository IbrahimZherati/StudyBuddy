using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Infrastructure.Seeds
{
    public interface ISeed
    {
        Task Seed(string root);
        Task SeedUser();
        Task SeedMajors();
        Task SeedDays();
        Task SeedCountriesAndCities();

        Task SeedNotificationType();
    }
}
