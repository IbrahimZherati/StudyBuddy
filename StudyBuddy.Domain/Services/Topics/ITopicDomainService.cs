using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudyBuddy.Shared.DTOs.TopicDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Topics
{
    public interface ITopicDomainService
    {
        Task<Result> Create(CreateTopicDTO topicDTO);
        Task<Result> Update(UpdateTopicDTO topicDTO);
        Task<Result> Delete(int Id);
    } 
}
