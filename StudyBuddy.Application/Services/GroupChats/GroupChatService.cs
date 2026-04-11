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

        public async Task<Result> AddMemberToGroupChat(int clientId, int groupId)
        {
            var valid = await groupChatDomainService.AddMemberToGroupChat(clientId, groupId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var clientUserGroupChat = ClientUserGroupChat.Create(clientId , groupId);
           
            await clientUserGroupChatRepo.AddAsync(clientUserGroupChat);
            try
            {
                await clientUserGroupChatRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.AddFailed);
            }

            return Result.Success();
        }

        public async Task<Result> Create(CreateGroupChatDTO groupChatDTO)
        {
            var valid = await groupChatDomainService.Create(groupChatDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var resultCreateGroupChat = GroupChat.Create(groupChatDTO);
            if (!resultCreateGroupChat.IsSuccess)
                return Result.Failure(resultCreateGroupChat.Error!);

            var group = resultCreateGroupChat.Value;
            if (group == null)
                return Result.Failure(Error.GroupChatNotFound);
            await groupChatRepo.AddAsync(group);
            try
            {
                await groupChatRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.CreateFailed);
            }

            return Result.Success();
        }

        public async Task<Result> Delete(int id)
        {
            var valid = await groupChatDomainService.Delete(id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var group = await groupChatRepo.GetByIdAsync(id);
            if(group == null)
                return Result.Failure(Error.GroupChatNotFound);
            groupChatRepo.Remove(group);
            try
            {
                await groupChatRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.DeleteFailed);
            }
            return Result.Success();
        }

        public async Task<Result<GetGroupChatDTO>> GetById(int id)
        {
            var group = await groupChatRepo.GetByIdAsync(id);
            if (group == null)
                return Result<GetGroupChatDTO>.Failure(Error.GroupChatNotFound);
            var groupDTO = group.Adapt<GetGroupChatDTO>();
            return Result<GetGroupChatDTO>.Success(groupDTO);
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
            data.Data = await query.Skip(skip).Take(take).ToListAsync();

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

        public async Task<Result> RemoveMemberFromGroupChat(int clientId, int groupId)
        {
            var valid = await groupChatDomainService.RemoveMemberFromGroupChat(clientId , groupId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var clientGroup = await clientUserGroupChatRepo.GetQuery()
                 .FirstOrDefaultAsync(cg => cg.GroupChatId == groupId && cg.ClientUserId == clientId);
            if (clientGroup == null)
                return Result.Failure(Error.GroupChatNotFound);
            clientUserGroupChatRepo.Remove(clientGroup);
            try
            {
                await clientUserGroupChatRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.RemoveFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Update(UpdateGroupChatDTO groupChatDTO)
        {
            var valid = await groupChatDomainService.Update(groupChatDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var group = await groupChatRepo.GetByIdAsync(groupChatDTO.Id);
            if (group == null)
                return Result.Failure(Error.GroupChatNotFound);

            var resultUpdateGroupChat = group.Update(groupChatDTO);
            if (!resultUpdateGroupChat.IsSuccess)
                return Result.Failure(resultUpdateGroupChat.Error!);
            groupChatRepo.Update(group);
            try
            {
                await groupChatRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.UpdateFailed);
            }

            return Result.Success();
        }
    }
}
