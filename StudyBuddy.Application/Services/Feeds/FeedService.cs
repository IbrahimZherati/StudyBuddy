using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Feeds
{
    public class FeedService : IFeedService
    {
        private readonly IRepo<ClientUser> clientUserRepo;

        public FeedService(IRepo<ClientUser> clientUserRepo)
        {
            this.clientUserRepo = clientUserRepo;
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

            var random = new Random(clientId);

            // First get recent posts (last 7 days for example)
            var cutoffDate = DateTime.UtcNow.AddDays(-7);
            var recentPosts = await result.Where(p => p.CreateDate >= cutoffDate)
                .ToListAsync();

            var recentPostsRandomized = recentPosts.OrderBy(x => random.Next()).ToList();

            // Then get older posts randomly
            var olderPosts = await result.Where(p => p.CreateDate < cutoffDate)
                 .ToListAsync();  // First get data from database

            var olderPostsRandomized = olderPosts
                .OrderBy(x => random.Next())  // Then randomize in memory
                .ToList();
            // Combine: recent first (ordered), then random older posts
            var combined = recentPostsRandomized.Concat(olderPostsRandomized).ToList();

            var data = new DataResponse<GetPostDTO>();
            data.Count = combined.Count;
            data.Data = combined.Skip(skip).Take(take).ToList();

            return Result<DataResponse<GetPostDTO>>.Success(data);
        }
    }
}
