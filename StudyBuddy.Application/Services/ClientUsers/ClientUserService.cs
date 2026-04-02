using Mapster;
using StudyBuddy.Application.Services.Shared.AutoGenerateSkills;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
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
        private readonly IAutoGenrateSkill autoGenrateSkill;

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
            IAutoGenrateSkill autoGenerateSkill

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
            this.autoGenrateSkill = autoGenerateSkill;
        }

        public async Task<Result<GetProfileClientUserDTO>> GetProfile(string userId)
        {
            var profile = await clientUserRepo.GetQuery()
                .Where(c => c.UserId.ToString() == userId)
                .ProjectToType<GetProfileClientUserDTO>()
                .FirstOrDefaultAsync();

            if (profile == null)
                return Result<GetProfileClientUserDTO>.Failure(Error.UserNotFound);

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
                    University = g.First().GroupChat.University.Name,
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





        public async Task<Result> Update(UpdateClientUserDTO clientUserDTO)
        {
            //Check
            if (clientUserDTO.MajorId != null && !await majorRepo.ExistsAsync(m => m.Id == clientUserDTO.MajorId))
                return Result.Failure(Error.MajorNotFound);
            if (clientUserDTO.UniversityId != null && !await universityRepo.ExistsAsync(u => u.Id == clientUserDTO.UniversityId))
                return Result.Failure(Error.UniversityNotFound);
            if (clientUserDTO.CityId != null && !await cityRepo.ExistsAsync(c => c.Id == clientUserDTO.CityId))
                return Result.Failure(Error.CityNotFound);
            if (clientUserDTO.CountryId != null && !await countryRepo.ExistsAsync(c => c.Id == clientUserDTO.CountryId))
                return Result.Failure(Error.CountryNotFound);

            var clientUser = await clientUserRepo.GetByIdAsync(clientUserDTO.Id);
            if (clientUser == null)
                return Result.Failure(Error.UserNotFound);

            //Generate Skills

            //Check Bio Change
            if (clientUser.Bio != clientUserDTO.Bio)
            {


                var result = await autoGenrateSkill.GetSkillFromBio(clientUserDTO!.Bio);
                if (!result.IsSuccess)
                    return Result.Failure(result.Error ?? Error.GenerateSkillFailed);

                //Create new Skills and select need skills
                var newClientUserSkills = new List<ClientUserSkill>();
                foreach (var skill in result.Value!)
                {
                    var skillIn = await skillRepo.GetQuery()
                        .FirstOrDefaultAsync(s => s.Name.ToLower() == skill.ToLower());
                    if (skillIn == null)
                    {
                        skillIn = new Skill();
                        skillIn.Name = skill.ToLower();
                        await skillRepo.AddAsync(skillIn);
                    }
                    newClientUserSkills.Add(new ClientUserSkill
                    {
                        Skill = skillIn,
                        ClientUser = clientUser
                    });

                }



                //Delete Old Skills
                var oldSkills = await clientUserSkillRepo.GetQuery()
                    .Where(cs => cs.ClientUserId == clientUserDTO.Id)
                    .ToListAsync();
                clientUserSkillRepo.RemoveRange(oldSkills);

                //Add New Skills
                await clientUserSkillRepo.AddRangeAsync(newClientUserSkills);

            }
            clientUserDTO.Adapt(clientUser);
            try
            {
                await clientUserRepo.SaveAsync();

            }
            catch
            {
                return Result.Failure(Error.UpdateFailed);
            }

            return Result.Success();
        }
    }
}
