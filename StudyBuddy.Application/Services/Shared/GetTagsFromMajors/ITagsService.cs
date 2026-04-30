using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Shared.GetTagsFromMajors
{
    public interface ITagsService
    {
        Task<Result<ClientUser>> GenerateTags(ClientUser client , string rootPath);
    }
}
