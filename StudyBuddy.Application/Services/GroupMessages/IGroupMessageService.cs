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
        Task<Result<GetGroupMessageDTO>> Create(int clientId, CreateGroupMessageDTO groupmessageDTO);
        Task<Result<GetGroupMessageDTO>> Update(int clientId ,UpdateGroupMessageDTO groupmessageDTO);
        Task<Result<GetGroupMessageDTO>> GetGroupMessageById(int clientId ,Guid id);
        Task<Result> Delete(int clientId, Guid id);
        Task<Result<DataResponse<GetGroupMessageDTO>>> GetMessagesForGroup(int clientId, int GroupId, int skip, int take, Order orderby);
    }
}
