using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Application.Services.Chats;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        [HttpGet("GetPrivateChats")]
        public async Task<IActionResult> GetPrivateChats(int skip = 0, int take = Option.Take)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await chatService.GetPrivateChats(clientId,skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
       
        [HttpGet("GetGroupChats")]
        public async Task<IActionResult> GetGroupChats(int skip = 0, int take = Option.Take)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await chatService.GetGroupChats(clientId,skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
       
        [HttpGet("GetUnReadPrivateChats")]
        public async Task<IActionResult> GetUnReadPrivateChats(int skip = 0, int take = Option.Take)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await chatService.GetUnReadPrivateChats(clientId,skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
       
        [HttpGet("GetUnReadGroupChats")]
        public async Task<IActionResult> GetUnReadGroupChats(int skip = 0, int take = Option.Take)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await chatService.GetUnReadGroupChats(clientId,skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
       
    }
}
