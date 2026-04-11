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

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupGroupMessageController : ControllerBase
    {
        private readonly IGroupMessageService GroupMessageService;

        public GroupGroupMessageController(IGroupMessageService GroupMessageService)
        {
            this.GroupMessageService = GroupMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupMessagesForPrivateChat(int groupId, int skip = 0, int take = Option.Take, Order orderBy = Order.Desc)
        {
            var result = await GroupMessageService.GetMessagesForGroup(groupId, skip, take, orderBy);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var result = await GroupMessageService.GetById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }






    }
}
