
using StudyBuddy.Shared.DTOs.MajorDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Majors
{
    public interface IMajorDomainService
    {
        Task<Result> Create(CreateMajorDTO majorDTO);
        Task<Result> Update(UpdateMajorDTO majorDTO);
        Task<Result> Delete(int Id);
    } 
}
