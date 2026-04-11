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
        public async Task<IActionResult> GetMessagesForPrivateChat(int FirstClientId, int SecondClientId, int skip = 0, int take = Option.Take , Order orderBy = Order.Desc)
        {
            var result = await MessageService.GetMessagesForPrivateChat(FirstClientId , SecondClientId,skip, take , orderBy);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var result = await MessageService.GetById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

      




    }
}
