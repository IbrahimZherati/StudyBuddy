using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.TopicDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Topics
{
    public class TopicDomainService : ITopicDomainService
    {
        private readonly IRepo<Topic> topicRepo;


        public TopicDomainService(IRepo<Topic> topicRepo
        )
        {
            this.topicRepo = topicRepo;

        }

        public async Task<Result> Create(CreateTopicDTO topicDTO)
        {
            
            if(await topicRepo.ExistsAsync(a => a.Name == topicDTO.Name))
                return Result.Failure(Error.TopicAlreadyExists);
            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await topicRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.TopicNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateTopicDTO topicDTO)
        { 
            if (!await topicRepo.ExistsAsync(a => a.Id == topicDTO.Id))
                return Result.Failure(Error.TopicNotFound);
            
            if (await topicRepo.ExistsAsync(a => a.Name == topicDTO.Name && a.Id != topicDTO.Id ))
                return Result.Failure(Error.TopicAlreadyExists);
            return Result.Success();
        }
    }
}
