using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Topics;
using StudyBuddy.Shared.DTOs.TopicDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class TopicService : ITopicService
    {
        private readonly IRepo<Topic> topicRepo;
        private readonly ITopicDomainService topicDomainService;


        public TopicService(IRepo<Topic> topicRepo, ITopicDomainService topicDomainService)
        {
            this.topicRepo = topicRepo;
            this.topicDomainService = topicDomainService;

        }

        public async Task<Result<GetTopicDTO>> Create(CreateTopicDTO topicDTO)
        {
            var valid = await topicDomainService.Create(topicDTO);
            if (!valid.IsSuccess)
                return Result<GetTopicDTO>.Failure(valid.Error!);

            var result = Topic.Create(topicDTO);

            if (!result.IsSuccess)
                return Result<GetTopicDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetTopicDTO>.Failure(Error.CreateFailed);

            var topic = result.Value;
            await topicRepo.AddAsync(topic);

            try
            {
                await topicRepo.SaveAsync();
                var dto = topic.Adapt<GetTopicDTO>();
                return Result<GetTopicDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetTopicDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int id)
        {
            var valid = await topicDomainService.Delete(id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var topic = await topicRepo.GetByIdAsync(id);
            if (topic == null)
                return Result.Failure(Error.TopicNotFound);
            topicRepo.Remove(topic);
            try
            {
                await topicRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetTopicDTO>> GetTopicById(int id)
        {
            var topic = await topicRepo.GetByIdAsync(id);
            if (topic == null)
                return Result<GetTopicDTO>.Failure(Error.TopicNotFound);
            var topicDTO = topic.Adapt<GetTopicDTO>();
            return Result<GetTopicDTO>.Success(topicDTO);
        }

        public async Task<Result<DataResponse<GetTopicDTO>>> GetTopics(int skip, int take)
        {
            var result = topicRepo.GetQuery();

            var query = result.ProjectToType<GetTopicDTO>();

            var data = new DataResponse<GetTopicDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetTopicDTO>>.Success(data);
        }

        public async Task<Result<GetTopicDTO>> Update(UpdateTopicDTO topicDTO)
        {
            var valid = await topicDomainService.Update(topicDTO);
            if (!valid.IsSuccess)
                return Result<GetTopicDTO>.Failure(valid.Error!);

            var topic = await topicRepo.GetByIdAsync(topicDTO.Id);
            if (topic == null)
                return Result<GetTopicDTO>.Failure(Error.TopicNotFound);

            var result = topic.Update(topicDTO);

            if (!result.IsSuccess)
                return Result<GetTopicDTO>.Failure(result.Error!);

            topicRepo.Update(topic);
            try
            {
                await topicRepo.SaveAsync();
                var dto = topic.Adapt<GetTopicDTO>();
                return Result<GetTopicDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetTopicDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
