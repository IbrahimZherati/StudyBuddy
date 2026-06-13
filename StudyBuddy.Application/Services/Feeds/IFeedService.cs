using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Feeds
{
    public interface IFeedService
    {
        Task<Result<DataResponse<GetPostDTO>>> GetFeed(int clientId,int skip, int take);
    }
}
