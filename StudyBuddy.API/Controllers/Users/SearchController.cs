using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Application.Services.Searchs;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;
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
        public async Task<IActionResult> GetFriends(int skip = 0 , int take = Option.Take ,string? filter = null, bool sameMajor = false)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await searchService.GetFriends(clientId,skip, take,filter ,sameMajor);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpGet("Friends/GetFriendRequest")]
        public async Task<IActionResult> GetFriendRequest(int skip = 0, int  take = Option.Take,string? filter = null, bool sameMajor = false)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await searchService.GetFriendRequest(clientId,skip, take, filter, sameMajor);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpGet("MyGroups")]
        public async Task<IActionResult> GetMyGroups(int skip = 0, int take = Option.Take, string? filter = null,int? majorId = null)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await searchService.GetMyGroups(clientId,skip, take, filter, majorId );
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpGet("SuggestedGroups")]
        public async Task<IActionResult> SuggestedGroups(int skip = 0, int take = Option.Take, string? filter = null, int? majorId = null)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await searchService.SuggestedGroups(clientId, skip, take, filter, majorId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpGet("Buddy")]
        public async Task<IActionResult> SearchBuddy(int skip = 0, int take = Option.Take, string? filter = null, bool SameUniversity = false , bool SameInterest = false, bool SameMajor = false)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");

            var result = await searchService.SearchBuddy(clientId,skip, take, filter, SameUniversity, SameInterest , SameMajor);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
    }
}



