using Mapster;
using StudyBuddy.Shared.DTOs.NoteDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Note : EntityBase<int>
{
     public int ClientUserId { get; private set; }
     public string Title { get; private set; } = null!;
     public string Notes { get; private set; } = null!;
     public virtual ClientUser ClientUser { get; private set; } = null!;

     private Note() { }

     public static Result<Note> Create(CreateNoteDTO noteDTO)
     {
         var newNote = new Note();
         noteDTO.Adapt(newNote);
         newNote.CreateDate = DateTime.Now;
         return Result<Note>.Success(newNote);
     }

     public Result<Note> Update(UpdateNoteDTO noteDTO)
     {
         noteDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<Note>.Success(this);
     }


 }
