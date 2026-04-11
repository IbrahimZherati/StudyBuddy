
using StudyBuddy.Shared.DTOs.ClientFileDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.ClientFiles
{
    public interface IClientFileDomainService
    {
        Task<Result> Create(CreateClientFileDTO clientFileDTO);
        Task<Result> Update(UpdateClientFileDTO clientFileDTO);
        Task<Result> GetClientFilesByClientId(int clientId);
        Task<Result> Delete(int Id);
    } 
}
