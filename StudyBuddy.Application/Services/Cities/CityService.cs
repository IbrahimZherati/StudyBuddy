using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Results;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.DTOs.Shared;

namespace StudyBuddy.Application.Services.Cities
{
    public class CityService : ICityService
    {
        private readonly IRepo<City> cityRepo;
        private readonly IRepo<Country> countryRepo;

        public CityService(
            IRepo<City> cityRepo,
            IRepo<Country> countryRepo)
        {
            this.cityRepo = cityRepo;
            this.countryRepo = countryRepo;
        }

        public async Task<Result> Create(CreateCityDTO cityDTO)
        {

            if (await cityRepo.ExistsAsync(m => m.Name == cityDTO.Name))
                return Result.Failure(Error.CityAlreadyExists);

            if (!await countryRepo.ExistsAsync(c => c.Id == cityDTO.CountryId))
                return Result.Failure(Error.CountryNotFound);

            var city = new City();
            cityDTO.Adapt(city);
            await cityRepo.AddAsync(city);
            try
            {
                await cityRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.CreateFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Delete(int id)
        {
            var city = await cityRepo.GetByIdAsync(id);
            if (city == null)
                return Result.Failure(Error.ItemNotFound);
            cityRepo.Remove(city);
            try
            {
                await cityRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.DeleteFailed);
            }
            return Result.Success();
        }

        public async Task<Result<GetCityDTO>> GetCityById(int id)
        {
            var city = await cityRepo.GetByIdAsync(id);
            if (city == null)
                return Result<GetCityDTO>.Failure(Error.ItemNotFound);
            var cityDTO = city.Adapt<GetCityDTO>();
            return Result<GetCityDTO>.Success(cityDTO);
        }

        public async Task<Result<DataResponse<GetCityDTO>>> GetCities(int skip, int take)
        {
            var result = cityRepo.GetQuery();

            var query = result.ProjectToType<GetCityDTO>();

            var data = new DataResponse<GetCityDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetCityDTO>>.Success(data);
        }

        public async Task<Result> Update(UpdateCityDTO cityDTO)
        {
            var city = await cityRepo.GetByIdAsync(cityDTO.Id);
            if (city == null)
                return Result.Failure(Error.ItemNotFound);

            if (await cityRepo.ExistsAsync(m => m.Name == cityDTO.Name && m.Id != cityDTO.Id))
                return Result.Failure(Error.CityAlreadyExists);

            if (!await countryRepo.ExistsAsync(c => c.Id == cityDTO.CountryId))
                return Result.Failure(Error.CountryNotFound);


            cityDTO.Adapt(city);
            cityRepo.Update(city);
            try
            {
                await cityRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.UpdateFailed);
            }

            return Result.Success();
        }
    }
}