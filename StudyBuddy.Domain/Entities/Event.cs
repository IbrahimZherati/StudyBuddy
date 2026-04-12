using Mapster;
using StudyBuddy.Shared.DTOs.EventDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Event : EntityBase<int>
{
     public string Title { get; private set; } = null!;
     public string Description { get; private set; } = null!;
     public DateTime Date { get; private set; }
     public int ClientUserId { get; private set; }
     public virtual ClientUser ClientUser { get; private set; } = null!;

     private Event() { }

     public static Result<Event> Create(int clientId ,CreateEventDTO eventDTO)
     {
         var newEvent = new Event();
         eventDTO.Adapt(newEvent);
        newEvent.ClientUserId = clientId;
         newEvent.CreateDate = DateTime.Now;
         return Result<Event>.Success(newEvent);
     }

     public Result<Event> Update(UpdateEventDTO eventDTO)
     {
         eventDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<Event>.Success(this);
     }


 }
