
using StudyBuddy.Shared.DTOs.EventDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Events
{
    public interface IEventDomainService
    {
        Task<Result> Create(int clientId, CreateEventDTO eventDTO);
        Task<Result> Update(int clientId, UpdateEventDTO eventDTO);
        Task<Result> Delete(int clientId, int Id);
        Task<Result> GetEvents(int clientId);
    } 
}
