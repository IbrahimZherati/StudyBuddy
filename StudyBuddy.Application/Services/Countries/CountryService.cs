using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.CountryDTO;
using StudyBuddy.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace StudyBuddy.Application.Services.Countries
{
    public class CountryService : ICountryService
    {
        private readonly IRepo<Country> countryRepo;

        public CountryService(IRepo<Country> countryRepo)
        {
            this.countryRepo = countryRepo;
        }

        public async Task<Result> Create(CreateCountryDTO countryDTO)
        {
            if (await countryRepo.ExistsAsync(m => m.Name == countryDTO.Name))
                return Result.Failure(Error.CountryAlreadyExists);

            var country = new Country();
            countryDTO.Adapt(country);
            await countryRepo.AddAsync(country);
            try
            {
                await countryRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.CreateFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Delete(int id)
        {
            var country = await countryRepo.GetByIdAsync(id);
            if (country == null)
                return Result.Failure(Error.ItemNotFound);
            countryRepo.Remove(country);
            try
            {
                await countryRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.DeleteFailed);
            }
            return Result.Success();
        }

        public async Task<Result<GetCountryDTO>> GetCountryById(int id)
        {
            var country = await countryRepo.GetByIdAsync(id);
            if (country == null)
                return Result<GetCountryDTO>.Failure(Error.ItemNotFound);
            var countryDTO = country.Adapt<GetCountryDTO>();
            return Result<GetCountryDTO>.Success(countryDTO);
        }

        public async Task<Result<List<GetCountryDTO>>> GetCountries(int skip, int take)
        {
            var result = countryRepo.GetQuery();

            var query = result.ProjectToType<GetCountryDTO>();

            var data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<List<GetCountryDTO>>.Success(data);
        }

        public async Task<Result> Update(UpdateCountryDTO countryDTO)
        {
            var country = await countryRepo.GetByIdAsync(countryDTO.Id);
            if (country == null)
                return Result.Failure(Error.ItemNotFound);

            if (await countryRepo.ExistsAsync(m => m.Name == countryDTO.Name && m.Id != countryDTO.Id))
                return Result.Failure(Error.CountryAlreadyExists);

            countryDTO.Adapt(country);
            countryRepo.Update(country);
            try
            {
                await countryRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.UpdateFailed);
            }

            return Result.Success();
        }
    }
}