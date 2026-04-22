using Mapster;
using StudyBuddy.Application.Services.Shared.AutoGenerateSkills;
using StudyBuddy.Application.Services.Shared.GetTagsFromMajors;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.ClientUsers;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.FriendRequestDTO;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.ClientUsers
{
    public class ClientUserService : IClientUserService
    {
        private readonly IRepo<Major> majorRepo;
        private readonly IRepo<University> universityRepo;
        private readonly IRepo<City> cityRepo;
        private readonly IRepo<Country> countryRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<Skill> skillRepo;
        private readonly IRepo<ClientUserSkill> clientUserSkillRepo;
        private readonly IRepo<GroupMessage> groupMessageRepo;
        private readonly IRepo<Message> messageRepo;
        private readonly IRepo<ClientUserAvailableDay> clientUserAvailableDayRepo;
        private readonly IRepo<FriendRequest> friendRequestRepo;
        private readonly IRepo<Friend> friendRepo;
        private readonly IRepo<ClientUserGroupChat> clientUserGroupChatRepo;
        private readonly IClientUserDomainService clientUserDomainService;
        private readonly ITagsService tagsService;

        public ClientUserService(
            IRepo<Major> majorRepo,
            IRepo<University> universityRepo,
            IRepo<City> cityRepo,
            IRepo<Country> countryRepo,
            IRepo<ClientUser> clientUserRepo,
            IRepo<Skill> skillRepo,
            IRepo<ClientUserSkill> clientUserSkillRepo,
            IRepo<GroupMessage> groupMessageRepo,
            IRepo<Message> messageRepo,
            IRepo<ClientUserAvailableDay> clientUserAvailableDayRepo,
            IRepo<FriendRequest> friendRequestRepo,
            IRepo<Friend> friendRepo,
            IRepo<ClientUserGroupChat> clientUserGroupChatRepo,
            IClientUserDomainService clientUserDomainService,
            ITagsService tagsService

            )
        {
            this.majorRepo = majorRepo;
            this.universityRepo = universityRepo;
            this.cityRepo = cityRepo;
            this.countryRepo = countryRepo;
            this.clientUserRepo = clientUserRepo;
            this.skillRepo = skillRepo;
            this.clientUserSkillRepo = clientUserSkillRepo;
            this.groupMessageRepo = groupMessageRepo;
            this.messageRepo = messageRepo;
            this.clientUserAvailableDayRepo = clientUserAvailableDayRepo;
            this.friendRequestRepo = friendRequestRepo;
            this.friendRepo = friendRepo;
            this.clientUserGroupChatRepo = clientUserGroupChatRepo;
            this.clientUserDomainService = clientUserDomainService;
            this.tagsService = tagsService;
        }

        public async Task<Result> AcceptFriendReqesut(int clientUserId, int requestId)
        {
            var valid = await clientUserDomainService.AcceptFriendReqesut(clientUserId, requestId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var request = await friendRequestRepo.GetByIdAsync(requestId);
            if (request == null)
                return Result.Failure(Error.FriendRequestNotFound);
            var friendShip = Friend.Create(request.FromClientUserId, request.ToClientUserId);
            friendRequestRepo.Remove(request);
            await friendRepo.AddAsync(friendShip);
            try
            {
                await friendRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.AddFailed);
            }
        }

        public async Task<Result> FriendReqesut(int clientUserId, int reqesutClientUserId)
        {
            var valid = await clientUserDomainService.FriendReqesut(clientUserId, reqesutClientUserId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var result = FriendRequest.Create(clientUserId, reqesutClientUserId);
            if (!result.IsSuccess)
                return Result.Failure(result.Error!);
            var request = result.Value;
            if (request == null)
                return Result.Failure(Error.FriendRequestNotFound);
            await friendRequestRepo.AddAsync(request);
            try
            {
                await friendRequestRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.RequestFailed);
            }
        }

        public async Task<Result<DataResponse<GetFriendRequestDTO>>> GetFriendRequest(int clientUserId, int skip, int take)
        {
            var reuslt = friendRequestRepo.GetQuery()
                .Where(f => f.ToClientUserId == clientUserId);
            var query = reuslt.ProjectToType<GetFriendRequestDTO>();
            var data = new DataResponse<GetFriendRequestDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetFriendRequestDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<InfoClientUserDTO>>> GetFriends(int clientUserId, int skip, int take)
        {

            var result = clientUserRepo.GetQuery()
                .Where(c => c.Id == clientUserId)
                .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
                .Union(
                clientUserRepo.GetQuery()
                .Where(c => c.Id == clientUserId)
                .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
                );

            var query = result.ProjectToType<InfoClientUserDTO>();

            var data = new DataResponse<InfoClientUserDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<InfoClientUserDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<InfoGroupChatDTO>>> GetGroups(int clientUserId, int skip, int take)
        {
            var result =  clientUserGroupChatRepo.GetQuery()
                .Where(g => g.ClientUserId == clientUserId)
                .Select(g => g.GroupChat);

            var query = result.ProjectToType<InfoGroupChatDTO>();

            var data = new DataResponse<InfoGroupChatDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<InfoGroupChatDTO>>.Success(data);
        }

        public async Task<Result<GetProfileClientUserDTO>> GetProfile(Guid userId)
        {
            var profile = await clientUserRepo.GetQuery()
                .Where(c => c.UserId == userId)
                .ProjectToType<GetProfileClientUserDTO>()
                .FirstOrDefaultAsync();

            if (profile == null)
                return Result<GetProfileClientUserDTO>.Failure(Error.ClientUserNotFound);

            profile.FavoriteGroups = await groupMessageRepo.GetQuery()
                .Where(g => g.FromClientUserId == profile.Id)
                .GroupBy(g => g.GroupChatId)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new InfoGroupChatDTO
                {
                    Id = g.Key,
                    Name = g.First().GroupChat.Name,
                    Photo = g.First().GroupChat.Photo,
                    Bio = g.First().GroupChat.Bio,
                    Major = g.First().GroupChat.Major.Name,
                    MemberCount = g.First().GroupChat.ClientUserGroupChats.Count()
                })
                .ToListAsync();

            profile.BestBuddies = await messageRepo.GetQuery()
                .Where(m => m.FromClientUserId == profile.Id)
                .GroupBy(m => m.ToClientUserId)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new InfoClientUserDTO
                {
                    Id = g.Key,
                    UserName = g.First().ToClientUser.UserName,
                    Major = (g.First().ToClientUser.Major != null)
                            ? g.First().ToClientUser.Major!.Name
                            : string.Empty,
                    University = g.First().ToClientUser.University != null
                                 ? g.First().ToClientUser.University!.Name
                                 : string.Empty
                })
                .ToListAsync();

            return Result<GetProfileClientUserDTO>.Success(profile);
        }





        public async Task<Result<InfoClientUserDTO>> Update(int clientId, UpdateClientUserDTO clientUserDTO , string rootPath)
        {
            var valid = await clientUserDomainService.Update(clientId, clientUserDTO);
            if (!valid.IsSuccess)
                return Result<InfoClientUserDTO>.Failure(valid.Error!);

            var clientUser = await clientUserRepo.GetQuery()
                .Where(c => c.Id == clientId)
                .Include(c => c.Major)
                .FirstOrDefaultAsync();
            if (clientUser == null)
                return Result<InfoClientUserDTO>.Failure(Error.ClientUserNotFound);

            //Generate Skills

            //Check Bio Change
            if (clientUser.Bio != clientUserDTO.Bio)
            {
                clientUser.UpdateBio(clientUserDTO.Bio);

                var reuslt = await tagsService.GenerateTags(clientUser, rootPath);
                if(!reuslt.IsSuccess)
                    return Result<InfoClientUserDTO>.Failure(reuslt.Error!);
                if (reuslt.Value != null)
                    clientUser = reuslt.Value;

                //Delete Old Skills
                var oldSkills = await clientUserSkillRepo.GetQuery()
                    .Where(cs => cs.ClientUserId == clientId)
                    .ToListAsync();
                clientUserSkillRepo.RemoveRange(oldSkills);


            }

            //Delete Old available days
            var oldDays = await clientUserAvailableDayRepo.GetQuery()
                .Where(cd => cd.ClientUserId == clientId)
                .ToListAsync();
            clientUserAvailableDayRepo.RemoveRange(oldDays);

            var newDays = new List<ClientUserAvailableDay>();

            foreach (var day in clientUserDTO.availableDays)
            {
                var newClientUserAvailableDay = ClientUserAvailableDay.Create(clientId, day.Id);

                await clientUserAvailableDayRepo.AddAsync(newClientUserAvailableDay);
            }
            var resultUpdateClientUser = clientUser.Update(clientUserDTO);
            if (!resultUpdateClientUser.IsSuccess)
                return Result<InfoClientUserDTO>.Failure(resultUpdateClientUser.Error!);
            clientUserRepo.Update(clientUser);
            try
            {
                await clientUserRepo.SaveAsync();
                var dto = clientUser.Adapt<InfoClientUserDTO>();
                return Result<InfoClientUserDTO>.Success(dto);

            }
            catch (DbUpdateException e)
            {
                return Result<InfoClientUserDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
