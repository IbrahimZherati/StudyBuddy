using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Application.Services.Notifications;
using StudyBuddy.Application.Services.Searchs;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Enum;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public TestController(INotificationService notificationService)
        {
          
            this.notificationService = notificationService;
        }

        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await notificationService.Create(new CreateNotificationDTO
            {
                FromClientUserId = 2,
                ToClientUserId = 2,
                FromClientUserName = "test",
                FromClientPhoto = null,
                Type = NotificationTypes.RequestAccepted.ToString(),
                Title = "Request Accepted",
                Description = $"Friend Request Accepted"
            });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}



