using Microsoft.AspNetCore.Identity;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Domain.Interfaces.AppUsers
{
    public interface IAppUserRepository
    {
        Task SignInAsync(AppUser user, bool rememberMe);
        Task<IdentityResult> CreateAsync(AppUser user, string password);

        Task SignOutAsync();

        Task<AppUser?> FindByEmailAsync(string email);

        Task<bool> CheckPasswordSignInAsync(AppUser user, string password, bool logoutOnFialed);
    }
}
