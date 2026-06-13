using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.PostReplyDTO;
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
    public class PostReplyController : ControllerBase
    {
        private readonly IPostReplyService postReplyService;

        public PostReplyController(IPostReplyService postReplyService)
        {
            this.postReplyService = postReplyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostReplys(int skip = 0, int take = Option.Take)
        {
            var result = await postReplyService.GetPostReplys(skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPostReplyById(Guid Id)
        {
            var result = await postReplyService.GetPostReplyById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostReplyDTO PostReplyDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postReplyService.Create(clientId, PostReplyDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePostReplyDTO PostReplyDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postReplyService.Update(clientId, PostReplyDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await postReplyService.Delete(clientId, Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
