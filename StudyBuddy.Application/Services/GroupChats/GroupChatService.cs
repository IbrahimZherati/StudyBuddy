using Mapster;
using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.GroupChats;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.GroupChats
{
    public class GroupChatService : IGroupChatService
    {
        private readonly IRepo<GroupChat> groupChatRepo;
        private readonly IRepo<ClientUserGroupChat> clientUserGroupChatRepo;
        private readonly IRepo<Major> majorRepo;
        private readonly IRepo<University> universityRepo;
        private readonly IGroupChatDomainService groupChatDomainService;
        private readonly IRepo<ClientUser> clientUserRepo;

        public GroupChatService(
            IRepo<GroupChat> groupChatRepo ,
            IRepo<ClientUser> clientUserRepo ,
            IRepo<ClientUserGroupChat> clientUserGroupChatRepo,
            IRepo<Major> majorRepo,
            IRepo<University> universityRepo,
            IGroupChatDomainService groupChatDomainService)
        {
            this.groupChatRepo = groupChatRepo;
            this.clientUserGroupChatRepo = clientUserGroupChatRepo;
            this.majorRepo = majorRepo;
            this.universityRepo = universityRepo;
            this.groupChatDomainService = groupChatDomainService;
            this.clientUserRepo = clientUserRepo;
        }

       

        public async Task<Result<GetGroupChatDTO>> Create(int currentId, CreateGroupChatDTO groupChatDTO)
        {
            var valid = await groupChatDomainService.Create(currentId,groupChatDTO);
            if (!valid.IsSuccess)
                return Result<GetGroupChatDTO>.Failure(valid.Error!);

            var result = GroupChat.Create(currentId,groupChatDTO);

            if (!result.IsSuccess)
                return Result<GetGroupChatDTO>.Failure(result.Error!);

            if (result.Value == null)
                return Result<GetGroupChatDTO>.Failure(Error.CreateFailed);

            var groupChat = result.Value;
            await groupChatRepo.AddAsync(groupChat);

            try
            {
                await groupChatRepo.SaveAsync();
                var dto = groupChat.Adapt<GetGroupChatDTO>();
                return Result<GetGroupChatDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetGroupChatDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int currentId, int id)
        {
            var valid = await groupChatDomainService.Delete(currentId, id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var groupChat = await groupChatRepo.GetByIdAsync(id);
            if (groupChat == null)
                return Result.Failure(Error.GroupChatNotFound);
            groupChatRepo.Remove(groupChat);
            try
            {
                await groupChatRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetGroupChatDTO>> GetGroupChatById(int id)
        {
            var groupChat = await groupChatRepo.GetByIdAsync(id);
            if (groupChat == null)
                return Result<GetGroupChatDTO>.Failure(Error.GroupChatNotFound);
            var groupChatDTO = groupChat.Adapt<GetGroupChatDTO>();
            return Result<GetGroupChatDTO>.Success(groupChatDTO);
        }

        public async Task<Result<DataResponse<GetGroupChatDTO>>> GetGroupForClient(int clientId, int skip, int take)
        {
            var result = clientUserGroupChatRepo
                .GetQuery()
                .Include(cg => cg.GroupChat)
                .Where(cg => cg.ClientUserId == clientId)
                .Select(cg => cg.GroupChat);

            var query = result.ProjectToType<GetGroupChatDTO>();

            var data = new DataResponse<GetGroupChatDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();

            return Result<DataResponse<GetGroupChatDTO>>.Success(data);

        }

        public async Task<Result<int>> GetGroupMemberCount(int groupId)
        {
            var valid = await groupChatDomainService.GetGroupMemberCount(groupId);
            if (!valid.IsSuccess)
                return Result<int>.Failure(valid.Error!);

            var group = await groupChatRepo.GetByIdAsync(groupId);
            if (group == null)
                return Result<int>.Failure(Error.GroupChatNotFound);

            int count = await clientUserGroupChatRepo.GetQuery().Where(cg => cg.GroupChatId == groupId).CountAsync();
            return Result<int>.Success(count);
        }

        public async Task<Result> RemoveMemberFromGroupChat(int currentId, int clientId, int groupId)
        {
            var valid = await groupChatDomainService.RemoveMemberFromGroupChat(currentId, clientId , groupId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var clientGroup = await clientUserGroupChatRepo.GetQuery()
                 .FirstOrDefaultAsync(cg => cg.GroupChatId == groupId && cg.ClientUserId == clientId);
            if (clientGroup == null)
                return Result.Failure(Error.ClientUserNotInThisGroup);
            clientUserGroupChatRepo.Remove(clientGroup);
            try
            {
                await clientUserGroupChatRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.RemoveFailed);
            }
        }

        public async Task<Result<GetGroupChatDTO>> Update(int currentId, UpdateGroupChatDTO groupChatDTO)
        {
            var valid = await groupChatDomainService.Update(currentId, groupChatDTO);
            if (!valid.IsSuccess)
                return Result<GetGroupChatDTO>.Failure(valid.Error!);

            var groupChat = await groupChatRepo.GetByIdAsync(groupChatDTO.Id);
            if (groupChat == null)
                return Result<GetGroupChatDTO>.Failure(Error.GroupChatNotFound);

            var result = groupChat.Update(groupChatDTO);

            if (!result.IsSuccess)
                return Result<GetGroupChatDTO>.Failure(result.Error!);

            groupChatRepo.Update(groupChat);
            try
            {
                await groupChatRepo.SaveAsync();
                var dto = groupChat.Adapt<GetGroupChatDTO>();
                return Result<GetGroupChatDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetGroupChatDTO>.Failure(Error.UpdateFailed);
            }

        }
        public async Task<Result> AddMemberToGroupChat(int clientId, int groupId)
        {
            var valid = await groupChatDomainService.AddMemberToGroupChat(clientId, groupId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var clientUserGroupChat = ClientUserGroupChat.Create(clientId, groupId);

            await clientUserGroupChatRepo.AddAsync(clientUserGroupChat);
            try
            {
                await clientUserGroupChatRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.AddFailed);
            }

        }
    }
}
