using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Application.Services.Majors;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.MajorDTO;

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MajorController : ControllerBase
    {
        private readonly IMajorService MajorService;

        public MajorController(IMajorService MajorService)
        {
            this.MajorService = MajorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMojors(int skip = 0, int take = Option.Take)
        {
            var result = await MajorService.GetMojors(skip , take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetMajorById(int Id)
        {
            var result = await MajorService.GetMajorById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMajorDTO MajorDTO)
        {
            var result = await MajorService.Create(MajorDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMajorDTO MajorDTO)
        {
            var result = await MajorService.Update(MajorDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await MajorService.Delete(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }




    }
}
