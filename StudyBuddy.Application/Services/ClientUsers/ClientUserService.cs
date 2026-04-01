using Mapster;
using StudyBuddy.Application.Services.Shared.AutoGenerateSkills;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
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
        private readonly IAutoGenrateSkill autoGenrateSkill;

        public ClientUserService(
            IRepo<Major> majorRepo,
            IRepo<University> universityRepo,
            IRepo<City> cityRepo,
            IRepo<Country> countryRepo,
            IRepo<ClientUser> clientUserRepo,
            IRepo<Skill> skillRepo,
            IRepo<ClientUserSkill> clientUserSkillRepo,
            IAutoGenrateSkill autoGenrateSkill

            )
        {
            this.majorRepo = majorRepo;
            this.universityRepo = universityRepo;
            this.cityRepo = cityRepo;
            this.countryRepo = countryRepo;
            this.clientUserRepo = clientUserRepo;
            this.skillRepo = skillRepo;
            this.clientUserSkillRepo = clientUserSkillRepo;
            this.autoGenrateSkill = autoGenrateSkill;
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
