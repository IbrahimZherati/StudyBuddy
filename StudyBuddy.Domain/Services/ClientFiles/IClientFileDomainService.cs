
using StudyBuddy.Shared.DTOs.ClientFileDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.ClientFiles
{
    public interface IClientFileDomainService
    {
        Task<Result> Create(int clientId, CreateClientFileDTO clientFileDTO);
        Task<Result> Update(int clientId, UpdateClientFileDTO clientFileDTO);
        Task<Result> GetClientFilesByClientId(int clientId);
        Task<Result> Delete(int clientId, int Id);
    } 
}
