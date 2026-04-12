using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Services;
using StudyBuddy.Shared;
using StudyBuddy.Shared.DTOs.EventDTO;
using StudyBuddy.Shared.Helpers;
using System.Security.Claims;
namespace StudyBuddy.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents(int clientId ,int skip = 0, int take = Option.Take)
        {
            var result = await eventService.GetEvents(clientId ,skip, take);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
     
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEventById(int Id)
        {
            var result = await eventService.GetEventById(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventDTO EventDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await eventService.Create(clientId, EventDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEventDTO EventDTO)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await eventService.Update(clientId, EventDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var clientId = int.Parse(User.FindFirstValue(AuthHelper.CleintId) ?? "0");
            var result = await eventService.Delete(clientId, Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
