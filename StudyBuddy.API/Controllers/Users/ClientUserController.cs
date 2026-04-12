using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services;
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
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.Update(clientId, clientUserDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

   
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile(Guid userId)
        {
            var result = await clientUserService.GetProfile(userId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("FriendRequest")]
        public async Task<IActionResult> FriendRequest(int requestClientUserId)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.FriendReqesut(clientId, requestClientUserId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetFriendRequest")]
        public async Task<IActionResult> GetFriendRequest(int clientUserId , int skip , int take)
        {
            var result = await clientUserService.GetFriendRequest(clientUserId, skip , take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("AcceptFriendReqesut")]
        public async Task<IActionResult> AcceptFriendReqesut(int requestId)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.AcceptFriendReqesut(clientId, requestId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }



    }
}
