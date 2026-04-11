using Mapster;
using StudyBuddy.Shared.DTOs.MajorDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Major : EntityBase<int>
{
    public string Name { get; private set; } = null!;

    private readonly List<ClientUser> _clientUsers = new();
    public virtual IReadOnlyCollection<ClientUser> ClientUsers => _clientUsers;

    private readonly List<GroupChat> _groupChats = new();
    public virtual IReadOnlyCollection<GroupChat> GroupChats => _groupChats;


    private Major() { }

    public static Result<Major> Create(CreateMajorDTO majorDTO)
    {
        var newMajor = new Major();
        majorDTO.Adapt(newMajor);
        newMajor.CreateDate = DateTime.Now;
        return Result<Major>.Success(newMajor);
    }

    public static Major Create(string Name)
    {
        var newMajor = new Major();
        newMajor.CreateDate = DateTime.Now;
        return newMajor;
    }
    public Result<Major> Update(UpdateMajorDTO majorDTO)
    {
        majorDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<Major>.Success(this);
    }


}
