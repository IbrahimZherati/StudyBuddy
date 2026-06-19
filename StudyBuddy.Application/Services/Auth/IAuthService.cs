using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Shared.DTOs.AuthDTOs;
using StudyBuddy.Shared.Results;
using System.Security.Claims;

namespace StudyBuddy.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<Result<string>> Login(LoginDTO loginDTO);
        Task<Result> Logout();
        Task<Result> Register(RegisterDTO registerDTO , string rootPath);

        UserInfoDTO GetUserInfo(ClaimsPrincipal user);

        Task<Result> ConfirmEmail(string email , string token);

        Task<Result> SendToken(string email);
    }
}
