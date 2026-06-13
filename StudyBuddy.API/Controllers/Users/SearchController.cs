using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Application.Services.Searchs;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.CityDTO;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }


        [HttpGet("Friends/GetFriends")]
        public async Task<IActionResult> GetFriends(int skip , int take ,string? filter, bool sameMajor)
        {
            var result = await searchService.GetFriends(skip, take,filter ,sameMajor);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpGet("Friends/GetFriendRequest")]
        public async Task<IActionResult> GetFriendRequest(int skip , int  take ,string? filter, bool sameMajor)
        {
            var result = await searchService.GetFriendRequest(skip, take, filter, sameMajor);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpGet("MyGroups")]
        public async Task<IActionResult> GetMyGroups(int skip , int take ,string? filter ,string? major , string? university)
        {
            var result = await searchService.GetMyGroups(skip, take, filter, major , university);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpGet("SuggestedGroups")]
        public async Task<IActionResult> SuggestedGroups(int skip , int take ,string? filter ,string? major , string? university)
        {
            var result = await searchService.SuggestedGroups(skip, take, filter, major, university);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpGet("Buddy")]
        public async Task<IActionResult> SearchBuddy(int skip , int take, string? filter, bool SameUniversity, bool SameInterest , bool SameMajor)
        {
            var result = await searchService.SearchBuddy(skip, take, filter, SameUniversity, SameInterest , SameMajor);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
    }
}



