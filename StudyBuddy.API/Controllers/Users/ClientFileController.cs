using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.ClientFileDTO;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientFileController : ControllerBase
    {
        private readonly IClientFileService clientFileService;

        public ClientFileController(IClientFileService clientFileService)
        {
            this.clientFileService = clientFileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientFiles(int skip = 0, int take = Option.Take)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientFileService.GetClientFiles(clientId, skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }



        [HttpGet("{Id}")]
        public async Task<IActionResult> GetClientFileById(int Id)
        {
            var result = await clientFileService.GetClientFileById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetSummary")]
        public async Task<IActionResult> GetSummary(int Id)
        {
            var result = await clientFileService.GetSummary(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetFlashCards")]
        public async Task<IActionResult> GetFlashCards(int Id, int take)
        {
            var result = await clientFileService.GetFlashCards(Id, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientFileDTO ClientFileDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientFileService.Create(clientId, ClientFileDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateClientFileDTO ClientFileDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientFileService.Update(clientId, ClientFileDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }




        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await clientFileService.Delete(clientId, Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


    }
}
