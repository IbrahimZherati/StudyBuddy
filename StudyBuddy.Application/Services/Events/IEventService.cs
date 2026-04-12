using Mapster;
using StudyBuddy.Shared.DTOs.EventDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IEventService
     {
         Task<Result<GetEventDTO>> Create(CreateEventDTO eventDTO);
         Task<Result<GetEventDTO>> Update(UpdateEventDTO eventDTO);
         Task<Result<GetEventDTO>> GetEventById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetEventDTO>>> GetEvents(int clientId ,int skip, int take);
     }
}
     
