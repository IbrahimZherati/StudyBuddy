using Mapster;
using StudyBuddy.Shared.DTOs.FeedReplayDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class FeedReplay : EntityBase<int>
{
     public int FeedId { get; private set; }
     public virtual Feed Feed { get; private set; } = null!;

     private FeedReplay() { }

     public static Result<FeedReplay> Create(CreateFeedReplayDTO feedReplayDTO)
     {
         var newFeedReplay = new FeedReplay();
         feedReplayDTO.Adapt(newFeedReplay);
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
