using Mapster;
using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Post : EntityBase<Guid>
{
     public int ClientUserId { get; private set; }
     public byte[]? Photo { get; private set; } 
     public string Title { get; private set; } = null!;
     public string Text { get; private set; } = null!;    public int ShareCount { get; private set; }    public virtual ClientUser ClientUser { get; private set; } = null!;

    private readonly List<ClientUserLikePost> _clientUserLikePosts = new();
    public virtual IReadOnlyCollection<ClientUserLikePost> ClientUserLikePosts => _clientUserLikePosts;

    private readonly List<PostReply> _postReplys = new();
    public virtual IReadOnlyCollection<PostReply> PostReplys => _postReplys;



    private Post() { }

     public static Result<Post> Create(int clientId, CreatePostDTO postDTO)
     {
         var newPost = new Post();
         postDTO.Adapt(newPost);
        newPost.ClientUserId = clientId;
         newPost.CreateDate = DateTime.Now;
         return Result<Post>.Success(newPost);
     }

     public Result<Post> Update(UpdatePostDTO postDTO)
     {
         postDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<Post>.Success(this);
     }


    public void Share()
    {
        ShareCount++;
    }

    public void Like(ClientUserLikePost like)
    {
        if (like == null)
            return;
        if (_clientUserLikePosts.Contains(like))
            return;
        _clientUserLikePosts.Add(like);
        return;
    }

    public void UnLike(ClientUserLikePost like)
    {
        if (like == null)
            return;
        if(!_clientUserLikePosts.Contains(like))
            return;
        _clientUserLikePosts.Remove(like);
        return;
    }
 }
