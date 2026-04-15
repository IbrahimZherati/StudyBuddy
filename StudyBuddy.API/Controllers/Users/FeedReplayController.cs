using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.FeedReplayDTO;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedReplayController : ControllerBase
    {
        private readonly IFeedReplayService feedReplayService;

        public FeedReplayController(IFeedReplayService feedReplayService)
        {
            this.feedReplayService = feedReplayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedReplays(int feedId ,int skip = 0, int take = Option.Take)
        {
            var result = await feedReplayService.GetFeedReplays(feedId,skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetFeedReplayById(int Id)
        {
            var result = await feedReplayService.GetFeedReplayById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFeedReplayDTO FeedReplayDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await feedReplayService.Create(clientId ,FeedReplayDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFeedReplayDTO FeedReplayDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await feedReplayService.Update(clientId, FeedReplayDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await feedReplayService.Delete(clientId, Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
