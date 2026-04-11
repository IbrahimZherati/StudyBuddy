
using StudyBuddy.Shared.DTOs.UniversityDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Universities
{
    public interface IUniversityDomainService
    {
        Task<Result> Create(CreateUniversityDTO universityDTO);
        Task<Result> Update(UpdateUniversityDTO universityDTO);
        Task<Result> Delete(int Id);
    } 
}
