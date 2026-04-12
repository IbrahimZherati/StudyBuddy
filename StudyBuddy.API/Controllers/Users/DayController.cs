using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.DayDTO;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DayController : ControllerBase
    {
        private readonly IDayService dayService;

        public DayController(IDayService dayService)
        {
            this.dayService = dayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDays(int skip = 0, int take = Option.Take)
        {
            var result = await dayService.GetDays(skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetDayById(int Id)
        {
            var result = await dayService.GetDayById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDayDTO DayDTO)
        {
            var result = await dayService.Create(DayDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDayDTO DayDTO)
        {
            var result = await dayService.Update(DayDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await dayService.Delete(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
