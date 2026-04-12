
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Cities
{
    public class CityDomainService : ICityDomainService
    {
        private readonly IRepo<City> cityRepo;
        private readonly IRepo<Country> countryRepo;


        public CityDomainService(IRepo<City> cityRepo
        ,IRepo<Country> countryRepo
        )
        {
            this.cityRepo = cityRepo;
            this.countryRepo = countryRepo;

        }

        public async Task<Result> Create(CreateCityDTO cityDTO)
        {
            
            if (!await countryRepo.ExistsAsync(c => c.Id == cityDTO.CountryId))
                return Result.Failure(Error.CountryNotFound);


            if(await cityRepo.ExistsAsync(a => a.Name == cityDTO.Name))
                return Result.Failure(Error.CityAlreadyExists);
            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await cityRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.CityNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateCityDTO cityDTO)
        { 
            if (!await cityRepo.ExistsAsync(a => a.Id == cityDTO.Id))
                return Result.Failure(Error.CityNotFound);
            
            if (!await countryRepo.ExistsAsync(c => c.Id == cityDTO.CountryId))
                return Result.Failure(Error.CountryNotFound);


            if (await cityRepo.ExistsAsync(a => a.Name == cityDTO.Name && a.Id != cityDTO.Id ))
                return Result.Failure(Error.CityAlreadyExists);
            return Result.Success();
        }
    }
}
