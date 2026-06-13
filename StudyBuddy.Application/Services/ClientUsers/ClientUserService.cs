using Mapster;
using StudyBuddy.Application.Abstractions;
using StudyBuddy.Application.Services.Notifications;
using StudyBuddy.Application.Services.Shared.AutoGenerateSkills;
using StudyBuddy.Application.Services.Shared.GetTagsFromMajors;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.ClientUsers;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.FriendRequestDTO;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.DTOs.GroupInviteDTOs;
using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.DTOs.StudyInterestDTO;
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
        private readonly IRepo<Message, Guid> messageRepo;
        private readonly IRepo<ClientUserAvailableDay> clientUserAvailableDayRepo;
        private readonly IRepo<FriendRequest> friendRequestRepo;
        private readonly IRepo<Friend> friendRepo;
        private readonly IRepo<ClientUserGroupChat> clientUserGroupChatRepo;
        private readonly IRepo<StudyInterest> studyInterestRepo;
        private readonly IRepo<GroupInvite> groupInviteRepo;
        private readonly IClientUserDomainService clientUserDomainService;
        private readonly IRepo<Notification> notificationRepo;
        private readonly IRepo<GroupChat> groupChatRepo;
        private readonly IRepo<NotificationType> notificationTypeRepo;
        private readonly IRepo<ClientUserGroupMessageRead> clientUserGroupMessageReadRepo;
        private readonly ITagsService tagsService;
        private readonly INotificationService notificationService;

        public ClientUserService(
            IRepo<Major> majorRepo,
            IRepo<University> universityRepo,
            IRepo<City> cityRepo,
            IRepo<Country> countryRepo,
            IRepo<ClientUser> clientUserRepo,
            IRepo<Skill> skillRepo,
            IRepo<ClientUserSkill> clientUserSkillRepo,
            IRepo<GroupMessage> groupMessageRepo,
            IRepo<Message,Guid> messageRepo,
            IRepo<ClientUserAvailableDay> clientUserAvailableDayRepo,
            IRepo<FriendRequest> friendRequestRepo,
            IRepo<Friend> friendRepo,
            IRepo<ClientUserGroupChat> clientUserGroupChatRepo,
            IRepo<StudyInterest> studyInterestRepo,
            IRepo<GroupInvite> groupInviteRepo,
            IClientUserDomainService clientUserDomainService,
            IRepo<Notification> notificationRepo,
            IRepo<GroupChat> groupChatRepo,
            IRepo<NotificationType> notificationTypeRepo,
            IRepo<ClientUserGroupMessageRead> clientUserGroupMessageReadRepo,
            ITagsService tagsService,
            INotificationService notificationService


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
            this.studyInterestRepo = studyInterestRepo;
            this.groupInviteRepo = groupInviteRepo;
            this.clientUserDomainService = clientUserDomainService;
            this.notificationRepo = notificationRepo;
            this.groupChatRepo = groupChatRepo;
            this.notificationTypeRepo = notificationTypeRepo;
            this.clientUserGroupMessageReadRepo = clientUserGroupMessageReadRepo;
            this.tagsService = tagsService;
            this.notificationService = notificationService;
        }

        public async Task<Result> AcceptFriendRequest(int clientUserId, int requestId)
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

        public async Task<Result> AcceptGroupInviteRequest(int clientUserId, int requestId)
        {
            var valid = await clientUserDomainService.AcceptGroupInviteReqesut(clientUserId, requestId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var request = await groupInviteRepo.GetByIdAsync(requestId);
            if (request == null)
                return Result.Failure(Error.InviteNotFound);
            var member = ClientUserGroupChat.Create(request.ClientUserToId, request.GroupChatId);
            groupInviteRepo.Remove(request);
            await clientUserGroupChatRepo.AddAsync(member);
            try
            {
                await groupInviteRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.AddFailed);
            }

        }

        public async Task<Result> FriendRequest(int clientUserId, int reqesutClientUserId)
        {
            var valid = await clientUserDomainService.FriendReqesut(clientUserId, reqesutClientUserId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var result = Domain.Entities.FriendRequest.Create(clientUserId, reqesutClientUserId);
            if (!result.IsSuccess)
                return Result.Failure(result.Error!);
            var request = result.Value;
            if (request == null)
                return Result.Failure(Error.FriendRequestNotFound);
            await friendRequestRepo.AddAsync(request);
            try
            {
                await friendRequestRepo.SaveAsync();
                await notificationService.Create(new CreateNotificationDTO
                {
                    FromClientUserId = clientUserId,
                    RequestId = request.Id,
                    ToClientUserId = reqesutClientUserId,
                    Type = NotificationTypes.FriendRequest.ToString(),
                    Title = "Friend Request",
                    Description = "this person need request friend"

                });
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

        public async Task<Result<DataResponse<FriendInfoDTO>>> GetFriends(int clientUserId, int skip, int take)
        {

            var result = clientUserRepo.GetQuery()
                .Where(c => c.Id == clientUserId)
                .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
                .Union(
                clientUserRepo.GetQuery()
                .Where(c => c.Id == clientUserId)
                .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
                );

            

            var query = result.ProjectToType<FriendInfoDTO>();

            var data = new DataResponse<FriendInfoDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            foreach (var friend in data.Data)
            {
                friend.UnReadMessageCount = await messageRepo.GetQuery().Where(m => m.IsRead == false && m.ToClientUserId == clientUserId && m.FromClientUserId == friend.Id).CountAsync();
                var lastMessage = await messageRepo.GetQuery().Where(m => m.FromClientUserId == friend.Id && m.ToClientUserId == clientUserId).OrderByDescending(c => c.CreateDate).FirstOrDefaultAsync();
                friend.LastMessage = lastMessage.Adapt<GetMessageDTO>();
            }
            return Result<DataResponse<FriendInfoDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<InfoClientUserDTO>>> GetFriendsFriends(int clientUserId, int skip, int take)
        {
            var result = clientUserRepo.GetQuery()
                .Where(c => c.Id == clientUserId)
                .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
                .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
                .Union(clientUserRepo.GetQuery()
                .Where(c => c.Id == clientUserId)
                .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
                .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
                   .Union(clientUserRepo.GetQuery()
                .Where(c => c.Id == clientUserId)
                .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
                .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
                .Union(clientUserRepo.GetQuery()
                .Where(c => c.Id == clientUserId)
                .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
                .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend)))));

            result = result.Where(c => c.Id != clientUserId);

            var query = result.ProjectToType<InfoClientUserDTO>();

            var data = new DataResponse<InfoClientUserDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<InfoClientUserDTO>>.Success(data);

        }

        public async Task<Result<DataResponse<JoinedGroupInfo>>> GetGroups(int clientUserId, int skip, int take)
        {
            var result = clientUserGroupChatRepo.GetQuery()
                .Where(g => g.ClientUserId == clientUserId)
                .Select(g => g.GroupChat);

            var query = result.ProjectToType<JoinedGroupInfo>();

            var data = new DataResponse<JoinedGroupInfo>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            foreach(var group in data.Data)
            {
                group.UnReadCount = await groupMessageRepo.GetQuery().Where(m => m.GroupChatId == group.Id && 
                !m.ClientUserGroupMessageReads.Select(cg => cg.ClientUserId)
                .Contains(clientUserId)).CountAsync();
                var lastMessage = await groupMessageRepo.GetQuery().Where(m => m.GroupChatId == group.Id).OrderByDescending(m => m.CreateDate).FirstOrDefaultAsync();
                group.LastMessage = lastMessage.Adapt<GetGroupMessageDTO>();
            }
            return Result<DataResponse<JoinedGroupInfo>>.Success(data);
        }

        public async Task<Result<DataResponse<GetGroupInviteDTO>>> GetInvitesRequest(int clientUserId, int skip, int take)
        {
            var reuslt = groupInviteRepo.GetQuery()
            .Where(f => f.ClientUserToId == clientUserId);
            var query = reuslt.ProjectToType<GetGroupInviteDTO>();
            var data = new DataResponse<GetGroupInviteDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetGroupInviteDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<GetNotificationDTO>>> GetAllNotifications(int clientUserId, int skip, int take, Order orderby)
        {
            var result = notificationRepo.GetQuery().Where(n => n.ToClientUserId == clientUserId);


            if (orderby == Order.Asc)
                result = result.OrderBy(n => n.CreateDate);
            else
                result = result.OrderByDescending(n => n.CreateDate);

            var query = result.ProjectToType<GetNotificationDTO>();

            var data = new DataResponse<GetNotificationDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetNotificationDTO>>.Success(data);
        }

        public async Task<Result<GetProfileClientUserDTO>> GetProfile(int currentId, Guid userId)
        {
            var profile = await clientUserRepo.GetQuery()
                .Where(c => c.UserId == userId)
                .ProjectToType<GetProfileClientUserDTO>()
                .FirstOrDefaultAsync();

            if (profile == null)
                return Result<GetProfileClientUserDTO>.Failure(Error.ClientUserNotFound);

            //isFriend
            profile.IsFriend = await friendRepo.ExistsAsync(c =>
             (c.FirstFriendId == currentId && c.SecondFriendId == profile.Id)
             ||
             (c.SecondFriendId == currentId && c.FirstFriendId == profile.Id)
             );

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
                    Bio = g.First().ToClientUser.Bio,
                    IsSkillFromMajor = g.First().ToClientUser.IsSkillFromMajor,
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

        public async Task<Result<GetProfileClientUserDTO>> GetProfileByClientId(int currentId,int clientId)
        {
            var profile = await clientUserRepo.GetQuery()
              .Where(c => c.Id == clientId)
              .ProjectToType<GetProfileClientUserDTO>()
              .FirstOrDefaultAsync();

            if (profile == null)
                return Result<GetProfileClientUserDTO>.Failure(Error.ClientUserNotFound);

            //isFriend
            profile.IsFriend = await friendRepo.ExistsAsync(c =>
             (c.FirstFriendId == currentId && c.SecondFriendId == profile.Id)
             ||
             (c.SecondFriendId == currentId && c.FirstFriendId == profile.Id)
             );

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

        public async Task<Result> GroupInviteRequest(int clientUserId, int requestClientUserId, int groupId)
        {
            var valid = await clientUserDomainService.GroupInviteReqesut(clientUserId, requestClientUserId);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var result = GroupInvite.Create(clientUserId, requestClientUserId, groupId);
            if (!result.IsSuccess)
                return Result.Failure(result.Error!);
            var request = result.Value;
            if (request == null)
                return Result.Failure(Error.FriendRequestNotFound);
            var group = await groupChatRepo.GetByIdAsync(groupId);
            if (group == null)
                return Result.Failure(Error.GroupChatNotFound);
            await groupInviteRepo.AddAsync(request);
            try
            {
                await groupInviteRepo.SaveAsync();
                await notificationService.Create(new CreateNotificationDTO
                {
                    FromClientUserId = clientUserId,
                    ToClientUserId = requestClientUserId,
                    GroupChatId = groupId,
                    Type = NotificationTypes.GroupInvite.ToString(),
                    Title = "Group Invite",
                    Description = $"invited you to {group.Name}"

                });
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.RequestFailed);
            }
        }

        public async Task<Result<InfoClientUserDTO>> Update(int clientId, UpdateClientUserDTO clientUserDTO, string rootPath)
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
                if (!reuslt.IsSuccess)
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

            foreach (var dayId in clientUserDTO.availableDayIds)
            {
                var newClientUserAvailableDay = ClientUserAvailableDay.Create(clientId, dayId);

                await clientUserAvailableDayRepo.AddAsync(newClientUserAvailableDay);
            }

            //Delete Old StudyIntrests
            var oldStudyInterests = await studyInterestRepo.GetQuery()
                .Where(s => s.ClientUserId == clientId)
                .ToListAsync();
            studyInterestRepo.RemoveRange(oldStudyInterests);

            var newStudyInterests = new List<StudyInterest>();

            foreach (var interest in clientUserDTO.studyInterests)
            {
                var newStudyInterest = StudyInterest.Create(clientId, new CreateStudyInterestDTO
                {
                    Name = interest.Name,
                });
                await studyInterestRepo.AddAsync(newStudyInterest);

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

        public async Task<Result<DataResponse<GetNotificationDTO>>> GetFriendRequestNotifications(int clientUserId, int skip, int take, Order orderby)
        {
            var result = notificationRepo.GetQuery()
                .Where(n => n.ToClientUserId == clientUserId && n.NotificationType.Type == NotificationTypes.FriendRequest.ToString());


            if (orderby == Order.Asc)
                result = result.OrderBy(n => n.CreateDate);
            else
                result = result.OrderByDescending(n => n.CreateDate);

            var query = result.ProjectToType<GetNotificationDTO>();

            var data = new DataResponse<GetNotificationDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetNotificationDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<GetNotificationDTO>>> GetGroupInviteNotifications(int clientUserId, int skip, int take, Order orderby)
        {
            var result = notificationRepo.GetQuery()
                .Where(n => n.ToClientUserId == clientUserId && n.NotificationType.Type == NotificationTypes.GroupInvite.ToString());


            if (orderby == Order.Asc)
                result = result.OrderBy(n => n.CreateDate);
            else
                result = result.OrderByDescending(n => n.CreateDate);

            var query = result.ProjectToType<GetNotificationDTO>();

            var data = new DataResponse<GetNotificationDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetNotificationDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<GetNotificationDTO>>> GetMessageChatNotifications(int clientUserId, int skip, int take, Order orderby)
        {
            var result = notificationRepo.GetQuery()
               .Where(n => n.ToClientUserId == clientUserId && n.NotificationType.Type == NotificationTypes.Message.ToString());


            if (orderby == Order.Asc)
                result = result.OrderBy(n => n.CreateDate);
            else
                result = result.OrderByDescending(n => n.CreateDate);

            var query = result.ProjectToType<GetNotificationDTO>();

            var data = new DataResponse<GetNotificationDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetNotificationDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<FriendInfoDTO>>> GetUnReadFriends(int clientUserId, int skip, int take)
        {
            var result = clientUserRepo.GetQuery()
             .Where(c => c.Id == clientUserId)
             .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
             .Union(
             clientUserRepo.GetQuery()
             .Where(c => c.Id == clientUserId)
             .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
             )
             .Where(f => f.MessageFromClientUsers.Any(m => m.IsRead == false));



            var query = result.ProjectToType<FriendInfoDTO>();

            var data = new DataResponse<FriendInfoDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            foreach (var friend in data.Data)
            {
                friend.UnReadMessageCount = await messageRepo.GetQuery().Where(m => m.IsRead == false && m.ToClientUserId == clientUserId && m.FromClientUserId == friend.Id).CountAsync();
                var lastMessage = await messageRepo.GetQuery().Where(m => m.FromClientUserId == friend.Id && m.ToClientUserId == clientUserId).OrderByDescending(c => c.CreateDate).FirstOrDefaultAsync();
                friend.LastMessage = lastMessage.Adapt<GetMessageDTO>();

            }
            return Result<DataResponse<FriendInfoDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<JoinedGroupInfo>>> GetUnReadGroups(int clientUserId, int skip, int take)
        {
            var result = clientUserGroupChatRepo.GetQuery()
             .Where(g => g.ClientUserId == clientUserId)
             .Select(g => g.GroupChat)
             .Where(g => g.GroupMessages.Any(g => !g.ClientUserGroupMessageReads.Select(c => c.ClientUserId).Contains(clientUserId)));

            var query = result.ProjectToType<JoinedGroupInfo>();

            var data = new DataResponse<JoinedGroupInfo>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            foreach (var group in data.Data)
            {
                group.UnReadCount = await groupMessageRepo.GetQuery().Where(m => m.GroupChatId == group.Id &&
                !m.ClientUserGroupMessageReads.Select(cg => cg.ClientUserId)
                .Contains(clientUserId)).CountAsync();
                var lastMessage = await groupMessageRepo.GetQuery().Where(m => m.GroupChatId == group.Id).OrderByDescending(m => m.CreateDate).FirstOrDefaultAsync();
                group.LastMessage = lastMessage.Adapt<GetGroupMessageDTO>();

            }
            return Result<DataResponse<JoinedGroupInfo>>.Success(data);
        }
    }
}
