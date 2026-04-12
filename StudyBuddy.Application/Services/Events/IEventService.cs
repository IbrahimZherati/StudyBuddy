using Mapster;
using StudyBuddy.Shared.DTOs.EventDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IEventService
     {
         Task<Result<GetEventDTO>> Create(int clientId, CreateEventDTO eventDTO);
         Task<Result<GetEventDTO>> Update(int clientId ,UpdateEventDTO eventDTO);
         Task<Result> Delete(int clientId ,int id);
         Task<Result<GetEventDTO>> GetEventById(int id);
         Task<Result<DataResponse<GetEventDTO>>> GetEvents(int clientId ,int skip, int take);
     }
}
     
