using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using StudyBuddy.Shared.DTOs.TopicDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface ITopicService
     {
         Task<Result<GetTopicDTO>> Create(CreateTopicDTO topicDTO);
         Task<Result<GetTopicDTO>> Update(UpdateTopicDTO topicDTO);
         Task<Result<GetTopicDTO>> GetTopicById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetTopicDTO>>> GetTopics(int skip, int take);
     }
}
     
