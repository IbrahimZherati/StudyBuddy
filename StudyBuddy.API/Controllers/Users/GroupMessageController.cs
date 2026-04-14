using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Application.Services.GroupMessages;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.Enum;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupMessageController : ControllerBase
    {
        private readonly IGroupMessageService GroupMessageService;

        public GroupMessageController(IGroupMessageService GroupMessageService)
        {
            this.GroupMessageService = GroupMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupMessagesForPrivateChat(int groupId, int skip = 0, int take = Option.Take, Order orderBy = Order.Desc)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await GroupMessageService.GetMessagesForGroup(clientId ,groupId, skip, take, orderBy);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await GroupMessageService.GetGroupMessageById(clientId, Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }






    }
}
