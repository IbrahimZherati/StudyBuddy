
using Mapster;
using StudyBuddy.Domain;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Notes;
using StudyBuddy.Shared.DTOs.NoteDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepo<Note> noteRepo;
        private readonly IRepo<NoteTopic> noteTopicRepo;
        private readonly IRepo<Topic> topicRepo;
        private readonly INoteDomainService noteDomainService;


        public NoteService(IRepo<Note> noteRepo ,IRepo<NoteTopic> noteTopicRepo,IRepo<Topic> topicRepo, INoteDomainService noteDomainService)
        {
            this.noteRepo = noteRepo;
            this.noteTopicRepo = noteTopicRepo;
            this.topicRepo = topicRepo;
            this.noteDomainService = noteDomainService;

        }

        public async Task<Result<GetNoteDTO>> Create(int clientId ,CreateNoteDTO noteDTO)
        {
            var valid = await noteDomainService.Create(clientId ,noteDTO);
            if (!valid.IsSuccess)
                return Result<GetNoteDTO>.Failure(valid.Error!);

            var result = Note.Create(clientId, noteDTO);

            if (!result.IsSuccess)
                return Result<GetNoteDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetNoteDTO>.Failure(Error.CreateFailed);

            var note = result.Value;
            await noteRepo.AddAsync(note);

            //create topic
            foreach(var topicId in noteDTO.TopicIds)
            {
                var Topic = await topicRepo.GetByIdAsync(topicId);
                if (Topic == null)
                    continue;
                var noteTopic = NoteTopic.Create(note, topicId);
                await noteTopicRepo.AddAsync(noteTopic);
            }


            try
            {
                await noteRepo.SaveAsync();
                var dto = note.Adapt<GetNoteDTO>();
                return Result<GetNoteDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetNoteDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int clientId ,int id)
        {
            var valid = await noteDomainService.Delete(clientId, id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var note = await noteRepo.GetByIdAsync(id);
            if (note == null)
                return Result.Failure(Error.NoteNotFound);
            noteRepo.Remove(note);
            try
            {
                await noteRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result> Favorite(int clientId, int id)
        {
            var valid = await noteDomainService.Favorite(clientId, id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var note = await noteRepo.GetByIdAsync(id);
            if (note == null)
                return Result.Failure(Error.NoteNotFound);


            note.Favorite();
            noteRepo.Update(note);
            try
            {
                await noteRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.UpdateFailed);
            }
        }

        public async Task<Result<GetNoteDTO>> GetNoteById(int clientId, int id)
        {
            var valid = await noteDomainService.GetByid(clientId,id);
            if (!valid.IsSuccess)
                return Result<GetNoteDTO>.Failure(valid.Error!);
            var note = await noteRepo.GetByIdAsync(id);
            if (note == null)
                return Result<GetNoteDTO>.Failure(Error.NoteNotFound);
            var noteDTO = note.Adapt<GetNoteDTO>();
            return Result<GetNoteDTO>.Success(noteDTO);
        }

        public async Task<Result<DataResponse<GetNoteDTO>>> GetNotes(int clientId, int skip, int take, string? filter,bool IsFavorite, int? topicId)
        {
            var result = noteRepo.GetQuery()
                .Where(n => n.ClientUserId == clientId);


            if(!string.IsNullOrEmpty(filter))
            {
                var resultTitle = result.Where(n => n.Title.ToLower().Contains(filter.ToLower()));
                var resultNoteText = result.Where(n => n.Notes.ToLower().Contains(filter.ToLower()));
                var resultTopic = result.Where(n => n.NoteTopics.Any(nt => nt.Topic.Name.ToLower().Contains(filter.ToLower())));
                result = resultTitle.Union(resultNoteText).Union(resultTopic);
            }

            if (topicId != null)
                result = result.Where(n => n.NoteTopics.Select(n => n.Topic.Id).ToList().Contains(topicId.Value));

            if(IsFavorite)
                result = result.Where(n => n.IsFavorite == true);
            var query = result.ProjectToType<GetNoteDTO>();

            var data = new DataResponse<GetNoteDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetNoteDTO>>.Success(data);
        }

        public async Task<Result<GetNoteDTO>> Update(int clientId ,UpdateNoteDTO noteDTO)
        {
            var valid = await noteDomainService.Update(clientId ,noteDTO);
            if (!valid.IsSuccess)
                return Result<GetNoteDTO>.Failure(valid.Error!);

            var note = await noteRepo.GetByIdAsync(noteDTO.Id);
            if (note == null)
                return Result<GetNoteDTO>.Failure(Error.NoteNotFound);

            var result = note.Update(noteDTO);

            if (!result.IsSuccess)
                return Result<GetNoteDTO>.Failure(result.Error!);

            noteRepo.Update(note);

            //delete old topic
            var oldTopic = await noteTopicRepo.GetQuery().Where(n => n.NoteId == note.Id).ToListAsync();
            noteTopicRepo.RemoveRange(oldTopic);

            //add new topic
            foreach (var topicId in noteDTO.TopicIds)
            {
                var Topic = await topicRepo.GetByIdAsync(topicId);
                if (Topic == null)
                    continue;
                var noteTopic = NoteTopic.Create(note, topicId);
                await noteTopicRepo.AddAsync(noteTopic);
            }

            try
            {
                await noteRepo.SaveAsync();
                var dto = note.Adapt<GetNoteDTO>();
                return Result<GetNoteDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetNoteDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
