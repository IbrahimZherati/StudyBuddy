
using StudyBuddy.Shared.DTOs.StudyInterestDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.StudyInterests
{
    public interface IStudyInterestDomainService
    {
        Task<Result> Create(int clientId ,CreateStudyInterestDTO studyInterestDTO);
        Task<Result> Update(int clientId ,UpdateStudyInterestDTO studyInterestDTO);
        Task<Result> Delete(int clientId ,int Id);
        Task<Result> GetStudyInterests(int clientId);
        Task<Result> GetStudyInterestById(int clientId , int Id);
        
    } 
}
