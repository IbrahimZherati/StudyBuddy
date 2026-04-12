
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.ClientUsers
{
    public class ClientUserDomainService : IClientUserDomainService
    {
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<Major> majorRepo;
        private readonly IRepo<University> universityRepo;
        private readonly IRepo<City> cityRepo;
        private readonly IRepo<Country> countryRepo;


        public ClientUserDomainService(IRepo<ClientUser> clientUserRepo
        ,IRepo<Major> majorRepo
        ,IRepo<University> universityRepo
        ,IRepo<City> cityRepo
        ,IRepo<Country> countryRepo
        )
        {
            this.clientUserRepo = clientUserRepo;
            this.majorRepo = majorRepo;
            this.universityRepo = universityRepo;
            this.cityRepo = cityRepo;
            this.countryRepo = countryRepo;

        }

      
      

        public async Task<Result> Update(UpdateClientUserDTO clientUserDTO)
        { 
            if (!await clientUserRepo.ExistsAsync(a => a.Id == clientUserDTO.Id))
                return Result.Failure(Error.ClientUserNotFound);
            
         

            if (clientUserDTO.MajorId != null && !await majorRepo.ExistsAsync(m => m.Id == clientUserDTO.MajorId))
                return Result.Failure(Error.MajorNotFound);


            if (clientUserDTO.UniversityId != null && !await universityRepo.ExistsAsync(u => u.Id == clientUserDTO.UniversityId))
                return Result.Failure(Error.UniversityNotFound);


            if (clientUserDTO.CityId != null && !await cityRepo.ExistsAsync(c => c.Id == clientUserDTO.CityId))
                return Result.Failure(Error.CityNotFound);


            if (clientUserDTO.CountryId != null && !await countryRepo.ExistsAsync(c => c.Id == clientUserDTO.CountryId))
                return Result.Failure(Error.CountryNotFound);


            return Result.Success();
        }
    }
}
