
using StudyBuddy.Shared.DTOs.EventDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Events
{
    public interface IEventDomainService
    {
        Task<Result> Create(CreateEventDTO eventDTO);
        Task<Result> Update(UpdateEventDTO eventDTO);
        Task<Result> Delete(int Id);
        Task<Result> GetEvents(int clientId);
    } 
}
