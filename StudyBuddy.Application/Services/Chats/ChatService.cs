using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ChatDTOs;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Chats
{
    internal class ChatService : IChatService
    {
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<Message> messageRepo;
        private readonly IRepo<ClientUserGroupChat> clientUserGroupChatRepo;
        private readonly IRepo<GroupMessage> groupMessageRepo;

        public ChatService(IRepo<ClientUser> clientUserRepo,
            IRepo<Message> messageRepo,
            IRepo<ClientUserGroupChat> clientUserGroupChatRepo,
            IRepo<GroupMessage> groupMessageRepo)
        {
            this.clientUserRepo = clientUserRepo;
            this.messageRepo = messageRepo;
            this.clientUserGroupChatRepo = clientUserGroupChatRepo;
            this.groupMessageRepo = groupMessageRepo;
        }

        public async Task<Result<DataResponse<ChatDTO>>> GetGroupChats(int clientId, int skip, int take)
        {
            var result = clientUserGroupChatRepo.GetQuery()
               .Where(g => g.ClientUserId == clientId)
               .Select(g => g.GroupChat);

            var query = result.ProjectToType<ChatDTO>();

            var data = new DataResponse<ChatDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            foreach (var group in data.Data)
            {
                group.UnReadMessages = await groupMessageRepo.GetQuery().Where(m => m.GroupChatId == group.Id &&
                !m.ClientUserGroupMessageReads.Select(cg => cg.ClientUserId)
                .Contains(clientId)).CountAsync();
                var lastMessage = await groupMessageRepo.GetQuery().Where(m => m.GroupChatId == group.Id).OrderByDescending(m => m.CreateDate).FirstOrDefaultAsync();
                group.LastMessage = lastMessage.Adapt<ChatMessageDTO>();
            }
            return Result<DataResponse<ChatDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<ChatDTO>>> GetPrivateChats(int clientId, int skip, int take)
        {
            var result = clientUserRepo.GetQuery()
             .Where(c => c.Id == clientId)
             .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
             .Union(
             clientUserRepo.GetQuery()
             .Where(c => c.Id == clientId)
             .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
             );

           

            var query = result
                .Where(c => c.MessageFromClientUsers.Any(c => c.ToClientUserId == clientId) || c.MessageToClientUsers.Any(c => c.FromClientUserId == clientId))
                .OrderByDescending(c => c.MessageFromClientUsers.Where(c => c.ToClientUserId == clientId).Concat(c.MessageToClientUsers.Where(c => c.FromClientUserId == clientId))
                .Select(m => m.CreateDate)
                .OrderByDescending(m => m)
                .FirstOrDefault())
                .ProjectToType<ChatDTO>();

            var data = new DataResponse<ChatDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.Skip(skip).Take(take).ToListAsync();
            foreach (var friend in data.Data)
            {
                friend.UnReadMessages = await messageRepo.GetQuery().Where(m => m.IsRead == false && m.ToClientUserId == clientId && m.FromClientUserId == friend.Id).CountAsync();
                var lastMessage = await messageRepo.GetQuery().Where(m => m.FromClientUserId == friend.Id && m.ToClientUserId == clientId || m.ToClientUserId == friend.Id && m.FromClientUserId == clientId ).OrderByDescending(c => c.CreateDate).FirstOrDefaultAsync();
                friend.LastMessage = lastMessage.Adapt<ChatMessageDTO>();
            }
            return Result<DataResponse<ChatDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<ChatDTO>>> GetUnReadGroupChats(int clientId, int skip, int take)
        {
            var result = clientUserGroupChatRepo.GetQuery()
                .Where(g => g.ClientUserId == clientId)
                .Select(g => g.GroupChat)
                .Where(g => g.GroupMessages.Any(g => !g.ClientUserGroupMessageReads.Select(c => c.ClientUserId).Contains(clientId)));

            var query = result.ProjectToType<ChatDTO>();

            var data = new DataResponse<ChatDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            foreach (var group in data.Data)
            {
                group.UnReadMessages = await groupMessageRepo.GetQuery().Where(m => m.GroupChatId == group.Id &&
                !m.ClientUserGroupMessageReads.Select(cg => cg.ClientUserId)
                .Contains(clientId)).CountAsync();
                var lastMessage = await groupMessageRepo.GetQuery().Where(m => m.GroupChatId == group.Id).OrderByDescending(m => m.CreateDate).FirstOrDefaultAsync();
                group.LastMessage = lastMessage.Adapt<ChatMessageDTO>();
            }
            return Result<DataResponse<ChatDTO>>.Success(data);

        }

        public async Task<Result<DataResponse<ChatDTO>>> GetUnReadPrivateChats(int clientId, int skip, int take)
        {
            var result = clientUserRepo.GetQuery()
            .Where(c => c.Id == clientId)
            .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
            .Union(
            clientUserRepo.GetQuery()
            .Where(c => c.Id == clientId)
            .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
            )
            .Where(f => f.MessageFromClientUsers.Any(m => m.IsRead == false));

            var query = result.ProjectToType<ChatDTO>();

            var data = new DataResponse<ChatDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            foreach (var friend in data.Data)
            {
                friend.UnReadMessages = await messageRepo.GetQuery().Where(m => m.IsRead == false && m.ToClientUserId == clientId && m.FromClientUserId == friend.Id).CountAsync();
                var lastMessage = await messageRepo.GetQuery().Where(m => m.FromClientUserId == friend.Id && m.ToClientUserId == clientId || m.ToClientUserId == friend.Id && m.FromClientUserId == clientId ).OrderByDescending(c => c.CreateDate).FirstOrDefaultAsync();
                friend.LastMessage = lastMessage.Adapt<ChatMessageDTO>();
            }
            return Result<DataResponse<ChatDTO>>.Success(data);

        }


    }
}
