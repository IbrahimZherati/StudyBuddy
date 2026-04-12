
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.CountryDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Countries
{
    public class CountryDomainService : ICountryDomainService
    {
        private readonly IRepo<Country> countryRepo;


        public CountryDomainService(IRepo<Country> countryRepo
        )
        {
            this.countryRepo = countryRepo;

        }

        public async Task<Result> Create(CreateCountryDTO countryDTO)
        {
            
            if(await countryRepo.ExistsAsync(a => a.Name == countryDTO.Name))
                return Result.Failure(Error.CountryAlreadyExists);
            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await countryRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.CountryNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateCountryDTO countryDTO)
        { 
            if (!await countryRepo.ExistsAsync(a => a.Id == countryDTO.Id))
                return Result.Failure(Error.CountryNotFound);
            
            if (await countryRepo.ExistsAsync(a => a.Name == countryDTO.Name && a.Id != countryDTO.Id ))
                return Result.Failure(Error.CountryAlreadyExists);
            return Result.Success();
        }
    }
}
