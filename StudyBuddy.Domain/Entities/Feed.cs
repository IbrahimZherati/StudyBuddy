using Mapster;
using StudyBuddy.Shared.DTOs.FeedDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Feed : EntityBase<int>
{
     public int ClientUserId { get; private set; }
     public string Description { get; private set; } = null!;
     public int ShareCount { get; private set; }
     public int LikeCount { get; private set; }
     public virtual ClientUser ClientUser { get; private set; } = null!;
     private readonly List<FeedReplay> _feedReplays = new();
     public virtual IReadOnlyCollection<FeedReplay> FeedReplays => _feedReplays;


     private Feed() { }

     public static Result<Feed> Create(CreateFeedDTO feedDTO)
     {
         var newFeed = new Feed();
         feedDTO.Adapt(newFeed);
         newFeed.CreateDate = DateTime.Now;
         return Result<Feed>.Success(newFeed);
     }

     public Result<Feed> Update(UpdateFeedDTO feedDTO)
     {
         feedDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<Feed>.Success(this);
     }


 }
