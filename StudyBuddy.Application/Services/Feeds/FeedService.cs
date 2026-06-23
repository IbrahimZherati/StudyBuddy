using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.Feeds
{
    public class FeedService : IFeedService
    {
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<Post> postRepo;
        private readonly IRepo<ClientUserLikePost> clientUserLikePost;

        public FeedService(IRepo<ClientUser> clientUserRepo, IRepo<Post> postRepo, IRepo<ClientUserLikePost> clientUserLikePost)
        {
            this.clientUserRepo = clientUserRepo;
            this.postRepo = postRepo;
            this.clientUserLikePost = clientUserLikePost;
        }
        public async Task<Result<DataResponse<GetPostDTO>>> GetFeed(int clientId, int skip, int take)
        {
            var result = clientUserRepo.GetQuery()
                .Where(c => c.Id == clientId)
                .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
                .Union(
                clientUserRepo.GetQuery()
                .Where(c => c.Id == clientId)
                .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
                ).Where(c => c.Id != clientId)
                .SelectMany(c => c.Posts)
                .ProjectToType<GetPostDTO>();


            var skills = await clientUserRepo.GetQuery()
                .Where(c => c.Id == clientId)
                .SelectMany(c => c.ClientUserSkills)
                .Select(s => s.Skill.Name.ToLower())
                .ToListAsync();

            var result2 = clientUserRepo.GetQuery()
                .Where(c => c.ClientUserSkills.Any(s => skills.Contains(s.Skill.Name.ToLower())))
             .SelectMany(c => c.Posts)
                .ProjectToType<GetPostDTO>();

            result = result.Union(result2);
            var resultPosts = postRepo.GetQuery().ProjectToType<GetPostDTO>();

            var random = new Random(clientId);

            // First get recent posts (last 7 days for example)
            var cutoffDate = DateTime.UtcNow.AddDays(-7);
            var recentPostsMostRecommend = await result.Where(p => p.CreateDate >= cutoffDate)
                .ToListAsync();
            var recentPosts = await resultPosts.Where(p => p.CreateDate >= cutoffDate)
                .ToListAsync();
            var recentPostsMostRecommendRandomized = recentPostsMostRecommend.OrderBy(x => random.Next()).ToList();
            var recentPostsRandomized = recentPosts.OrderBy(x => random.Next()).ToList();

            // Then get older posts randomly
            var olderPostsMostRecommend = await result.Where(p => p.CreateDate < cutoffDate)
                 .ToListAsync();  // First get data from database
            var olderPosts = await resultPosts.Where(p => p.CreateDate < cutoffDate)
                 .ToListAsync();  // First get data from database
            var olderPostsMostRecommendRandomized = olderPostsMostRecommend
                .OrderBy(x => random.Next())  // Then randomize in memory
                .ToList();
            var olderPostsRandomized = olderPosts.OrderBy(x => random.Next()).ToList();
            // Combine: recent first (ordered), then random older posts
            var combinedIds = recentPostsMostRecommendRandomized.Concat(recentPostsRandomized).Concat(olderPostsMostRecommendRandomized).Concat(olderPostsRandomized).Select(x => x.Id).Distinct().ToList();

            var combined = await postRepo.GetQuery().Where(p => combinedIds.Contains(p.Id))
                .ProjectToType<GetPostDTO>()
                .ToListAsync();


            var data = new DataResponse<GetPostDTO>();
            data.Count = combined.Count;
            data.Data = combined.Skip(skip).Take(take).ToList();
            foreach (var post in data.Data)
            {
                post.IsLiked = await clientUserLikePost.ExistsAsync(c => c.ClientUserId == clientId && c.PostId == post.Id);
            }
            return Result<DataResponse<GetPostDTO>>.Success(data);
        }
    }
}
