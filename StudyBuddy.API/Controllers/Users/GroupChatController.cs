using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Application.Services.GroupChats;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupChatController : ControllerBase
    {
        private readonly IGroupChatService groupChatService;

        public GroupChatController(IGroupChatService groupChatService)
        {
            this.groupChatService = groupChatService;
        }

        [HttpGet("GetGroupMemberCount")]
        public async Task<IActionResult> GetGroupMemberCount(int groupId)
        {
            var result = await groupChatService.GetGroupMemberCount(groupId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetGroupForClient")]
        public async Task<IActionResult> GetGroupForClient(int clientId , int skip = 0, int take = Option.Take)
        {
            var result = await groupChatService.GetGroupForClient(clientId , skip , take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = await groupChatService.GetGroupChatById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupChatDTO groupChatDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await groupChatService.Create(clientId,groupChatDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateGroupChatDTO groupChatDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await groupChatService.Update(clientId,groupChatDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("RemoveMember")]
        public async Task<IActionResult> RemoveMember(int memberId , int groupId)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await groupChatService.RemoveMemberFromGroupChat(clientId,memberId,groupId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int groupId)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await groupChatService.Delete(clientId,groupId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

       


    }
}
