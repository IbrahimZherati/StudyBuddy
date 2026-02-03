using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Infrastructure.Context
{
    internal class AppIdentityDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public AppIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppIdentityDbContext()
        {
        }
    }
}
