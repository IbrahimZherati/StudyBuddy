
using StudyBuddy.Shared.DTOs.NoteDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Notes
{
    public interface INoteDomainService
    {
        Task<Result> Create(int clientId, CreateNoteDTO noteDTO);
        Task<Result> Update(int clientId, UpdateNoteDTO noteDTO);
        Task<Result> Delete(int clientId, int Id);
        Task<Result> GetByid(int clientId, int Id);
    } 
}
