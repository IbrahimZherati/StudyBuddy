using Mapster;
using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Post : EntityBase<Guid>
{
     public int ClientUserId { get; private set; }
     public byte[] Photo { get; private set; } = null!;
     public string Title { get; private set; } = null!;
     public string Text { get; private set; } = null!;
     public virtual ClientUser ClientUser { get; private set; } = null!;

     private Post() { }

     public static Result<Post> Create(CreatePostDTO postDTO)
     {
         var newPost = new Post();
         postDTO.Adapt(newPost);
         newPost.CreateDate = DateTime.Now;
         return Result<Post>.Success(newPost);
     }

     public Result<Post> Update(UpdatePostDTO postDTO)
     {
         postDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<Post>.Success(this);
     }


 }
