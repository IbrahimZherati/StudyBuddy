
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.NoteDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Notes
{
    public class NoteDomainService : INoteDomainService
    {
        private readonly IRepo<Note> noteRepo;
        private readonly IRepo<ClientUser> clientUserRepo;


        public NoteDomainService(IRepo<Note> noteRepo
        ,IRepo<ClientUser> clientUserRepo
        )
        {
            this.noteRepo = noteRepo;
            this.clientUserRepo = clientUserRepo;

        }

        public async Task<Result> Create(int clientId, CreateNoteDTO noteDTO)
        {
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            return Result.Success();
        }

        public async Task<Result> Delete(int clientId, int Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            var note = await noteRepo.GetByIdAsync(Id);
            if (note == null)
                return Result.Failure(Error.NoteNotFound);

            if (note.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> Favorite(int clientId, int Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            var note = await noteRepo.GetByIdAsync(Id);
            if (note == null)
                return Result.Failure(Error.NoteNotFound);

            if (note.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> GetByid(int clientId, int Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            var note = await noteRepo.GetByIdAsync(Id);
            if (note == null)
                return Result.Failure(Error.NoteNotFound);

            if (note.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> Update(int clientId,UpdateNoteDTO noteDTO)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            var note = await noteRepo.GetByIdAsync(noteDTO.Id);
            if (note == null)
                return Result.Failure(Error.NoteNotFound);

            if (note.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);


     
            return Result.Success();
        }
    }
}
