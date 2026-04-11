using Mapster;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface ICityService
     {
         Task<Result> Create(CreateCityDTO cityDTO);
         Task<Result> Update(UpdateCityDTO cityDTO);
         Task<Result<GetCityDTO>> GetCityById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetCityDTO>>> GetCities(int skip, int take);
     }
}
     
