
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Cities
{
    public interface ICityDomainService
    {
        Task<Result> Create(CreateCityDTO cityDTO);
        Task<Result> Update(UpdateCityDTO cityDTO);
        Task<Result> Delete(int Id);
    } 
}
