using Mapster;
using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.GroupChats
{
    public class GroupChatService : IGroupChatService
    {
        private readonly IRepo<GroupChat> groupChatRepo;
        private readonly IRepo<ClientUserGroupChat> clientUserGroupChatRepo;
        private readonly IRepo<Major> majorRepo;
        private readonly IRepo<University> universityRepo;
        private readonly IRepo<ClientUser> clientUserRepo;

        public GroupChatService(
            IRepo<GroupChat> groupChatRepo ,
            IRepo<ClientUser> clientUserRepo ,
            IRepo<ClientUserGroupChat> clientUserGroupChatRepo,
            IRepo<Major> majorRepo,
            IRepo<University> universityRepo)
        {
            this.groupChatRepo = groupChatRepo;
            this.clientUserGroupChatRepo = clientUserGroupChatRepo;
            this.majorRepo = majorRepo;
            this.universityRepo = universityRepo;
            this.clientUserRepo = clientUserRepo;
        }

        public async Task<Result> AddMemberToGroupChat(int clientId, int groupId)
        {
            var group = await groupChatRepo.GetByIdAsync(groupId);
            if(group == null)
                return Result.Failure(Error.ItemNotFound);
            var client = await clientUserRepo.GetByIdAsync(clientId);
            if(client == null)
                return Result.Failure(Error.UserNotFound);

            if(await clientUserGroupChatRepo.ExistsAsync(cg => cg.GroupChatId == groupId && cg.ClientUserId == clientId))
                return Result.Failure(Error.UserAlreadyInGroupChat);

            var clientUserGroupChat = new ClientUserGroupChat();
            clientUserGroupChat.ClientUserId = clientId;
            clientUserGroupChat.GroupChatId = groupId;
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
            if (!await majorRepo.ExistsAsync(m => m.Id == groupChatDTO.MajorId))
                return Result.Failure(Error.MajorNotFound);
            if (!await universityRepo.ExistsAsync(u => u.Id == groupChatDTO.UniversityId))
                return Result.Failure(Error.UniversityNotFound);
            if (await groupChatRepo.ExistsAsync(g => g.Name == groupChatDTO.Name))
                return Result.Failure(Error.GroupChatNameAlreadyExists);
            var group = new GroupChat();
            groupChatDTO.Adapt(group);

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
            var group = await groupChatRepo.GetByIdAsync(id);
            if(group == null)
                return Result.Failure(Error.ItemNotFound);
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
                return Result<GetGroupChatDTO>.Failure(Error.ItemNotFound);
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
            var group = await groupChatRepo.GetByIdAsync(groupId);
            if (group == null)
                return Result<int>.Failure(Error.ItemNotFound);
            int count = await clientUserGroupChatRepo.GetQuery().Where(cg => cg.GroupChatId == groupId).CountAsync();
            return Result<int>.Success(count);
        }

        public async Task<Result> RemoveMemberFromGroupChat(int clientId, int groupId)
        {
            var group = await groupChatRepo.GetByIdAsync(groupId);
            if (group == null)
                return Result.Failure(Error.ItemNotFound);
            var client = await clientUserRepo.GetByIdAsync(clientId);
            if (client == null)
                return Result.Failure(Error.UserNotFound);

            var clientGroup = await clientUserGroupChatRepo.GetQuery()
                 .FirstOrDefaultAsync(cg => cg.GroupChatId == groupId && cg.ClientUserId == clientId);
            if (clientGroup == null)
                return Result.Failure(Error.UserNotInThisGroup);
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
            if (!await majorRepo.ExistsAsync(m => m.Id == groupChatDTO.MajorId))
                return Result.Failure(Error.MajorNotFound);
            if (!await universityRepo.ExistsAsync(u => u.Id == groupChatDTO.UniversityId))
                return Result.Failure(Error.UniversityNotFound);

            var group = await groupChatRepo.GetByIdAsync(groupChatDTO.Id);
            if (group == null)
                return Result.Failure(Error.ItemNotFound);

            if (await groupChatRepo.ExistsAsync(g => g.Name == groupChatDTO.Name && g.Id != groupChatDTO.Id))
                return Result.Failure(Error.GroupChatNameAlreadyExists);

            groupChatDTO.Adapt(group);
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
