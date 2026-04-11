using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Domain.Entities;
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
        Task<Result<Message>> Create(CreateMessageDTO messageDTO);
        Task<Result> Update(UpdateMessageDTO messageDTO);
        Task<Result> Delete(Guid id);
        Task<Result<DataResponse<GetMessageDTO>>> GetMessagesForPrivateChat(int FirstClientId, int SecondClientId, int skip, int take , Order orderby);
        Task<Result<GetMessageDTO>> GetById(Guid id);
    }
}
