using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using StudyBuddy.Shared.DTOs.TopicDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Topic : EntityBase<int>
{
    public string Name { get; private set; }


    private Topic() { }

    public static Result<Topic> Create(CreateTopicDTO topicDTO)
    {
        var newTopic = new Topic();
        topicDTO.Adapt(newTopic);
        newTopic.CreateDate = DateTime.Now;
        return Result<Topic>.Success(newTopic);
    }

    public Result<Topic> Update(UpdateTopicDTO topicDTO)
    {
        topicDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<Topic>.Success(this);
    }


}
