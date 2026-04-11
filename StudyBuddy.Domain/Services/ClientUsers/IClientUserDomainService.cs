
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.ClientUsers
{
    public interface IClientUserDomainService
    {
        Task<Result> Update(UpdateClientUserDTO clientUserDTO);
    } 
}
