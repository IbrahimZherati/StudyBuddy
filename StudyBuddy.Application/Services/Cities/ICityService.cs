using Mapster;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface ICityService
     {
         Task<Result<GetCityDTO>> Create(CreateCityDTO cityDTO);
         Task<Result<GetCityDTO>> Update(UpdateCityDTO cityDTO);
         Task<Result<GetCityDTO>> GetCityById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetCityDTO>>> GetCities(int skip, int take);
     }
}
     
