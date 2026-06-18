using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.FriendDTOs;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Searchs
{
    public class SearchService : ISearchService
    {
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<Message, Guid> messageRepo;
        private readonly IRepo<ClientUserGroupChat> clientUserGroupChatRepo;
        private readonly IRepo<GroupMessage> groupMessageRepo;
        private readonly IRepo<FriendRequest> friendRequestRepo;

        public SearchService(IRepo<ClientUser> clientUserRepo,
            IRepo<Message, Guid> messageRepo,
            IRepo<ClientUserGroupChat> clientUserGroupChatRepo,
            IRepo<GroupMessage> groupMessageRepo,
            IRepo<FriendRequest> friendRequestRepo)
        {
            this.clientUserRepo = clientUserRepo;
            this.messageRepo = messageRepo;
            this.clientUserGroupChatRepo = clientUserGroupChatRepo;
            this.groupMessageRepo = groupMessageRepo;
            this.friendRequestRepo = friendRequestRepo;
        }

        public async Task<Result<DataResponse<InfoClientUserDTO>>> GetFriendRequest(int clientId, int skip, int take, string? filter, bool sameMajor)
        {
            var result = friendRequestRepo.GetQuery()
                .Where(f => f.ToClientUserId == clientId)
                .Select(f => f.FromClientUser);

            //filter Skill | major | userName | studyInterest
            var random = new Random(clientId);
            if (!string.IsNullOrEmpty(filter))
            {
                var resultSameSkill = result.Where(f => f.ClientUserSkills.Any(s => s.Skill.Name.ToLower().Contains(filter.ToLower())));
                var resultSameMajor = result.Where(f => f.Major.Name.ToLower().Contains(filter.ToLower()));
                var resultSameUserName = result.Where(f => f.UserName.ToLower().Contains(filter.ToLower()));
                var resultSameStudyInterest = result.Where(f => f.StudyInterests.Any(s => s.Name.ToLower().Contains(filter.ToLower())));
                result = resultSameSkill.Union(resultSameMajor).Union(resultSameUserName).Union(resultSameStudyInterest);
            }

            if (sameMajor)
            {
                var currentMajor = await clientUserRepo.GetQuery().Where(c => c.Id == clientId).Select(c => c.Major).FirstOrDefaultAsync();
                if (currentMajor != null)
                    result = result.Where(f => f.MajorId == currentMajor.Id);
            }

            var friends = await result.ProjectToType<InfoClientUserDTO>().ToListAsync();
            var randomFriends = friends.OrderBy(x => random.Next()).ToList();
            var data = new DataResponse<InfoClientUserDTO>();
            data.Count = randomFriends.Count();
            data.Data = randomFriends.Skip(skip).Take(take).ToList();

            return Result<DataResponse<InfoClientUserDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<FriendInfoDTO>>> GetFriends(int clientId, int skip, int take, string? filter, bool sameMajor)
        {
            //Get All Friend
            var result = clientUserRepo.GetQuery()
               .Where(c => c.Id == clientId)
               .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
               .Union(
               clientUserRepo.GetQuery()
               .Where(c => c.Id == clientId)
               .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
               );

            //filter Skill | major | userName | studyInterest
            var random = new Random(clientId);
            if (!string.IsNullOrEmpty(filter))
            {
                var resultSameSkill = result.Where(f => f.ClientUserSkills.Any(s => s.Skill.Name.ToLower().Contains(filter.ToLower())));
                var resultSameMajor = result.Where(f => f.Major.Name.ToLower().Contains(filter.ToLower()));
                var resultSameUserName = result.Where(f => f.UserName.ToLower().Contains(filter.ToLower()));
                var resultSameStudyInterest = result.Where(f => f.StudyInterests.Any(s => s.Name.ToLower().Contains(filter.ToLower())));
                result = resultSameSkill.Union(resultSameMajor).Union(resultSameUserName).Union(resultSameStudyInterest);
            }

            if (sameMajor)
            {
                var currentMajor = await clientUserRepo.GetQuery().Where(c => c.Id == clientId).Select(c => c.Major).FirstOrDefaultAsync();
                if (currentMajor != null)
                    result = result.Where(f => f.MajorId == currentMajor.Id);
            }

            var friends = await result.ProjectToType<FriendInfoDTO>().ToListAsync();
            var randomFriends = friends.OrderBy(x => random.Next()).ToList();
            var data = new DataResponse<FriendInfoDTO>();
            data.Count = randomFriends.Count();
            data.Data = randomFriends.Skip(skip).Take(take).ToList();
            return Result<DataResponse<FriendInfoDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<JoinedGroupInfo>>> GetMyGroups(int clientId, int skip, int take, string? filter, int? majorId)
        {
            var result = clientUserGroupChatRepo.GetQuery()
               .Where(g => g.ClientUserId == clientId)
               .Select(g => g.GroupChat);

            if (!string.IsNullOrEmpty(filter))
            {
                var resultSameName = result.Where(g => g.Name.ToLower().Contains(filter.ToLower()));
                var resultSameMajor = result.Where(g => g.Major.Name.ToLower().Contains(filter.ToLower()));
                result = resultSameName.Union(resultSameMajor);
            }

            if (majorId != null)
                result = result.Where(g => g.MajorId == majorId);


            var random = new Random(clientId);
            var groups = await result.ProjectToType<JoinedGroupInfo>().ToListAsync();
            var randomGroups = groups.OrderBy(x => random.Next()).ToList();
            var data = new DataResponse<JoinedGroupInfo>();
            data.Count = randomGroups.Count();
            data.Data = randomGroups.OrderBy(q => q.Id).Skip(skip).Take(take).ToList();
            return Result<DataResponse<JoinedGroupInfo>>.Success(data);
        }

        public async Task<Result<DataResponse<InfoClientUserDTO>>> SearchBuddy(int clientId, int skip, int take, string? filter, bool SameUniversity, bool SameInterest, bool SameMajor)
        {
            var result = clientUserRepo.GetQuery()
                .Where(c => c.Id != clientId);

            if (!string.IsNullOrEmpty(filter))
            {
                var resultSameSkill = result.Where(f => f.ClientUserSkills.Any(s => s.Skill.Name.ToLower().Contains(filter.ToLower())));
                var resultSameMajor = result.Where(f => f.Major.Name.ToLower().Contains(filter.ToLower()));
                var resultSameUserName = result.Where(f => f.UserName.ToLower().Contains(filter.ToLower()));
                var studyInterests = await result.SelectMany(c => c.StudyInterests).ToListAsync();
                IQueryable<ClientUser>? resultSameStudyInterest = Enumerable.Empty<ClientUser>().AsQueryable();
                if (studyInterests != null && studyInterests.Any())
                {
                    var interestNames = studyInterests.Where(i => i?.Name != null).Select(i => i.Name.ToLower()).ToList();
                    resultSameStudyInterest = result.Where(f => f.StudyInterests.Any(s => s.Name != null && interestNames.Contains(filter.ToLower())));
                }
                result = resultSameSkill.Union(resultSameMajor).Union(resultSameUserName).Union(resultSameStudyInterest);
            }

            if (SameMajor)
            {
                var major = await clientUserRepo.GetQuery().Where(c => c.Id == clientId).Select(c => c.Major).FirstOrDefaultAsync();
                if (major != null)
                    result = result.Where(c => c.MajorId == major.Id);
            }

            if (SameInterest)
            {
                var interests = await clientUserRepo.GetQuery().Where(c => c.Id == clientId).SelectMany(c => c.StudyInterests).ToListAsync();
                if (interests != null && interests.Count() > 0)
                    result = result.Where(c => c.StudyInterests.Any(s => interests.Select(i => i.Name.ToLower()).Contains(s.Name.ToLower())));
            }

            if (SameUniversity)
            {
                var university = await clientUserRepo.GetQuery().Where(c => c.Id == clientId).Select(c => c.University).FirstOrDefaultAsync();
                if (university != null)
                    result = result.Where(c => c.UniversityId == university.Id);
            }

            var needIds = await result.Select(c => c.Id).ToListAsync();

            var clientResults = clientUserRepo.GetQuery().Where(c => needIds.Contains(c.Id));

            var boddies = await clientResults.ProjectToType<InfoClientUserDTO>().ToListAsync();

            var random = new Random(clientId);

            var randomBoddies = boddies.OrderBy(x => random.Next()).ToList();
            var data = new DataResponse<InfoClientUserDTO>();
            data.Count = randomBoddies.Count();
            data.Data = randomBoddies.Skip(skip).Take(take).ToList();
            return Result<DataResponse<InfoClientUserDTO>>.Success(data);
        }

        public async Task<Result<DataResponse<InfoClientUserDTO>>> SuggestedClients(int clientId, int skip, int take)
        {
            var result = friendRequestRepo.GetQuery()
               .Where(f => f.ToClientUserId == clientId)
               .Select(f => f.FromClientUser)
               .Where(c => c.Id != clientId);

            var random = new Random(clientId);

            var currentMajor = await clientUserRepo.GetQuery().Where(c => c.Id == clientId).Select(c => c.Major).FirstOrDefaultAsync();
            if (currentMajor != null)
                result = result.Union(result.Where(f => f.MajorId == currentMajor.Id));

            var currentUniversity = await clientUserRepo.GetQuery().Where(c => c.Id == clientId).Select(c => c.University).FirstOrDefaultAsync();
            if (currentUniversity != null)
                result = result.Union(result.Where(f => f.UniversityId == currentUniversity.Id));

            // FIX 1: Safely extract and filter null interests
            var interests = await clientUserRepo.GetQuery().Where(c => c.Id == clientId).SelectMany(c => c.StudyInterests).ToListAsync();
            if (interests != null && interests.Any())
            {
                var interestNames = interests.Where(i => i?.Name != null).Select(i => i.Name.ToLower()).ToList();
                if (interestNames.Any())
                {
                    result = result.Union(result.Where(c => c.StudyInterests.Any(s => s.Name != null && interestNames.Contains(s.Name.ToLower()))));
                }
            }

            // FIX 2: Safely extract and filter null skills
            var skills = await clientUserRepo.GetQuery().Where(c => c.Id == clientId).SelectMany(c => c.ClientUserSkills).ToListAsync();
            if (skills != null && skills.Any())
            {
                var skillNames = skills.Where(s => s?.Skill?.Name != null).Select(s => s.Skill.Name.ToLower()).ToList();
                if (skillNames.Any())
                {
                    result = result.Union(result.Where(c => c.ClientUserSkills.Any(cus => cus.Skill != null && cus.Skill.Name != null && skillNames.Contains(cus.Skill.Name.ToLower()))));
                }
            }

            var friendFriend = clientUserRepo.GetQuery()
                .Where(c => c.Id == clientId)
                .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
                .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
                .Union(clientUserRepo.GetQuery()
                .Where(c => c.Id == clientId)
                .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
                .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
                   .Union(clientUserRepo.GetQuery()
                .Where(c => c.Id == clientId)
                .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
                .SelectMany(c => c.FirstFriends.Select(f => f.SecondFriend))
                .Union(clientUserRepo.GetQuery()
                .Where(c => c.Id == clientId)
                .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend))
                .SelectMany(c => c.SecondFriends.Select(f => f.FirstFriend)))));

            result = result.Union(friendFriend);

            var needIds = await result.Select(c => c.Id).ToListAsync();

            var clientResults = clientUserRepo.GetQuery().Where(c => needIds.Contains(c.Id));

            // FIX 3: Get total count and paginate on DB to avoid severe server memory strain
            var data = new DataResponse<InfoClientUserDTO>();
            data.Count = await clientResults.CountAsync();

            var clients = await clientResults.ProjectToType<InfoClientUserDTO>().ToListAsync();
            data.Data = clients.OrderBy(x => random.Next()).Skip(skip).Take(take).ToList();

            return Result<DataResponse<InfoClientUserDTO>>.Success(data);
        }


        public async Task<Result<DataResponse<InfoGroupChatDTO>>> SuggestedGroups(int clientId, int skip, int take, string? filter, int? majorId)
        {
            var result = clientUserGroupChatRepo.GetQuery()
                .Where(g => g.ClientUserId != clientId)
                .Select(g => g.GroupChat);

            if (!string.IsNullOrEmpty(filter))
            {
                var resultSameName = result.Where(g => g.Name.ToLower().Contains(filter.ToLower()));
                var resultSameMajor = result.Where(g => g.Major.Name.ToLower().Contains(filter.ToLower()));
                result = resultSameName.Union(resultSameMajor);
            }

            if (majorId != null)
                result = result.Where(g => g.MajorId == majorId);

            var random = new Random(clientId);
            var groups = await result.ProjectToType<InfoGroupChatDTO>().ToListAsync();
            var randomGroups = groups.OrderBy(x => random.Next()).ToList();
            var data = new DataResponse<InfoGroupChatDTO>();
            data.Count = randomGroups.Count();
            data.Data = randomGroups.OrderBy(q => q.Id).Skip(skip).Take(take).ToList();

            return Result<DataResponse<InfoGroupChatDTO>>.Success(data);

        }
    }
}
