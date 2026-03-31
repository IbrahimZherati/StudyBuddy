using Mapster;
using StudyBuddy.Domain.Entities;
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

        public GroupChatService(IRepo<GroupChat> groupChatRepo , IRepo<ClientUserGroupChat> clientUserGroupChatRepo)
        {
            this.groupChatRepo = groupChatRepo;
            this.clientUserGroupChatRepo = clientUserGroupChatRepo;
        }
        public async Task<Result> Create(CreateGroupChatDTO groupChatDTO)
        {
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

        public async Task<Result<List<GetGroupChatDTO>>> GetGroupForClient(int clientId, int skip, int take)
        {
            var result = clientUserGroupChatRepo
                .GetQuery()
                .Include(cg => cg.GroupChat)
                .Where(cg => cg.ClientUserId == clientId)
                .Select(cg => cg.GroupChat);

            var query = result.ProjectToType<GetGroupChatDTO>();

            var data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<List<GetGroupChatDTO>>.Success(data);

        }

        public async Task<Result> Update(UpdateGroupChatDTO groupChatDTO)
        {
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
