
using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Countries;
using StudyBuddy.Shared.DTOs.CountryDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepo<Country> countryRepo;
        private readonly ICountryDomainService countryDomainService;


        public CountryService(IRepo<Country> countryRepo, ICountryDomainService countryDomainService)
        {
            this.countryRepo = countryRepo;
            this.countryDomainService = countryDomainService;

        }

        public async Task<Result<GetCountryDTO>> Create(CreateCountryDTO countryDTO)
        {
            var valid = await countryDomainService.Create(countryDTO);
            if (!valid.IsSuccess)
                return Result<GetCountryDTO>.Failure(valid.Error!);

            var result = Country.Create(countryDTO);

            if (!result.IsSuccess)
                return Result<GetCountryDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetCountryDTO>.Failure(Error.CreateFailed);

            var country = result.Value;
            await countryRepo.AddAsync(country);

            try
            {
                await countryRepo.SaveAsync();
                var dto = country.Adapt<GetCountryDTO>();
                return Result<GetCountryDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetCountryDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int id)
        {
            var valid = await countryDomainService.Delete(id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var country = await countryRepo.GetByIdAsync(id);
            if (country == null)
                return Result.Failure(Error.CountryNotFound);
            countryRepo.Remove(country);
            try
            {
                await countryRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetCountryDTO>> GetCountryById(int id)
        {
            var country = await countryRepo.GetByIdAsync(id);
            if (country == null)
                return Result<GetCountryDTO>.Failure(Error.CountryNotFound);
            var countryDTO = country.Adapt<GetCountryDTO>();
            return Result<GetCountryDTO>.Success(countryDTO);
        }

        public async Task<Result<DataResponse<GetCountryDTO>>> GetCountries(int skip, int take)
        {
            var result = countryRepo.GetQuery();

            var query = result.ProjectToType<GetCountryDTO>();

            var data = new DataResponse<GetCountryDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetCountryDTO>>.Success(data);
        }

        public async Task<Result<GetCountryDTO>> Update(UpdateCountryDTO countryDTO)
        {
            var valid = await countryDomainService.Update(countryDTO);
            if (!valid.IsSuccess)
                return Result<GetCountryDTO>.Failure(valid.Error!);

            var country = await countryRepo.GetByIdAsync(countryDTO.Id);
            if (country == null)
                return Result<GetCountryDTO>.Failure(Error.CountryNotFound);

            var result = country.Update(countryDTO);

            if (!result.IsSuccess)
                return Result<GetCountryDTO>.Failure(result.Error!);

            countryRepo.Update(country);
            try
            {
                await countryRepo.SaveAsync();
                var dto = country.Adapt<GetCountryDTO>();
                return Result<GetCountryDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetCountryDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
