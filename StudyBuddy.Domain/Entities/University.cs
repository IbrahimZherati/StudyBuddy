using Mapster;
using StudyBuddy.Shared.DTOs.UniversityDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class University : EntityBase<int>
{
     public string Name { get; private set; } = null!;
     private readonly List<ClientUser> _clientUsers = new();
     public virtual IReadOnlyCollection<ClientUser> ClientUsers => _clientUsers;

     private readonly List<GroupChat> _groupChats = new();
     public virtual IReadOnlyCollection<GroupChat> GroupChats => _groupChats;


     private University() { }

     public static Result<University> Create(CreateUniversityDTO universityDTO)
     {
         var newUniversity = new University();
         universityDTO.Adapt(newUniversity);
         newUniversity.CreateDate = DateTime.Now;
         return Result<University>.Success(newUniversity);
     }

     public Result<University> Update(UpdateUniversityDTO universityDTO)
     {
         universityDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<University>.Success(this);
     }

        

 }
