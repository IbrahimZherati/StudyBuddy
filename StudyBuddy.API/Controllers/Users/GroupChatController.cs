using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Application.Services.GroupChats;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.GroupChatDTO;

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
            var result = await groupChatService.GetById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupChatDTO groupChatDTO)
        {
            var result = await groupChatService.Create(groupChatDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateGroupChatDTO groupChatDTO)
        {
            var result = await groupChatService.Update(groupChatDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("AddMemberToGroupChat")]
        public async Task<IActionResult> AddMemberToGroupChat(int clientId , int groupId)
        {
            var result = await groupChatService.AddMemberToGroupChat(clientId , groupId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("RemoveMemberFromGroupChat")]
        public async Task<IActionResult> RemoveMemberFromGroupChat(int clientId , int groupId)
        {
            var result = await groupChatService.RemoveMemberFromGroupChat(clientId , groupId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int groupId)
        {
            var result = await groupChatService.Delete(groupId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

       


    }
}
