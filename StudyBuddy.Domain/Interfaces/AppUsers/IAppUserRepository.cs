using Microsoft.AspNetCore.Identity;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Domain.Interfaces.AppUsers
{
    public interface IAppUserRepository
    {
        Task SignInAsync(AppUser user, bool rememberMe);
        Task<IdentityResult> CreateAsync(AppUser user, string password);

        Task SignOutAsync();

        Task<IdentityResult> ConfirmEmail(AppUser appUser, string token);
        Task<AppUser?> FindByEmailAsync(string email);
        Task<string> GenerateToken(AppUser appUser);
        Task<bool> CheckPasswordSignInAsync(AppUser user, string password, bool logoutOnFialed);
    }
}
