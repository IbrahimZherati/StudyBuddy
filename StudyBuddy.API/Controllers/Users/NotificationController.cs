using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Application.Services.Notifications;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Enum;

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService NotificationService;

        public NotificationController(INotificationService NotificationService)
        {
            this.NotificationService = NotificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications(int skip = 0, int take = Option.Take , Order orderby = Order.Desc)
        {
            var result = await NotificationService.GetNotifications(skip, take , orderby);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetNotificationById(Guid Id)
        {
            var result = await NotificationService.GetNotificationById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

       

      
    }
}