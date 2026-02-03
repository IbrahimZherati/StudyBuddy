using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Interfaces.AppUsers;

namespace StudyBuddy.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AppUserRepository(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> CheckPasswordSignInAsync(AppUser user, string password, bool logoutOnFailed)
        {
            var result = await signInManager.CheckPasswordSignInAsync(user, password, logoutOnFailed);
            return result.Succeeded;
        }

        public async Task<AppUser?> FindByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user;
        }



        public async Task SignInAsync(AppUser user, bool rememberMe)
        {
            await signInManager.SignInAsync(user, rememberMe);
        }


        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> CreateAsync(AppUser user , string password)
        {
           return await userManager.CreateAsync(user, password);
        }
    }
}
