using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Interfaces.AppUsers;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;
using System.Security.Claims;
using System.Text.Json;


namespace StudyBuddy.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAppUserRepository appUserRepository;

        public AuthService(IAppUserRepository appUserRepository)
        {
            this.appUserRepository = appUserRepository;
        }

        public UserInfoDTO GetUserInfo(ClaimsPrincipal user)
        {
            var claims = user.Claims
                .Where(c => c.Type != ClaimTypes.Role && !c.Type.StartsWith("Permission"))
                .ToDictionary(c => c.Type, c => c.Value);

            var roles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToArray();

            var json = JsonSerializer.Serialize(roles);

            return new UserInfoDTO
            {
                IsAuthenticated = user.Identity.IsAuthenticated,
                UserName = user.Identity.Name,
                ExposedClaims = claims,  // now a Dictionary<string,string>
                Json = json
            };
        }

        public async Task<Result<string>> Login(LoginDTO loginDTO)
        {
            var user = await appUserRepository.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                return Result<string>.Failure(AuthErrorMessage.UserCannotFound);

            var check = await appUserRepository.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!check)
                return Result<string>.Failure(AuthErrorMessage.PasswordNotCorrect);

            try
            {
                await appUserRepository.SignInAsync(user, loginDTO.RememberMe);
            }
            catch
            {
                return Result<string>.Failure(AuthErrorMessage.LoginFailed);
            }

            return Result<string>.Success(user.Id.ToString());
        }

        public async Task<Result> Logout()
        {
            try
            {
                await appUserRepository.SignOutAsync();
            }
            catch
            {
                return Result.Failure(AuthErrorMessage.LogoutFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Register(RegisterDTO registerDTO)
        {
            var newUser = new AppUser
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };

            var result = await appUserRepository.CreateAsync(newUser, registerDTO.Password);
            if (!result.Succeeded)
                return Result.Failure(AuthErrorMessage.RegisterFailed);


            var newLogin = new LoginDTO
            {
                Email = registerDTO.Email,
                Password = registerDTO.Password,
            };

            var loginResult = await Login(newLogin);

            if (!loginResult.IsSuccess)
                return Result.Failure(AuthErrorMessage.LoginFailed);

            return Result.Success();

        }
    }
}
