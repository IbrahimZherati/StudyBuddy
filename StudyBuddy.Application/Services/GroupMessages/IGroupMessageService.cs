using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.GroupMessages
{
    public interface IGroupMessageService
    {
        Task<Result> Create(CreateGroupMessageDTO messageDTO);
        Task<Result> Update(UpdateGroupMessageDTO messageDTO);
        Task<Result> Delete(int id);
        Task<Result<DataResponse<GetGroupMessageDTO>>> GetMessagesForGroup(int GroupId, int skip, int take, Order orderby);
        Task<Result<GetGroupMessageDTO>> GetById(int id);
    }
}
