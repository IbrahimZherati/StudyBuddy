using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services;
using StudyBuddy.Application.Services.App;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppServiceController : ControllerBase
    {
        private readonly IAppService appService;
        private readonly IWebHostEnvironment env;

        public AppServiceController(IAppService appService, IWebHostEnvironment env)
        {
            this.appService = appService;
            this.env = env;
        }

        

        [HttpPost("Start")]
        public async Task<IActionResult> Start()
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await appService.Start(clientId, env.WebRootPath);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

      



    }
}
