using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.Enum;
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
        private readonly IWebHostEnvironment env;

        public ClientUserController(IClientUserService clientUserService,IWebHostEnvironment env)
        {
            this.clientUserService = clientUserService;
            this.env = env;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateClientUserDTO clientUserDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.Update(clientId, clientUserDTO ,env.WebRootPath);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

   
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile(Guid userId)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await clientUserService.GetProfile(clientId,userId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetProfileByClientId")]
        public async Task<IActionResult> GetProfileByClientId(int clientId)
        {
            var currentId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.GetProfileByClientId(currentId,clientId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetFriends")]
        public async Task<IActionResult> GetFriends(int skip = 0,int take = 10)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.GetFriends(clientId , skip , take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
      
        [HttpGet("GetFriendsFriends/{clientId}")]
        public async Task<IActionResult> GetFriendsFriends(int clientId,int skip = 0,int take = 10)
        {
            var result = await clientUserService.GetFriendsFriends(clientId , skip , take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetGroups")]
        public async Task<IActionResult> GetGroups(int skip = 0, int take = 10)
        {

            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.GetGroups(clientId , skip ,take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     

        [HttpGet("GetAllNotifications")]
        public async Task<IActionResult> GetNotifications(int skip = 0, int take = Option.Take , Order orderby = Order.Desc)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.GetAllNotifications(clientId , skip ,take , orderby);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        
        }
        [HttpGet("GetFriendRequestNotifications")]
        public async Task<IActionResult> GetFriendRequestNotifications(int skip = 0, int take = Option.Take , Order orderby = Order.Desc)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.GetFriendRequestNotifications(clientId , skip ,take , orderby);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        
        }
        [HttpGet("GetGroupInviteRequestNotifications")]
        public async Task<IActionResult> GetGroupInviteRequestNotifications(int skip = 0, int take = Option.Take , Order orderby = Order.Desc)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.GetGroupInviteNotifications(clientId , skip ,take , orderby);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        
        }
        [HttpGet("GetMessageChatNotifications")]
        public async Task<IActionResult> GetMessageChatNotifications(int skip = 0, int take = Option.Take , Order orderby = Order.Desc)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.GetMessageChatNotifications(clientId , skip ,take , orderby);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        
        }

        [HttpPost("FriendRequest")]
        public async Task<IActionResult> FriendRequest(int requestClientUserId)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.FriendRequest(clientId, requestClientUserId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPost("GroupInviteRequest")]
        public async Task<IActionResult> InviteGroup(int requestClientUserId , int groupId)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.GroupInviteRequest(clientId, requestClientUserId , groupId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetFriendRequest")]
        public async Task<IActionResult> GetFriendRequest(int skip = 0, int take = 10)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.GetFriendRequest(clientId, skip , take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetGroupInviteRequest")]
        public async Task<IActionResult> GetGroupInviteRequest(int skip = 0, int take = 10)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.GetInvitesRequest(clientId, skip , take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("AcceptFriendRequest")]
        public async Task<IActionResult> AcceptFriendRequest(int requestId)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.AcceptFriendRequestByRequestId(clientId, requestId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("AcceptFriendRequestByClientId")]
        public async Task<IActionResult> AcceptFriendRequestByClientId(int fromClientId)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.AcceptFriendRequestByRequestId(clientId, fromClientId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("AcceptGroupInviteRequest")]
        public async Task<IActionResult> AcceptGroupInviteRequest(int requestId)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientUserService.AcceptGroupInviteRequest(clientId, requestId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }



    }
}
