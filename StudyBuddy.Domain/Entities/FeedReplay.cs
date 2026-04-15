using Mapster;
using StudyBuddy.Shared.DTOs.FeedReplayDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class FeedReplay : EntityBase<int>
{
     public int FeedId { get; private set; }    public int ClientUserId { get; private set; }    public string Text { get; private set; } = null!;    public virtual Feed Feed { get; private set; } = null!;
    public virtual ClientUser ClientUser { get; private set; } = null!;

     private FeedReplay() { }

     public static Result<FeedReplay> Create(int clientId, CreateFeedReplayDTO feedReplayDTO)
     {
         var newFeedReplay = new FeedReplay();
         feedReplayDTO.Adapt(newFeedReplay);
        newFeedReplay.ClientUserId = clientId;
         newFeedReplay.CreateDate = DateTime.Now;
         return Result<FeedReplay>.Success(newFeedReplay);
     }

     public Result<FeedReplay> Update(UpdateFeedReplayDTO feedReplayDTO)
     {
         feedReplayDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<FeedReplay>.Success(this);
     }


 }
