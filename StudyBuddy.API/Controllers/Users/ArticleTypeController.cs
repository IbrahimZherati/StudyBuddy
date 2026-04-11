using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ArticleTypeDTO;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleTypeController : ControllerBase
    {
        private readonly IArticleTypeService articleTypeService;

        public ArticleTypeController(IArticleTypeService articleTypeService)
        {
            this.articleTypeService = articleTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticleTypes(int skip = 0, int take = Option.Take)
        {
            var result = await articleTypeService.GetArticleTypes(skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetArticleTypeById(int Id)
        {
            var result = await articleTypeService.GetArticleTypeById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleTypeDTO ArticleTypeDTO)
        {
            var result = await articleTypeService.Create(ArticleTypeDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateArticleTypeDTO ArticleTypeDTO)
        {
            var result = await articleTypeService.Update(ArticleTypeDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await articleTypeService.Delete(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
