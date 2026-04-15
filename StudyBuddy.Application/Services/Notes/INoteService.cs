using Mapster;
using StudyBuddy.Shared.DTOs.NoteDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface INoteService
     {
         Task<Result<GetNoteDTO>> Create(int clientId ,CreateNoteDTO noteDTO);
         Task<Result<GetNoteDTO>> Update(int clientId ,UpdateNoteDTO noteDTO);
         Task<Result<GetNoteDTO>> GetNoteById(int clientId ,int id);
         Task<Result> Delete(int clientId, int id);
         Task<Result<DataResponse<GetNoteDTO>>> GetNotes(int clientId,int skip, int take);
     }
}
     
