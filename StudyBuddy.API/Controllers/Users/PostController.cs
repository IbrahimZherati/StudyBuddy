using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet("GetMyPosts")]
        public async Task<IActionResult> GetPosts(int skip = 0, int take = Option.Take)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postService.GetMyPosts(clientId,skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpGet("{Id}/Replies")]
        public async Task<IActionResult> GetPostReplies(Guid Id,int skip = 0, int take = Option.Take)
        {
            var result = await postService.GetPostReplys(Id , skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPostById(Guid Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postService.GetPostById(clientId ,Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDTO PostDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postService.Create(clientId ,PostDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePostDTO PostDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postService.Update(clientId ,PostDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("Like")]
        public async Task<IActionResult> Like(Guid Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postService.Like(clientId ,Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Unlike")]
        public async Task<IActionResult> Unlike(Guid Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await postService.UnLike(clientId ,Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Share")]
        public async Task<IActionResult> Share(Guid Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await postService.Share(clientId ,Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postService.Delete(clientId ,Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        
    }
}
