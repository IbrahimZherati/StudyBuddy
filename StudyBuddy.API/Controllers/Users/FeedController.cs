using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.FeedDTO;
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
        public async Task<IActionResult> GetFeeds(int skip = 0, int take = Option.Take)
        {
            var result = await feedService.GetFeeds(skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetFeedById(int Id)
        {
            var result = await feedService.GetFeedById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFeedDTO FeedDTO)
        {
            var result = await feedService.Create(FeedDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("Like")]
        public async Task<IActionResult> Like(int clientUserId , int feedId)
        {
            var result = await feedService.Like(clientUserId , feedId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UnLike")]
        public async Task<IActionResult> UnLike(int clientUserId, int feedId)
        {
            var result = await feedService.Unlike(clientUserId, feedId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("Share")]
        public async Task<IActionResult> UnLike(int feedId)
        {
            var result = await feedService.Share(feedId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFeedDTO FeedDTO)
        {
            var result = await feedService.Update(FeedDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await feedService.Delete(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
