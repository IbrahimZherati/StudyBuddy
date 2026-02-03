using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Shared.Results;
using System.Security.Claims;

namespace StudyBuddy.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<Result<string>> Login(LoginDTO loginDTO);
        Task<Result> Logout();
        Task<Result> Register(RegisterDTO registerDTO);

        UserInfoDTO GetUserInfo(ClaimsPrincipal user);
    }
}
