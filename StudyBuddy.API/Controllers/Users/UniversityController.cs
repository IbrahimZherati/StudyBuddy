using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Application.Services.Universities;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.UniversityDTO;

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService UniversityService;

        public UniversityController(IUniversityService UniversityService)
        {
            this.UniversityService = UniversityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUniversities(int skip = 0, int take = Option.Take)
        {
            var result = await UniversityService.GetUniversities(skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUniversityById(int Id)
        {
            var result = await UniversityService.GetUniversityById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUniversityDTO universityDTO)
        {
            var result = await UniversityService.Create(universityDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUniversityDTO universityDTO)
        {
            var result = await UniversityService.Update(universityDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int groupId)
        {
            var result = await UniversityService.Delete(groupId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}