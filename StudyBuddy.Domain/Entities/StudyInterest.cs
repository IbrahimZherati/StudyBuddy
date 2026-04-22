using Mapster;
using StudyBuddy.Shared.DTOs.StudyInterestDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class StudyInterest : EntityBase<int>
{
    public string Name { get; private set; } = null!;

    public int ClientUserId { get; private set; }

    public virtual ClientUser ClientUser { get; private set; } = null!;


    private StudyInterest() { }

    public static Result<StudyInterest> Create(int clientId ,CreateStudyInterestDTO studyInterestDTO)
    {
        var newStudyInterest = new StudyInterest();
        studyInterestDTO.Adapt(newStudyInterest);
        newStudyInterest.ClientUserId = clientId;
        newStudyInterest.CreateDate = DateTime.Now;
        return Result<StudyInterest>.Success(newStudyInterest);
    }

    public Result<StudyInterest> Update(UpdateStudyInterestDTO studyInterestDTO)
    {
        studyInterestDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<StudyInterest>.Success(this);
    }

}
