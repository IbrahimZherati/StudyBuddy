using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Infrastructure.Seeds
{
    public interface ISeed
    {
        Task Seed();
        Task SeedDays();
    }
}
