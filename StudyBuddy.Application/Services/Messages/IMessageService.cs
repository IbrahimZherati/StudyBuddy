using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Messages
{
    public interface IMessageService
    {
        Task<Result> Create(CreateMessageDTO messageDTO);
        Task<Result> Update(UpdateMessageDTO messageDTO);
        Task<Result> Delete(int id);
        Task<Result<DataResponse<GetMessageDTO>>> GetMessagesForPrivateChat(int FirstClientId, int SecondClientId, int skip, int take , Order orderby);
        Task<Result<GetMessageDTO>> GetById(int id);
    }
}
