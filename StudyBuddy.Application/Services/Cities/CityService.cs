using Mapster;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Cities;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IRepo<City> cityRepo;
        private readonly ICityDomainService cityDomainService;


        public CityService(IRepo<City> cityRepo, ICityDomainService cityDomainService)
        {
            this.cityRepo = cityRepo;
            this.cityDomainService = cityDomainService;

        }

        public async Task<Result> Create(CreateCityDTO cityDTO)
        {
            var valid = await cityDomainService.Create(cityDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var result = City.Create(cityDTO);

            if (!result.IsSuccess)
                return Result.Failure(result.Error!);

            if(result.Value == null)
                return Result.Failure(Error.CreateFailed);

            var city = result.Value;
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
            var valid = await cityDomainService.Delete(id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var city = await cityRepo.GetByIdAsync(id);
            if (city == null)
                return Result.Failure(Error.CityNotFound);
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
                return Result<GetCityDTO>.Failure(Error.CityNotFound);
            var cityDTO = city.Adapt<GetCityDTO>();
            return Result<GetCityDTO>.Success(cityDTO);
        }

        public async Task<Result<List<GetCityDTO>>> GetCities(int skip, int take)
        {
            var result = cityRepo.GetQuery();

            var query = result.ProjectToType<GetCityDTO>();

            var data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<List<GetCityDTO>>.Success(data);
        }

        public async Task<Result> Update(UpdateCityDTO cityDTO)
        {
            var valid = await cityDomainService.Update(cityDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var city = await cityRepo.GetByIdAsync(cityDTO.Id);
            if (city == null)
                return Result.Failure(Error.CityNotFound);

            var result = city.Update(cityDTO);

            if (!result.IsSuccess)
                return Result.Failure(result.Error!);

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
