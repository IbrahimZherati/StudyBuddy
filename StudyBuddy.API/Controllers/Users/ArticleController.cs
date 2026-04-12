using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ArticleDTO;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles(int skip = 0, int take = Option.Take)
        {
            var result = await articleService.GetArticles(skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetArticleById(int Id)
        {
            var result = await articleService.GetArticleById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleDTO ArticleDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await articleService.Create(clientId,ArticleDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateArticleDTO ArticleDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await articleService.Update(clientId, ArticleDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await articleService.Delete(clientId, Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
