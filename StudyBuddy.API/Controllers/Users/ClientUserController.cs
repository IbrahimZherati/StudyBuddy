using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Shared.DTOs.ClientUserDTO;

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientUserController : ControllerBase
    {
        private readonly IClientUserService clientUserService;

        public ClientUserController(IClientUserService clientUserService)
        {
            this.clientUserService = clientUserService;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateClientUserDTO clientUserDTO)
        {
            var result = await clientUserService.Update(clientUserDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

   
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile(string userId)
        {
            var result = await clientUserService.GetProfile(userId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

   
    }
}
