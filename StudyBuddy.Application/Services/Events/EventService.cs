
using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Events;
using StudyBuddy.Shared.DTOs.EventDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IRepo<Event> eventEntityRepo;
        private readonly IEventDomainService eventEntityDomainService;


        public EventService(IRepo<Event> eventEntityRepo, IEventDomainService eventEntityDomainService)
        {
            this.eventEntityRepo = eventEntityRepo;
            this.eventEntityDomainService = eventEntityDomainService;

        }

        public async Task<Result<GetEventDTO>> Create(int clientId, CreateEventDTO eventEntityDTO)
        {
            var valid = await eventEntityDomainService.Create(clientId,eventEntityDTO);
            if (!valid.IsSuccess)
                return Result<GetEventDTO>.Failure(valid.Error!);

            var result = Event.Create(clientId, eventEntityDTO);

            if (!result.IsSuccess)
                return Result<GetEventDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetEventDTO>.Failure(Error.CreateFailed);

            var eventEntity = result.Value;
            await eventEntityRepo.AddAsync(eventEntity);

            try
            {
                await eventEntityRepo.SaveAsync();
                var dto = eventEntity.Adapt<GetEventDTO>();
                return Result<GetEventDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetEventDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int clientId ,int id)
        {
            var valid = await eventEntityDomainService.Delete(clientId, id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var eventEntity = await eventEntityRepo.GetByIdAsync(id);
            if (eventEntity == null)
                return Result.Failure(Error.EventNotFound);
            eventEntityRepo.Remove(eventEntity);
            try
            {
                await eventEntityRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetEventDTO>> GetEventById(int id)
        {
            var eventEntity = await eventEntityRepo.GetByIdAsync(id);
            if (eventEntity == null)
                return Result<GetEventDTO>.Failure(Error.EventNotFound);
            var eventEntityDTO = eventEntity.Adapt<GetEventDTO>();
            return Result<GetEventDTO>.Success(eventEntityDTO);
        }

        public async Task<Result<DataResponse<GetEventDTO>>> GetEvents(int clientId ,int skip, int take)
        {
            var valid = await eventEntityDomainService.GetEvents(clientId);
            if (!valid.IsSuccess)
                return Result<DataResponse<GetEventDTO>>.Failure(valid.Error!);
            var result = eventEntityRepo.GetQuery().Where(e => e.ClientUserId == clientId);

            var query = result.ProjectToType<GetEventDTO>();

            var data = new DataResponse<GetEventDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderByDescending(q => q.Date).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetEventDTO>>.Success(data);
        }

        public async Task<Result<GetEventDTO>> Update(int clientId, UpdateEventDTO eventEntityDTO)
        {
            var valid = await eventEntityDomainService.Update(clientId, eventEntityDTO);
            if (!valid.IsSuccess)
                return Result<GetEventDTO>.Failure(valid.Error!);

            var eventEntity = await eventEntityRepo.GetByIdAsync(eventEntityDTO.Id);
            if (eventEntity == null)
                return Result<GetEventDTO>.Failure(Error.EventNotFound);

            var result = eventEntity.Update(eventEntityDTO);

            if (!result.IsSuccess)
                return Result<GetEventDTO>.Failure(result.Error!);

            eventEntityRepo.Update(eventEntity);
            try
            {
                await eventEntityRepo.SaveAsync();
                var dto = eventEntity.Adapt<GetEventDTO>();
                return Result<GetEventDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetEventDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
