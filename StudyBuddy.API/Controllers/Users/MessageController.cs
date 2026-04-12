using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Application.Services.Messages;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Enum;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService MessageService;

        public MessageController(IMessageService MessageService)
        {
            this.MessageService = MessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForPrivateChat(int SecondClientId, int skip = 0, int take = Option.Take , Order orderBy = Order.Desc)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await MessageService.GetMessagesForPrivateChat(clientId , SecondClientId,skip, take , orderBy);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await MessageService.GetMessageById(clientId, Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

      




    }
}
