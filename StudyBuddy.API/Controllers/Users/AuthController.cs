using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Auth;

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {


            this.authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await authService.Login(loginDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result = await authService.Register(registerDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await authService.Logout();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("UserInfo")]
        public IActionResult GetUserInfo()
        {
            var userInfo = authService.GetUserInfo(User);
            return Ok(userInfo);
        }

    }
}
