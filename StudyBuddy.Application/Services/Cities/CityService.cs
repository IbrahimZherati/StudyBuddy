
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

        public async Task<Result<GetCityDTO>> Create(CreateCityDTO cityDTO)
        {
            var valid = await cityDomainService.Create(cityDTO);
            if (!valid.IsSuccess)
                return Result<GetCityDTO>.Failure(valid.Error!);

            var result = City.Create(cityDTO);

            if (!result.IsSuccess)
                return Result<GetCityDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetCityDTO>.Failure(Error.CreateFailed);

            var city = result.Value;
            await cityRepo.AddAsync(city);

            try
            {
                await cityRepo.SaveAsync();
                var dto = city.Adapt<GetCityDTO>();
                return Result<GetCityDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetCityDTO>.Failure(Error.CreateFailed);
            }
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
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetCityDTO>> GetCityById(int id)
        {
            var city = await cityRepo.GetByIdAsync(id);
            if (city == null)
                return Result<GetCityDTO>.Failure(Error.CityNotFound);
            var cityDTO = city.Adapt<GetCityDTO>();
            return Result<GetCityDTO>.Success(cityDTO);
        }

        public async Task<Result<DataResponse<GetCityDTO>>> GetCities(int skip, int take)
        {
            var result = cityRepo.GetQuery();

            var query = result.ProjectToType<GetCityDTO>();

            var data = new DataResponse<GetCityDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetCityDTO>>.Success(data);
        }

        public async Task<Result<GetCityDTO>> Update(UpdateCityDTO cityDTO)
        {
            var valid = await cityDomainService.Update(cityDTO);
            if (!valid.IsSuccess)
                return Result<GetCityDTO>.Failure(valid.Error!);

            var city = await cityRepo.GetByIdAsync(cityDTO.Id);
            if (city == null)
                return Result<GetCityDTO>.Failure(Error.CityNotFound);

            var result = city.Update(cityDTO);

            if (!result.IsSuccess)
                return Result<GetCityDTO>.Failure(result.Error!);

            cityRepo.Update(city);
            try
            {
                await cityRepo.SaveAsync();
                var dto = city.Adapt<GetCityDTO>();
                return Result<GetCityDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetCityDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
