using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Shared;
using StudyBuddy.Shared.Helpers;

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService authService;
        private readonly IMajorService majorService;
        private readonly IWebHostEnvironment env;

        public AuthController(IAuthService authService ,IMajorService majorService, IWebHostEnvironment env)
        {


            this.authService = authService;
            this.majorService = majorService;
            this.env = env;
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
            var result = await authService.Register(registerDTO,env.WebRootPath);
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

        [HttpGet("GetMajors")]
        public async Task<IActionResult> GetMojors(int skip = 0, int take = Option.Take)
        {
            var result = await majorService.GetMajors(skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromForm]string email , [FromForm]string  token)
        {
            var result = await authService.ConfirmEmail(email , token);
            return Redirect($"{AppHelper.FrontAppHost}/");
        }

        [HttpPost("SendToken")]
        public async Task<IActionResult> SendToken(string email)
        {
            var result = await authService.SendToken(email);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


    }
}
