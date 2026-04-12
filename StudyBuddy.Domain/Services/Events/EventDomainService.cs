
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
        , IRepo<ClientUser> clientUserRepo
        )
        {
            this.eventRepo = eventRepo;
            this.clientUserRepo = clientUserRepo;

        }

        public async Task<Result> Create(int clientId, CreateEventDTO eventDTO)
        {

            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);


            return Result.Success();
        }

        public async Task<Result> Delete(int clientId, int Id)
        {
            var eventEntity = await eventRepo.GetByIdAsync(Id);
            if (eventEntity == null)
                return Result.Failure(Error.EventNotFound);

            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            if (eventEntity.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);
            return Result.Success();
        }

        public async Task<Result> GetEvents(int clientId)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(int clientId, UpdateEventDTO eventDTO)
        {
            var eventEntity = await eventRepo.GetByIdAsync(eventDTO.Id);
            if (eventEntity == null)
                return Result.Failure(Error.EventNotFound);

            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            if (eventEntity.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);




            return Result.Success();
        }
    }
}
