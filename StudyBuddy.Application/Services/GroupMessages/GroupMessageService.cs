using Mapster;
using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.GroupMessages;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.DTOs.UniversityDTO;
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
        private readonly IRepo<GroupMessage,Guid> groupMessageRepo;
        private readonly IRepo<GroupChat> groupChatRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IGroupMessageDomainService groupMessageDomainService;

        public GroupMessageService(
            IRepo<GroupMessage, Guid> groupMessageRepo,
            IRepo<GroupChat> groupChatRepo,
            IRepo<ClientUser> clientUserRepo,
            IGroupMessageDomainService groupMessageDomainService
            )
        {
            this.groupMessageRepo = groupMessageRepo;
            this.groupChatRepo = groupChatRepo;
            this.clientUserRepo = clientUserRepo;
            this.groupMessageDomainService = groupMessageDomainService;
        }
        public async Task<Result> Create(CreateGroupMessageDTO messageDTO)
        {
            var valid = await groupMessageDomainService.Create(messageDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var result = GroupMessage.Create(messageDTO);

            if (!result.IsSuccess)
                return Result.Failure(result.Error!);

            if (result.Value == null)
                return Result.Failure(Error.CreateFailed);

            var groupMessage = result.Value;
            await groupMessageRepo.AddAsync(groupMessage);
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

        public async Task<Result> Delete(Guid id)
        {
            var valid = await groupMessageDomainService.Delete(id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var groupMessage = await groupMessageRepo.GetByIdAsync(id);
            if (groupMessage == null)
                return Result.Failure(Error.MessageNotFound);
            groupMessageRepo.Remove(groupMessage);
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

        public async Task<Result<GetGroupMessageDTO>> GetById(Guid id)
        {
            var message = await groupMessageRepo.GetByIdAsync(id);
            if (message == null)
                return Result<GetGroupMessageDTO>.Failure(Error.MessageNotFound);
            var messageDTO = message.Adapt<GetGroupMessageDTO>();
            return Result<GetGroupMessageDTO>.Success(messageDTO);
        }

        public async Task<Result<DataResponse<GetGroupMessageDTO>>> GetMessagesForGroup(int GroupId, int skip, int take, Order orderby)
        {
            var valid = await groupMessageDomainService.GetMessagesForGroup(GroupId);
            if (!valid.IsSuccess)
                return Result<DataResponse<GetGroupMessageDTO>>.Failure(valid.Error!);

            var result = groupMessageRepo.GetQuery()
                .Where(m => m.GroupChatId == GroupId);

            if (orderby == Order.Asc)
                result = result.OrderBy(m => m.CreateDate);
            else
                result = result.OrderByDescending(m => m.CreateDate);

            var query = result.ProjectToType<GetGroupMessageDTO>();



            var data = new DataResponse<GetGroupMessageDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetGroupMessageDTO>>.Success(data);
        }

        public async Task<Result> Update(UpdateGroupMessageDTO messageDTO)
        {
            var valid = await groupMessageDomainService.Update(messageDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var groupMessage = await groupMessageRepo.GetByIdAsync(messageDTO.Id);
            if (groupMessage == null)
                return Result.Failure(Error.MessageNotFound);

            var result = groupMessage.Update(messageDTO);

            if (!result.IsSuccess)
                return Result.Failure(result.Error!);

            groupMessageRepo.Update(groupMessage);
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
