using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.DTOs.AuthDTOs;
using StudyBuddy.Application.Services.Auth;
using StudyBuddy.Application.Services.ClientUsers;
using StudyBuddy.Application.Services.Countries;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.CountryDTO;

namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService CountryService;

        public CountryController(ICountryService CountryService)
        {
            this.CountryService = CountryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries(int skip = 0, int take = Option.Take)
        {
            var result = await CountryService.GetCountries(skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCountryById(int Id)
        {
            var result = await CountryService.GetCountryById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCountryDTO countryDTO)
        {
            var result = await CountryService.Create(countryDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCountryDTO countryDTO)
        {
            var result = await CountryService.Update(countryDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await CountryService.Delete(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}