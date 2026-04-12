
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.EventDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Events
{
    public class EventDomainService : IEventDomainService
    {
        private readonly IRepo<Event> eventRepo;
        private readonly IRepo<ClientUser> clientUserRepo;


        public EventDomainService(IRepo<Event> eventRepo
        ,IRepo<ClientUser> clientUserRepo
        )
        {
            this.eventRepo = eventRepo;
            this.clientUserRepo = clientUserRepo;

        }

        public async Task<Result> Create(CreateEventDTO eventDTO)
        {
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == eventDTO.ClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await eventRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.EventNotFound);
            return Result.Success();
        }

        public async Task<Result> GetEvents(int clientId)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateEventDTO eventDTO)
        { 
            if (!await eventRepo.ExistsAsync(a => a.Id == eventDTO.Id))
                return Result.Failure(Error.EventNotFound);
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == eventDTO.ClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


            return Result.Success();
        }
    }
}
