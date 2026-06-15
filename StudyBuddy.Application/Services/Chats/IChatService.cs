using StudyBuddy.Shared.DTOs.ChatDTOs;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Chats
{
    public interface IChatService
    {
        Task<Result<DataResponse<ChatDTO>>> GetGroupChats(int clientId, int skip, int take);
        Task<Result<DataResponse<ChatDTO>>> GetPrivateChats(int clientId, int skip, int take);
        Task<Result<DataResponse<ChatDTO>>> GetUnReadPrivateChats(int clientId, int skip, int take);
        Task<Result<DataResponse<ChatDTO>>> GetUnReadGroupChats(int clientId, int skip, int take);
    }
}
