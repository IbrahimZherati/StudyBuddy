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
        public async Task<Result<GetGroupMessageDTO>> Create(int clientId, CreateGroupMessageDTO groupMessageDTO)
        {
            var valid = await groupMessageDomainService.Create(clientId, groupMessageDTO);
            if (!valid.IsSuccess)
                return Result<GetGroupMessageDTO>.Failure(valid.Error!);

            var result = GroupMessage.Create(clientId,groupMessageDTO);

            if (!result.IsSuccess)
                return Result<GetGroupMessageDTO>.Failure(result.Error!);

            if (result.Value == null)
                return Result<GetGroupMessageDTO>.Failure(Error.CreateFailed);

            var groupMessage = result.Value;
            await groupMessageRepo.AddAsync(groupMessage);

            try
            {
                await groupMessageRepo.SaveAsync();
                var dto = groupMessage.Adapt<GetGroupMessageDTO>();
                return Result<GetGroupMessageDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetGroupMessageDTO>.Failure(Error.CreateFailed);
            }
        }


        public async Task<Result> Delete(int clientId, Guid id)
        {
            var valid = await groupMessageDomainService.Delete(clientId ,id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var groupMessage = await groupMessageRepo.GetByIdAsync(id);
            if (groupMessage == null)
                return Result.Failure(Error.GroupMessageNotFound);
            groupMessageRepo.Remove(groupMessage);
            try
            {
                await groupMessageRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetGroupMessageDTO>> GetGroupMessageById(int clientId ,Guid id)
        {
            var valid = await groupMessageDomainService.GetGroupMessageById(clientId, id);
            if (!valid.IsSuccess)
                return Result<GetGroupMessageDTO>.Failure(valid.Error!);
            var groupMessage = await groupMessageRepo.GetByIdAsync(id);
            if (groupMessage == null)
                return Result<GetGroupMessageDTO>.Failure(Error.GroupMessageNotFound);
            var groupMessageDTO = groupMessage.Adapt<GetGroupMessageDTO>();
            return Result<GetGroupMessageDTO>.Success(groupMessageDTO);
        }

        public async Task<Result<DataResponse<GetGroupMessageDTO>>> GetMessagesForGroup(int clientId ,int GroupId, int skip, int take, Order orderby)
        {
            var valid = await groupMessageDomainService.GetMessagesForGroup(clientId ,GroupId);
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

        public async Task<Result<GetGroupMessageDTO>> Update(int clientId ,UpdateGroupMessageDTO groupMessageDTO)
        {
            var valid = await groupMessageDomainService.Update(clientId, groupMessageDTO);
            if (!valid.IsSuccess)
                return Result<GetGroupMessageDTO>.Failure(valid.Error!);

            var groupMessage = await groupMessageRepo.GetByIdAsync(groupMessageDTO.Id);
            if (groupMessage == null)
                return Result<GetGroupMessageDTO>.Failure(Error.GroupMessageNotFound);

            var result = groupMessage.Update(groupMessageDTO);

            if (!result.IsSuccess)
                return Result<GetGroupMessageDTO>.Failure(result.Error!);

            groupMessageRepo.Update(groupMessage);
            try
            {
                await groupMessageRepo.SaveAsync();
                var dto = groupMessage.Adapt<GetGroupMessageDTO>();
                return Result<GetGroupMessageDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetGroupMessageDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
