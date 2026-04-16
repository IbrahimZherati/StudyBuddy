using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.PostReplayDTO;
using StudyBuddy.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostReplayController : ControllerBase
    {
        private readonly IPostReplayService postReplayService;

        public PostReplayController(IPostReplayService postReplayService)
        {
            this.postReplayService = postReplayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostReplays(int skip = 0, int take = Option.Take)
        {
            var result = await postReplayService.GetPostReplays(skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPostReplayById(Guid Id)
        {
            var result = await postReplayService.GetPostReplayById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostReplayDTO PostReplayDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postReplayService.Create(clientId, PostReplayDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePostReplayDTO PostReplayDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postReplayService.Update(clientId, PostReplayDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postReplayService.Delete(clientId, Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
