
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.GroupChats
{
    public class GroupChatDomainService : IGroupChatDomainService
    {
        private readonly IRepo<GroupChat> groupChatRepo;
        private readonly IRepo<Major> majorRepo;
        private readonly IRepo<University> universityRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<ClientUserGroupChat> clientUserGroupChatRepo;

        public GroupChatDomainService(IRepo<GroupChat> groupChatRepo
        , IRepo<Major> majorRepo
        , IRepo<University> universityRepo,
            IRepo<ClientUser> clientUserRepo,
            IRepo<ClientUserGroupChat> clientUserGroupChatRepo
        )
        {
            this.groupChatRepo = groupChatRepo;
            this.majorRepo = majorRepo;
            this.universityRepo = universityRepo;
            this.clientUserRepo = clientUserRepo;
            this.clientUserGroupChatRepo = clientUserGroupChatRepo;
        }

        public async Task<Result> AddMemberToGroupChat(int clientId, int groupId)
        {
            var group = await groupChatRepo.GetByIdAsync(groupId);
            if (group == null)
                return Result.Failure(Error.GroupChatNotFound);
            var client = await clientUserRepo.GetByIdAsync(clientId);
            if (client == null)
                return Result.Failure(Error.ClientUserNotFound);

            if (await clientUserGroupChatRepo.ExistsAsync(cg => cg.GroupChatId == groupId && cg.ClientUserId == clientId))
                return Result.Failure(Error.UserAlreadyInGroupChat);

            return Result.Success();
        }

        public async Task<Result> Create(CreateGroupChatDTO groupChatDTO)
        {

            if (!await majorRepo.ExistsAsync(m => m.Id == groupChatDTO.MajorId))
                return Result.Failure(Error.MajorNotFound);


            if (!await universityRepo.ExistsAsync(u => u.Id == groupChatDTO.UniversityId))
                return Result.Failure(Error.UniversityNotFound);


            if (await groupChatRepo.ExistsAsync(a => a.Name == groupChatDTO.Name))
                return Result.Failure(Error.GroupChatAlreadyExists);
            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if (!await groupChatRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.GroupChatNotFound);
            return Result.Success();
        }

        public async Task<Result> GetGroupMemberCount(int groupId)
        {
            if (!await groupChatRepo.ExistsAsync(g => g.Id == groupId))
                return Result.Failure(Error.GroupChatNotFound);
            return Result.Success();
        }

        public async Task<Result> RemoveMemberFromGroupChat(int clientId, int groupId)
        {
            var group = await groupChatRepo.GetByIdAsync(groupId);
            if (group == null)
                return Result.Failure(Error.GroupChatNotFound);
            var client = await clientUserRepo.GetByIdAsync(clientId);
            if (client == null)
                return Result.Failure(Error.ClientUserNotFound);

            return Result.Success();
        }

        public async Task<Result> Update(UpdateGroupChatDTO groupChatDTO)
        {
            if (!await groupChatRepo.ExistsAsync(a => a.Id == groupChatDTO.Id))
                return Result.Failure(Error.GroupChatNotFound);

            if (!await majorRepo.ExistsAsync(m => m.Id == groupChatDTO.MajorId))
                return Result.Failure(Error.MajorNotFound);


            if (!await universityRepo.ExistsAsync(u => u.Id == groupChatDTO.UniversityId))
                return Result.Failure(Error.UniversityNotFound);


            if (await groupChatRepo.ExistsAsync(a => a.Name == groupChatDTO.Name && a.Id != groupChatDTO.Id))
                return Result.Failure(Error.GroupChatAlreadyExists);
            return Result.Success();
        }
    }
}
