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
        Task<Result> Create(CreateMessageDTO messageDTO);
        Task<Result> Update(UpdateMessageDTO messageDTO);
        Task<Result> Delete(int id);
        Task<Result<List<GetMessageDTO>>> GetMessagesForGroup(int GroupId, int skip, int take, Order orderby);
        Task<Result<GetMessageDTO>> GetById(int id);
    }
}
