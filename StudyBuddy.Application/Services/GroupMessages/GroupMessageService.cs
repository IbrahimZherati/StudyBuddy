using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.GroupMessages
{
    public class GroupMessageService : IGroupMessageService
    {
        private readonly IRepo<GroupMessage> groupMessageRepo;
        private readonly IRepo<GroupChat> groupChatRepo;
        private readonly IRepo<ClientUser> clientUserRepo;

        public GroupMessageService(
            IRepo<GroupMessage> groupMessageRepo,
            IRepo<GroupChat> groupChatRepo,
            IRepo<ClientUser> clientUserRepo
            )
        {
            this.groupMessageRepo = groupMessageRepo;
            this.groupChatRepo = groupChatRepo;
            this.clientUserRepo = clientUserRepo;
        }
        public async Task<Result> Create(CreateGroupMessageDTO messageDTO)
        {
            if(!await groupChatRepo.ExistsAsync(g => g.Id == messageDTO.GroupChatId))
                return Result.Failure(Error.GroupChatNotFound);
            if (!await clientUserRepo.ExistsAsync(c => c.Id == messageDTO.FromClientUserId))
                return Result.Failure(Error.UserNotFound);
            var message = new GroupMessage
            {
                Text = messageDTO.Text,
                FromClientUserId = messageDTO.FromClientUserId,
                GroupChatId = messageDTO.GroupChatId,
                CreateDate = DateTime.Now
            };
            await groupMessageRepo.AddAsync(message);
            try
            {
                await groupMessageRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.CreateFailed);
            }
            
            return Result.Success();
        }

        public async Task<Result> Delete(int id)
        {
            var message = await groupMessageRepo.GetByIdAsync(id);
            if (message == null)
                return Result.Failure(Error.ItemNotFound);
            groupMessageRepo.Remove(message);
            try
            {
                await groupMessageRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.DeleteFailed);
            }

            return Result.Success();
        }

        public async Task<Result<GetGroupMessageDTO>> GetById(int id)
        {
            var message = await groupMessageRepo.GetByIdAsync(id);
            if (message == null)
                return Result<GetGroupMessageDTO>.Failure(Error.ItemNotFound);
            var messageDTO = message.Adapt<GetGroupMessageDTO>();
            return Result<GetGroupMessageDTO>.Success(messageDTO);
        }

        public async Task<Result<List<GetGroupMessageDTO>>> GetMessagesForGroup(int GroupId, int skip, int take, Order orderby)
        {
            if (!await groupChatRepo.ExistsAsync(g => g.Id == GroupId))
                return Result<List<GetGroupMessageDTO>>.Failure(Error.GroupChatNotFound);

            var result = groupMessageRepo.GetQuery()
                .Where(m => m.GroupChatId == GroupId);
            var query = result.ProjectToType<GetGroupMessageDTO>();

            if (orderby == Order.Asc)
                query = query.OrderBy(m => m.Id);
            else
                query = query.OrderByDescending(m => m.Id);

            var data = await query.Skip(skip).Take(take).ToListAsync();

            return Result<List<GetGroupMessageDTO>>.Success(data);
        }

        public async Task<Result> Update(UpdateGroupMessageDTO messageDTO)
        {
            if (!await groupChatRepo.ExistsAsync(g => g.Id == messageDTO.GroupChatId))
                return Result.Failure(Error.GroupChatNotFound);
            if (!await clientUserRepo.ExistsAsync(c => c.Id == messageDTO.FromClientUserId))
                return Result.Failure(Error.UserNotFound);

            var message = await groupMessageRepo.GetByIdAsync(messageDTO.Id);
            if (message == null)
                return Result.Failure(Error.ItemNotFound);
            messageDTO.Adapt(message);
            groupMessageRepo.Update(message);
            try
            {
                await groupMessageRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.UpdateFailed);
            }

            return Result.Success();
        }
    }
}
