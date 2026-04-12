using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;

public class AppClaimsFactory : UserClaimsPrincipalFactory<AppUser>
{
    private readonly IRepo<ClientUser> clientUserRepo;

    public AppClaimsFactory(UserManager<AppUser> userManager, IOptions<IdentityOptions> optionsAccessor,IRepo<ClientUser> clientUserRepo)
        : base(userManager, optionsAccessor)
    {
        this.clientUserRepo = clientUserRepo;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        var client = await clientUserRepo.GetQuery().FirstOrDefaultAsync(c => c.UserId == user.Id);
        identity.AddClaim(new Claim(AuthHelper.CleintId, client?.Id.ToString() ?? ""));

        return identity;
    }
}
