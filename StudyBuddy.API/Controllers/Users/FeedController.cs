using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Application.Services.Feeds;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService feedService;

        public FeedController(IFeedService feedService)
        {
            this.feedService = feedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeed(int skip = 0, int take= 10)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await feedService.GetFeed(clientId, skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

    }
}
